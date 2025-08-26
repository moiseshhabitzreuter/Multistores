using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Multistores.Domain.Interfaces;
using Multistores.EntityFrameworkCore.Data;

namespace Multistores.EntityFrameworkCore.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly MultistoresDbContext _dbContext;

        public StoreRepository(MultistoresDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Store>> GetAllAsync() =>
            await _dbContext.Stores.Where(x=> !x.IsDeleted).ToListAsync();

        public async Task<Store?> GetByIdAsync(Guid id) =>
            await _dbContext.Stores.FirstOrDefaultAsync(x=>x.Id == id && !x.IsDeleted);

        public async Task<Store> AddAsync(Store store)
        {
            _dbContext.Stores.Add(store);
            await _dbContext.SaveChangesAsync();
            return store;
        }

        public async Task UpdateAsync(Store store)
        {
            _dbContext.Stores.Update(store);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var store = await _dbContext.Stores.FirstOrDefaultAsync(s => s.Id == id);
            if (store != null)
            {
                store.IsDeleted = true;
                _dbContext.Stores.Update(store);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
