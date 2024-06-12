using CompanyEmployees.Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Controller
{
    public class EmployeesControllerTests
    {
        private readonly EmployeesController _controller;
        private readonly Mock<IEmployeeService> _mockEmployeeService;
        private readonly Mock<IServiceManager> _mockService;

        public EmployeesControllerTests()
        {

            _mockEmployeeService = new Mock<IEmployeeService>();
           
            _mockService = new Mock<IServiceManager>();
            _mockService
                .SetupGet(m => m.EmployeeService)
                .Returns(_mockEmployeeService.Object);

            _controller = new EmployeesController(_mockService.Object);
        }

        [Fact]
        public async Task GetEmployeeForCompany_Employee_Success()
        {
            // Arrange
            Guid companyId = new Guid("C9D4C053-49B6-410C-BC78-2D54A9991870");
            Guid id = new Guid("80ABBCA8-664D-4B20-B5DE-024705497D4A");
            bool trackChanges = false;

            _mockEmployeeService
                .Setup(m => m.GetEmployeeAsync(companyId, id, trackChanges))
                .ReturnsAsync(
                    new EmployeeDto(id, "Sam Raiden", 26, "Software developer")
                );


            // Act
            var result = await _controller.GetEmployeeForCompany(companyId, id);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
        }

    }
}
