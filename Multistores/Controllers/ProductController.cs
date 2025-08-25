using Contracts.DTOs;
using Contracts.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Multistores.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet(Name = "GetProducts")]
        public async Task<IEnumerable<ProductDto>> Get()
        {
            return await _productService.GetAllAsync();
        }

        [HttpPost(Name = "Create")]
        public async Task<ProductDto> Create(ProductDto input)
        {
           return await _productService.CreateAsync(input);
        }

        [HttpGet("{id}", Name = "GetById")]
        public async Task<ProductDto?> Get(Guid id)
        {
            return await _productService.GetByIdAsync(id);
        }
    }
}
