using Volo.Abp.Domain.Entities;

namespace RenewalTML.Data.Model
{
    public class Role : Entity<int>
    {
        public string RoleName { get; set; } // Имя роли
        public string RoleStyle { get; set; } // Стиль роли, при отображении.
        public string RequereName { get; set; } // дефолтные имена для дефолтных ролей.

        /* Permission */

        public bool isHaveAccessToSite { get; set; } // Имеет ли доступ к сайту

        public bool isHaveAccesToAdminPanel { get; set; } // Имеет доступ к админ-панеле, т.е
        // пользователь получит возможность зайти в админ панель и будет видеть общую админ статистику,
        // но редактировать сайт по разделам пользователь сможет только при других пермишинах.

        public bool isBlockedEconomic { get; set; } // блокируются все действия с экономикой для данного аккаунта ( нужно админам )

        public bool isHaveAccesToEditRoles { get; set; } // Имеет доступ к редактированю данных о ролях и т.д

        public bool isHaveToModerateTransactions { get; set; } // может ли пользователь просматриваьт полный список транзакций
        // может ли отменять транзакции ( или просто удалять )

        public bool isHaveToViewUserList { get; set; } // может ли просматривать список людей
        public bool isHaveToModerateUserAccount { get; set; } // может ли модерировать аккаунты пользователей
        // Это: Изменять информацию о пользователе, изменять его картинки, изменять screenName, просматривать статистику пользователя
        // просматривать действия над пользователем. 


        public bool isHaveToAdministrateUserAccount { get; set; } // может ли администрировать аккаунты пользователей
        // блокировать, изменять экономику, изменять пароль, удалять и создавать новые аккаунты

        public bool isHaveAccesToUltimateEditor { get; set; } // имеет ли доступ к админскому редактору текста.

        public bool isHaveAccesToViewSystemSettings { get; set; } // имеет ли доступ к просмотру списку настроек
        public bool isHaveAccesToEditSettings_System { get; set; } // изменять настройки системы
        public bool isHaveAccesToEditSettings_Economic { get; set; } // изменять настройки экономики

        public bool isHaveAccesToOfflineSite { get; set; } // есть ли доступ к выключенному сайту?

        /* premium zone */

        public bool isHaveAccesToPremiumEditor { get; set; } // имеет ли доступ к большим настройкам в редакторе

        public double OffToExchange { get; set; } // уменьшение процента на перевод между аккаунтами
        public int AddMoneyLikesRequest { get; set; } // добавление денег за 1 лайк на посте

        /* TODO:
         * + 1. Добавить поля OffToExchange и AddMoneyLikesRequest
         * + 2. Сделать проверку не равна ли скидка OffToExchange больше чем процент за перевод
         * + 3. При изменении процента за перевод, проверить не является ли ставка ниже чем скидка у одной из роли
         * + 4. Сделать логику уменшьшения процента и сделать визуализацию этого ( в информации и зачеркнутость в панельке )
         * 5. Сделать информацию о количестве денег за лайки в Action а так же добавочную стоимость AddMoneyLikesRequest, а так же
         * 6. показывать админу сколько пользователь может получить за этот пост денег ( показывать админу ставку с учётом премиума )
         * 7. Добавить в SystemSettings ставку по лайкам
         */
    }
}
