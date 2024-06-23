using Contracts;
using Entities.Models;
using Moq;
using Shared.RequestFeature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Mocks
{
    internal class MockIEmployeeRepository
    {
        public static Mock<IEmployeeRepository> GetMock()
        {
            var mock = new Mock<IEmployeeRepository>();
            
            // Database Double
            var employees = new List<Employee>(){
                new Employee()
                {
                    Id = Guid.Parse("80ABBCA8-664D-4B20-B5DE-024705497D4A"),
                    Name = "Sam Raiden",
                    Age = 26,
                    Position = "Software developer",
                    CompanyId = Guid.Parse("C9D4C053-49B6-410C-BC78-2D54A9991870")
                },
                new Employee()
                {
                    Id = Guid.Parse("446793C0-C162-433B-59D2-08DC84C78066"),
                    Name = "Mihael Worth",
                    Age = 30,
                    Position = "Marketing expert",
                    CompanyId = Guid.Parse("C9D4C053-49B6-410C-BC78-2D54A9991870")
                }
                
            };

            // Set up the mock
            mock.Setup(m => m.GetEmployeesAsync(It.IsAny<Guid>(), It.IsAny<EmployeeParameters>(), It.IsAny<bool>()))
                .ReturnsAsync((Guid companyId, EmployeeParameters employeeParameters, bool trackChanges) =>
                {
                    var employeesFromDb = employees.Where(e => e.CompanyId == companyId).ToList();
                    
                    return PagedList<Employee>.ToPagedList(employeesFromDb, employeeParameters.PageNumber, employeeParameters.PageSize);
                });

            mock.Setup(m => m.GetEmployeeAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<bool>()))
                .ReturnsAsync((Guid companyId, Guid id, bool trackChanges) =>
                {
                    return employees.FirstOrDefault(e => e.CompanyId == companyId && e.Id == id);
                });

            mock.Setup(m => m.CreateEmployeeForCompany(It.IsAny<Guid>(), It.IsAny<Employee>()))
                .Callback(() => { return; });
                
            mock.Setup(m => m.DeleteEmployee(It.IsAny<Employee>()))
                .Callback(() =>
                { return; });

            return mock;
        }
    }
}
