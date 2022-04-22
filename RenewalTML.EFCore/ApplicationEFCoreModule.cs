using Hangfire;
using Hangfire.MySql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RenewalTML.EFCore.Extenstion;
using System;
using System.IO;
using System.Threading;
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
            var connString = GetConnectionString();

            var builder = new DbContextOptionsBuilder<ApplicationContext>()
                .UseMySql(
                        connString,
                        ServerVersion.AutoDetect(connString)
                        );


            return new ApplicationContext(builder.Options);
        }

        public static string GetConnectionString()
        {
            var root = Directory.GetParent(Environment.CurrentDirectory).FullName;
            Console.WriteLine("trusted connection .env root: " + root);
            var dotenv = Path.Combine(root, ".env");
            EnvFileLoader.Load(dotenv);

            var host = Environment.GetEnvironmentVariable("DBHOST");
            var port = Environment.GetEnvironmentVariable("DBPORT");
            var password = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");
            var userid = Environment.GetEnvironmentVariable("MYSQL_USER");
            var usersDataBase = Environment.GetEnvironmentVariable("MYSQL_DATABASE");

            Console.WriteLine($"connection string: server={host};userid={userid};pwd={password};port={port};database={usersDataBase};Allow User Variables=true");

            return $"server={host};userid={userid};pwd={password};port={port};database={usersDataBase};Allow User Variables=true";
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var connString = GetConnectionString();


            context.Services.AddAbpDbContext<ApplicationContext>(options =>
            {
                options.AddDefaultRepositories(includeAllEntities: true); // Авто-генерация репозиториев
            });

            context.Services.AddHangfire(config =>
            {
                config.UseStorage(new MySqlStorage(connString, new MySqlStorageOptions
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
                        connString,
                        ServerVersion.AutoDetect(connString));
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
    }
}
