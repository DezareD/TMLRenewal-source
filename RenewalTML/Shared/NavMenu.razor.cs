using Microsoft.AspNetCore.Components;
using RenewalTML.Data;
using RenewalTML.Data.Dto;
using RenewalTML.Data.Model;
using RenewalTML.Shared.Exstention;
using RenewalTML.Shared.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenewalTML.Shared
{
    public class NavMenuBase : ComponentBase
    {
        [Inject] protected IClientAuthServices _userServices { get; set; }
        [Inject] protected IRolePermissionServices _permissionService { get; set; }
        [Inject] protected ITicketServices _ticketServices { get; set; }
        [Inject] protected NavigationManager _nm { get; set; }

        /* MAIN */
        protected NavigationBlock MainNavigationBlock { get; set; }
        protected MainMenuModel MainMenuModel { get; set; }

        /* PROFILE */
        protected NavigationBlock ProfileMenuBlock { get; set; }
        protected MainMenuModel ProfileMenu { get; set; }

        /* ADMIN */
        protected NavigationBlock AdminMenuBlock { get; set; }
        protected MainMenuModel AdminMenu { get; set; }

        public List<NavigationBlock> navigationBlocksList = new List<NavigationBlock>();

        protected override async Task OnInitializedAsync()
        {
            //await OnUpdatepPermission(); // CreateModel

            await base.OnInitializedAsync();
        }

        /*protected async Task OnUpdatepPermission()
        {
            var user = await _userServices.GetClient();
            var permission = await _permissionService.GetClientRole(user);

            ReadyMainMenu(permission, user);
            ReadyAdminMenu(permission, user);
            await ReadyProfileMenu(permission, user);
        }*/

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                navigationBlocksList = new List<NavigationBlock>
                {
                    AdminMenuBlock,
                    ProfileMenuBlock,
                    MainNavigationBlock
                };
            }

            return base.OnAfterRenderAsync(firstRender);
        }

        protected async Task CheckMenuUrling()
        {
            var url = _nm.ToBaseRelativePath(_nm.Uri);

            try
            {
                var user = await _userServices.GetClient();
                var permission = await _permissionService.GetClientRole(user);

                ReadyMainMenu(permission, user);
                await ReadyAdminMenu(permission, user);
                await ReadyProfileMenu(permission, user);

                if (url.StartsWith("auth") || url.StartsWith("error"))
                    MainMenuModel.isShow = false;
                else MainMenuModel.isShow = true;

                if (url.StartsWith("apanel/") || url == "apanel") // admin panel
                {
                    AdminMenu.isShow = true;
                    MainMenuModel.isMenuCollapsed = true;
                }
                else if (url.StartsWith("profile/")) // profile menu
                {
                    ProfileMenu.isShow = true;
                    MainMenuModel.isMenuCollapsed = true;
                }
                else
                {
                    ProfileMenu.isShow = false;
                    AdminMenu.isShow = false;

                    MainMenuModel.isMenuCollapsed = false;
                }

                /*if (navigationBlocksList != null)
                {
                    foreach (var item in navigationBlocksList)
                    {
                        if (item != null && item.Model != null)
                        {
                            ClearMenu(item);
                            FocusMenu(item);
                        }
                    }
                }*/

                CompleteMenu(AdminMenu);
                CompleteMenu(ProfileMenu);
                CompleteMenu(MainMenuModel);
            }
            catch (Exception) { } // is user is speed clicked
        }

        private void CompleteMenu(MainMenuModel nb)
        {
            ClearMenu(nb);
            FocusMenu(nb);
        }

        private void ClearMenu(MainMenuModel nb)
        {
            foreach (var item in nb.menuFields)
            {
                item.isOpen = false;
                item.item.isFocus = false;
                if (item.childItems != null)
                {
                    foreach (var _item in item.childItems)
                    {
                        _item.isFocus = false;
                    }
                }
            }
        }

        private void FocusMenu(MainMenuModel nb)
        {
            var url = _nm.ToBaseRelativePath(_nm.Uri);

            foreach (var item in nb.menuFields)
            {
                if (item.item.url == url)
                {
                    item.item.isFocus = true;
                }
                else
                {
                    if (item.childItems != null)
                    {
                        foreach (var _item in item.childItems)
                        {
                            if (_item.url == url)
                            {
                                item.isOpen = true;
                                _item.isFocus = true;
                            }
                        }
                    }
                }
            }
        }

        private async Task ReadyProfileMenu(Role permission, Client user)
        {
            var url = _nm.ToBaseRelativePath(_nm.Uri);
            Client urlUser = null;

            // profile urling: profile/{PROFILE_ID}/arg1/arg2/.../argN

            if (url.StartsWith("profile/")) // Если мы на страницах о профиле
            {
                var userIdContext = Convert.ToInt32(url.Split('/')[1]);

                urlUser = await _userServices.FindUserById(userIdContext);
            }

            var profileMenuFields = new List<MenuField>();


            profileMenuFields.Add(new MenuField()
            {
                item = new MenuItem("Профиль", url: (urlUser != null ? $"profile/{urlUser.Id}" : "profile/"), icon: "<i class=\"fal fa-user\"></i>", addedContent: (urlUser != null? $"<span class=\"navmenu-added-text-dynamic\">({urlUser.ScreenName})</span>": "")),
            });

            // Если мы на странице свого профиля, или [ на странице своих достижений к примеру ]
            if(urlUser != null && user.Id == urlUser.Id)
            {
                profileMenuFields.Add(new MenuField()
                {
                    item = new MenuItem("Действия", url: $"profile/{urlUser.Id}/actions#transferMoney", icon: "<i class=\"fal fa-tasks\"></i>")
                });

                profileMenuFields.Add(new MenuField()
                {
                    item = new MenuItem("Настройки", url: $"profile/{urlUser.Id}/settings", icon: "<i class=\"fal fa-cogs\"></i>")
                });

                profileMenuFields.Add(new MenuField()
                {
                    item = new MenuItem("Мои достижения", url: $"profile/{urlUser.Id}/awards", icon: "<i class=\"fal fa-award\"></i>")
                });

                profileMenuFields.Add(new MenuField()
                {
                    item = new MenuItem("Мой уровень", url: $"profile/{urlUser.Id}/leveling", icon: "<i class=\"fal fa-star\"></i>")
                });

                profileMenuFields.Add(new MenuField()
                {
                    item = new MenuItem("Мои заявки", url: $"profile/{urlUser.Id}/tickets", icon: "<i class=\"fal fa-ticket\"></i>",
                    addedContent: (urlUser.TicketsNonViewCount > 0 ? "<span class=\"admin-info-badge-menu\">" + urlUser.TicketsNonViewCount + "</span>" : ""))
                });
            }

            ProfileMenu = new MainMenuModel(profileMenuFields, "Профиль", isShow: false);
        }

        private async Task ReadyAdminMenu(Role permission, Client user)
        {
            var adminMenuFields = new List<MenuField>();

            adminMenuFields.Add(new MenuField()
            {
                item = new MenuItem("Роли", icon: "<i class=\"fal fa-user-tag\"></i>", url: "apanel/roles", isActive: permission.isHaveAccesToEditRoles)
            });

            adminMenuFields.Add(new MenuField()
            { // todo change permission
                item = new MenuItem("Все пользователи", icon: "<i class=\"fal fa-users\"></i>", url: "apanel/userlist", isActive: permission.isHaveAccesToEditRoles)
            });

            adminMenuFields.Add(new MenuField()
            { // todo change permission
                item = new MenuItem("Настройки сайта", icon: "<i class=\"fal fa-cogs\"></i>", url: "apanel/systemSettings", isActive: permission.isHaveAccesToViewSystemSettings)
            });

            var requestMoneyTicketCount = await _ticketServices.GetFillBalanceLastTicketsCount();

            adminMenuFields.Add(new MenuField()
            { // todo change permission
                item = new MenuItem("Заявки", icon: "<i class=\"fal fa-ticket\"></i>", isActive: permission.isHaveToModerateUserAccount, 
                addedContent: (requestMoneyTicketCount > 0? "<span class=\"admin-info-badge-menu\">" + requestMoneyTicketCount + "</span>" : "")),
                childItems = new List<MenuItem>()
                {
                    new MenuItem("Заявки на пополнения", icon: "<i class=\"fal fa-comments-dollar\"></i>", addedContent: (requestMoneyTicketCount > 0? "<span class=\"admin-info-badge-menu\">" + requestMoneyTicketCount + "</span>" : ""),
                    url: "apanel/tickets/requestMoney")
                }
            });

            AdminMenu = new MainMenuModel(adminMenuFields, "Админ-Панель", isShow: false);
        }

        private void ReadyMainMenu(Role permission, Client user)
        {
            var mainMenuFields = new List<MenuField>();

            mainMenuFields.Add(new MenuField()
            {
                item = new MenuItem("Главная", icon: "<i class=\"fal fa-home\"></i>", url: "")
            });

            if (permission.isHaveAccesToAdminPanel)
            {
                mainMenuFields.Add(new MenuField()
                {
                    item = new MenuItem("Админ Панель", icon: "<i class=\"fal fa-user-cog\"></i>", url: "apanel", addedContent: "<div class=\"admin-badge\">A</div>")
                });
            }
            MainMenuModel = new MainMenuModel(mainMenuFields, "Меню", true);
        }
    }
}
