using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RenewalTML.Data;
using System;
using Volo.Abp.Application.Services;
using System.Threading.Tasks;

namespace RenewalTML.Data
{
    public interface IJSModularityServices : IApplicationService
    {
        Task<TValue> InvokeAsync<TValue>(string moduleName, string methodName, params object[] args);
        Task InvokeVoidAsync(string moduleName, string methodName, params object[] args);
    }

    public class JSModularityServices : ApplicationService, IJSModularityServices, IDisposable
    {
        public JSModularityServices(IJSRuntime js)
        {
            _js = js;
        }

        private readonly IJSRuntime _js;

        /* Javascript Module */

        private IJSObjectReference _blazorIntegrationModule { get; set; }
        private IJSObjectReference _cookiesModule { get; set; }
        private IJSObjectReference _loadingModule { get; set; }
        private IJSObjectReference _logicUtilsModule { get; set; }
        private IJSObjectReference _uiUtilsModule { get; set; }
        private IJSObjectReference _loadScriptsModule { get; set; }
        private IJSObjectReference _cropperModule { get; set; }
        private IJSObjectReference _tippyModule { get; set; }

        private IJSObjectReference _navigationModule { get; set; }
        private IJSObjectReference _vkAuthorizeModule { get; set; }
        private IJSObjectReference _chartModule { get; set; }
        private IJSObjectReference _markDownModule { get; set; }
        private IJSObjectReference _autoCompleteModule { get; set; }
        private IJSObjectReference _notificationModule { get; set; }

        private async Task<IJSObjectReference> CheckModulesStatus(string moduleName)
        {
            if (moduleName == "cookiesModule")
            {
                if(_cookiesModule == null) _cookiesModule = await _js.InvokeAsync<IJSObjectReference>("import", "./scripts/System/CookiesRepository.js");
                return _cookiesModule;
            }
            else if(moduleName == "loadingModule")
            {
                if(_loadingModule == null) _loadingModule = await _js.InvokeAsync<IJSObjectReference>("import", "./scripts/System/LoadingRepository.js");
                return _loadingModule;
            }
            else if(moduleName == "logicUtils")
            {
                if (_logicUtilsModule == null) _logicUtilsModule = await _js.InvokeAsync<IJSObjectReference>("import", "./scripts/Logic/UtilitsRepository.js");
                return _logicUtilsModule;
            }
            else if(moduleName == "blazorIntegration")
            {
                if (_blazorIntegrationModule == null) _blazorIntegrationModule = await _js.InvokeAsync<IJSObjectReference>("import", "./scripts/BlazorIntegrationModule.js");
                return _blazorIntegrationModule;
            }
            else if(moduleName == "uiUtilsModule")
            {
                if(_uiUtilsModule == null) _uiUtilsModule = await _js.InvokeAsync<IJSObjectReference>("import", "./scripts/UserInterface/UtilitsRepository.js");
                return _uiUtilsModule;
            }
            else if(moduleName == "loadSciptsModule")
            {
                if (_loadScriptsModule == null) _loadScriptsModule = await _js.InvokeAsync<IJSObjectReference>("import", "./scripts/System/ScriptsLoaderUtils.js");
                return _loadScriptsModule;
            }
            else if(moduleName == "cropperModule")
            {
                if (_cropperModule == null) _cropperModule = await _js.InvokeAsync<IJSObjectReference>("import", "./scripts/UserInterface/CropperRepository.js");
                return _cropperModule;
            }
            else if(moduleName == "tippyModule")
            {
                if(_tippyModule == null) _tippyModule = await _js.InvokeAsync<IJSObjectReference>("import", "./scripts/UserInterface/TippyIntegrationRepository.js");
                return _tippyModule;
            }
            else if(moduleName == "navigationModule")
            {
                if(_navigationModule == null) _navigationModule = await _js.InvokeAsync<IJSObjectReference>("import", "./scripts/System/NavigationRepository.js");
                return _navigationModule;
            }
            else if(moduleName == "vkAuthorizeModule")
            {
                if(_vkAuthorizeModule == null) _vkAuthorizeModule = await _js.InvokeAsync<IJSObjectReference>("import", "./scripts/Logic/VKAuthorizeRepository.js");
                return _vkAuthorizeModule;
            }
            else if(moduleName == "chartModule")
            {
                if(_chartModule == null) _chartModule = await _js.InvokeAsync<IJSObjectReference>("import", "./scripts/UserInterface/GraphicsRepository.js");
                return _chartModule;
            }
            else if(moduleName == "markDownModule")
            {
                if(_markDownModule == null) _markDownModule = await _js.InvokeAsync<IJSObjectReference>("import", "./scripts/UserInterface/MarkDownRepository.js");
                return _markDownModule;
            }
            else if(moduleName == "autoCompleteModule")
            {
                if(_autoCompleteModule == null) _autoCompleteModule = await _js.InvokeAsync<IJSObjectReference>("import", "./scripts/UserInterface/AutoCompleteRepository.js");
                return _autoCompleteModule;
            }
            else if(moduleName == "notificationModule")
            {
                if(_notificationModule == null) _notificationModule = await _js.InvokeAsync<IJSObjectReference>("import", "./scripts/Logic/NotificationRepository.js");
                return _notificationModule;
            }
            else throw new ArgumentException("Module not found");
        }

        public async Task<TValue> InvokeAsync<TValue>(string moduleName, string methodName, params object[] args)
        {
            var module = await CheckModulesStatus(moduleName);
            return await module.InvokeAsync<TValue>(methodName, args);
        }

        public async Task InvokeVoidAsync(string moduleName, string methodName, params object[] args)
        {
            var module = await CheckModulesStatus(moduleName);
            await module.InvokeVoidAsync(methodName, args);
        }

        public void Dispose()
        {
            _notificationModule?.DisposeAsync();
            _autoCompleteModule?.DisposeAsync();
            _markDownModule?.DisposeAsync();
            _chartModule?.DisposeAsync();
            _vkAuthorizeModule?.DisposeAsync();
            _navigationModule?.DisposeAsync();
            _tippyModule?.DisposeAsync();
            _cropperModule?.DisposeAsync();
            _loadScriptsModule?.DisposeAsync();
            _uiUtilsModule?.DisposeAsync();
            _logicUtilsModule?.DisposeAsync();
            _loadingModule?.DisposeAsync();
            _blazorIntegrationModule?.DisposeAsync();
            _cookiesModule?.DisposeAsync();
        }
    }
}
