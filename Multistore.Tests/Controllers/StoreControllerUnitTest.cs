using Contracts.DTOs;
using Contracts.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Multistores.Contracts.DTOs;
using Multistores.Controllers;

namespace Multistores.Tests.Controllers
{
    public class StoreControllerUnitTest
    {
        private readonly Mock<IStoreService> _mockService;
        private readonly StoreController _controller;

        public StoreControllerUnitTest()
        {
            _mockService = new Mock<IStoreService>();
            _controller = new StoreController(_mockService.Object);
        }

        [Fact]
        public async Task Get_ShouldReturnOkWithStores()
        {
            var stores = new List<StoreDto>
            {
                new StoreDto { Id = Guid.NewGuid(), Code = "0050", Name = "TesteStore1", IdentificationCode = "IdCodeTeste" }
            };
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(stores);

            var result = await _controller.Get();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<StoreDto>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task Create_ShouldReturnOkWithCreatedStore()
        {
            var input = new CreateUpdateStoreDto { Code = "0050", Name = "TesteStore1", IdentificationCode = "IdCodeTeste" };
            var expected = new StoreDto { Id = Guid.NewGuid(), Code = input.Code, Name = input.Name , IdentificationCode = input.IdentificationCode};

            _mockService.Setup(s => s.CreateAsync(input)).ReturnsAsync(expected);

            var result = await _controller.Create(input);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<StoreDto>(okResult.Value);
            Assert.Equal(expected.Code, returnValue.Code);
            Assert.Equal(expected.Name, returnValue.Name);
            Assert.Equal(expected.IdentificationCode, returnValue.IdentificationCode);
        }

        [Fact]
        public async Task GetById_ShouldReturnOk_WhenStoreExists()
        {
            var id = Guid.NewGuid();
            var store = new StoreDto { Id = id, Code = "0050", Name = "TesteStore1", IdentificationCode = "IdCodeTeste" };

            _mockService.Setup(s => s.GetByIdAsync(id)).ReturnsAsync(store);

            var result = await _controller.Get(id);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<StoreDto>(okResult.Value);
            Assert.Equal(store.Id, returnValue.Id);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenStoreDoesNotExist()
        {
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.GetByIdAsync(id)).ReturnsAsync((StoreDto?)null);

            var result = await _controller.Get(id);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Update_ShouldReturnOk_WhenStoreUpdated()
        {
            var id = Guid.NewGuid();
            var dto = new CreateUpdateStoreDto { Code = "0051", Name = "TesteStoreUpdate", IdentificationCode = "IdCodeTeste" };
            var updatedStore = new StoreDto { Id = id, Code = dto.Code, Name = dto.Name };

            _mockService.Setup(s => s.UpdateStoreAsync(id, dto)).ReturnsAsync(updatedStore);

            var result = await _controller.Update(id, dto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<StoreDto>(okResult.Value);
            Assert.Equal(updatedStore.Name, returnValue.Name);
            Assert.Equal(updatedStore.Code, returnValue.Code);
            Assert.Equal(updatedStore.IdentificationCode, returnValue.IdentificationCode);
        }

        [Fact]
        public async Task Update_ShouldReturnNotFound_WhenStoreDoesNotExist()
        {
            var id = Guid.NewGuid();
            var dto = new CreateUpdateStoreDto { Code = "0051", Name = "TesteNotExistingStore", IdentificationCode = "IdCodeTeste" };

            _mockService.Setup(s => s.UpdateStoreAsync(id, dto)).ReturnsAsync((StoreDto?)null);

            var result = await _controller.Update(id, dto);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnOk_WhenDeleted()
        {
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.DeleteStoreAsync(id)).ReturnsAsync(true);

            var result = await _controller.Delete(id);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFound_WhenNotDeleted()
        {
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.DeleteStoreAsync(id)).ReturnsAsync(false);

            var result = await _controller.Delete(id);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
