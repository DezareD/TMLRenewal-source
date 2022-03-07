using RenewalTML.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace RenewalTML.Data
{
    public interface INotificationServices
    {
        Task CreateAndSendNotification(Notification notification, Client user);
        Task<List<Notification>> GetNotViewedNotificationAsync(Client client);
        Task<List<Notification>> GetNotificationClientAsync(Client client, int limit = 74);
        Task SetNotificationViewed(Client client);
    }

    public class NotificationServices : ApplicationService, INotificationServices
    {
        private readonly NotificationManager _notificationManager;
        public NotificationServices(NotificationManager notificationManager)
        {
            _notificationManager = notificationManager;
        }

        public async Task SetNotificationViewed(Client client) => await _notificationManager.SetNotificationClientViewed(client);

        public async Task<List<Notification>> GetNotViewedNotificationAsync(Client client)
        {
            return await _notificationManager.GetNonViewedAsync(client);
        }

        public async Task<List<Notification>> GetNotificationClientAsync(Client client, int limit = 74)
        {
            return await _notificationManager.GetUserNotificationViewedAsync(client, limit);
        }

        public async Task CreateAndSendNotification(Notification notification, Client user)
        {
            await _notificationManager.AddAsync(notification);
            await _notificationManager.SendAsync(notification, user);
        }
    }
}
