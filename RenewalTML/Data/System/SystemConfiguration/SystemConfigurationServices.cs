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
    public interface ISystemConfigurationServices
    {
        Task<IEnumerable<SystemConfiguration>> GetAllAsync();
        Task<SystemConfiguration> GetByKey(string key);
        Task<SystemConfiguration> GetById(int id);
        Task UpdateConfigurtion(SystemConfiguration config);
        Task<string> Get(string key);
    }

    public class SystemConfigurationServices : ApplicationService, ISystemConfigurationServices
    {
        private readonly SystemConfigurationManager _systemConfigurationManager;
        private readonly ClientManager _clientManager;

        public SystemConfigurationServices(SystemConfigurationManager systemConfigurationManager, ClientManager clientManager)
        {
            _systemConfigurationManager = systemConfigurationManager;
            _clientManager = clientManager;
        }

        public async Task<string> Get(string key) => await _systemConfigurationManager.GetValue(key);

        public async Task<SystemConfiguration> GetById(int id) => await _systemConfigurationManager.GetAsync(id);
        public async Task<SystemConfiguration> GetByKey(string key) => await _systemConfigurationManager.GetByKey(key);
        public async Task<IEnumerable<SystemConfiguration>> GetAllAsync() => await _systemConfigurationManager.GetAllAsync();
        public async Task UpdateConfigurtion(SystemConfiguration config) => await _systemConfigurationManager.UpdateAsync(config);
    }
}
