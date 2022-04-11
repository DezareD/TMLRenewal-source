using Microsoft.AspNetCore.Http;
using RenewalTML.Data.Model;
using System;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Security.Encryption;
using Microsoft.JSInterop;
using RenewalTML.Data.JSInteropHelper;
using System.Security.Cryptography;
using RenewalTML.Data.Dto;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using RenewalTML.Shared.Exstention.ClassAddons;
using Serilog;
using System.Collections.Generic;
using System.IO;
using File = RenewalTML.Data.Model.File;

namespace RenewalTML.Data
{
    public interface IDataSeederServices : IApplicationService
    {
        Task SeedData();
    }

    public static class DataSeedStaticElements
    {
        public static List<Role> seedStaticRoleList = new List<Role>()
        {
            new Role()
            {
                RoleName = "Гость",
                RequereName = RoleManager.defaultRoleName_guest
            },
            new Role()
            {
                RoleName = "Пользователь",
                isHaveAccessToSite = true,
                RequereName = RoleManager.defaultRoleName_user
            },
            new Role()
            {
                RoleName = "Забаненный",
                RequereName = RoleManager.defaultRoleName_banned
            },
            new Role()
            {
                RoleName = "Премиум",
                RequereName = RoleManager.defaultRoleName_premium_default
            }
        };

        public static List<File> seedDataFiledList = new List<File>()
        {
            new File()
            {
                Name = "_img_defaultavatar",
                Extension = ".webp",
                Path = Directory.GetCurrentDirectory() + @"\wwwroot\imgs\defaultavatar",
                Size = 0,
                SitePath = @$"imgs\defaultavatar",
                hourseToDelete = 0
            },
            new File()
            {
                Name = "x64__img_defaultavatar",
                Extension = ".webp",
                Path = Directory.GetCurrentDirectory() + @"\wwwroot\imgs\defaultavatar",
                Size = 0,
                SitePath = @$"imgs\defaultavatar",
                hourseToDelete = 0
            }
        };

        public static List<CroppedImageFile> croppedSeedDataList = new List<CroppedImageFile>()
        {
            new CroppedImageFile()
            {
                x = 0,
                y = 0,
                width = 240,
                height = 240,
                ImageId = 1,
            },
            new CroppedImageFile()
            {
                x = 0,
                y = 0,
                width = 64,
                height = 64,
                ImageId = 2,
            }
        };

        public static List<Award> seedStaticAwardList = new List<Award>()
        {
            new Award()
            {
                AwardType = "type_money",
                Icon = "<i class=\"fad fa-user-plus\"></i>",
                ProgressFinal = 1,
                Name = "Добро пожаловать!",
                Value = 500,
                Text = "Добро пожаловать в TML Renewal! За успешную регистрацию на нашем сайте вы получаете эту ачивку, а так же символическое вознаграждение в виде 500 Кеклар.",
                requereName = "achievement_register"
            },
            new Award()
            {
                AwardType = "type_money",
                Icon = "<i class=\"fad fa-browser\"></i>",
                ProgressFinal = 10,
                Name = "Актив, актив и ещё раз актив...",
                Value = 10000,
                Text = "Сделайте 10 успешных заявок на пополнение счёта и получите 10.000 Валюты.",
                requereName = "achievement_request"
            }
        };

        public static List<SystemConfiguration> systemConfigurationsList = new List<SystemConfiguration>()
        {
            new SystemConfiguration()
            {
                Information = "Налог на перевод в процентах. ( до 100% ).",
                RequeredType = "double",
                Type = "type_economic",
                UniqId = "economic_transfer.tax",
                Value = "3.5"
            },
            new SystemConfiguration()
            {
                Information = "Дефолтная плашка над названием сайта.",
                RequeredType = "string",
                Type = "type_system",
                Value = "beta",
                UniqId = "system_main.subname"
            },
            new SystemConfiguration()
            {
                Information = "Название сайта в верхнем меню.",
                RequeredType = "string",
                Type = "type_system",
                UniqId = "system_main.brandname",
                Value = "TML Renewal"
            },
            new SystemConfiguration()
            {
                Information = "Включен ли сайт?",
                RequeredType = "bool",
                Type = "type_system",
                UniqId = "system_main.siteisonline",
                Value = "True"
            },
            new SystemConfiguration()
            {
                Information = "Включена ли регистрация?",
                RequeredType = "bool",
                Type = "type_system",
                UniqId = "system_main.registerison",
                Value = "True"
            },
            new SystemConfiguration()
            {
                Information = "Тик рейт бота",
                RequeredType = "int",
                Type = "type_system",
                UniqId = "system_main.bottickrate",
                Value = "2000"
            },
            new SystemConfiguration()
            {
                Information = "Минимальная стоимость доната VK Donuts",
                RequeredType = "int",
                Type = "type_system",
                UniqId = "system_main.costsubscrive",
                Value = "200"
            },
            new SystemConfiguration()
            {
                Information = "Количество денег за лайки ( дефолтно )",
                RequeredType = "int",
                Type = "type_economic",
                UniqId = "economic_request.count",
                Value = "100"
            }
        };
    }

