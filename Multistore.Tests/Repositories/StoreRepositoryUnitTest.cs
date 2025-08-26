using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Multistores.EntityFrameworkCore.Data;
using Multistores.EntityFrameworkCore.Repositories;

namespace Multistores.Tests.Repositories
{
    public class StoreRepositoryUnitTest
    {
        private MultistoresDbContext GetDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<MultistoresDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return new MultistoresDbContext(options);
        }

        [Fact]
        public async Task AddAsync_ShouldAddStore()
        {
            var context = GetDbContext(Guid.NewGuid().ToString());
            var repo = new StoreRepository(context);

            var store = new Store
            {
                Id = Guid.NewGuid(),
                Code = "0050",
                Name = "TesteStore",
                IdentificationCode = "IdCodeTeste"
            };

            var result = await repo.AddAsync(store);

            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.Equal("TesteStore", result.Name);
            Assert.Equal("0050", result.Code);
            Assert.Equal("IdCodeTeste", result.IdentificationCode);
            Assert.Single(context.Stores);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnOnlyNotDeletedStores()
        {
            var context = GetDbContext(Guid.NewGuid().ToString());
            context.Stores.AddRange(
                new Store { Id = Guid.NewGuid(), Name = "TesteStore1", Code = "0050", IdentificationCode = "IdCodeTeste1", IsDeleted = false },
                new Store { Id = Guid.NewGuid(), Name = "TesteStore2", Code = "0051", IdentificationCode = "IdCodeTeste2", IsDeleted = true }
            );
            await context.SaveChangesAsync();

            var repo = new StoreRepository(context);
            var stores = await repo.GetAllAsync();

            Assert.Single(stores);
            Assert.Equal("TesteStore1", stores.First().Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnStore_WhenNotDeleted()
        {
            var context = GetDbContext(Guid.NewGuid().ToString());
            var id = Guid.NewGuid();
            context.Stores.Add(new Store { Id = id, Name = "TesteStore1", Code = "0050", IdentificationCode = "IdCodeTeste1" });
            await context.SaveChangesAsync();

            var repo = new StoreRepository(context);
            var store = await repo.GetByIdAsync(id);

            Assert.NotNull(store);
            Assert.Equal("TesteStore1", store.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenDeleted()
        {
            var context = GetDbContext(Guid.NewGuid().ToString());
            var id = Guid.NewGuid();
            context.Stores.Add(new Store { Id = id, Name = "TesteStore1", Code = "0050", IdentificationCode = "IdCodeTeste1", IsDeleted = true });
            await context.SaveChangesAsync();

            var repo = new StoreRepository(context);
            var store = await repo.GetByIdAsync(id);

            Assert.Null(store);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateStore()
        {
            var context = GetDbContext(Guid.NewGuid().ToString());
            var store = new Store { Id = Guid.NewGuid(), Name = "TesteStore1", Code = "0050", IdentificationCode = "IdCodeTeste1" };
            context.Stores.Add(store);
            await context.SaveChangesAsync();

            var repo = new StoreRepository(context);
            store.Name = "TesteStoreUpdate";

            await repo.UpdateAsync(store);

            var updated = await context.Stores.FindAsync(store.Id);
            Assert.Equal("TesteStoreUpdate", updated.Name);
        }

        [Fact]
        public async Task DeleteAsync_ShouldSetIsDeletedTrue()
        {
            var context = GetDbContext(Guid.NewGuid().ToString());
            var store = new Store { Id = Guid.NewGuid(), Name = "TesteStore1", Code = "0050", IdentificationCode = "IdCodeTeste1" };
            context.Stores.Add(store);
            await context.SaveChangesAsync();

            var repo = new StoreRepository(context);
            await repo.DeleteAsync(store);

            var deleted = await context.Stores.FindAsync(store.Id);
            Assert.True(deleted.IsDeleted);
        }
    }
}
