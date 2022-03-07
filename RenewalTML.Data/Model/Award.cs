using Volo.Abp.Domain.Entities;

namespace RenewalTML.Data.Model
{
    public class Award : Entity<int>
    {
        public string Name { get; set; } // название 
        public string Text { get; set; } // описание
        public string AwardType { get; set; }

        /*  Award's type
         *  type_money - начислить деньги за выполнения данного достижения
         *  type_item - TODO: ... предметы в дальнейшем
         *  type_exp - опыт для уровня
         *  и т.д
         * 
         */

        public int Value { get; set; } // количество награды
        public int ProgressFinal { get; set; } // сколько нужно прогресса для получения ачивки

        public string Icon { get; set; } // in font awesome duatone 

        public string requereName { get; set; }
    }

    public class ClientAward : Entity<int>
    {
        public int AwardId { get; set; }
        public int ClientId { get; set; }
        public int Progress { get; set; } // Прогресс пользователя по ачивке
    }
}
