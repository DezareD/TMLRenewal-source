using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.Modularity;

namespace RenewalTML.EFCore
{
    [DependsOn(
        typeof(AbpEntityFrameworkCoreMySQLModule))]
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
                        ServerVersion.AutoDetect(configuration.GetConnectionString("Default")));

            return new ApplicationContext(builder.Options);

        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = BuildConfiguration();

            context.Services.AddAbpDbContext<ApplicationContext>(options =>
            {
                options.AddDefaultRepositories(includeAllEntities: true); // Авто-генерация репозиториев
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

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder() // C:\Users\Fearp\source\repos\RenewalTML\RenewalTML\Properties
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../RenewalTML/Properties/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
