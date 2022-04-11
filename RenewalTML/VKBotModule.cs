using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RenewalTML.Data;
using RenewalTML.Shared.Exstention.ClassAddons;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using VkNet;
using VkNet.Model;
using VkNet.Model.RequestParams;
using Volo.Abp;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;
using Volo.Abp.Validation;

namespace RenewalTML
{
    public class VKBotModule : AbpModule
    {
        /* SYSTEM CONFIGURATION */

        public static int _longPullTickRate { get; set; } // Тайминг между тика longpull

        /* VK DONUT FIELDS */

        public static int _costSubscrbe { get; set; }

        /* services */

        public IClientAuthServices _userServices { get; set; }
        public ITransactionServices _transactionServices { get; set; }
        public INotificationServices _notificationServices { get; set; }
        public IRolePermissionServices _rolePermissionServices { get; set; }
        public ISystemConfigurationServices _configurationServices { get; set; }

        [UnitOfWork]
        [DisableValidation]
        private async Task StartBotVKContainer(VkApi api)
        {
            var s = api.Groups.GetLongPollServer(191094689);

            var rnd = new Random();

            var aTimer = new System.Timers.Timer(_longPullTickRate);

            var ts = s.Ts;

            aTimer.Elapsed += async (e, a) =>
            {
                try
                {
                    var poll = api.Groups.GetBotsLongPollHistory(
                                            new BotsLongPollHistoryParams()
                                            { Server = s.Server, Ts = ts, Key = s.Key, Wait = 1 });

                    ts = poll.Ts;

                    if (poll?.Updates == null || poll?.Updates.Count() <= 0) return;

                    foreach (var pull in poll.Updates)
                    {
                        /* Подписка была подключена */
                        if (pull.Type == VkNet.Enums.SafetyEnums.GroupUpdateType.DonutSubscriptionCreate)
                        {
                            var find = await _userServices.FindClientWithVKID((long)pull.DonutSubscriptionNew.UserId);

                            // Проверяем, зарегистрирован ли на сайте пользователь, если нет то срочно пишем ему зарегистрироваться и ждать админа
                            //

                            var moneyAdded = 0;

                            if (find == null)
                            {
                                api.Messages.Send(new MessagesSendParams()
                                {
                                    UserId = pull.DonutSubscriptionNew.UserId,
                                    RandomId = rnd.Next(0, 100000),
                                    Message = $"❗ Внимание, вы пожертвовали {pull.DonutSubscriptionNew.AmountWithoutFee} рублей проекту TML Renewal, ваша часть сделки выполнена, дайте нам выполнить свою. Вы не зарегистрированы на сайте! Немедленно пройдите регистрацию и отпишитесь сюда, администратор выдаст вам заслуженные вами бонусы. Удачи!"
                                });
                            }
                            else
                            {
                                var role = await _rolePermissionServices.GetRoleAsync(find.RoleId);

                                if (!role.isHaveAccessToSite)
                                {
                                    api.Messages.Send(new MessagesSendParams()
                                    {
                                        UserId = pull.DonutSubscriptionNew.UserId,
                                        RandomId = rnd.Next(0, 100000),
                                        Message = "🚫 Ваш аккаунт заблокирован на проекте TML Renewal. По возможности мы попытаемся вернуть вам часть средств, с учётом комиссии."
                                    });

                                    return;
                                }

                                if (role.RequereName == RoleManager.defaultRoleName_user)
                                {
                                    var premiumRole = await _rolePermissionServices.GetRoleByUniqId(RoleManager.defaultRoleName_premium_default);
                                    find.RoleId = premiumRole.Id;
                                    await _userServices.UpdateUser(find);
                                }
                                else
                                {
                                    api.Messages.Send(new MessagesSendParams()
                                    {
                                        UserId = pull.DonutSubscriptionNew.UserId,
                                        RandomId = rnd.Next(0, 100000),
                                        Message = "⚠ Вам не выдан премиум на сайте по определённым причинам, срочно отпишитесь тут для связи с администрацией."
                                    });
                                }

                                if (find.IsHavePremiumBefore == false)
                                {
                                    moneyAdded += 50000;
                                    find.IsHavePremiumBefore = true;
                                    await _userServices.UpdateUser(find);
                                }

                                if ((int)pull.DonutSubscriptionNew.AmountWithoutFee > _costSubscrbe)
                                    moneyAdded += ((int)pull.DonutSubscriptionNew.AmountWithoutFee - _costSubscrbe) * 500;

                                await _transactionServices.CreateAndApplyTransaction(new Data.Model.Transaction()
                                {
                                    Date = DateTimeAddon.NowDateTimeStrings(),
                                    Name = "Донат в системе VK Donut.",
                                    Information = "Вознаграждение за донат в системе VK Donuts.",
                                    OutEntityId = 68, // TODO: fix to organization
                                    ToEntityId = find.Id,
                                    TransactionType = "{user:user}",
                                    Value = moneyAdded
                                });

                                await _notificationServices.CreateAndSendNotification(new Data.Model.Notification()
                                {
                                    ClientOwnerId = find.Id,
                                    Date = DateTimeAddon.NowDateTimeStrings(),
                                    Information = "Награда за донат!",
                                    Text = "Вы получили свою награду за донат в проект TML Renewal. Подробнее на странице транзакций в профиле.",
                                    LogoType = "logotype_award"
                                }, find);

                                var message = $"⭐ Поздравляю, вы поддержали сообщество TML Renewal на {(int)pull.DonutSubscriptionNew.AmountWithoutFee} рублей! Спасибо вам огромное, вы сталью частью активно развивающегося комьюнити TML Rewneal. За это вы получите следующие бонусы:\n\n" +
                                    $"- На ваш аккаунт {find.ScreenName} на сайте уже был начислен премиум на время, пока действует подписка.";

                                if (find.IsHavePremiumBefore == false)
                                    message += "\n- Так же вы получили подарок в виде 50.000 Кеклар за первую покупку на вашем аккаунте.";

                                message += "\n\n- Вы получили доступ в закрытую беседу для донов, либо найдите её на странице группы, либо перейдите по ссылке: https://vk.cc/cbW3hC"
                                        + "\n- Вы получили доступ к эксклюзивным записям на странице группы"
                                        + "\n\nНе забывайте продлить подписку, что бы ваш статус премиум аккаунта сохранился.";

                                if ((int)pull.DonutSubscriptionNew.AmountWithoutFee > _costSubscrbe)
                                    message += $"\n\nКо всему прочему, вы задонатили больше положенной суммы на {((int)pull.DonutSubscriptionNew.AmountWithoutFee - _costSubscrbe)} рубля, а значит что вас ждет ещё подарочек: дополнительные {((int)pull.DonutSubscriptionNew.AmountWithoutFee - _costSubscrbe) * 500} Кеклар!";


                                api.Messages.Send(new MessagesSendParams()
                                {
                                    UserId = pull.DonutSubscriptionNew.UserId,
                                    RandomId = rnd.Next(0, 100000),
                                    Message = message
                                });
                            }
                        }
                        else if (pull.Type == VkNet.Enums.SafetyEnums.GroupUpdateType.DonutSubscriptionCanceled ||
                            pull.Type == VkNet.Enums.SafetyEnums.GroupUpdateType.DonutSubscriptionExpired) // Если подписка закончилась или отменена
                        {
                            var find = await _userServices.FindClientWithVKID((long)pull.DonutSubscriptionEnd.UserId);
                            var role = await _rolePermissionServices.GetRoleAsync(find.RoleId);

                            if (role.isHaveAccessToSite)
                            {
                                api.Messages.Send(new MessagesSendParams()
                                {
                                    UserId = pull.DonutSubscriptionEnd.UserId,
                                    RandomId = rnd.Next(0, 100000),
                                    Message = "⚠ Спешу вам сообщить, ваша премиум подписка в TML Renewal к сожалению отменена. Для её продления необходим продлить подписку VK Donuts в этой группе: https://vk.com/donut/kekland_bank"
                                });

                                if (role.RequereName == RoleManager.defaultRoleName_premium_default)
                                {
                                    var defaultUserRole = await _rolePermissionServices.GetRoleByUniqId(RoleManager.defaultRoleName_user);

                                    find.RoleId = defaultUserRole.Id;

                                    await _userServices.UpdateUser(find);
                                }
                                else
                                {
                                    api.Messages.Send(new MessagesSendParams()
                                    {
                                        UserId = pull.DonutSubscriptionEnd.UserId,
                                        RandomId = rnd.Next(0, 100000),
                                        Message = "⚠ У вас не был выключен премиум на сайте по определённым причинам, администрация скоро свяжется с вами."
                                    });
                                }
                            }

                            // Оповещаем пользователя о том что подписка закончилась и премиум на аккаунте был выключен

                        }
                        else if (pull.Type == VkNet.Enums.SafetyEnums.GroupUpdateType.DonutSubscriptionProlonged) // продление подиски
                        {
                            var find = await _userServices.FindClientWithVKID((long)pull.DonutSubscriptionNew.UserId);

                            var role = await _rolePermissionServices.GetRoleAsync(find.RoleId);

                            if (!role.isHaveAccessToSite)
                            {
                                api.Messages.Send(new MessagesSendParams()
                                {
                                    UserId = pull.DonutSubscriptionNew.UserId,
                                    RandomId = rnd.Next(0, 100000),
                                    Message = "🚫 Ваш аккаунт заблокирован на проекте TML Renewal. По возможности мы попытаемся вернуть вам часть средств, с учётом комиссии."
                                });

                                return;
                            }

                            var moneyAdded = 0;
                            var message = "⭐ Спасибо за продление подписки! Ваш премиум статус сохранён.";

                            if ((int)pull.DonutSubscriptionNew.AmountWithoutFee > _costSubscrbe)
                            {
                                moneyAdded += ((int)pull.DonutSubscriptionNew.AmountWithoutFee - _costSubscrbe) * 500;

                                await _transactionServices.CreateAndApplyTransaction(new Data.Model.Transaction()
                                {
                                    Date = DateTimeAddon.NowDateTimeStrings(),
                                    Name = "Донат в системе VK Donut.",
                                    Information = "Вознаграждение за донат в системе VK Donuts.",
                                    OutEntityId = 68, // TODO: fix to organization
                                    ToEntityId = find.Id,
                                    TransactionType = "{user:user}",
                                    Value = moneyAdded
                                });

                                await _notificationServices.CreateAndSendNotification(new Data.Model.Notification()
                                {
                                    ClientOwnerId = find.Id,
                                    Date = DateTimeAddon.NowDateTimeStrings(),
                                    Information = "Награда за донат!",
                                    Text = "Вы получили свою награду за донат в проект TML Renewal. Подробнее на странице транзакций в профиле.",
                                    LogoType = "logotype_award"
                                }, find);

                                message += $" Из-за переплаты в {(int)pull.DonutSubscriptionNew.AmountWithoutFee - _costSubscrbe} рублей вы получите в виде бонуса {((int)pull.DonutSubscriptionNew.AmountWithoutFee - _costSubscrbe) * 500} Кеклар.";
                            }


                            api.Messages.Send(new MessagesSendParams()
                            {
                                UserId = pull.DonutSubscriptionNew.UserId,
                                RandomId = rnd.Next(0, 100000),
                                Message = message
                            });


                            // Так же проверка на пользователя

                            // оповещаем что пользователь крутой 
                            // даём денег за переплату
                        }
                    }

                }
                catch (Exception err)
                {
                    Log.Logger.Error("Bot API Connected lost: " + err.Message);
                    return;
                }
            };
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void StartApplicationContainer()
        {
            Log.Logger.Information("Start bot container");

            var apiCode = Environment.GetEnvironmentVariable("vkConnectionKey");

            var api = new VkApi();
            api.Authorize(new ApiAuthParams() { AccessToken = apiCode });

            Task taskA = new Task(async () => await StartBotVKContainer(api));
            taskA.Start();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            _userServices = (IClientAuthServices)app.ApplicationServices.GetRequiredService(typeof(IClientAuthServices));
            _transactionServices = (ITransactionServices)app.ApplicationServices.GetRequiredService(typeof(ITransactionServices));
            _notificationServices = (INotificationServices)app.ApplicationServices.GetRequiredService(typeof(INotificationServices));
            _configurationServices = (ISystemConfigurationServices)app.ApplicationServices.GetRequiredService(typeof(ISystemConfigurationServices));
            _rolePermissionServices = (IRolePermissionServices)app.ApplicationServices.GetRequiredService(typeof(IRolePermissionServices));

            try
            {
                _longPullTickRate = Convert.ToInt32(_configurationServices.GetByKey("system_main.bottickrate").Result.Value);
                _costSubscrbe = Convert.ToInt32(_configurationServices.GetByKey("system_main.costsubscrive").Result.Value);
            }
            catch (Exception)
            {
                _longPullTickRate = 2000;
                _costSubscrbe = 200;
                Log.Logger.Warning("Bot module don't setting up, because system settings do not seed. Please restart server after end seeding job services.");
            }

            StartApplicationContainer();
        }
    }
}
