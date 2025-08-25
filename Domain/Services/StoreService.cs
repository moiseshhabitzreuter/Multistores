using Contracts.DTOs;
using Contracts.Interfaces.Services;
using Domain.Entities;
using Multistores.Domain.Interfaces;

namespace Domain.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;

        public StoreService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public async Task<IEnumerable<StoreDto>> GetAllAsync()
        {
            var stores = await _storeRepository.GetAllAsync();
            return stores.Select(p => new StoreDto
            {
                Name = p.Name,
                Code = p.Code,
                IdentificationCode = p.IdentificationCode,
            }
            ).ToList();
        }

        public async Task<StoreDto?> GetByIdAsync(Guid id)
        {
            var store = await _storeRepository.GetByIdAsync(id);
            return store != null ? new StoreDto
            {
                Name = store.Name,
                Code = store.Code,
                IdentificationCode = store.IdentificationCode,
            }
            :
            null;
        }

        public async Task<StoreDto> CreateAsync(StoreDto dto)
        {
            var store = new Store
            {
                Name = dto.Name,
                Code = dto.Code,
                IdentificationCode = dto.IdentificationCode,
            };

            await _storeRepository.AddAsync(store);
            return dto;
        }
    }
}
