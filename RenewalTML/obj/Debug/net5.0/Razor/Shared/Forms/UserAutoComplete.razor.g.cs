#pragma checksum "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Shared\Forms\UserAutoComplete.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3ff695830c555b61f5a46ce9fdcfb979f769ae49"
// <auto-generated/>
#pragma warning disable 1591
namespace RenewalTML.Shared.Forms
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
    public partial class UserAutoComplete : BaseTextInput<string>
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "input-group");
            __builder.OpenElement(2, "div");
            __builder.AddAttribute(3, "style", 
#nullable restore
#line 9 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Shared\Forms\UserAutoComplete.razor"
                  isLoaded == false? "display:none;" : ""

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(4, "class", "input-group-prepend");
            __builder.AddMarkupContent(5, "<span class=\"input-group-text\">@</span>");
            __builder.CloseElement();
            __builder.AddMarkupContent(6, "\r\n    ");
            __builder.OpenElement(7, "input");
            __builder.AddAttribute(8, "style", "width:100%;" + " " + (
#nullable restore
#line 12 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Shared\Forms\UserAutoComplete.razor"
                                isLoaded == false? "display:none;" : ""

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(9, "id", "userAutoComplete-" + (
#nullable restore
#line 12 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Shared\Forms\UserAutoComplete.razor"
                                                                                                InputCode

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(10, "class", "input-left-none" + " form-control" + " " + (
#nullable restore
#line 12 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Shared\Forms\UserAutoComplete.razor"
                                                                                                                                                ClassNames

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(11, "oninput", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.ChangeEventArgs>(this, 
#nullable restore
#line 13 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Shared\Forms\UserAutoComplete.razor"
                     OnChange

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(12, "value", 
#nullable restore
#line 13 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Shared\Forms\UserAutoComplete.razor"
                                       Text

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(13, "\r\n    ");
#nullable restore
#line 14 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Shared\Forms\UserAutoComplete.razor"
__builder.AddContent(14, Feedback);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(15, "\r\n\r\n");
#nullable restore
#line 17 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Shared\Forms\UserAutoComplete.razor"
__builder.AddContent(16, ChildContent);

#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Shared\Forms\UserAutoComplete.razor"
 if (!isLoaded)
{

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(17, "<div class=\"loader-wrapper\"><div class=\"btn-loader page\"></div></div>");
#nullable restore
#line 22 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Shared\Forms\UserAutoComplete.razor"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
#nullable restore
#line 24 "C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Shared\Forms\UserAutoComplete.razor"
       
    [Parameter] public string Text { get; set; }

    [Parameter] public EventCallback<string> TextChanged { get; set; }

    [Parameter] public Expression<Func<string>> Field { get; set; }

    protected override string InternalValue { get => Text; set => Text = value; }

    private string InputCode { get; set; }
    private bool isLoaded { get; set; }

    protected override async Task OnInitializedAsync()
    {
        InputCode = ClientAuthServices.GenerateRandomString(10, false).ToUpperInvariant();

        var userList = await _clientServices.GetAllFilterClient(false, false);

        var jsonList = new List<AutoCompleteDataInterop>();

        foreach (var k in userList)
        {
            var image = await _fileServices.GetPhysicCroppedFile(k.UserAvatar_cropp64x64);
            jsonList.Add(new AutoCompleteDataInterop() { ScreenName = k.ScreenName, Login = k.UserName, ImageUrl = image });
        }

        await _js.InvokeVoidAsync("loadScript", "libs/autocomplete/autocomplete.min.js");
        await _js.InvokeVoidAsync("AutoComplete.GenerateAutoComplete_userlist", "#userAutoComplete-" + InputCode, jsonList, Placeholder);

        isLoaded = true;
    }

    private async Task OnChange(ChangeEventArgs e)
    {
        var context = new ValidationContext((string)e.Value, null, null);
        var results = new List<ValidationResult>();

        var attrs = ComponentUtils.GetExpressionCustomAttributes(Field);

        Validator.TryValidateValue((string)e.Value, context, results, attrs);

        if (results.Any()) ParentValidation.NotifyValidationStatusChanged(ValidationStatus.Error, results.Select(m => m.ErrorMessage).ToList());
        else ParentValidation.NotifyValidationStatusChanged(ValidationStatus.Success);

        await TextChanged.InvokeAsync((string)e.Value);
    }

    /* INHERITS */

    protected override void BuildClasses(ClassBuilder builder)
    {
        builder.Append("is-invalid", ParentValidation?.Status == ValidationStatus.Error);
        builder.Append("is-valid", ParentValidation?.Status == ValidationStatus.Success);

        base.BuildClasses(builder);
    }

    protected override Task OnInternalValueChanged(string value) => TextChanged.InvokeAsync(value);
    protected override Task<ParseValue<string>> ParseValueFromStringAsync(string value) => Task.FromResult(new ParseValue<string>(true, value, null));

    public async override Task SetParametersAsync(ParameterView parameters)
    {
        parameters.SetParameterProperties(this);
        await base.SetParametersAsync(ParameterView.Empty);

        if (ParentValidation != null)
            await InitializeValidation();
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IVirtualFileServices _fileServices { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IClientAuthServices _clientServices { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime _js { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager UriHelper { get; set; }
    }
}
#pragma warning restore 1591