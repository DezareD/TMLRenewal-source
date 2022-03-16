using Microsoft.EntityFrameworkCore;
using RenewalTML.Data.Model;
using RenewalTML.Shared.Exstention.ClassAddons;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace RenewalTML.Data
{
    public class AdminActionManager : GenericManager<AdminAction>
    {
        public AdminActionManager(IRepository<AdminAction, int> _adminActionRepository)
        {
            _genericRepository = _adminActionRepository;
        }

        public async Task<List<AdminAction>> GetAdminActionForUser(Client client, int limit)
        {
            var list = await (await _genericRepository.GetQueryableAsync()).Where(m => m.Type == ("{to:user:" + client.Id + "}")).ToListAsync();
            return list.OrderByDescending(m => DateTimeAddon.StringToDateTime(m.Date).Ticks).Take(limit).ToList();
        }

        public async Task<int> GetAdminActionForUserCount(Client client)
        {
            var list = await (await _genericRepository.GetQueryableAsync()).Where(m => m.Type == ("{to:user:" + client.Id + "}")).ToListAsync();
            return list.Count();
        }

        public async Task<List<AdminAction>> GetAdminActionForAdmin(Client client, int limit) => await (await _genericRepository.GetQueryableAsync()).Where(m => m.AdminId == client.Id).OrderByDescending(m => DateTimeAddon.StringToDateTime(m.Date).Ticks).Take(limit).ToListAsync();

        // админ действия только на определенном юзере.         +
        // админ транзакции только для определенного админа.    +
    }
}
