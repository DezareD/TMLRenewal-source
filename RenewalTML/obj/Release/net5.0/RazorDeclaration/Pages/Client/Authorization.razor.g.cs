// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace RenewalTML.Pages.Client
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
#nullable restore
#line 1 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\_Imports.razor"
using RenewalTML;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\_Imports.razor"
using RenewalTML.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\_Imports.razor"
using RenewalTML.Shared.Designing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\_Imports.razor"
using RenewalTML.Shared.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\_Imports.razor"
using RenewalTML.Shared.Navigation;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\_Imports.razor"
using RenewalTML.Shared.Notifications;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\_Imports.razor"
using RenewalTML.Shared.Transactions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\_Imports.razor"
using RenewalTML.Shared.Validation;

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\_Imports.razor"
using RenewalTML.Shared.Clients;

#line default
#line hidden
#nullable disable
#nullable restore
#line 18 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\_Imports.razor"
using RenewalTML.Shared.Admin;

#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\_Imports.razor"
using RenewalTML.Shared.Exstention.ClassAddons;

#line default
#line hidden
#nullable disable
#nullable restore
#line 20 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\_Imports.razor"
using RenewalTML.Shared.Exstention;

#line default
#line hidden
#nullable disable
#nullable restore
#line 21 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\_Imports.razor"
using RenewalTML.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 22 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\_Imports.razor"
using RenewalTML.Data.Model;

#line default
#line hidden
#nullable disable
#nullable restore
#line 23 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\_Imports.razor"
using RenewalTML.Pages.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 24 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\_Imports.razor"
using Blazorise;

#line default
#line hidden
#nullable disable
#nullable restore
#line 25 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\_Imports.razor"
using RenewalTML.Data.Dto;

#line default
#line hidden
#nullable disable
#nullable restore
#line 26 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\_Imports.razor"
using RenewalTML.Data.JSInteropHelper;

#line default
#line hidden
#nullable disable
#nullable restore
#line 27 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\_Imports.razor"
using Newtonsoft.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 28 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\_Imports.razor"
using Microsoft.AspNetCore.SignalR.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 29 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\_Imports.razor"
using Blazorise.Utilities;

#line default
#line hidden
#nullable disable
#nullable restore
#line 30 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\_Imports.razor"
using Microsoft.AspNetCore.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 31 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\_Imports.razor"
using System.Threading.Tasks;

#line default
#line hidden
#nullable disable
#nullable restore
#line 32 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\_Imports.razor"
using System.ComponentModel.DataAnnotations;

