using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Multistores.EntityFrameworkCore.Data
{
    public class MultistoresDbContext : DbContext
    {
        public MultistoresDbContext(DbContextOptions<MultistoresDbContext> options)
            : base(options) { }

        public DbSet<Store> Stores => Set<Store>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Store>()
                .HasIndex(s => s.Code)
                .IsUnique()
                .HasFilter("[IsDeleted] = 0");
        }
    }
}
