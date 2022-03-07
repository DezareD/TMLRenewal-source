using Microsoft.JSInterop;
using RenewalTML.Data;
using RenewalTML.Data.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.EventBus.Local;

namespace RenewalTML.Data
{
    public interface IVirtualNavigationServices
    {
        Task<CompleteVirtualRedirect> GenerateRouterBlock(string url);
        Task<CompleteVirtualRedirect> ReRedirectClient(string url, bool isPopsatate = false, bool forceLoad = false, bool hardLoad = false);
        void PageAddedServices(App _applicationContainer, IClientAuthServices _userServices);
        void SetPageInformationForEnchantedNavigation(RenewalTMLComponentBase componentBase);
    }

    public class VirtualNavigationServices : ApplicationService, IVirtualNavigationServices
    {
        /* Виртуальная Система Навигации - сервис, по вверх классической системы навигации на странице
         * Существует 4 вида навигации:
         * 
         * 1. Нажатие кнопки, т.е активация какой-то url. Захват действия происходит при нажатии кнопки. После чего система уже сама решает куда
         * снавигировать пользователя
         * 2. Системный переход, т.е например при переходе на страницу, система перебрасывает пользователя на страницу блокировки. Тесно связан с
         * третьим пунктом, можно считать что это один и тот же пункт практически, потому что 3 пункт использует реализацию второго. Происходит
         * путём блокировки рендеринга страницы до момента получения результата маршрутизации, и переносит на нужную страницу если это нужно.
         * 3. Престраничная навигация, т.е навигация при заходе на страницу. Использует метод системного перехода. Проверяет на всевозможные паттерны.
         * По типу тот ли это пользователя, можно ли сюда таким заходить. ВАЖНО: Permission проверяется вне этой системы, они задаются в RouterBlock's
         * 4. Браузерная навигация, т.е нажатие на стрелочки в браузере. Использует методы навигации 1 вида.
         * 
         * 
         * Функция GenerateRouterBlock принимает на вход текстовую строку - ссылку, на которую должен перейти пользователь. RouterBlock - выходные
         * данные, информация о странице, на которую нужно в итоге перенести пользователя. Страницы блокировки, ошибки 404 и т.д будут сделаны в
         * виде отдельных страниц позже. 
         * 
         * 
         * 
         * 
         * 
         * 
         */

        /*
         *  new RouterRedirector(""),
            new RouterRedirector("auth", guestAcces: true, isShowNavMenu: false, isShowBarMenu: false,
                title: "Авторизация"),
            new RouterRedirector("error/accessdenied", isShowNavMenu: false, isShowBarMenu: false),
            new RouterRedirector("error", guestAcces: false, isShowNavMenu: false, isShowBarMenu: false, permissionMask: "1",
                title: "Ошибка", informer_mini: "error"),
            new RouterRedirector("profile/{int}/awards", urlStringBitMask: "101", title: "Ваши достижения"),
            new RouterRedirector("profile/{int}", urlStringBitMask: "10", title: "Профиль"),
            new RouterRedirector("profile/{int}/settings", urlStringBitMask: "101", title: "Настройки пользователя"),
            new RouterRedirector("profile/{int}/leveling", urlStringBitMask: "101", title: "Уровни и прокачка"),
            new RouterRedirector("profile/{int}/actions/{string}", urlStringBitMask: "1010", title: "Действия"),
            new RouterRedirector("apanel/userlist", permissionMask: "111", informer_mini: "admin", title: "Список пользователей",
                prefix: "APanel - "), // todo: change permission mask
            new RouterRedirector("apanel/userSingle/{int}",  informer_mini: "admin", title: "Управление пользователем", prefix: "APanel - ", urlStringBitMask: "110"),
            new RouterRedirector("apanel/roles", permissionMask: "111", informer_mini: "admin", title: "Управления ролями",
                prefix: "APanel - "),
            new RouterRedirector("apanel", permissionMask: "11", informer_mini: "admin", title: "Главная", prefix: "APanel - ")
        */

