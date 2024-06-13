using Moq;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Mocks
{
    internal class MockServiceManager
    {
        public static Mock<IServiceManager> GetMock()
        {
            var mock = new Mock<IServiceManager>();
            var employeeServiceMock = MockIEmployeeService.GetMock();

            // Setup the mock
            mock.Setup(m => m.CompanyService)
                .Returns(new Mock<ICompanyService>().Object);
            mock.Setup(m => m.EmployeeService)
                .Returns(() => employeeServiceMock.Object);
            mock.Setup(m => m.AuthenticationService)
                .Returns(new Mock<IAuthenticationService>().Object);

            return mock;
        }
    }
}
