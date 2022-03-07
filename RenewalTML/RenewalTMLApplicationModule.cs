using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RenewalTML.EFCore;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Packages;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Volo.Abp.Security;
using Volo.Abp.Security.Encryption;
using System.Text;
using Blazorise;
using Blazorise.Bootstrap;
using Microsoft.AspNetCore.ResponseCompression;
using System.Linq;
using RenewalTML.Hubs;
using Volo.Abp.Validation;
using RenewalTML.Data;
using Volo.Abp.AspNetCore.SignalR;
using Blazorise.Icons.FontAwesome;

namespace RenewalTML
{
    [DependsOn(
        typeof(ApplicationEFCoreModule),
        typeof(AbpAutofacModule),
        typeof(AbpAspNetCoreMvcUiBundlingModule),
        typeof(AbpAspNetCoreMvcUiPackagesModule),
        typeof(AbpSecurityModule)
    )]
    public class RenewalTMLApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            context.Services.AddRazorPages();

            context.Services.AddServerSideBlazor().AddCircuitOptions(o =>
            {
                if (hostingEnvironment.IsDevelopment())
                {
                    o.DetailedErrors = true;
                }
            });

            context.Services.AddSignalR();

            context.Services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            context.Services.AddBlazorise()/*.AddEmptyProviders();*/.AddBootstrapProviders().AddFontAwesomeIcons();

            Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            context.Services.AddAuthentication(
                CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            Configure<AbpBundlingOptions>(options =>
            {
                options.Mode = BundlingMode.BundleAndMinify;
            });

            Configure<AbpStringEncryptionOptions>(opts =>
            {
                opts.DefaultPassPhrase = "0:Ghda;l/n[3?-=D";
                opts.DefaultSalt = Encoding.UTF8.GetBytes("f8djioRktSBXkPOHt");
                opts.InitVectorBytes = Encoding.UTF8.GetBytes("3tt09JNgs4W1hMtS");
                opts.Keysize = 256;
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapHub<SystemHub>("/system");
                endpoints.MapFallbackToPage("/_Host");
            });

            var srvc = (IDataSeederServices)app.ApplicationServices.GetRequiredService(typeof(IDataSeederServices));
            srvc.SeedData();
        }
    }
}
