using RenewalTML.Data.Model;
using System.ComponentModel.DataAnnotations;

namespace RenewalTML.Data.Dto
{
    public class RoleModel
    {
        [DisableModuleValidation]
        public int Id { get; set; } // is edit 

        [MinLength(3, ErrorMessage = "Минимальная длинна 3 символа")]
        [MaxLength(20, ErrorMessage = "Максимальная длинна 20 символов")]
        public string Name { get; set; }

        public string Class { get; set; }

        [DisableModuleValidation] public bool isHaveAccessToSite { get; set; } // Имеет ли доступ к сайту
        [DisableModuleValidation] public bool isHaveAccesToAdminPanel { get; set; } // Имеет доступ к админ-панеле, т.е
        [DisableModuleValidation] public bool isHaveAccesToEditRoles { get; set; } // Имеет доступ к редактированю данных о ролях и т.д
        [DisableModuleValidation] public bool isHaveToModerateTransaction { get; set; }

        [DisableModuleValidation] public bool isHaveToViewUserList { get; set; } // может ли просматривать список людей
        [DisableModuleValidation] public bool isHaveToModerateUserAccount { get; set; } // может ли модерировать аккаунты пользователей
        [DisableModuleValidation] public bool isHaveToAdministrateUserAccount { get; set; } // может ли администрировать аккаунты пользователей
        [DisableModuleValidation] public bool isHaveAccesToPremiumEditor { get; set; } // имеет ли доступ к большим настройкам в редакторе 
        [DisableModuleValidation] public bool isHaveAccesToUltimateEditor { get; set; } // имеет ли доступ к админскому редактору текста.
        [DisableModuleValidation] public bool isBlockedEconomic { get; set; } // заблокирована ли экономика?
        [DisableModuleValidation] public bool isHaveAccesToViewSystemSettings { get; set; } // имеет ли доступ к просмотру списку настроек
        [DisableModuleValidation] public bool isHaveAccesToEditSettings_System { get; set; } // изменять настройки системы
        [DisableModuleValidation] public bool isHaveAccesToEditSettings_Economic { get; set; } // изменять настройки экономики
        [DisableModuleValidation] public bool isHaveAccesToOfflineSite { get; set; } // есть ли доступ к выключенному сайту?

        [Required(ErrorMessage = "Обязательное поле")]
        public double OffToExchange { get; set; } // уменьшение процента на перевод между аккаунтами

        [Required(ErrorMessage = "Обязательное поле")]
        public int AddMoneyLikesRequest { get; set; } // добавление денег за 1 лайк на посте
    }

    public class RoleEnchantedModel
    {
        public Role role { get; set; }
        public int UserCount { get; set; } // сколько аккаунтoв с данной ролью
    }
}
