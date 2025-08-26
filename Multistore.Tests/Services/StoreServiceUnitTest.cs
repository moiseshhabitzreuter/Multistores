using Domain.Entities;
using Domain.Services;
using Moq;
using Multistores.Contracts.DTOs;
using Multistores.Domain.Interfaces;

namespace Multistores.Tests.Services
{
    public class StoreServiceUnitTest
    {
        private readonly Mock<IStoreRepository> _mockRepository;
        private readonly StoreService _service;

        public StoreServiceUnitTest()
        {
            _mockRepository = new Mock<IStoreRepository>();
            _service = new StoreService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnMappedStores()
        {
            var stores = new List<Store>
            {
                new Store { Id = Guid.NewGuid(), Code = "0050", Name = "TesteStore1", IdentificationCode = "IdCodeTeste1" },
                new Store { Id = Guid.NewGuid(), Code = "0051", Name = "TesteStore2", IdentificationCode = "IdCodeTeste2" }
            };

            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(stores);

            var result = await _service.GetAllAsync();

            Assert.Collection(result,
                s =>
                {
                    Assert.Equal("TesteStore1", s.Name);
                    Assert.Equal("0050", s.Code);
                    Assert.Equal("IdCodeTeste1", s.IdentificationCode);
                },
                s =>
                {
                    Assert.Equal("TesteStore2", s.Name);
                    Assert.Equal("0051", s.Code);
                    Assert.Equal("IdCodeTeste2", s.IdentificationCode);
                }
            );
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnStore_WhenExists()
        {
            var id = Guid.NewGuid();
            var store = new Store { Id = id, Code = "0050", Name = "TesteStore", IdentificationCode = "IdCodeTeste" };

            _mockRepository.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(store);

            var result = await _service.GetByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal("TesteStore", result.Name);
            Assert.Equal("0050", result.Code);
            Assert.Equal("IdCodeTeste", result.IdentificationCode);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            var id = Guid.NewGuid();
            _mockRepository.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Store?)null);

            var result = await _service.GetByIdAsync(id);

            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnCreatedStoreDto()
        {
            var dto = new CreateUpdateStoreDto { Code = "0050", Name = "TesteStore", IdentificationCode = "IdCodeTeste" };
            var id = Guid.NewGuid();

            var store = new Store
            {
                Id = id,
                Code = dto.Code,
                Name = dto.Name,
                IdentificationCode = dto.IdentificationCode
            };

            _mockRepository.Setup(r => r.AddAsync(It.IsAny<Store>()))
                             .ReturnsAsync(store);

            var result = await _service.CreateAsync(dto);

            Assert.NotNull(result);
            Assert.Equal("TesteStore", result.Name);
            Assert.Equal("0050", result.Code);
            Assert.Equal("IdCodeTeste", result.IdentificationCode);
            Assert.Equal(id, store.Id);
        }

        [Fact]
        public async Task UpdateStoreAsync_ShouldReturnUpdatedStore_WhenExists()
        {
            var id = Guid.NewGuid();
            var existing = new Store { Id = id, Code = "0050", Name = "TesteStore", IdentificationCode = "IdCodeTeste" };
            var dto = new CreateUpdateStoreDto { Code = "0051", Name = "TesteStoreUpdate", IdentificationCode = "IdCodeTeste" };

            _mockRepository.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(existing);
            _mockRepository.Setup(r => r.UpdateAsync(existing)).Returns(Task.CompletedTask);

            var result = await _service.UpdateStoreAsync(id, dto);

            Assert.NotNull(result);
            Assert.Equal("TesteStoreUpdate", result.Name);
            Assert.Equal("0051", result.Code);
            Assert.Equal("IdCodeTeste", result.IdentificationCode);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task UpdateStoreAsync_ShouldReturnNull_WhenNotExists()
        {
            var id = Guid.NewGuid();
            var dto = new CreateUpdateStoreDto { Code = "0050", Name = "TesteNotExistingStore", IdentificationCode = "IdCodeTeste" };

            _mockRepository.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Store?)null);

            var result = await _service.UpdateStoreAsync(id, dto);

            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteStoreAsync_ShouldReturnTrue_WhenDeleted()
        {
            var id = Guid.NewGuid();
            var store = new Store { Id = id, Code = "0050", Name = "TesteStore", IdentificationCode = "IdCodeTeste" };

            _mockRepository.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(store);
            _mockRepository.Setup(r => r.DeleteAsync(store)).Returns(Task.CompletedTask);

            var result = await _service.DeleteStoreAsync(id);

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteStoreAsync_ShouldReturnFalse_WhenNotExists()
        {
            var id = Guid.NewGuid();
            _mockRepository.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Store?)null);

            var result = await _service.DeleteStoreAsync(id);

            Assert.False(result);
        }
    }
}
