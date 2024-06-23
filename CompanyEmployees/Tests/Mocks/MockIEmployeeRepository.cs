using Contracts;
using Moq;
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

            // Set up the mock

            return mock;
        }
    }
}
