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
using Microsoft.AspNetCore.Hosting;
using RenewalTML.Shared.Exstention.ClassAddons;
using System.Collections.Generic;

namespace RenewalTML.Data
{
    public interface IClientAuthServices : IApplicationService
    {
        Task Authenticate(Client client, bool isPersistant = true);
        Task LogOut();
        Task<Client> GetClient();

        bool IsSessionAuthComplete(VKAuthorizeDataInterop model);
        Task<bool> IsUserRegisterVK(VKAuthorizeDataInterop model);
        Task<Client> GetUserWithVK(VKAuthorizeDataInterop model);
        Task<Client> RegisterClient(TwoPhaseRegistrationVK model, VKAuthorizeDataInterop vkModel);
        Task<bool> IsFreeNickName(string name);
        Task<bool> LoginIsReady(UserLoginModel model);
        Task<Client> FindUserByNickname(string name);
        Task<Client> FindUserById(int id);
        Task UpdateUserScreenName(Client user, string screename);

        Task<bool> UserPasswordIsOk(Client client, string password);
        Task UpdateUserPassword(Client user, string password);

        Task UpdateUser(Client user);

        Task<List<Client>> GetAllFilterClient(bool getBanned = false, bool getAdmin = true, bool getMe = false, bool getBlockedEconomicUser = false);
        Task<int> GetCountClientByRoleId(int id);
    }

    public class ClientAuthServices : ApplicationService, IClientAuthServices
    {
        private readonly ClientManager _clientManager;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IStringEncryptionService _encryptService;
        private readonly RoleManager _rm;
        private readonly IVirtualFileServices _virtualFileServices;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly INotificationServices _notificationServices;
        private readonly IAwardServices _awardServices;
        private readonly IJSModularityServices _jsModularityServices;

        private readonly string cookie_authKey = "_cl_tmlRenewalApplicationCookie_auth";

        public ClientAuthServices(ClientManager clientManager,
            IHttpContextAccessor httpcontext,
            IStringEncryptionService encryptService,
            RoleManager rm,
            IVirtualFileServices virtualFileServices,
            IWebHostEnvironment webHostEnvironment,
            INotificationServices notificationServices,
            IAwardServices awardServices,
            IJSModularityServices jsModularityServices)
        {
            _clientManager = clientManager;
            _httpContext = httpcontext;
            _encryptService = encryptService;
            _rm = rm;
            _virtualFileServices = virtualFileServices;
            _webHostEnvironment = webHostEnvironment;
            _notificationServices = notificationServices;
            _awardServices = awardServices;
            _jsModularityServices = jsModularityServices;
        }

        /* Добавить пользователю в браузер зашифрованные куки с уникальными данными пользователя ( в совокупности ) 
         * для проверки подлинности. */
        public async Task Authenticate(Client client, bool isPersistant = true)
        {
            string cookieUserData = $"id:'{client.Id}' vkid:'{client.VkId}'";

            var cryptcookieUserData = _encryptService.Encrypt(cookieUserData);

            await _jsModularityServices.InvokeVoidAsync("cookiesModule", "addCookie", cookie_authKey, cryptcookieUserData, isPersistant ? 365 : 1);
        }

        /* Удаляёт куки у текущего пользователя. */
        public async Task LogOut()
        {
            await _jsModularityServices.InvokeVoidAsync("cookiesModule", "deleteCookie", cookie_authKey);
        }

        public async Task<int> GetCountClientByRoleId(int id) => await _clientManager.GetCountClientByRoleId(id); 

        public async Task<bool> UserPasswordIsOk(Client client, string password)
        {
            return await LoginIsReady(new UserLoginModel()
            {
                Name = client.UserName,
                Password = password
            });
        }

        public async Task<Client> GetClient()
        {
            var authString = await _jsModularityServices.InvokeAsync<string>("cookiesModule", "getCookie", cookie_authKey);

            if (String.IsNullOrEmpty(authString)) return null;

            try
            {
                var decryptData = _encryptService.Decrypt(authString).Split('\'');

                int userId = Convert.ToInt32(decryptData[1]);
                var vkId = decryptData[3];

                var user = await _clientManager.GetAsync(userId);

                if (user != null && user.VkId == vkId)
                    return user;
            }
            catch (Exception)
            {
                return null;
            }

            return null;
        }

        /* Верна ли сессия входа VK OPEN API */
        public bool IsSessionAuthComplete(VKAuthorizeDataInterop model)
        {
            var claimString = "expire=" + model.session.expire
                + "mid=" + model.session.mid
                + "secret=" + model.session.secret
                + "sid=" + model.session.sid
                + "Rri5i3jGhppIkYwRT08w";

            var md5claims = PHPMd5Hash(claimString);

            if (md5claims == model.session.sig)
                return true;
            else return false;

        }

        public async Task UpdateUserScreenName(Client user, string screename)
        {
            var search = await _clientManager.GetAsync(user.Id);
            search.ScreenName = screename;
            await _clientManager.UpdateAsync(search);
        }

        public async Task UpdateUser(Client user) => await _clientManager.UpdateAsync(user);

        public async Task UpdateUserPassword(Client user, string password)
        {
            var search = await _clientManager.GetAsync(user.Id);

            var salt = GenerateRandomString(16);
            var passwordHash = _encryptService.Encrypt(password, salt: Encoding.UTF8.GetBytes(salt));

            search.PasswordHash = passwordHash;
            search.PasswordSalt = salt;

            await _clientManager.UpdateAsync(search);
        }

