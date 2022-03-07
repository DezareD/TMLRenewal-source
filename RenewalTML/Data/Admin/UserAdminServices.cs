using RenewalTML.Data.Model;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Security.Encryption;

namespace RenewalTML.Data
{
    // TODO: 1. Сделать систему админ статистики.
    // 2. Сделать систему просмотра админ статистики ( как общую, так и для определенных админов ( отдельная панелька ))
    // 3. Доделать редактирования пользователя
    // 4. Доделать пополние счёта. 

    public interface IUserAdminServices
    {
        Task SetUserScreenName(Client user, string newScreenName);
        Task SetUserLogin(Client user, string newlogin);
        Task SetUserPassword(Client user, string newPassword);
        Task ToggleUserBanned(Client user);
    }

    public class UserAdminServices : ApplicationService, IUserAdminServices
    {
        private readonly RoleManager _roleManager;
        private readonly ClientManager _clientManager;
        private readonly IStringEncryptionService _encryptService;

        public UserAdminServices(RoleManager roleManager, ClientManager clientManager, IStringEncryptionService encryptService)
        {
            _roleManager = roleManager;
            _clientManager = clientManager;
            _encryptService = encryptService;
        }

        // Если не забанен - сделать забаненным
        // Если забанен - сделать обычным пользователем
        public async Task ToggleUserBanned(Client user)
        {
            var find = await _clientManager.GetAsync(user.Id);
            var role = await _roleManager.GetAsync(find.RoleId);

            if(role.RequereName == RoleManager.defaultRoleName_banned)
            {
                var defaultRole = await _roleManager.GetDefaultRoleUser();
                find.RoleId = defaultRole.Id;
            }
            else
            {
                var bannedRole = await _roleManager.GetDefaultRoleBanned();
                find.RoleId = bannedRole.Id;
            }

            await _clientManager.UpdateAsync(find);
        }

        public async Task SetUserScreenName(Client user, string newScreenName)
        {
            var find = await _clientManager.GetAsync(user.Id);
            find.ScreenName = newScreenName;
            await _clientManager.UpdateAsync(find);
        }

        public async Task SetUserLogin(Client user, string newlogin)
        {
            var find = await _clientManager.GetAsync(user.Id);
            find.UserName = newlogin;
            await _clientManager.UpdateAsync(find);
        }

        public async Task SetUserPassword(Client user, string newPassword)
        {
            var find = await _clientManager.GetAsync(user.Id);

            var salt = ClientAuthServices.GenerateRandomString(16);
            var passwordHash = _encryptService.Encrypt(newPassword, salt: Encoding.UTF8.GetBytes(salt));

            find.PasswordHash = passwordHash;
            find.PasswordSalt = salt;

            await _clientManager.UpdateAsync(find);
        }
    }
}
