using Contracts.DTOs;
using Contracts.Interfaces.Services;
using Domain.Entities;
using Multistores.Contracts.DTOs;
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
                Id = p.Id
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
                Id = id
            }
            :
            null;
        }

        public async Task<StoreDto> CreateAsync(CreateUpdateStoreDto dto)
        {
            var store = new Store
            {
                Name = dto.Name,
                Code = dto.Code,
                IdentificationCode = dto.IdentificationCode,
            };

            await _storeRepository.AddAsync(store);
            return new StoreDto().FromDto(dto, store.Id);
        }

        public async Task<StoreDto> UpdateStoreAsync(Guid id, CreateUpdateStoreDto dto)
        {
            var store = await _storeRepository.GetByIdAsync(id);
            if(store is null)
            {
                return new StoreDto();
            }

            store.Name = dto.Name;
            store.Code = dto.Code;
            store.IdentificationCode = dto.IdentificationCode;

            await _storeRepository.UpdateAsync(store);
            return new StoreDto().FromDto(dto, store.Id);
        }

        public async Task DeleteStoreAsync(Guid id)
        {
            await _storeRepository.DeleteAsync(id);
        }
    }
}