        public static List<VirtualRouterBlock> routerRedirectors = new List<VirtualRouterBlock>()
        {
            new VirtualRouterBlock(new string[] { "" }, title: "Главная", urlStringBitMask: "1"),
            new VirtualRouterBlock(new string[] { "profile", "?{int}" }, title: "Профиль", urlStringBitMask: "10"),
            new VirtualRouterBlock(new string[] { "profile", "?{int}", "settings" },  title: "Настройки пользователя",  urlStringBitMask: "101"),
            new VirtualRouterBlock(new string[] { "profile", "?{int}", "awards" },  title: "Ваши достижения",  urlStringBitMask: "101"),
            new VirtualRouterBlock(new string[] { "profile", "?{int}", "tickets" },  title: "Ваши заявки",  urlStringBitMask: "101"),
            new VirtualRouterBlock(new string[] { "profile", "?{int}", "leveling" },  title: "Уровни и прокачка",  urlStringBitMask: "101"),
            new VirtualRouterBlock(new string[] { "profile", "?{int}", "actions", "?{string}" },  title: "Хаб действий",  urlStringBitMask: "1010"),
            new VirtualRouterBlock(new string[] { "apanel" },
                title: "Админ-панель",
                permissionMask: PermissionBitTransfer.PermissionToMask(new Role() { isHaveAccessToSite = true,
                    isHaveAccesToAdminPanel = true
                }),
                prefix: "APanel -",
                urlStringBitMask: "1",
                informer_mini: "admin" ),
            new VirtualRouterBlock(new string[] { "apanel", "roles" },
                title: "Управление ролями",
                permissionMask: PermissionBitTransfer.PermissionToMask(new Role() { isHaveAccessToSite = true,
                    isHaveAccesToAdminPanel = true,
                    isHaveAccesToEditRoles = true
                }), prefix: "APanel -",
                urlStringBitMask: "11",
                informer_mini: "admin" ),
            new VirtualRouterBlock(new string[] { "apanel", "userSingle", "?{int}" },
                title: "Редактирование пользователя",
                permissionMask: PermissionBitTransfer.PermissionToMask(new Role() { isHaveAccessToSite = true,
                    isHaveAccesToAdminPanel = true
                    // other on page
                }),
                urlStringBitMask: "110",
                prefix: "APanel -",
                informer_mini: "admin" ),
            new VirtualRouterBlock(new string[] { "apanel", "userlist" },
                title: "Список пользователей",
                permissionMask: PermissionBitTransfer.PermissionToMask(new Role() { isHaveAccessToSite = true,
                    isHaveAccesToAdminPanel = true,
                    isHaveToViewUserList = true
                }),
                urlStringBitMask: "11",
                prefix: "APanel -",
                informer_mini: "admin" ),
            new VirtualRouterBlock(new string[] { "apanel", "tickets", "requestMoney" },
                title: "Проверка прошений",
                permissionMask: PermissionBitTransfer.PermissionToMask(new Role() { isHaveAccessToSite = true,
                    isHaveToModerateUserAccount = true
                }),
                urlStringBitMask: "111",
                prefix: "APanel -",
                informer_mini: "admin"
            ),
            new VirtualRouterBlock(new string[] {"apanel", "systemSettings"},
                title: "Настройки сайта",
                permissionMask: PermissionBitTransfer.PermissionToMask(new Role() { isHaveAccessToSite = true,
                    isHaveAccesToAdminPanel = true,
                    isHaveAccesToViewSystemSettings = true
                }),
                urlStringBitMask: "11",
                prefix: "APanel -",
                informer_mini: "admin")
        };

        public static readonly string bannedUrl = "user/banned";
        public static readonly string errorUrl = "user/error/"; // + added content
        public static readonly string accessDeniedUrl = "user/accessDenid";

        private readonly ClientManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly SystemConfigurationManager _configurationManager;
        private readonly IJSRuntime _js;

        // added

        private App _applicationContainer { get; set; }
        private IClientAuthServices _userServices { get; set; }
        private RenewalTMLComponentBase _componentBase { get; set; }

        public VirtualNavigationServices(SystemConfigurationManager configurationManager, ClientManager userManager, RoleManager roleManager, IJSRuntime js)
        {
            _configurationManager = configurationManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _js = js;
        }

        public void SetPageInformationForEnchantedNavigation(RenewalTMLComponentBase componentBase) => _componentBase = componentBase;

        public void PageAddedServices(App _applicationContainer, IClientAuthServices _userServices)
        {
            this._applicationContainer = _applicationContainer;
            this._userServices = _userServices;
        }

        private CompleteVirtualRedirect FindVirtualBlock(string url)
        {
            // logic

            var urlSplit = url.Split(new char[] { '/', '#' });

            foreach (var block in routerRedirectors)
            {
                if (block.url.Length == urlSplit.Length)
                {
                    var accept = true;

                    for (int i = 0; i < urlSplit.Length; i++)
                        if (block.urlStringBitMask[i] == '1')
                            accept = accept && (block.url[i] == urlSplit[i]);

                    if (accept)
                    {
                        return new CompleteVirtualRedirect()
                        {
                            completeUrl = url,
                            informer = block.informer_mini,
                            isShowBar = block.isShowBarMenu,
                            isShowMenu = block.isShowNavMenu,
                            Title = block.prefix + block.title,
                            permissionMask = block.permissionMask
                        };
                    }
                }
            }

            return null;
        }

