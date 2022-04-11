using Volo.Abp.Domain.Entities;

namespace RenewalTML.Data.Model
{
    public class Client : Entity<int>
    {
        /*
         * Id - уникальный индфикатор, простой ( 1,2,3... ) для указания пользователя при каких-либо транзакциях. Нельзя менять
         * 
         * UserName - уникальное название пользователя, можно поменять, но есть проверка на уникальность. Так же используется при
         * транзакциях.
         * 
         * Balance - количество валюты клиента. Целое число от 1 до maxBalance ( разное для разных ролей )
         * 
         * DepositeBalance - баланс депозита, так же доступ для определённых ролей. Депозитные начисления так же разные для разных ролей.
         * 
         * RoleId - привязка профиля к определённой роли с определёнными настройками и возможностями.
         * 
         * ImageId - привязка к картинке. ( профиль пользователя. )
         * 
         * PasswordHash - хэш пароля.
         * PasswordSalt - соль пароля.
         * 
         * Screen Name - не уникальный никнейм, который показывается другим людям, можно менять, но обычный UserName менять нельзя. 
         */

        public string UserName { get; set; }
        public string ScreenName { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public int RoleId { get; set; }
        public string VkId { get; set; }
        public int Balance { get; set; }

        /* img's */
        public int UserAvatar_main { get; set; }
        public int UserAvatar_cropp { get; set; }
        public int UserAvatar_cropp64x64 { get; set; }

        /* leveling */
        public int Level { get; set; }
        public int CurrencyExp { get; set; } // опыт на текущем уровне
        public int GeneralExp { get; set; } // сколько всего получено опыта. ( для статистики и т.д )

        /* ticket's */
        public int TicketsNonViewCount { get; set; }

        /* premium */
        public bool IsHavePremiumBefore { get; set; } // имел ли пользователь премиум до этого?
    }
}
