using RenewalTML.Data.Dto;
using RenewalTML.Data.Model;
using RenewalTML.Shared.Exstention.ClassAddons;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Uow;

namespace RenewalTML.Data
{
    public interface ITransactionServices
    {
        Task<TransactionModule> GetEntityTransaction(Client client, IRawTextHtmlizer rawTextHtmlizer, int limit = 16, int skipped = 0);
        Task CreateAndApplyTransaction(Transaction model);
    }
    
    public class TransactionServices : ApplicationService, ITransactionServices
    {
        private readonly TransactionManager _transactionManager;
        private readonly ClientManager _clientManager;

        public TransactionServices(TransactionManager transactionManager, ClientManager clientManager)
        {
            _transactionManager = transactionManager;
            _clientManager = clientManager;
        }

        [UnitOfWork]
        public async Task CreateAndApplyTransaction(Transaction model)
        {
            try
            {
                switch (model.TransactionType)
                {
                    case "{user:user}":
                        var userOut = await _clientManager.GetAsync(model.OutEntityId);
                        var userTo = await _clientManager.GetAsync(model.ToEntityId);

                        userOut.Balance -= model.Value;
                        // TODO: user balancd changed
                        await _clientManager.UpdateAsync(userOut);

                        userTo.Balance += model.Value;
                        // TODO: user balancd changed
                        await _clientManager.UpdateAsync(userTo);

                        await _transactionManager.AddAsync(model);

                        break;

                    // TODO: Первоначальные операции будут идти от пользователя главный админ как от системы. 
                    // Потом все будет перенесено в главную и другие организации!
                    // TODO: Other...


                    default:
                        throw new ArgumentException("TransactionModelType is invalid.", nameof(model));
                }
            }
            catch (Exception) { } // TODO: Exceptions :0
        }

        public async Task<TransactionModule> GetEntityTransaction(Client client, IRawTextHtmlizer rawTextHtmlizer, int limit = 16, int skipped = 0)
        {
            if (limit % 2 != 0)
                throw new ArgumentException("Value 'limit' cannot be divisible by two.", nameof(limit));

            var earnList = await _transactionManager.GetEntityTransaction("user", client.Id, true, limit / 2, skipped);
            var gaveList = await _transactionManager.GetEntityTransaction("user", client.Id, false, limit / 2, skipped);

            var count = await _transactionManager.GetEntityTransactionCount("user", client.Id);

            var ret = new TransactionModule();
            await ret.TransactionModuleComplete(earnList, gaveList, rawTextHtmlizer, count);
            return ret;
        }
    }
}
