using Microsoft.AspNetCore.SignalR;
using RenewalTML.Data.Dto;
using RenewalTML.Data.Dto.Admin;
using RenewalTML.Data.Model;
using RenewalTML.Hubs;
using RenewalTML.Shared.Exstention.ClassAddons;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Uow;
using Volo.Abp.Validation;

namespace RenewalTML.Data
{
    public interface IEconomicsServices
    {
        Task CompleteTransferPay(Client inTrans, Client toTrans, int spendMoney, int SentMoney);
        Task CompleteRequest(bool isTransactionApproved, FillBalanceTicketModel model, Client toApproval);
    }

    public class EconomicsServices : ApplicationService, IEconomicsServices
    {
        private readonly ClientManager _clientManager;
        private readonly SystemHubConnection _systemHub;
        private readonly INotificationServices _notificationServices;
        private readonly ITransactionServices _transactionServices;

        public EconomicsServices(ClientManager clientManager,
            INotificationServices notificationServices, ITransactionServices transactionServices, IHubContext<SystemHub> systemHub)
        {
            _clientManager = clientManager;
            _notificationServices = notificationServices;
            _transactionServices = transactionServices;
            _systemHub = new SystemHubConnection(systemHub);
        }

        [UnitOfWork]
        [DisableValidation]
        public async Task CompleteRequest(bool isTransactionApproved, FillBalanceTicketModel model, Client toApproval)
        {
            if (isTransactionApproved)
            {
                await _transactionServices.CreateAndApplyTransaction(new Transaction()
                {
                    Date = DateTimeAddon.NowDateTimeStrings(),
                    Name = "Одобренное пополнение счёта.",
                    Information = "Администратор одобрил пополнение счёта {user:" + toApproval.Id + ":userImgName}" +
                                    "</span>",
                    OutEntityId = 68, // TODO: fix to organization
                    ToEntityId = model._ticket.UserCreateId,
                    TransactionType = "{user:user}",
                    Value = model.CountMoney
                });

                await _systemHub.ChangeUserBarBalance(toApproval, model.CountMoney);
            }

            toApproval.TicketsNonViewCount++;
            await _clientManager.UpdateAsync(toApproval);

            await _notificationServices.CreateAndSendNotification(new Notification()
            {
                ClientOwnerId = toApproval.Id,
                Date = DateTimeAddon.NowDateTimeStrings(),
                Information = "Заявка на пополнение счёт рассмотрена",
                Text = "Посмотрите список ваших заявок на пополнение счёта. Одна из них была проверена администратором.",
                LogoType = "logotype_info"
            }, toApproval);
        }

        // TODO: Сделать доступ только с n-ого уровня,и сделать дизайн к этому
        [UnitOfWork]
        [DisableValidation]
        public async Task CompleteTransferPay(Client inTrans, Client toTrans, int spendMoney, int SentMoney)
        {
            /*inTrans.Balance -= spendMoney;
            toTrans.Balance += SentMoney;

            await _clientManager.UpdateAsync(inTrans);
            await _clientManager.UpdateAsync(toTrans);*/

            // INFO: Транзакции сами изменяют баланс пользователей 

            // Транзакция между пользователями. Количество кеклар: сколько было отправлено.
            await _transactionServices.CreateAndApplyTransaction(new Transaction()
            {
                Date = DateTimeAddon.NowDateTimeStrings(),
                Name = "Перевод с счёта на счёт.",
                Information = "{user:" + inTrans.Id + ":userImgName}<span> перевел деньги на счёт {user:" + toTrans.Id + ":userImgName}" +
                                "</span>",
                OutEntityId = inTrans.Id,
                ToEntityId = toTrans.Id,
                TransactionType = "{user:user}",
                Value = SentMoney
            });

            await _transactionServices.CreateAndApplyTransaction(new Transaction()
            {
                Date = DateTimeAddon.NowDateTimeStrings(),
                Name = "Оплата налога за перевод.",
                Information = "{user:" + inTrans.Id + ":userImgName}<span> оплатил налог за перевод." +
                                "</span>",
                OutEntityId = inTrans.Id,
                ToEntityId = toTrans.Id,
                TransactionType = "{user:user}",
                Value = spendMoney-SentMoney
            });


            // TODO: Изменить на перевод государству или огранизации.

            await _notificationServices.CreateAndSendNotification(new Notification()
            {
                ClientOwnerId = toTrans.Id,
                Date = DateTimeAddon.NowDateTimeStrings(),
                Information = "Пополнение счёта ( перевод )",
                Text = "Пользователь {user:" + inTrans.Id + ":userProfileUrl} перевёл вам {money:" + SentMoney + ":complex:default} {memlarLogotype:mini grey}.",
                LogoType = "logotype_money"
            }, toTrans);

            // обновляем баланс на сайте
            await _systemHub.ChangeUserBarBalance(inTrans, -1 * spendMoney);
            await _systemHub.ChangeUserBarBalance(toTrans, SentMoney);
        }
    }
}
