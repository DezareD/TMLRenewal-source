using RenewalTML.Data.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace RenewalTML.Data
{
    public interface ILevelsServices
    {
        int GetRequeryGainExpToLevel(int level); // сколько нужно опыта в общем для полученя level уровня.
        int GetLevelByExp(int generalExp); // получить уровень в соответствии общего опыта. ( для статистики )
        Task AddUserExp(Client client, int exp); // дать пользователю опыта
    }

    public class LevelsServices : ApplicationService, ILevelsServices
    {
        private readonly ClientManager _clientManager;
        public LevelsServices(ClientManager clientManager)
        {
            _clientManager = clientManager;
        }

        /* consts and etc. */
        
        private const double e = 2.7182818; // число e
        private const double kGrowth = 0.2; // коэф. роста
        private const int maxLevel = 50;

        public int GetRequeryGainExpToLevel(int level) => Convert.ToInt32(100 * Math.Pow(e, level * kGrowth));
        public int GetLevelByExp(int generalExp)
        {
            if (generalExp <= 0) return 1; // если опыт нулевой или отрицательный ( почему то )

            for(int i = 1; i <= 50; i++)
            {
                if(GetRequeryGainExpToLevel(i) > generalExp) // если нужный опыт для уровня больше чем сейчас.
                    return i;
            }

            return maxLevel; // если опыт выходит за область
        }

        public async Task AddUserExp(Client client, int exp)
        {
            var nexlevelexp = GetRequeryGainExpToLevel(client.Level + 1);

            client.GeneralExp += exp;

            if (client.CurrencyExp + exp >= nexlevelexp)
            {
                client.Level++;
                client.CurrencyExp = 0;

                // TODO: Событие получение нового уровня, если оно нужно.
            }
            else client.CurrencyExp += exp;

            await _clientManager.UpdateAsync(client);
        }
    }
}
