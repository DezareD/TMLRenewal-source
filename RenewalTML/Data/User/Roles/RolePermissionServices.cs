using Microsoft.AspNetCore.Http;
using RenewalTML.Data.Model;
using System;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Security.Encryption;
using Microsoft.JSInterop;
using RenewalTML.Data.JSInteropHelper;
using System.Security.Cryptography;
using RenewalTML.Data.Dto;
using System.Linq;
using Volo.Abp.Validation;
using System.Collections.Generic;

namespace RenewalTML.Data
{
    public interface IRolePermissionServices : IApplicationService
    {
        Task<Role> GetClientRole(Client client);
        Task<List<Role>> GetAllRoles();
        Task<List<RoleEnchantedModel>> GetAllEnchantedRoles();
        Task UpdateRole(Role role);
        Task<Role> GetRoleAsync(int id);
        Task CreateRole(Role role);
        Task DeleteRole(Role role);

        Task RebaseBeforeDelete(int deleteRoleId);
    }

    public class RolePermissionServices : ApplicationService, IRolePermissionServices
    {
        private readonly RoleManager _roleManager;
        private readonly ClientManager _clientManager;

        public RolePermissionServices(RoleManager roleManager, ClientManager clientManager)
        {
            _roleManager = roleManager;
            _clientManager = clientManager;
        }

        [DisableValidation]
        public async Task<Role> GetClientRole(Client client)
        {
            if (client != null)
            {
                var role = await _roleManager.GetAsync(client.RoleId);

                if(role == null)
                    return await _roleManager.GetDefaultRoleGuest();
                else return role;
            }
            else return await _roleManager.GetDefaultRoleGuest();
        }

        public async Task RebaseBeforeDelete(int deleteRoleId)
        {
            var userList = await _clientManager.GetAllAsync();
            var defaultRoleId = await _roleManager.GetDefaultRoleUser();
            var filtered = userList.Where(m => m.RoleId == deleteRoleId);

            foreach(var k in filtered)
                k.RoleId = defaultRoleId.Id;

            await _clientManager.UpdateManyAsync(filtered);
        }

        public async Task UpdateRole(Role role) => await _roleManager.UpdateAsync(role);
        public async Task<List<RoleEnchantedModel>> GetAllEnchantedRoles()
        {
            var list = await _roleManager.GetAllAsync();
            var retList = new List<RoleEnchantedModel>();

            foreach(var item in list)
            {
                int count = await _clientManager.GetCountClientByRoleId(item.Id);

                retList.Add(new RoleEnchantedModel()
                {
                    role = item,
                    UserCount = count
                });
            }

            return retList;
        }

        public async Task<List<Role>> GetAllRoles() => (await _roleManager.GetAllAsync()).ToList();
        public async Task<Role> GetRoleAsync(int id) => await _roleManager.GetAsync(id);
        public async Task CreateRole(Role role) => await _roleManager.AddAsync(role);
        public async Task DeleteRole(Role role) => await _roleManager.DeleteAsync(role);
    }
}
