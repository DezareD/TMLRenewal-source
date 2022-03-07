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
using Volo.Abp.Validation;
using System.Collections.Generic;
using RenewalTML.Data.Dto.Admin;
using Newtonsoft.Json;

namespace RenewalTML.Data
{
    public interface ITicketServices : IApplicationService
    {
        Task<int> GetFillBalanceLastTicketsCount();
        Task<FillBalanceTicketModel> GetFillBalanceLastTickets();
        Task<List<Ticket>> GetLastUserTicket(Client user, int count = 25);
        Task UpdateTicket(Ticket ticket);
        Task CreateTicket(Ticket ticket);
        Task<Ticket> GetTicket(int id);
    }

    public class TicketServices : ApplicationService, ITicketServices
    {
        private readonly TicketManager _ticketManager;
        public TicketServices(TicketManager ticketManager)
        {
            _ticketManager = ticketManager;
        }

        public async Task CreateTicket(Ticket ticket) => await _ticketManager.AddAsync(ticket);
        public async Task UpdateTicket(Ticket ticket) => await _ticketManager.UpdateAsync(ticket);
        public async Task<Ticket> GetTicket(int id) => await _ticketManager.GetAsync(id);

        public async Task<List<Ticket>> GetLastUserTicket(Client user, int count = 25) => await _ticketManager.GetLastUserTicket(user, count);

        public async Task<FillBalanceTicketModel> GetFillBalanceLastTickets()
        {
            var list = await _ticketManager.GetFillBalanceLastTickets();
            var retList = new List<FillBalanceTicketModel>();

            foreach(var model in list)
            {
                var desiriles = JsonConvert.DeserializeObject<FillBalanceTicketModel>(model.JsonObject);

                var fillbalanceModel = new FillBalanceTicketModel()
                {
                    _ticket = model,
                    CountMoney = desiriles.CountMoney,
                    ImagesIds = desiriles.ImagesIds,
                    TextInformation = desiriles.TextInformation
                };

                retList.Add(fillbalanceModel);
            }

            return retList.LastOrDefault();
        }

        public async Task<int> GetFillBalanceLastTicketsCount() => await _ticketManager.GetFillBalanceLastTicketsCount();
    }
}