        public async Task<CompleteVirtualRedirect> ReRedirectClient(string url, bool isPopsatate = false, bool forceLoad = false, bool hardLoad = false)
        {
            var block = await GenerateRouterBlock(url);

            await _applicationContainer.RedirectingStart(block, isPopsatate);
            await _js.InvokeVoidAsync("Blazor._internal.navigationManager.navigateTo", block.completeUrl, forceLoad, hardLoad || isPopsatate);

            if (_componentBase != null)
                await _componentBase.OnInitializedComponent();

            return block;
        }

        public async Task<CompleteVirtualRedirect> GenerateRouterBlock(string url)
        {
            if (!String.IsNullOrWhiteSpace(url))
            {
                if (url[0] == '/')
                    url = url.Substring(1);

                if (url[url.Length - 1] == '/')
                    url = url.Remove(url.Length - 1, 1);
            }

            var user = await _userServices.GetClient();
            var permission = await _roleManager.GetDefaultRoleGuest();

            var informer = await _configurationManager.GetValue("system_main.subname");
            var brandname = await _configurationManager.GetValue("system_main.brandname");

            var siteIsOn = await _configurationManager.GetValue("system_main.siteisonline");

            if (user != null)
                permission = await _roleManager.GetAsync(user.RoleId);

            if (permission != null)
            {
                if (!permission.isHaveAccesToOfflineSite)
                    if (Convert.ToBoolean(siteIsOn) == false)
                        return new CompleteVirtualRedirect()
                        {
                            completeUrl = "offline",
                            informer = "site is offline",
                            isShowBar = false,
                            isShowMenu = false,
                            Title = "Сайт выключен",
                            BrandName = brandname
                        };
            }
            else
            {
                if (Convert.ToBoolean(siteIsOn) == false)
                    return new CompleteVirtualRedirect()
                    {
                        completeUrl = "offline",
                        informer = "site is offline",
                        isShowBar = false,
                        isShowMenu = false,
                        Title = "Сайт выключен",
                        BrandName = brandname
                    };
            }

            if (user == null)
                return new CompleteVirtualRedirect()
                {
                    completeUrl = "auth",
                    informer = "authorization",
                    isShowBar = false,
                    isShowMenu = false,
                    Title = "Авторизация пользователя",
                    BrandName = brandname
                };
            else if (url.Replace("/", "") == "auth")
            {
                // проверка на бан после авторизации ( fix )
                if (!permission.isHaveAccessToSite)
                    return new CompleteVirtualRedirect()
                    {
                        completeUrl = bannedUrl,
                        informer = "banned",
                        isShowBar = false,
                        isShowMenu = false,
                        Title = "Блокировка",
                        BrandName = brandname
                    };

                return new CompleteVirtualRedirect()
                {
                    completeUrl = "",
                    informer = informer,
                    isShowBar = true,
                    isShowMenu = true,
                    Title = "Главная",
                    BrandName = brandname
                };
            }

            // Есл пользователь не имеет доступ к сайту - сразу в бан
            if (!permission.isHaveAccessToSite)
                return new CompleteVirtualRedirect()
                {
                    completeUrl = bannedUrl,
                    informer = "banned",
                    isShowBar = false,
                    isShowMenu = false,
                    Title = "Блокировка",
                    BrandName = brandname
                };

            // remove string spliter's
            var block = FindVirtualBlock(url);


            if (block == null)
            {
                return new CompleteVirtualRedirect()
                {
                    completeUrl = errorUrl + "404",
                    informer = "error",
                    isShowBar = true,
                    isShowMenu = true,
                    Title = "Страницы не существует",
                    BrandName = brandname
                };
            }
            else
            {
                // Проверяем доступность сайта с правилами permission's
                if (!PermissionBitTransfer.CompareMask(PermissionBitTransfer.PermissionToMask(permission), block.permissionMask))
                    return new CompleteVirtualRedirect()
                    {
                        completeUrl = accessDeniedUrl,
                        informer = "access_denied",
                        isShowBar = true,
                        isShowMenu = false,
                        Title = "Доступ запрещён",
                        BrandName = brandname
                    };

                // В случае, если пользователь зашёл на страницы, без доступа и для забанненых, но не является не забанненым, и доступ не запрещен
                // переходим на главную
                if (url.StartsWith(accessDeniedUrl) || url.StartsWith(bannedUrl))
                {
                    
                    var mainPage = routerRedirectors.First();
                    var informer_mini_settings = "";

                    if (mainPage.informer_mini == "__default")
                        informer_mini_settings = informer;
                    else informer_mini_settings = mainPage.informer_mini;

                    return new CompleteVirtualRedirect()
                    {
                        completeUrl = url,
                        informer = informer_mini_settings,
                        isShowBar = mainPage.isShowBarMenu,
                        isShowMenu = mainPage.isShowNavMenu,
                        Title = mainPage.prefix + mainPage.title,
                        BrandName = brandname
                    };
                }

                if (block.informer == "__default")
                    block.informer = informer;

                block.BrandName = brandname;

                return block;
            }
        }
    }

