using RenewalTML.Data.Dto;
using RenewalTML.Data.Model;
using RenewalTML.Shared.Exstention.ClassAddons;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace RenewalTML.Data
{
    public interface IAwardServices
    {
        Task<List<AwardView>> GetUserListAwards(Client client);
        Task AddProgressToAward(string awardRequery, Client client, int progress);
    }

    public class AwardServices : ApplicationService, IAwardServices
    {
        private readonly AwardManager _awardManager;
        private readonly ClientAwardManager _clientAwardManager;
        private readonly ClientManager _clientManager;
        private readonly INotificationServices _notificationServices;
        private readonly ITransactionServices _transactionServices;

        public AwardServices(AwardManager awardManager, ClientAwardManager clientAwardManager, ClientManager clientManager,
            INotificationServices notificationServices, ITransactionServices transactionServices)
        {
            _awardManager = awardManager;
            _clientAwardManager = clientAwardManager;
            _clientManager = clientManager;
            _notificationServices = notificationServices;
            _transactionServices = transactionServices;
        }

        public async Task AddProgressToAward(string awardRequery, Client client, int progress)
        {
            var award = await _awardManager.FindByRequeryName(awardRequery);

            if (award != null)
            {
                var entity = await _clientAwardManager.GetClientAward(client, award);

                if (entity != null)
                {
                    entity.Progress += progress;
                    await _clientAwardManager.UpdateAsync(entity);
                }
                else
                {
                    entity = new ClientAward()
                    {
                        AwardId = award.Id,
                        ClientId = client.Id,
                        Progress = progress
                    };
                    await _clientAwardManager.AddAsync(entity, true);
                }

                if (entity.Progress >= award.ProgressFinal)
                {
                    entity.Progress = award.ProgressFinal;

                    switch (award.AwardType)
                    {
                        case "type_money":

                            // TODO: Проверка на возможность получить деньги с системы.
                            await _transactionServices.CreateAndApplyTransaction(new Transaction()
                            {
                                Date = DateTimeAddon.NowDateTimeStrings(),
                                Name = "Бонус достижения.",
                                Information = "{user:" + client.Id + ":userImgName}<span> получил награду за выполнения достижения " +
                                "<span class=\"award_styletext\">'" + award.Name + "'</span></span>",
                                OutEntityId = 68, // TODO: Сделать что бы снимало с системы позже
                                ToEntityId = client.Id,
                                TransactionType = "{user:user}",
                                Value = award.Value
                            });

                            break;
                    }

                    var reEntity = await _clientAwardManager.GetClientAward(client, award);
                    // Нужно что бы исправить баг, потому что EF Core не дает просто изменить entity
                    // Из за того что блок транзакций в бд изолирован, нужно закрепить старый результат и получить новый ( точне то т же из бд )

                    await _clientAwardManager.UpdateAsync(reEntity); // повторное обновление

                    await _notificationServices.CreateAndSendNotification(new Notification() {
                        ClientOwnerId = client.Id,
                        Date = DateTimeAddon.NowDateTimeStrings(),
                        Information = "Получено новое достижение",
                        Text = "Вы получили новое достижение <strong>\"" + award.Name + "\"</strong>. А так же вы получили награду за достижение. Награда уже начислена на ваш аккаунт. Посмотреть ваши награды можно тут: " +
                        "<a onclick=\"event.preventDefault();\" href=\"" + "profile/" + client.Id + "/awards" + "\" class=\"js-dynaimcNavigate\">" + "список наград" + "</a>",
                        LogoType = "logotype_award"
                    }, client);
                }
            }
        }

        public async Task<List<AwardView>> GetUserListAwards(Client client)
        {
            var list = await _clientAwardManager.GetClientList(client);
            var ret = new List<AwardView>();

            foreach(var item in list)
            {
                var entity = await _awardManager.GetAsync(item.AwardId);

                if (entity != null)
                {
                    ret.Add(new AwardView()
                    {
                        Name = entity.Name,
                        ProgressFinal = entity.ProgressFinal,
                        HTMLIcon = entity.Icon,
                        HTMLText = entity.Text,
                        Progress = item.Progress,
                        isGet = item.Progress >= entity.ProgressFinal
                    });
                }
                else
                {
                    ret.Add(new AwardView()
                    {
                        Name = entity.Name,
                        ProgressFinal = entity.ProgressFinal,
                        HTMLIcon = entity.Icon,
                        HTMLText = entity.Text,
                        Progress = 0,
                        isGet = false
                    });
                }
            }

            return ret;
        }

    }
}
