using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace RenewalTML.Data.Model
{
    public class Ticket : Entity<int>
    {
        public string SystemName { get; set; }
        public string SystemInformation { get; set; }
        public string TicketStatus { get; set; }

        /*
         * status_accepted - заявка просмотрена и одобрена
         * status_declaim - заявка промеотрена и не одобрена
         * status_waiting - заявка ещё не просмотрена
         */

        public string TicketType { get; set; }

        /*
         * type_fillingBalance - заявка на пополнение баланса
         */

        public string Date { get; set; }
        public int UserCreateId { get; set; }
        public int AdminViewdId { get; set; }
        public string DataView { get; set; }
        public string AdminInformation { get; set; }
        public string JsonObject { get; set; }
    }
}
