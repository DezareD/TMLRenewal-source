using RenewalTML.Data.Model;
using System.ComponentModel.DataAnnotations;

namespace RenewalTML.Data.Dto
{
    public class RoleModel
    {
        public int Id { get; set; } // is edit 
        [MinLength(3, ErrorMessage = "Имя должно содержать минимум 3 символа.")]
        [MaxLength(20, ErrorMessage = "Максимальная длина имени: 20 символов.")]
        public string Name { get; set; }
        public string Class { get; set; }

        public bool isHaveAccessToSite { get; set; } // Имеет ли доступ к сайту
        public bool isHaveAccesToAdminPanel { get; set; } // Имеет доступ к админ-панеле, т.е
        public bool isHaveAccesToEditRoles { get; set; } // Имеет доступ к редактированю данных о ролях и т.д
        public bool isHaveToModerateTransaction { get; set; }

        public bool isHaveToViewUserList { get; set; } // может ли просматривать список людей
        public bool isHaveToModerateUserAccount { get; set; } // может ли модерировать аккаунты пользователей
        public bool isHaveToAdministrateUserAccount { get; set; } // может ли администрировать аккаунты пользователей
        public bool isHaveAccesToPremiumEditor { get; set; } // имеет ли доступ к большим настройкам в редакторе 
        public bool isHaveAccesToUltimateEditor { get; set; } // имеет ли доступ к админскому редактору текста.
        public bool isBlockedEconomic { get; set; } // заблокирована ли экономика?
        public bool isHaveAccesToViewSystemSettings { get; set; } // имеет ли доступ к просмотру списку настроек
        public bool isHaveAccesToEditSettings_System { get; set; } // изменять настройки системы
        public bool isHaveAccesToEditSettings_Economic { get; set; } // изменять настройки экономики
        public bool isHaveAccesToOfflineSite { get; set; } // есть ли доступ к выключенному сайту?
    }

    public class RoleEnchantedModel
    {
        public Role role { get; set; }
        public int UserCount { get; set; } // сколько аккаунтoв с данной ролью
    }
}
