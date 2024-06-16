using Moq;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Mocks
{
    internal class MockIAuthenticationService
    {
        public static Mock<IAuthenticationService> GetMock()
        {
            var mock = new Mock<IAuthenticationService>();

            // Setup the mock
            mock.Setup(m => m.RegisterUser(It.IsAny<UserForRegistrationDto>()))
                .Callback(() =>
                {
                    return;
                });

            mock.Setup(m => m.ValidateUser(It.IsAny<UserForAuthenticationDto>()))
                .Callback(() =>
                {
                    return;
                });

            return mock;
        }
    }
}
