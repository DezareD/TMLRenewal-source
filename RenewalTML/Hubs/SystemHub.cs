using Microsoft.AspNetCore.SignalR;
using RenewalTML.Data;
using RenewalTML.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenewalTML.Hubs
{
    public class SystemHubConnection
    {
        private readonly IHubContext<SystemHub> hub;
        public SystemHubConnection(IHubContext<SystemHub> systemHub)
        {
            hub = systemHub;
        }

        public async Task SendUserNotification(Client user, Notification notification)
        {
            var findConnections = SystemHub.UserList.Where(m => m.clientId == user.Id).ToList();

            foreach (var connection in findConnections)
            {
                await hub.Clients.Client(connection.ConntectedId).SendAsync("AddNotification", notification);
            }
        }

        public async Task ChangeUserBarBalance(Client user, int changedBalanceValue)
        {
            var findConnections = SystemHub.UserList.Where(m => m.clientId == user.Id).ToList();

            foreach (var connection in findConnections)
            {
                await hub.Clients.Client(connection.ConntectedId).SendAsync("ChangeBarBalance", changedBalanceValue);
            }
        }
    }

    public class SystemHub : Hub
    {
        public static List<ConntectedClient> UserList = new List<ConntectedClient>(); // Список пользователей

        public void CompleteHubConnection(int clientId)
        {
            UserList.Add(new ConntectedClient() { ConntectedId = Context.ConnectionId, clientId = clientId });
        }

        public void CompleteHubConnectionDispose(int clientId)
        {
            var findConnections = UserList.Where(m => m.clientId == clientId).Where(m => m.ConntectedId == Context.ConnectionId).FirstOrDefault();
            if (findConnections != null)
                UserList.Remove(findConnections);
        }
    }

    public class ConntectedClient
    {
        public int clientId { get; set; }
        public string ConntectedId { get; set; }
    }
}
