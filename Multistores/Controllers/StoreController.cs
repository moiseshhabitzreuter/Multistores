using Contracts.DTOs;
using Contracts.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Multistores.Contracts.DTOs;

namespace Multistores.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService productService)
        {
            _storeService = productService;
        }

        [HttpGet(Name = "GetStores")]
        public async Task<IEnumerable<StoreDto>> Get()
        {
            return await _storeService.GetAllAsync();
        }

        [HttpPost(Name = "Create")]
        public async Task<StoreDto> Create(CreateUpdateStoreDto input)
        {
           return await _storeService.CreateAsync(input);
        }

        [HttpGet("{id}", Name = "GetById")]
        public async Task<StoreDto?> Get(Guid id)
        {
            return await _storeService.GetByIdAsync(id);
        }

        [HttpPut("{id:guid}", Name = "Update")]
        public async Task<StoreDto> Update(Guid id, [FromBody] CreateUpdateStoreDto dto)
        {
            return await _storeService.UpdateStoreAsync(id, dto);
        }

        [HttpDelete("{id:guid}", Name = "Delete")]
        public async Task Delete(Guid id)
        {
            await _storeService.DeleteStoreAsync(id);
        }
    }
}
