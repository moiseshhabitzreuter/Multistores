using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Multistores.Domain.Interfaces;
using Multistores.EntityFrameworkCore.Data;

namespace Multistores.EntityFrameworkCore.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MultistoresDbContext _dbContext;

        public ProductRepository(MultistoresDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAllAsync() =>
            await _dbContext.Products.ToListAsync();

        public async Task<Product?> GetByIdAsync(Guid id) =>
            await _dbContext.Products.FindAsync(id);

        public async Task<Product> AddAsync(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }
    }
}
