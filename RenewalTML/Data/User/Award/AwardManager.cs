using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RenewalTML.Data.Model;
using RenewalTML.EFCore;
using RenewalTML.Hubs;
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
    public class AwardManager : GenericManager<Award>
    {
        public AwardManager(IRepository<Award, int> _awardRepository)
        {
            _genericRepository = _awardRepository;
        }

        public async Task<Award> FindByRequeryName(string req_name) => await AsyncExecuter.FirstOrDefaultAsync(_genericRepository.Where(m => m.requereName == req_name));
    }

    public class ClientAwardManager : GenericManager<ClientAward>
    {
        public ClientAwardManager(IRepository<ClientAward, int> _awardRepository)
        {
            _genericRepository = _awardRepository;
        }

        public async Task<ClientAward> GetClientAward(Client client, Award award) => await AsyncExecuter.FirstOrDefaultAsync(_genericRepository.Where(m => m.ClientId == client.Id).Where(m => m.AwardId == award.Id));
        public async Task<List<ClientAward>> GetClientList(Client client) => await AsyncExecuter.ToListAsync(_genericRepository.Where(m => m.ClientId == client.Id));

    }
}
