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
    public class RoleManager : GenericManager<Role>
    {
        public static readonly string defaultRoleName_user = "default";
        public static readonly string defaultRoleName_guest = "guest";
        public static readonly string defaultRoleName_banned = "banned";
        public static readonly string defaultRoleName_premium_default = "premium_default";

        public RoleManager(IRepository<Role, int> roleManager)
        {
            _genericRepository = roleManager;
        }

        public async Task<Role> FindByRequeryName(string req_name) => await AsyncExecuter.FirstOrDefaultAsync((await _genericRepository.GetQueryableAsync()).Where(m => m.RequereName == req_name));
        public async Task<Role> GetDefaultRoleUser() => await AsyncExecuter.FirstOrDefaultAsync((await _genericRepository.GetQueryableAsync()).Where(m => m.RequereName == defaultRoleName_user));
        public async Task<Role> GetDefaultRoleGuest() => await AsyncExecuter.FirstOrDefaultAsync((await _genericRepository.GetQueryableAsync()).Where(m => m.RequereName == defaultRoleName_guest));
        public async Task<Role> GetDefaultRoleBanned() => await AsyncExecuter.FirstOrDefaultAsync((await _genericRepository.GetQueryableAsync()).Where(m => m.RequereName == defaultRoleName_banned));
        public async Task<Role> GetDefaultPremiumDefault() => await AsyncExecuter.FirstOrDefaultAsync((await _genericRepository.GetQueryableAsync()).Where(m => m.RequereName == defaultRoleName_premium_default));

    }
}
