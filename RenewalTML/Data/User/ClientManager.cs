using RenewalTML.Data.Model;
using RenewalTML.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Uow;

namespace RenewalTML.Data
{
    public class ClientManager : GenericManager<Client>
    {
        public ClientManager(IRepository<Client, int> _clientRepository)
        {
            _genericRepository = _clientRepository;
        }

        public async Task<int> GetCountClientByRoleId(int roleId) => await AsyncExecuter.CountAsync((await _genericRepository.GetQueryableAsync()).Where(m => m.RoleId == roleId));
        public async Task<Client> GetClientByNameAsync(string userName) => await AsyncExecuter.FirstOrDefaultAsync((await _genericRepository.GetQueryableAsync()).Where(m => m.UserName == userName));
        public async Task<Client> FindClientWithVKID(string vkid) => await AsyncExecuter.FirstOrDefaultAsync((await _genericRepository.GetQueryableAsync()).Where(m => m.VkId == vkid));

    }
}
