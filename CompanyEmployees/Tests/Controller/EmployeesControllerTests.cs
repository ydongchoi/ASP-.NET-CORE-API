using CompanyEmployees.Presentation.Controllers;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CreateEmployeeForCompany_EmployeeNull_BadRequest()
        {
            // Arrange
            Guid companyId = new Guid("C9D4C053-49B6-410C-BC78-2D54A9991870");
            EmployeeForCreationDto employeeForCreation = null;

            // Act
            var result = await _controller.CreateEmployeeForCompany(companyId, employeeForCreation);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateEmployeeForCompany_ModelStateInvalid_UnprocessableEntity()
        {
            // Arrange
            Guid companyId = new Guid("C9D4C053-49B6-410C-BC78-2D54A9991870");
            EmployeeForCreationDto employeeForCreation = new EmployeeForCreationDto
            {
                Age = 29,
                Position = "Software Developer"
            };

            _controller.ModelState.AddModelError("Name", "Required");

            // Act
            var result = await _controller.CreateEmployeeForCompany(companyId, employeeForCreation);

            // Assert
            Assert.IsType<UnprocessableEntityObjectResult>(result);
        }

        [Fact]
        public async Task CreateEmployeeForCompany_RequestEligibleEmployeeForCreation_CreatedAtRoute()
        {
            // Arrange
            Guid companyId = Guid.NewGuid();
            EmployeeForCreationDto employeeForCreation = new EmployeeForCreationDto
            {
                Name = "Yeongdong",
                Age = 31,
                Position = "Software Developer"
            };
            bool trackChanges = false;

            _mockEmployeeService
                .Setup(
                    m => m.CreateEmployeeForCompanyAsync(companyId, employeeForCreation, trackChanges))
                .ReturnsAsync(
                    new EmployeeDto(Guid.NewGuid(), employeeForCreation.Name, employeeForCreation.Age, employeeForCreation.Position)
                );

            // Act
            var result = await _controller.CreateEmployeeForCompany(companyId, employeeForCreation);

            // Assert
            Assert.IsType<CreatedAtRouteResult>(result);
        }

        [Fact]
        public async Task DeleteEmployeeForCompany_ExistingIdPassed_NoContent()
        {
            // Arrange
            Guid companyId = new Guid("C9D4C053-49B6-410C-BC78-2D54A9991870");
            Guid id = new Guid("80ABBCA8-664D-4B20-B5DE-024705497D4A");
            bool trackChanges = false;

            _mockEmployeeService
                .Setup(m => m.DeleteEmployeeForCompanyAsync(companyId, id, trackChanges))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteEmployeeForCompany(companyId, id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateEmployeeForCompany_EmployeeNull_BadRequest()
        {
            // Arrange
            var companyId = new Guid("C9D4C053-49B6-410C-BC78-2D54A9991870");
            Guid id = new Guid("80ABBCA8-664D-4B20-B5DE-024705497D4A");
            EmployeeForUpdateDto employeeForUpdateDto = null;

            // Act
            var result = await _controller.UpdateEmployeeForCompany(companyId, id, employeeForUpdateDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateEmployeeForCompany_ModelStateInvalid_UnprocessableEntity()
        {
            // Arrange
            var companyId = new Guid("C9D4C053-49B6-410C-BC78-2D54A9991870");
            Guid id = new Guid("80ABBCA8-664D-4B20-B5DE-024705497D4A");
            EmployeeForUpdateDto employeeForUpdateDto = new EmployeeForUpdateDto
            {
                Name = "Yeongdong",
                Age = 31
            };

            _controller.ModelState.AddModelError("Position", "Required");

            // Act
            var result = await _controller.UpdateEmployeeForCompany(companyId, id, employeeForUpdateDto);

            // Assert
            Assert.IsType<UnprocessableEntityObjectResult>(result);
        }

        [Fact]
        public async Task UpdateEmployeeForCompany_RequestEligibleEmployeeForUpdate_UnprocessableEntity()
        {
            // Arrange
            var companyId = new Guid("C9D4C053-49B6-410C-BC78-2D54A9991870");
            Guid id = new Guid("80ABBCA8-664D-4B20-B5DE-024705497D4A");
            EmployeeForUpdateDto employeeForUpdateDto = new EmployeeForUpdateDto
            {
                Name = "Yeongdong",
                Age = 31,
                Position = "Administrator"
            };
            bool compTrackChange = false; bool empTrackChange = false;

            _mockEmployeeService
                .Setup(m => m.UpdateEmployeeForCompanyAsync(companyId, id, employeeForUpdateDto, compTrackChange, empTrackChange))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateEmployeeForCompany(companyId, id, employeeForUpdateDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PartiallyUpdateEmployeeForCompany_PatchDocNull_BadRequest()
        {
            // Arrange
            var companyId = new Guid("C9D4C053-49B6-410C-BC78-2D54A9991870");
            Guid id = new Guid("80ABBCA8-664D-4B20-B5DE-024705497D4A");
            JsonPatchDocument<EmployeeForUpdateDto> patchDoc = null;

            // Act
            var result = await _controller.PartiallyUpdateEmployeeForCompany(companyId, id, patchDoc);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task PartiallyUpdateEmployeeForCompany_ModelStateInvalid_UnprocessableEntity()
        {
            // Arrange
            var companyId = new Guid("C9D4C053-49B6-410C-BC78-2D54A9991870");
            Guid id = new Guid("80ABBCA8-664D-4B20-B5DE-024705497D4A");
            bool compTrackChanges = false; bool empTrackChanges = true;

            var patchDoc = new JsonPatchDocument<EmployeeForUpdateDto>();
            patchDoc.Replace(item => item.Name, "Yeongdong Choi");

            var result = (
                employeeToPatch: new EmployeeForUpdateDto
                {
                    Name = "Yeongdong",
                    Age = 31,
                    Position = "Administrator"
                },
                employeeEntitiy: new Employee
                {
                    CompanyId = companyId,
                    Id = id,
                    Name = "Yeongdong",
                    Age = 31,
                    Position = "Administrator"
                });

            _mockEmployeeService
                .Setup(m => m.GetEmployeeForPatchAsync(companyId, id, compTrackChanges, empTrackChanges))
                .ReturnsAsync(result);

            var objectValidator = new Mock<IObjectModelValidator>();
            objectValidator.Setup(o => o.Validate(It.IsAny<ActionContext>(),
                                              It.IsAny<ValidationStateDictionary>(),
                                              It.IsAny<string>(),
                                              It.IsAny<Object>()));

            _controller.ObjectValidator = objectValidator.Object;
            _controller.ModelState.AddModelError("Name", "Required.");

            // Act
            var patchResult = await _controller.PartiallyUpdateEmployeeForCompany(companyId, id, patchDoc);

            // Assert
            Assert.IsType<UnprocessableEntityObjectResult>(patchResult);
        }

        [Fact]
        public async Task PartiallyUpdateEmployeeForCompany_ModelStateInvalid_NoContent()
        {
            // Arrange
            var companyId = new Guid("C9D4C053-49B6-410C-BC78-2D54A9991870");
            Guid id = new Guid("80ABBCA8-664D-4B20-B5DE-024705497D4A");
            bool compTrackChanges = false; bool empTrackChanges = true;

            var patchDoc = new JsonPatchDocument<EmployeeForUpdateDto>();
            patchDoc.Replace(item => item.Name, "Yeongdong Choi");

            var result = (
                employeeToPatch : new EmployeeForUpdateDto
                {
                    Name = "Yeongdong",
                    Age = 31,
                    Position = "Administrator"
                },
                employeeEntitiy : new Employee
                {
                    CompanyId = companyId,
                    Id = id,
                    Name = "Yeongdong",
                    Age = 31,
                    Position = "Administrator"
                });

            _mockEmployeeService
                .Setup(m => m.GetEmployeeForPatchAsync(companyId, id, compTrackChanges, empTrackChanges))
                .ReturnsAsync(result);

            _mockEmployeeService
                .Setup(m => m.SaveChangesForPatchAsync(result.employeeToPatch, result.employeeEntitiy))
                .Returns(Task.CompletedTask);

            var objectValidator = new Mock<IObjectModelValidator>();
            objectValidator.Setup(o => o.Validate(It.IsAny<ActionContext>(),
                                              It.IsAny<ValidationStateDictionary>(),
                                              It.IsAny<string>(),
                                              It.IsAny<Object>()));
            _controller.ObjectValidator = objectValidator.Object;

            // Act
            var patchResult = await _controller.PartiallyUpdateEmployeeForCompany(companyId, id, patchDoc);

            // Assert
            Assert.IsType<NoContentResult>(patchResult);
        }
    }
}
