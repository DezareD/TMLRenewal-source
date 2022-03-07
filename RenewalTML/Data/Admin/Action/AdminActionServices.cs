using Microsoft.EntityFrameworkCore;
using RenewalTML.Data.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace RenewalTML.Data
{
    public interface IAdminActionServices
    {
        Task<List<AdminAction>> GetAdminActionForUser(Client client, int limit = 14);
        Task<List<AdminAction>> GetAdminActionForAdmin(Client client, int limit = 14);
        Task CreateAdminTransaction(AdminAction action);
        Task<int> GetAdminActionForUserCount(Client client);
    }

    public class AdminActionServices : ApplicationService, IAdminActionServices
    {
        private readonly AdminActionManager _adminActionManager;
        public AdminActionServices(AdminActionManager adminActionManager)
        {
            _adminActionManager = adminActionManager;
        }

        public async Task<int> GetAdminActionForUserCount(Client client) => await _adminActionManager.GetAdminActionForUserCount(client);
        public async Task CreateAdminTransaction(AdminAction action) => await _adminActionManager.AddAsync(action);
        public async Task<List<AdminAction>> GetAdminActionForUser(Client client, int limit = 14) => await _adminActionManager.GetAdminActionForUser(client, limit);
        public async Task<List<AdminAction>> GetAdminActionForAdmin(Client client, int limit = 14) => await _adminActionManager.GetAdminActionForAdmin(client, limit);
    }
}
