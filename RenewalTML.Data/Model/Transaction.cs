using Volo.Abp.Domain.Entities;

namespace RenewalTML.Data.Model
{
    public class Transaction : Entity<int>
    {
        public string Name { get; set; }
        public string Information { get; set; } // rules {}:

        public string TransactionType { get; set; }

        // {user:user}
        // {organization:user}
        // {user:organization}

        public int OutEntityId { get; set; } // от кого
        public int ToEntityId { get; set; } // кому

        public int Value { get; set; } // Количество
        public string Date { get; set; }
    }

    /*public class ModuleTransaction : Entity<int>
    {
        // Модуль транзакций, несет в себе информацию о блоке транзакций. В основном нужен для технической реализации.

        public string ModuleTransactionUniqId { get; set; }
        public string Type { get; set; }
        
        // type_user - 
        // type_organization...

        public int EntityConnectId { get; set; }
        public int CountTransaction { get; set; }
    }*/
}
