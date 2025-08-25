using Contracts.DTOs;

namespace Contracts.Interfaces.Services
{
    public interface IStoreService
    {
        Task<IEnumerable<StoreDto>> GetAllAsync();
        Task<StoreDto?> GetByIdAsync(Guid id);
        Task<StoreDto> CreateAsync(StoreDto dto);
    }
}