#line default
#line hidden
#nullable disable
#nullable restore
#line 33 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\_Imports.razor"
using System.Linq.Expressions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 34 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\_Imports.razor"
using System.Reflection;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/auth")]
    public partial class Authorization : RenewalTMLComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 174 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Authorization.razor"
       
    [CascadingParameter(Name = "loyout_update")]
    public EventCallback loyout_update { get; set; }

    [CascadingParameter(Name = "navbar_update")]
    public EventCallback navbar_update { get; set; }

    [CascadingParameter(Name = "mainbar_updateImage")]
    public EventCallback<bool> mainbar_updateImage { get; set; }

    /* main auth */
    private bool authIsLoading { get; set; }
    private bool authVKIsLoading { get; set; }

    private ContextValidationModule<UserLoginModel> _userLoginModule { get; set; }

    /* two phase register */
    private ContextValidationModule<TwoPhaseRegistrationVK> _twoPhaseRegistrationModule { get; set; }
    private VKAuthorizeDataInterop vkSessionObject { get; set; }
    private bool isStartedTwoPhaseVKRegister { get; set; }
    private bool authIsLoadingTwoPhase { get; set; }

    public ValidationMessageBlock AuthErrorMessage { get; set; }
    public ValidationMessageBlock TwoPhaseInfo { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _userLoginModule = new ContextValidationModule<UserLoginModel>(new UserLoginModel());
        _twoPhaseRegistrationModule = new ContextValidationModule<TwoPhaseRegistrationVK>(new TwoPhaseRegistrationVK());

        TwoPhaseInfo = new ValidationMessageBlock("Вы проходите второй этап регистрации. Для завершения регистрации укажите никнейм и пароль по которому будет производиться вход в аккаунт. На логин и пароль действуют ограничения: <ul><li>Длина логина от 4 до 20 символов. Доступны символы латинского алфавита, а так же знак точки.</li><li>Длина пароля от 6 до 20 символов. Ограничений на знаки нет.</li></ul> В случае незаврешения данного этапа, регистрация будет отменена.", "info");

        /* tippy loader and create */

        await _js.InvokeVoidAsync("loadScript", "libs/tippy.js/tippy.min.js");

        await _js.InvokeVoidAsync("CreateTippyElement",
        new List<TooltipModel>() {
                                new TooltipModel(".-tp-remember-me", "Запомнить ваш аккаунт на день или на год?", ToolTipStyles.DefaultStyle, width: 350)
                                                    });

        await ChangePageLoadStatus(true);
    }

    private async Task LoginExecuted()
    {
        authIsLoading = true;

        _userLoginModule.SetFielAllStatus(ValidationStatus.None);

        if (_userLoginModule._validationContext.ValidateAll())
        {
            var isLoginReady = await _userServices.LoginIsReady(_userLoginModule._model);

            if (!isLoginReady)
            {
                _userLoginModule.SetFielAllStatus(ValidationStatus.Error);
                AuthErrorMessage = new ValidationMessageBlock("Неверные данные для входа.", "error");
            }
            else
            {

                var user = await _userServices.FindUserByNickname(_userLoginModule._model.Name);
                await _userServices.Authenticate(user, _userLoginModule._model.isPersistant);
                //await mainbar_updateImage.InvokeAsync(true); // flag true = reupdate user
                //await navbar_update.InvokeAsync(); // Обновляем саму менюшку, если добавились поля с авторизацией или типо того

                //await _virtualNavigationServices.ReRedirectClient("");
                await _js.InvokeVoidAsync("LoadingRepositry.reloadPage");
            }
        }
        authIsLoading = false;
    }

    private async Task TwoPhaseExecuded()
    {
        authIsLoadingTwoPhase = true;

        if (isStartedTwoPhaseVKRegister)
        {
            var registerison = await _systemConfigurationServices.Get("system_main.registerison");

            _twoPhaseRegistrationModule.SetFielAllStatus(ValidationStatus.None);

            if (Convert.ToBoolean(registerison) == false)
            {
                AuthErrorMessage = new ValidationMessageBlock("Регистрация на сайте временно отключена.", "error");
                authIsLoadingTwoPhase = false;
                return;
            }

            if (_twoPhaseRegistrationModule._validationContext.ValidateAll())
            {
                var isFreenickname = await _userServices.IsFreeNickName(_twoPhaseRegistrationModule._model.Name);

                if (!isFreenickname)
                {
                    _twoPhaseRegistrationModule.SetFieldStatus("Name", ValidationStatus.Error);
                    AuthErrorMessage = new ValidationMessageBlock("Такой никнейм уже существует.", "error");
                }
                else
                {
                    var user = await _userServices.RegisterClient(_twoPhaseRegistrationModule._model, vkSessionObject);
                    await _userServices.Authenticate(user, true);
                    await _js.InvokeVoidAsync("LoadingRepositry.reloadPage");
                }
            }
        }
        authIsLoadingTwoPhase = false;
    }

    /* VK AUTHORIZATION */

    private async Task ExecutedVkAuthorization()
    {
        authVKIsLoading = true;
        await _js.InvokeVoidAsync("VKAuthorizeFunction.AuthorizeVk", DotNetObjectReference.Create(this));
    }

    [JSInvokable]
    public async Task VKAuthorizeComplete(string jsonStringify)
    {
        if (!ErrorCatchingFunc.isHaveError(jsonStringify).haveError)
        {
            vkSessionObject = JsonConvert.DeserializeObject<VKAuthorizeDataInterop>(jsonStringify);

            var sessionAccepted = _userServices.IsSessionAuthComplete(vkSessionObject);

            if (sessionAccepted)
            {
                var isRegister = await _userServices.IsUserRegisterVK(vkSessionObject);

                if (isRegister)
                {
                    // Пользовтель уже зарегистрирован, просто входим в аккаунт.
                    var user = await _userServices.GetUserWithVK(vkSessionObject);

                    await _userServices.Authenticate(user, true);

                    //await navbar_update.InvokeAsync(); // Обновляем саму менюшку, если добавились поля с авторизацией или типо того
                    //await mainbar_updateImage.InvokeAsync(true);

                    //await _virtualNavigationServices.ReRedirectClient("");
                    await _js.InvokeVoidAsync("LoadingRepositry.reloadPage");
                }
                else
                {
                    isStartedTwoPhaseVKRegister = true;
                    if (AuthErrorMessage != null)
                        AuthErrorMessage.isShow = false; // Если выдало ошибку при входе, то при переходе на регистрацию ошибка все ещё весела.
                    StateHasChanged();
                    // Нужно пройти второй этап регистрации и заполнить дополнительные данные.
                }
            }
        }

        authVKIsLoading = false;
        await InvokeAsync(() => { StateHasChanged(); });
    }

    // TODO: Проверить вход на обновление навбара

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ISystemConfigurationServices _systemConfigurationServices { get; set; }
    }
}
#pragma warning restore 1591