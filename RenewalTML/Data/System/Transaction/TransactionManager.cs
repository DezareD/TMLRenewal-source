using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RenewalTML.Data.Model;
using RenewalTML.EFCore;
using RenewalTML.Hubs;
using RenewalTML.Shared.Exstention.ClassAddons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Uow;

namespace RenewalTML.Data
{
    public class TransactionManager : GenericManager<Transaction>
    {
        public TransactionManager(IRepository<Transaction, int> _transactionRepository)
        {
            _genericRepository = _transactionRepository;
        }

        private bool IsTransactionTypeEquals(string entityName, string transactionType, bool isOut)
        {
            try
            {
                transactionType = transactionType.Replace("{", string.Empty).Replace("}", string.Empty);

                var args = transactionType.Split(':');

                if (isOut)
                {
                    if (args[0] == entityName)
                        return true;
                }
                else
                {
                    if (args[1] == entityName)
                        return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool IsTransactionEntityIdEquals(int entityId, int outTransactionEntityId, int inTransactionEntityId, bool isOut)
        {
            if (isOut) return outTransactionEntityId == entityId;
            else return inTransactionEntityId == entityId;
        }

        public async Task<int> GetEntityTransactionCount(string entityName, int entityId)
        {
            // берем только те, котоыре как то связаны с нашей сущьностью
            var list = await (await _genericRepository.GetQueryableAsync()).Where(m => m.OutEntityId == entityId || m.ToEntityId == entityId).ToListAsync();

            var cnt1 = list.Where(m => IsTransactionTypeEquals(entityName, m.TransactionType, true)).Where(m => IsTransactionEntityIdEquals(entityId, m.OutEntityId, m.ToEntityId, true)).Count();
            var cnt2 = list.Where(m => IsTransactionTypeEquals(entityName, m.TransactionType, false)).Where(m => IsTransactionEntityIdEquals(entityId, m.OutEntityId, m.ToEntityId, false)).Count();
            return cnt1 + cnt2;
        }

        public async Task<List<Transaction>> GetEntityTransaction(string entityName, int entityId, bool isOut, int limit, int skipped = 0)
        {
            // берем только те, котоыре как то связаны с нашей сущьностью
            var list = await (await _genericRepository.GetQueryableAsync()).Where(m => m.OutEntityId == entityId || m.ToEntityId == entityId).ToListAsync();

            // уже отсеиваем, фильтруем и т.д
            if(limit > 0) return list.Where(m => IsTransactionTypeEquals(entityName, m.TransactionType, isOut)).Where(m => IsTransactionEntityIdEquals(entityId, m.OutEntityId, m.ToEntityId, isOut)).OrderByDescending(m => DateTimeAddon.StringToDateTime(m.Date).Ticks).Skip(skipped).Take(limit).ToList();
            else return list.Where(m => IsTransactionTypeEquals(entityName, m.TransactionType, isOut)).Where(m => IsTransactionEntityIdEquals(entityId, m.OutEntityId, m.ToEntityId, isOut)).OrderByDescending(m => DateTimeAddon.StringToDateTime(m.Date).Ticks).Skip(skipped).ToList();
        }
    }
}