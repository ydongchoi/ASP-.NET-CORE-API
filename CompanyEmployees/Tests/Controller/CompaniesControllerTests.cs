using CompanyEmployees.Presentation.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Mocks;

namespace Tests.Controller
{
    public class CompaniesControllerTests
    {
        private readonly CompaniesController _controller;
        private readonly Mock<IServiceManager> _mockService;

        public CompaniesControllerTests()
        {
            _mockService = MockServiceManager.GetMock();
            _controller = new CompaniesController(_mockService.Object);
        }

        [Fact]
        public async Task GetCompanies_Companies_OkResponse()
        {
            // Arrange

            // Act
            var result = await _controller.GetCompanies() as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsAssignableFrom<IEnumerable<CompanyDto>>(result.Value);
            Assert.NotEmpty(result.Value as IEnumerable<CompanyDto>);
        }

        [Theory]
        [InlineData("43585C00-2346-4FEA-AA74-08DC81A68D90")]
        [InlineData("02946162-6D18-4AED-45D6-08DC81B4C1C5")]
        public async Task GetCompany_ExistingId_OkResponse(Guid id)
        {
            // Arrange

            // Act
            var result = await _controller.GetCompany(id) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsAssignableFrom<CompanyDto>(result.Value);
            Assert.NotNull(result.Value as CompanyDto);
        }

        [Theory]
        [InlineData("43585C00-2346-4FEA-AA74-08DC81A68320")]
        public async Task GetCompany_NonExistingId_NotFoundResponse(Guid id)
        {
            // Arrange

            // Act
            var result = await _controller.GetCompany(id) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async Task CreateCompany_Company_CreatedAtRoute()
        {
            // Arrange
            var company = new CompanyForCreationDto()
            {
                Name = "Hello",
                Address = "Back Street 123",
                Country = "Canada"
            };

            // Act
            var result = await _controller.CreateCompany(company) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<CreatedAtRouteResult>(result);
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
        }

        [Fact]
        public async Task GetCompanyCollection_ExisitingIds_OkResponse()
        {
            // Arrange
            var ids = new List<Guid>()
            {
                new Guid("43585C00-2346-4FEA-AA74-08DC81A68D90"),
                new Guid("02946162-6D18-4AED-45D6-08DC81B4C1C5")
            };

            // Act
            var result = await _controller.GetCompanyCollection(ids) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsAssignableFrom<IEnumerable<CompanyDto>>(result.Value);
            Assert.NotEmpty(result.Value as IEnumerable<CompanyDto>);
        }

        [Fact]
        public async Task CreateCompanyCollection_Companies_CreatedAtRoute()
        {
            // Arrage
            var companies = new List<CompanyForCreationDto>()
            {
                new CompanyForCreationDto()
                {
                    Name = "Hello",
                    Address = "Back Street 123",
                    Country = "Canada"
                },
                new CompanyForCreationDto()
                {
                    Name = "Hi",
                    Address = "Front Street 123",
                    Country = "USA"
                },
            };

            // Act
            var result = await _controller.CreateCompanyCollection(companies) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<CreatedAtRouteResult>(result);
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
        }

        [Fact]
        public async Task DeleteCompany_ExisitingId_NoContent()
        {
            // Arrange
            var id = new Guid("02946162-6D18-4AED-45D6-08DC81B4C1C5");

            // Act
            var result = await _controller.DeleteCompany(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateCompany_ExistingCompany_NoContent()
        {
            // Arrange
            var id = new Guid("02946162-6D18-4AED-45D6-08DC81B4C1C5");
            var company = new CompanyForUpdateDto("Hello", "Back Street 123", "Canada", null);

            // Act
            var result = await _controller.UpdateCompany(id, company);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<NoContentResult>(result);
        }

    }
}
