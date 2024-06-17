using Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Mocks
{
    internal class MockIRepositoryManager
    {
        public static Mock<IRepositoryManager> GetMock()
        {
            var mock = new Mock<IRepositoryManager>();

            // Set up the mock
            mock.Setup(m => m.Company)
                .Returns(() => new Mock<ICompanyRepository>());

            mock.Setup(m => m.Employee)
                .Returns(() => new Mock<IEmployeeRepository>());

            mock.Setup(m => m.SaveAsync())
                .Callback(() =>
                {
                    return;
                });

            return mock;
        }
    }
}
