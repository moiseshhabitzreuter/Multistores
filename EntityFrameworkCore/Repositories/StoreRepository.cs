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
            await _dbContext.Stores.ToListAsync();

        public async Task<Store?> GetByIdAsync(Guid id) =>
            await _dbContext.Stores.FindAsync(id);

        public async Task<Store> AddAsync(Store store)
        {
            _dbContext.Stores.Add(store);
            await _dbContext.SaveChangesAsync();
            return store;
        }
    }
}
