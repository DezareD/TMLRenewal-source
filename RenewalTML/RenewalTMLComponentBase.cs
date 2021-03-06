using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RenewalTML.Data;
using System;
using System.Threading.Tasks;

namespace RenewalTML
{
    public abstract class RenewalTMLComponentBase : ComponentBase
    {
        [Inject] protected IJSModularityServices _js { get; set; }
        [Inject] protected IClientAuthServices _userServices { get; set; }
        [Inject] protected IRolePermissionServices _roleServices { get; set; }
        [CascadingParameter(Name = "_virtualNavigationServices")] protected IVirtualNavigationServices _virtualNavigationServices { get; set; }
        [CascadingParameter(Name = "_applicationContainer")] protected App _applicationContainer { get; set; }

        protected bool _isComplete { get; set; }

        public async Task ChangePageLoadStatus(bool isComplete)
        {
            if (isComplete) // is page complete 
            {
                _isComplete = true;
                await _applicationContainer.RedirectingComplete();
            }
            else _isComplete = false;
        }

        protected override async Task OnInitializedAsync()
        {
            _virtualNavigationServices.SetPageInformationForEnchantedNavigation(this);
            await OnInitializedComponent();
        }

        public abstract Task OnInitializedComponent();

        protected override void OnInitialized()
        {
            _virtualNavigationServices.PageAddedServices(_applicationContainer, _userServices);
            base.OnInitialized();
        }
    }
}