        // Свободен ли login?
        public async Task<bool> IsFreeNickName(string name)
        {
            var search = await _clientManager.GetClientByNameAsync(name);

            if (search != null) return false;
            else return true;
        }

        /* Зарегистрирован ли пользователь? ( Проверка через данные ВК ) */
        public async Task<bool> IsUserRegisterVK(VKAuthorizeDataInterop model)
        {
            var user = await _clientManager.FindClientWithVKID(Convert.ToString(model.session.mid));

            if (user != null) return true;
            else return false;
        }

        public async Task<Client> GetUserWithVK(VKAuthorizeDataInterop model)
        {
            return await _clientManager.FindClientWithVKID(Convert.ToString(model.session.mid));
        }

        public static string GenerateRandomString(int length, bool withSpecialChars = true)
        {
            var rnd = new Random();
            string chars = "ABCEFGHJKPQRSTXYZ23456789abcdefghjkpqrstxyz";

            if (withSpecialChars)
                chars += ".@\\|/-_";

            string ret = "";

            for(int i = 0; i <length; i++)
            {
                ret += chars[rnd.Next(0, chars.Length)];
            }

            return ret;
        }

        public async Task<Client> RegisterClient(TwoPhaseRegistrationVK model, VKAuthorizeDataInterop vkModel)
        {
            var salt = GenerateRandomString(16);
            var passwordHash = _encryptService.Encrypt(model.Password, salt: Encoding.UTF8.GetBytes(salt));

            var defaultRole = await _rm.GetDefaultRoleUser(); // Делаем пользователя - пользователем.

            var vfs = new VirtualFileStatic(_webHostEnvironment, _virtualFileServices);

            var cropper = await vfs.CreateCopyDefaultCropperAvatarToUser();

            var client = new Client()
            {
                Balance = 0,
                UserName = model.Name,
                VkId = Convert.ToString(vkModel.session.mid),
                PasswordSalt = salt,
                PasswordHash = passwordHash,
                RoleId = defaultRole.Id,
                ScreenName = model.ScreenName,
                UserAvatar_main = 1,
                UserAvatar_cropp = cropper.MainImageId,
                UserAvatar_cropp64x64 = cropper.x64CroppedImageId,
                CurrencyExp = 0,
                Level = 1
            };

            // TODO: CLIENT REGISTER COMPLETE

            await _clientManager.AddAsync(client, true);

            await _awardServices.AddProgressToAward("achievement_register", client, 1);

            await _notificationServices.CreateAndSendNotification(new Notification()
            {
                ClientOwnerId = client.Id,
                Date = DateTimeAddon.NowDateTimeStrings(),
                Information = "Добро пожаловать в TML Renewal!",
                LogoType = "logotype_info",
                Text = "{user:" + client.Id + ":userProfileUrl}, добро пожаловать в TML Renewal. За успешное прохождение регистрации вы получили достижение <strong>\"Добро пожаловать!\"</strong>, и получили свои первые деньги. Прочитайте FAQ и изучите основную информацию на сайте или в <a href=\"https://vk.com/kekland_bank\" target=\"_blank\">группе вк</a>. Спасибо что стали нашим активным пользователем. С уваженимем администрация TML Renewal."
            }, client);
            return client;
        }

        /* Разрешён ли пользователю вход в систему? */
        public async Task<bool> LoginIsReady(UserLoginModel model)
        {
            var search = await _clientManager.GetClientByNameAsync(model.Name);
            if (search != null)
            {
                var passHash = _encryptService.Encrypt(model.Password, salt: Encoding.UTF8.GetBytes(search.PasswordSalt));

                if (passHash == search.PasswordHash)
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }

        public async Task<Client> FindUserByNickname(string name) => await _clientManager.GetClientByNameAsync(name);
        public async Task<Client> FindUserById(int id) => await _clientManager.GetAsync(id);

        /* TODO: 
         
        В связи с тем что в тмл будет действовать система, при которой админы не могут вообще делать что то с деньгами,
        а модераторы могут, всоре параметр isHaveAccesToAdminPanel будет расширен в isBlockAdminEconimcs которые точно задает то,
        что пользователь админ и запрещает ему передавать деньги.
         */
        public async Task<List<Client>> GetAllFilterClient(bool getBanned = false, bool getAdmin = true, bool getMe = false, bool getBlockedEconomicUser = false)
        {
            var list = await _clientManager.GetAllAsync();
            var client = await GetClient();
            var returnList = new List<Client>();

            foreach(var i in list)
            {
                var roleUser = await _rm.GetAsync(i.RoleId);

                // check user == user

                if (client.Id == i.Id && !getMe)
                    continue;

                // check is banned 

                if (!roleUser.isHaveAccessToSite && !getBanned)
                    continue;

                // check is admin

                if (roleUser.isHaveAccesToAdminPanel && !getAdmin)
                    continue;

                if(roleUser.isBlockedEconomic && !getBlockedEconomicUser)
                    continue;

                returnList.Add(i);
            }
                
            return returnList;
        }

        /* C# MD5 EQUALENT PHP */
        public static string PHPMd5Hash(string originalPassword)
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;

            // Conver the original password to bytes; then create the hash
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(originalPassword);
            encodedBytes = md5.ComputeHash(originalBytes);

            // Bytes to string
            return System.Text.RegularExpressions.Regex.Replace(BitConverter.ToString(encodedBytes), "-", "").ToLower();


        }
    }
}
