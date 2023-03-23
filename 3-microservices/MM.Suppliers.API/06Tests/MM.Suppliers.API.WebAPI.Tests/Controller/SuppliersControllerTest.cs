using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MM.Suppliers.API.Common.Interfaces;
using MM.Suppliers.API.Web.APIModels;
using MM.Suppliers.API.Web.Controllers;
using MM.Suppliers.API.Web.Mappings;
using MM.Suppliers.API.WebApi.Tests.Fake;
using Xunit;

namespace MM.Suppliers.API.Web.Tests.Controller
{
    public class SuppliersControllerTest
    {
        private readonly SuppliersController _controller;

        private readonly ISuppliersService _supplierService;
        private readonly ILogger<SuppliersController> _logger;
        private readonly IMapper _mapper;

        public SuppliersControllerTest()
        {
            _supplierService = new SuppliersServiceFake();
            if (_mapper == null)
            {
                MapperConfiguration mappingConfig = new(mc =>
                {
                    mc.AddProfile(new SuppliersProfiles());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            _controller = new SuppliersController(_logger, _supplierService, _mapper);
        }

        #region GetByIdDepotAsync()

        //[Fact]
        //public async Task GetByIdAsync_ReturnsOkResult()
        //{            
        //    // Act
        //    IActionResult okResult = await _controller.GetByIdAsync();

        //    // Assert
        //    Assert.IsType<OkObjectResult>(okResult);
        //}

        #endregion

        #region GetAllPaginatedAsync([FromQuery] SuppliersApiPagingRequestModel suppliersApiPagingRequestModel)

        [Fact]
        public async Task GetAllPaginatedAsync_ReturnsOkResult()
        {
            // Arrange
            SuppliersApiPagingRequestModel supplierApiPagingRequestModel = new()
            {
                PageNumber = 1,
                Limit = 1,
                //Search = new List<string> { "Name:Test" },
                SortColumn = "Name",
                SortBy = "ASC"
            };

            // Act
            IActionResult okResult = await _controller.GetAllSuppliersPaginatedAsync(supplierApiPagingRequestModel);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        #endregion

        #region GetDepotByIdAsync(int id)

        [Fact]
        public async Task GetByIdAsync_ReturnsOkResult()
        {
            // Act
            IActionResult okResult = await _controller.GetByIdAsync(1);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        #endregion

        #region CreateAsync(SuppliersCreateRequestModel suppliersCreateRequestRequestModel)        

        [Fact]
        public async Task CreateAsync_ReturnsOkResult()
        {
            // Arrange
            SuppliersCreateRequestModel suppliersCreateRequestModel = new()
            {
                SupplierName = "ABC Supplier1",
                ContactPerson = "Supplier Name1",
                ContactEmail = "Supplier Name1",

            };

            // Act
            IActionResult okResult = await _controller.CreateAsync(suppliersCreateRequestModel);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        #endregion

        #region  UpdateAsync(SuppliersUpdateRequestModel depotRequestModel, int id)

        [Fact]
        public async Task UpdateAsync_ReturnsOkResult()
        {
            // Arrange
            SuppliersUpdateRequestModel suppliersUpdateRequestModel = new()
            {
                SupplierName = "ABC Supplier1",
                ContactPerson = "Supplier Name1",
                ContactEmail = "Supplier Name1",
                Active = true
            };

            // Act
            IActionResult okResult = await _controller.Update(suppliersUpdateRequestModel, 1);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }
        #endregion

        #region  DeleteAsync(int id)

        [Fact]
        public async Task DeleteAsync_ReturnsOkResult()
        {
            // Act
            IActionResult okResult = await _controller.Delete(1);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        #endregion
    }
}