    public class DataSeederServices : ApplicationService, IDataSeederServices
    {
        private readonly RoleManager _roleManager;
        private readonly AwardManager _awardManager;
        private readonly SystemConfigurationManager _configurationManager;
        private readonly VirtualFileManager _fileManager;
        private readonly VirtualCroopedFileManager _virtualCroopedFileManager;

        public DataSeederServices(RoleManager roleManager, AwardManager awardManager, SystemConfigurationManager configurationManager,
            VirtualFileManager fileManager, VirtualCroopedFileManager virtualCroopedFileManager)
        {
            _roleManager = roleManager;
            _awardManager = awardManager;
            _configurationManager = configurationManager;
            _fileManager = fileManager;
            _virtualCroopedFileManager = virtualCroopedFileManager;
        }

        public async Task SeedData()
        {
            try
            {
                Log.Information("Starting Data Seed Process.");
                Log.Information("Phase 1. Entity: 'Role'");

                foreach (var role in DataSeedStaticElements.seedStaticRoleList)
                {
                    var search = await _roleManager.FindByRequeryName(role.RequereName);

                    if (search == null)
                    {
                        await _roleManager.AddAsync(role);
                        Log.Information($"Phase 1. Entity: 'Role'. Entity '{role.RequereName}' has been added by data seed process.");
                    }
                }

                Log.Information("Phase 2. Entity: 'Award'");

                foreach (var award in DataSeedStaticElements.seedStaticAwardList)
                {
                    var search = await _awardManager.FindByRequeryName(award.requereName);

                    if (search == null)
                    {
                        await _awardManager.AddAsync(award);
                        Log.Information($"Phase 2. Entity: 'Award'. Entity '{award.requereName}' has been added by data seed process.");
                    }
                }

                Log.Information("Phase 3. Entity: 'System Configuration'");

                foreach (var config in DataSeedStaticElements.systemConfigurationsList)
                {
                    var search = await _configurationManager.GetByKey(config.UniqId);

                    if (search == null)
                    {
                        await _configurationManager.AddAsync(config);
                        Log.Information($"Phase 3. Entity: 'SystemConfiguration'. Entity '{config.UniqId}' has been added by data seed process.");
                    }
                }


                Log.Information("Phase 4. Entity: 'Files'");

                foreach (var file in DataSeedStaticElements.seedDataFiledList)
                {
                    var search = await _fileManager.GetFileByName(file.Name);

                    if (search == null)
                    {
                        await _fileManager.AddAsync(file);
                        Log.Information($"Phase 4. Entity: 'SystemConfiguration'. Entity '{file.Name}' has been added by data seed process.");
                    }
                }

                var croppedFilesCount = (await _virtualCroopedFileManager.GetAllAsync()).Count();
                if(croppedFilesCount < 2)
                {
                    Log.Information("\tPhase 4.1. Entity: 'Files.CroppedFiles'");
                    await _virtualCroopedFileManager.AddAsync(DataSeedStaticElements.croppedSeedDataList[0]);
                    await _virtualCroopedFileManager.AddAsync(DataSeedStaticElements.croppedSeedDataList[1]);
                }

                Log.Information("Data seed process success finally completed.");
            }
            catch(Exception e)
            {
                Log.Error("Data seed has been error. Message stack: " + e.Message);
            }
        }
    }
}
