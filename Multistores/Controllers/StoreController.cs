using Contracts.DTOs;
using Contracts.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Get()
        {
            var stores = await _storeService.GetAllAsync();
            return Ok(stores);
        }

        [HttpPost(Name = "Create")]
        public async Task<IActionResult> Create(CreateUpdateStoreDto input)
        {
            try
            {
                var createdStore = await _storeService.CreateAsync(input);
                return Ok(createdStore);
            }
            catch (DbUpdateException)
            {
                return Conflict(new { message = $"A store with code '{input.Code}' already exists." });
            }
        }

        [HttpGet("{id}", Name = "GetById")]
        public async Task<IActionResult> Get(Guid id)
        {
            var store = await _storeService.GetByIdAsync(id);

            if (store == null)
            {
                return NotFound();
            }

            return Ok(store);
        }

        [HttpPut("{id:guid}", Name = "Update")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateUpdateStoreDto dto)
        {
            var store = await _storeService.UpdateStoreAsync(id, dto);
            if (store == null)
            {
                return NotFound();
            }

            return Ok(store);
        }

        [HttpDelete("{id:guid}", Name = "Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isDeletedSuccesfully = await _storeService.DeleteStoreAsync(id);
            if (!isDeletedSuccesfully)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
