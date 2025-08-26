using Contracts.DTOs;
using Multistores.Contracts.DTOs;

namespace Contracts.Interfaces.Services
{
    public interface IStoreService
    {
        Task<IEnumerable<StoreDto>> GetAllAsync();
        Task<StoreDto?> GetByIdAsync(Guid id);
        Task<StoreDto> CreateAsync(CreateUpdateStoreDto dto);
        Task<StoreDto> UpdateStoreAsync(Guid id, CreateUpdateStoreDto dto);
        Task<bool>DeleteStoreAsync(Guid id);
    }
}
