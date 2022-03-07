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
    public class SystemConfigurationManager : GenericManager<SystemConfiguration>
    {
        public SystemConfigurationManager(IRepository<SystemConfiguration, int> _configureationRepository)
        {
            _genericRepository = _configureationRepository;
        }

        public async Task<string> GetValue(string key)
        {
            var item = await _genericRepository.Where(m => m.UniqId == key).FirstOrDefaultAsync();
            return item.Value;
        }

        public async Task<SystemConfiguration> GetByKey(string key) => await _genericRepository.Where(m => m.UniqId == key).FirstOrDefaultAsync();
    }
}