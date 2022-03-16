using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using RenewalTML.Data;
using RenewalTML.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenewalTML.Shared
{
    public class MainLoyoutBase : LayoutComponentBase
    {
        [Inject] protected NavigationManager nm { get; set; }
        [Inject] protected IClientAuthServices _clientAuthServices { get; set; }
        [Inject] protected IRolePermissionServices _permissionService { get; set; }
        [Inject] protected IJSModularityServices _js { get; set; }
    }
}
