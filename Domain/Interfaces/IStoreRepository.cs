using Domain.Entities;

namespace Multistores.Domain.Interfaces
{
    public interface IStoreRepository
    {
        Task<IEnumerable<Store>> GetAllAsync();
        Task<Store?> GetByIdAsync(Guid id);
        Task<Store> AddAsync(Store store);
    }
}
