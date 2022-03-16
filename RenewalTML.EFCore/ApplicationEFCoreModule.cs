using Hangfire;
using Hangfire.MySql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using Volo.Abp;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.Modularity;

namespace RenewalTML.EFCore
{
    [DependsOn(
        typeof(AbpEntityFrameworkCoreMySQLModule),
        typeof(AbpBackgroundJobsHangfireModule))]
    public class ApplicationEFCoreModule : AbpModule, IDesignTimeDbContextFactory<ApplicationContext>
    {
        /* Этот метод нужен для работоспособности таких команд как,
         * Add-Migration, Update-Database и т.д */
        public ApplicationContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<ApplicationContext>()
                .UseMySql(
                        configuration.GetConnectionString("Default"),
                        ServerVersion.AutoDetect(configuration.GetConnectionString("Default"))
                        );


            return new ApplicationContext(builder.Options);

        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = BuildConfiguration();

            context.Services.AddAbpDbContext<ApplicationContext>(options =>
            {
                options.AddDefaultRepositories(includeAllEntities: true); // Авто-генерация репозиториев
            });

            context.Services.AddHangfire(config =>
            {
                config.UseStorage(new MySqlStorage(configuration.GetConnectionString("Default"), new MySqlStorageOptions
                {
                    TablesPrefix = "hfg."
                }));
            });

            context.Services.AddHangfireServer(options =>
            {
                options.TaskScheduler = null;
            });


            Configure<AbpDbContextOptions>(options =>
            {
                options.Configure(ctx =>
                {
                    ctx.DbContextOptions.UseMySql(
                        configuration.GetConnectionString("Default"),
                        ServerVersion.AutoDetect(configuration.GetConnectionString("Default")));
                });
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            //app.UseHangfireServer();

            app.UseHangfireDashboard("/jobs", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() }
            });
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../RenewalTML/Properties/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
