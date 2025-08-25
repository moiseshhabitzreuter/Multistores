using Contracts.DTOs;
using Contracts.Interfaces.Services;
using Domain.Entities;
using Multistores.Domain.Interfaces;

namespace Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(p => new ProductDto
            {
                Name = p.Name,
                Code = p.Code,
                Packaging = p.Packaging,
                PackagingWeight = p.PackagingWeight,
                IndividualWeight = p.IndividualWeight,
                IndividualQuantity = p.IndividualQuantity,
            }
            ).ToList();
        }

        public async Task<ProductDto?> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product != null ? new ProductDto
            {
                Name = product.Name,
                Code = product.Code,
                Packaging = product.Packaging,
                PackagingWeight = product.PackagingWeight,
                IndividualWeight = product.IndividualWeight,
                IndividualQuantity = product.IndividualQuantity,
            }
            :
            null;
        }

        public async Task<ProductDto> CreateAsync(ProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Code = dto.Code,
                Packaging = dto.Packaging,
                PackagingWeight = dto.PackagingWeight,
                IndividualWeight = dto.IndividualWeight,
                IndividualQuantity = dto.IndividualQuantity,
            };

            await _productRepository.AddAsync(product);
            return dto;
        }
    }
}
