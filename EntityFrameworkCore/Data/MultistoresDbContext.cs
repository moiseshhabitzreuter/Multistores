using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Multistores.EntityFrameworkCore.Data
{
    public class MultistoresDbContext : DbContext
    {
        public MultistoresDbContext(DbContextOptions<MultistoresDbContext> options)
            : base(options) { }

        public DbSet<Product> Products => Set<Product>();
    }
}
