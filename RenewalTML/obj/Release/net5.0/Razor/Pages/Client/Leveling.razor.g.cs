#pragma checksum "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8de9cfa3c777ead337bd66079644eb117b5178f2"
// <auto-generated/>
#pragma warning disable 1591
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
    [Microsoft.AspNetCore.Components.RouteAttribute("/profile/{userId:int}/leveling")]
    public partial class Leveling : RenewalTMLComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
#nullable restore
#line 8 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
 if (_isComplete)
{

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(0, @"<div class=""block block-page-information"" style=""background: url(/imgs/application/page_banner.png)""><p class=""title"">Опыт и таблица уровней</p>
    <p class=""info"">Просматривайте ваш текущий уровень и смотрите что откроется вам на всеразличых уровнях.</p></div>");
#nullable restore
#line 14 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
}

#line default
#line hidden
#nullable disable
            __builder.OpenElement(1, "div");
            __builder.AddAttribute(2, "class", "block p-normal -js-page-maxheight-relaod");
            __builder.AddAttribute(3, "style", 
#nullable restore
#line 15 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
                                                               _isComplete? "max-height:900px;":"max-height:90px;"

#line default
#line hidden
#nullable disable
            );
#nullable restore
#line 16 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
     if (_isComplete)
    {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(4, "div");
            __builder.AddAttribute(5, "class", "level-bar-info-text");
#nullable restore
#line 18 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
__builder.AddContent(6, user.CurrencyExp);

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(7, " exp. из ");
#nullable restore
#line 18 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
__builder.AddContent(8, nexLevelExp);

#line default
#line hidden
#nullable disable
            __builder.AddContent(9, " exp.");
            __builder.CloseElement();
            __builder.AddMarkupContent(10, "\r\n        ");
            __builder.OpenElement(11, "div");
            __builder.AddAttribute(12, "style", "display: flex; align-items: center; margin-bottom:20px;");
            __builder.OpenElement(13, "div");
            __builder.AddAttribute(14, "style", "flex:1;");
            __builder.AddAttribute(15, "class", "progress-bar-level");
            __builder.OpenElement(16, "img");
            __builder.AddAttribute(17, "src", "../imgs/userlevels/userlevel_" + (
#nullable restore
#line 20 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
                                                                                                     user.Level

#line default
#line hidden
#nullable disable
            ) + ".svg");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(18, "\r\n            ");
            __builder.OpenComponent<Blazorise.Progress>(19);
            __builder.AddAttribute(20, "Style", "flex:8;");
            __builder.AddAttribute(21, "Size", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Blazorise.Size?>(
#nullable restore
#line 21 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
                                            Size.Medium

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(22, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.OpenComponent<Blazorise.ProgressBar>(23);
                __builder2.AddAttribute(24, "Class", "texted");
                __builder2.AddAttribute(25, "Color", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Blazorise.Color>(
#nullable restore
#line 22 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
                                                   Color.Primary

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(26, "Style", "padding-left: 15px; padding-right: 15px;");
                __builder2.AddAttribute(27, "Animated", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 22 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
                                                                                                                             true

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(28, "Value", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Int32?>(
#nullable restore
#line 22 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
                                                                                                                                           levelProgress

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(29, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
#nullable restore
#line 22 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
__builder3.AddContent(30, levelProgress);

#line default
#line hidden
#nullable disable
                    __builder3.AddContent(31, "%");
                }
                ));
                __builder2.CloseComponent();
            }
            ));
            __builder.CloseComponent();
            __builder.AddMarkupContent(32, "\r\n            ");
            __builder.OpenElement(33, "div");
            __builder.AddAttribute(34, "style", "flex:1;");
            __builder.AddAttribute(35, "class", "progress-bar-level next");
            __builder.OpenElement(36, "img");
            __builder.AddAttribute(37, "src", "../imgs/userlevels/userlevel_" + (
#nullable restore
#line 24 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
                                                                                                          user.Level==50? "50":user.Level+1

#line default
#line hidden
#nullable disable
            ) + ".svg");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(38, "\r\n        ");
            __builder.OpenElement(39, "div");
            __builder.AddAttribute(40, "class", "level-outer");
            __builder.OpenElement(41, "div");
            __builder.AddAttribute(42, "class", "level-system-block");
#nullable restore
#line 28 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
                 for (int i = 1; i <= 25; i++)
                {
                    var this_level = levelDataInformation.Where(m => m.Level == i).FirstOrDefault();

                    if (this_level != null)
                    {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(43, "div");
            __builder.AddAttribute(44, "class", "level-block");
            __builder.AddAttribute(45, "style", 
#nullable restore
#line 34 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
                                                          (user.Level >= this_level.Level)? "" : "opacity: 0.5;"

#line default
#line hidden
#nullable disable
            );
            __builder.OpenElement(46, "div");
            __builder.AddAttribute(47, "class", "level-outer-image");
            __builder.OpenElement(48, "img");
            __builder.AddAttribute(49, "src", "../imgs/userlevels/userlevel_" + (
#nullable restore
#line 35 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
                                                                                                    this_level.Level

#line default
#line hidden
#nullable disable
            ) + ".svg");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(50, "\r\n                            ");
            __builder.OpenElement(51, "div");
            __builder.OpenElement(52, "p");
            __builder.AddAttribute(53, "class", "level-naming");
#nullable restore
#line 37 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
__builder.AddContent(54, this_level.Level);

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(55, " Уровень");
            __builder.CloseElement();
#nullable restore
#line 38 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
                                 foreach (var item in this_level.InformationAttributes)
                                {

#line default
#line hidden
#nullable disable
            __builder.OpenComponent<Blazorise.Check<bool>>(56);
            __builder.AddAttribute(57, "Class", "disabled");
            __builder.AddAttribute(58, "Disabled", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 40 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
                                                                                                                        true

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(59, "Checked", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<bool>(
#nullable restore
#line 40 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
                                                          userPermissions[this_level.Level]

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(60, "CheckedChanged", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<bool>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<bool>(this, global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => userPermissions[this_level.Level] = __value, userPermissions[this_level.Level]))));
            __builder.AddAttribute(61, "CheckedExpression", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Linq.Expressions.Expression<System.Func<bool>>>(() => userPermissions[this_level.Level]));
            __builder.AddAttribute(62, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
#nullable restore
#line 40 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
__builder2.AddContent(63, item);

#line default
#line hidden
#nullable disable
            }
            ));
            __builder.CloseComponent();
#nullable restore
#line 41 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
                                }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 44 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
                    }
                }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(64, "\r\n            ");
            __builder.OpenElement(65, "div");
            __builder.AddAttribute(66, "class", "level-system-block");
#nullable restore
#line 48 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
                 for (int i = 25; i <= 50; i++)
                {
                    var this_level = levelDataInformation.Where(m => m.Level == i).FirstOrDefault();

                    if (this_level != null)
                    {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(67, "div");
            __builder.AddAttribute(68, "class", "level-block");
            __builder.AddAttribute(69, "style", 
#nullable restore
#line 54 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
                                                          (user.Level >= this_level.Level)? "" : "opacity: 0.5;"

#line default
#line hidden
#nullable disable
            );
            __builder.OpenElement(70, "div");
            __builder.AddAttribute(71, "class", "level-outer-image");
            __builder.OpenElement(72, "img");
            __builder.AddAttribute(73, "src", "../imgs/userlevels/userlevel_" + (
#nullable restore
#line 55 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
                                                                                                    this_level.Level

#line default
#line hidden
#nullable disable
            ) + ".svg");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(74, "\r\n                            ");
            __builder.OpenElement(75, "div");
            __builder.OpenElement(76, "p");
            __builder.AddAttribute(77, "class", "level-naming");
#nullable restore
#line 57 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
__builder.AddContent(78, this_level.Level);

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(79, " Уровень");
            __builder.CloseElement();
#nullable restore
#line 58 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
                                 foreach (var item in this_level.InformationAttributes)
                                {

#line default
#line hidden
#nullable disable
            __builder.OpenComponent<Blazorise.Check<bool>>(80);
            __builder.AddAttribute(81, "Class", "disabled");
            __builder.AddAttribute(82, "Disabled", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 60 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
                                                                                                                        true

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(83, "Checked", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<bool>(
#nullable restore
#line 60 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
                                                          userPermissions[this_level.Level]

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(84, "CheckedChanged", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<bool>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<bool>(this, global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => userPermissions[this_level.Level] = __value, userPermissions[this_level.Level]))));
            __builder.AddAttribute(85, "CheckedExpression", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Linq.Expressions.Expression<System.Func<bool>>>(() => userPermissions[this_level.Level]));
            __builder.AddAttribute(86, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
#nullable restore
#line 60 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
__builder2.AddContent(87, item);

#line default
#line hidden
#nullable disable
            }
            ));
            __builder.CloseComponent();
#nullable restore
#line 61 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
                                }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 64 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
                    }
                }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 68 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
    }
    else
    {

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(88, "<div class=\"loader-wrapper\"><div class=\"btn-loader page\"></div></div>");
#nullable restore
#line 72 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
    }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 75 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Pages\Client\Leveling.razor"
       
    [Parameter]
    public int UserId { get; set; }

    private Client user { get; set; }

    private List<bool> userPermissions = new List<bool>();

    private int levelProgress { get; set; }
    private int nextLevel { get; set; }
    private int nexLevelExp { get; set; }

    private List<LevelDataInformation> levelDataInformation = new List<LevelDataInformation>()
    {
        new LevelDataInformation()
        {
            Level = 1,
            InformationAttributes = new List<string>()
            {
                "Доступ к сайту и основным функциям"
            }
        },
        new LevelDataInformation()
        {
            Level = 2,
            InformationAttributes = new List<string>()
            {
                "Доступ к переводам между пользователями.",
                "Доступ к пополнениям баланса."
            }
        },
        new LevelDataInformation()
        {
            Level = 48,
            InformationAttributes = new List<string>()
            {
                "Чиста тест."
            }
        }
    };

    protected override async Task OnInitializedAsync()
    {
        user = await _userServices.GetClient();

        if (user != null)
        {

            if (user.Id != UserId)
            {
                await _virtualNavigationServices.ReRedirectClient(VirtualNavigationServices.errorUrl + "404", hardLoad: true);
                return;
            }


            if (user.Level == 50) levelProgress = 100;
            else
            {
                nextLevel = user.Level + 1;
                nexLevelExp = _levelServices.GetRequeryGainExpToLevel(nextLevel);

                levelProgress = ((user.CurrencyExp / nexLevelExp) * 100);
            }

            for (int i = 1; i <= 50; i++)
            {
                userPermissions.Add(user.Level >= i);
            }

            await ChangePageLoadStatus(true);

            StateHasChanged();

            await _js.InvokeVoidAsync("MaxHeightLoader");
        }
        else await _virtualNavigationServices.ReRedirectClient(VirtualNavigationServices.accessDeniedUrl, hardLoad: true);
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ILevelsServices _levelServices { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IAwardServices _awardServices { get; set; }
    }
}
#pragma warning restore 1591