    public class CompleteVirtualRedirect
    {
        public string completeUrl { get; set; }
        public string Title { get; set; }
        public bool isShowMenu { get; set; }
        public bool isShowBar { get; set; }
        public string informer { get; set; }
        public string permissionMask { get; set; }
        public string BrandName { get; set; }
    }

    public class VirtualRouterBlock
    {
        // Единица информация о всех страничках.
        public VirtualRouterBlock(string[] url, string permissionMask = "-", bool isShowNavMenu = true,
            bool isShowBarMenu = true, string title = "Главная", string prefix = "TML Renewal - ", string informer_mini = "__default",
            string patternAccess = "", string urlStringBitMask = "1")
        {
            this.url = url;
            this.isShowBarMenu = isShowBarMenu;
            this.isShowNavMenu = isShowNavMenu;

            this.title = title;
            this.prefix = prefix;
            this.informer_mini = informer_mini;

            if (this.permissionMask == "-")
                this.permissionMask = PermissionBitTransfer.GetDefaultMask();
            else this.permissionMask = permissionMask;

            this.urlStringBitMask = urlStringBitMask;
        }

        public string urlStringBitMask { get; set; }

        // Ссылка
        public string[] url { get; set; }

        // Маска для Permission's
        public string permissionMask { get; set; }

        // Нужно ли показать главное меню.
        public bool isShowNavMenu { get; set; }

        // Нужно ли показать вернхее меню
        public bool isShowBarMenu { get; set; }

        // Название страницы
        public string title { get; set; }

        // Префикс в названии страницы
        public string prefix { get; set; }

        // плашка над брендингом
        public string informer_mini { get; set; }
    }

    public class EventUserPageReRender
    {
        public string newUrl { get; set; }
    }

    public class PermissionBitTransfer
    {
        public static int GetLenghtMask() => GetDefaultMask().Length;
        public static string GetDefaultMask() => PermissionToMask(new Role() { isHaveAccessToSite = true });

        public static string gi(bool b)
        {
            if (b) return "1";
            else return "0";
        }

        public static string PermissionToMask(Role permission)
        {
            return gi(permission.isHaveAccessToSite)        // 111
                + gi(permission.isHaveAccesToAdminPanel)
                + gi(permission.isHaveAccesToEditRoles)
                + gi(permission.isHaveToModerateTransactions)
                + gi(permission.isBlockedEconomic)
                + gi(permission.isHaveAccesToPremiumEditor)
                + gi(permission.isHaveAccesToUltimateEditor)
                + gi(permission.isHaveToAdministrateUserAccount)
                + gi(permission.isHaveToModerateUserAccount)
                + gi(permission.isHaveToViewUserList);
        }

        public static Role MaskToPermission(string mask)
        {
            if (mask.Length < GetLenghtMask())
                mask = mask.PadRight(GetLenghtMask() - mask.Length + 1, '0');

            var obj = new Role()
            {
                isHaveAccessToSite = Convert.ToBoolean(mask[0]),
                isHaveAccesToAdminPanel = Convert.ToBoolean(mask[1]),
                isHaveAccesToEditRoles = Convert.ToBoolean(mask[2]),
                isHaveToModerateTransactions = Convert.ToBoolean(mask[3]),
                isBlockedEconomic = Convert.ToBoolean(mask[4]),
                isHaveAccesToPremiumEditor = Convert.ToBoolean(mask[5]),
                isHaveAccesToUltimateEditor = Convert.ToBoolean(mask[6]),
                isHaveToAdministrateUserAccount = Convert.ToBoolean(mask[7]),
                isHaveToModerateUserAccount = Convert.ToBoolean(mask[8]),
                isHaveToViewUserList = Convert.ToBoolean(mask[9])
            };

            return obj;
        }

        public static bool CompareMask(string userMask, string permissionMask)
        {
            if (permissionMask.Length < GetLenghtMask())
                permissionMask = permissionMask.PadRight(GetLenghtMask(), '0');

            for (int i = 0; i < userMask.Length; i++)
            {
                if (userMask[i] == '0' && permissionMask[i] == '1') // Если роль пользователя не соответствует роли прав
                    return false;
            }

            return true;
        }
    }

}
