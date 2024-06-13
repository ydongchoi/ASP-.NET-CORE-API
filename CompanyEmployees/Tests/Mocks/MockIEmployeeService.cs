using Entities.Models;
using Moq;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Mocks
{
    internal class MockIEmployeeService
    {
        public static Mock<IEmployeeService> GetMock()
        {
            var mock = new Mock<IEmployeeService>();

            Guid companyId = new Guid("C9D4C053-49B6-410C-BC78-2D54A9991870");
            Guid id = new Guid("80ABBCA8-664D-4B20-B5DE-024705497D4A");
            
            bool trackChanges = false;
            bool compTrackChange = false; bool empTrackChange = false;
            
            EmployeeForUpdateDto employeeForUpdateDto = new EmployeeForUpdateDto
            {
                Name = "Yeongdong",
                Age = 31,
                Position = "Administrator"
            };

            EmployeeForCreationDto employeeForCreation = new EmployeeForCreationDto
            {
                Name = "Yeongdong",
                Age = 31,
                Position = "Software Developer"
            };

            var employeeForPatch = (
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

            // Setup the mock
            mock.Setup(m => m.GetEmployeeAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<bool>()))
                .ReturnsAsync(
                (Guid companyId, Guid id, bool trackChanges) =>
                {
                    return new EmployeeDto(id, "Sam Raiden", 26, "Software developer");
                }
                );

            mock.Setup(m => m.DeleteEmployeeForCompanyAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<bool>()))
            .Returns(
                    Task.CompletedTask
                );

            mock.Setup(m => m.UpdateEmployeeForCompanyAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<EmployeeForUpdateDto>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(
                    Task.CompletedTask)
                ;

            mock.Setup(m => m.GetEmployeeForPatchAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(
                    employeeForPatch
                );

            mock.Setup(m => m.SaveChangesForPatchAsync(It.IsAny<EmployeeForUpdateDto>(), It.IsAny<Employee>()))
                .Returns(
                    Task.CompletedTask
                );

            mock.Setup(m => m.CreateEmployeeForCompanyAsync(It.IsAny<Guid>(), It.IsAny<EmployeeForCreationDto>(), It.IsAny<bool>()))
                .ReturnsAsync(
                    new EmployeeDto(companyId, employeeForCreation.Name, employeeForCreation.Age, employeeForCreation.Position)
                );

            return mock;
        }
    }
}
