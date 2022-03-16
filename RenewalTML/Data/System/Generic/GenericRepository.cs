using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace RenewalTML.Data
{
    public class GenericManager<T> : DomainService where T : class, IEntity<int>
    {
        public IRepository<T, int> _genericRepository;

        public virtual async Task<T> GetAsync(int id) => await AsyncExecuter.FirstOrDefaultAsync((await _genericRepository.GetQueryableAsync()).Where(m => m.Id == id));
        public virtual async Task AddAsync(T entity, bool autoSave = false) => await _genericRepository.InsertAsync(entity, autoSave);
        public virtual async Task AddManyAsync(IEnumerable<T> entities) => await _genericRepository.InsertManyAsync(entities);
        public virtual async Task UpdateAsync(T entity) => await _genericRepository.UpdateAsync(entity);
        public virtual async Task UpdateManyAsync(IEnumerable<T> entities) => await _genericRepository.UpdateManyAsync(entities);
        public virtual async Task DeleteAsync(T entity) => await _genericRepository.DeleteAsync(entity);
        public virtual async Task<IEnumerable<T>> GetAllAsync() => await _genericRepository.ToListAsync();
    }
}
