using Contracts;
using Microsoft.AspNetCore.Http;
using Moq;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Mocks
{
    internal class MockIEmployeeLinks
    {
        public static Mock<IEmployeeLinks> GetMock()
        {
            var mock = new Mock<IEmployeeLinks>();

            // Set up the mock
            mock.Setup(m => m.TryGenerateLinks(It.IsAny<IEnumerable<EmployeeDto>>(), It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<HttpContext>()))
                .Returns(() =>
                {
                    return;
                });

            return mock;
        }
    }
}
