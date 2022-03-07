using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RenewalTML.Data.Model;
using RenewalTML.EFCore;
using RenewalTML.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Uow;

namespace RenewalTML.Data
{

    public class NotificationManager : GenericManager<Notification>
    {
        private readonly SystemHubConnection _systemHub;

        public NotificationManager(IRepository<Notification, int> _notificationRepository, IHubContext<SystemHub> systemHub)
        {
            _genericRepository = _notificationRepository;

            _systemHub = new SystemHubConnection(systemHub);
        }

        public async Task SendAsync(Notification entity, Client client)
        {
            await _systemHub.SendUserNotification(client, entity);
        }

        public async Task<List<Notification>> GetNonViewedAsync(Client client) => await _genericRepository.Where(m => m.ClientOwnerId == client.Id).Where(m => m.isViewed == false).ToListAsync();
        public async Task<List<Notification>> GetUserNotificationViewedAsync(Client client, int limit) => await _genericRepository.Where(m => m.ClientOwnerId == client.Id).Where(m => m.isViewed == true).Take(limit).ToListAsync();
        public async Task SetNotificationClientViewed(Client client)
        {
            var query = await _genericRepository.Where(m => m.ClientOwnerId == client.Id).Where(m => m.isViewed == false).ToListAsync();
            query.ForEach(m => m.isViewed = true);
            await UpdateManyAsync(query);
        }
    }
    /*public class NotificationManager : DomainService
    {
        private readonly IRepository<Notification, int> _notificationRepository;

        public NotificationManager(IRepository<Notification, int> notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<Client> GetClientAsync(int id) => await AsyncExecuter.FirstOrDefaultAsync(_notificationRepository.Where(m => m.Id == id));
        public async Task AddClient(Client client) => await _notificationRepository.InsertAsync(client);
        public async Task UpdateClient(Client client) => await _notificationRepository.UpdateAsync(client);
    }*/
}
