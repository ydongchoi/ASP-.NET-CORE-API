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


    }
}
