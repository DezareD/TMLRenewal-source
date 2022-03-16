using Microsoft.EntityFrameworkCore;
using RenewalTML.Data.Model;
using RenewalTML.EFCore;
using RenewalTML.Shared.Exstention.ClassAddons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Uow;

namespace RenewalTML.Data
{
    public class TicketManager : GenericManager<Ticket>
    {
        public TicketManager(IRepository<Ticket, int> ticketManager)
        {
            _genericRepository = ticketManager;
        }

        // непросмотренные заявки на пополнение баланса
        public async Task<List<Ticket>> GetFillBalanceLastTickets()
        {
            var list = await (await _genericRepository.GetQueryableAsync()).Where(m => m.TicketType == "type_fillingBalance").Where(m => m.TicketStatus == "status_waiting").ToListAsync();
            return list.OrderBy(m => DateTimeAddon.StringToDateTime(m.Date).Ticks).ToList();
        }

        // количество непросмотренных заявок на пополнеие баланса
        public async Task<int> GetFillBalanceLastTicketsCount()
        {
            var list = await (await _genericRepository.GetQueryableAsync()).Where(m => m.TicketType == "type_fillingBalance").Where(m => m.TicketStatus == "status_waiting").ToListAsync();
            return list.OrderBy(m => DateTimeAddon.StringToDateTime(m.Date).Ticks).ToList().Count();
        }

        public async Task<List<Ticket>> GetLastUserTicket(Client user, int count)
        {
            var list = await (await _genericRepository.GetQueryableAsync()).Where(m => m.UserCreateId == user.Id).ToListAsync();
            return list.OrderByDescending(m => DateTimeAddon.StringToDateTime(m.Date).Ticks).Take(count).ToList();
        }
    }
}
