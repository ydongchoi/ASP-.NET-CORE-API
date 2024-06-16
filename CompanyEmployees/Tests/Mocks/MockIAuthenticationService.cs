using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;
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
            var user = new UserForAuthenticationDto()
            {
                UserName = "hello",
                Password = "hello1234",
            };

            mock.Setup(m => m.RegisterUser(It.IsAny<UserForRegistrationDto>()))
                .ReturnsAsync(() =>
                {
                    return IdentityResult.Success;
                });

            mock.Setup(m => m.ValidateUser(It.IsAny<UserForAuthenticationDto>()))
                .ReturnsAsync((UserForAuthenticationDto userForAuth) =>
                {
                    if (userForAuth.UserName == user.UserName && userForAuth.Password == user.Password) return true;
                    else return false;
                });

            mock.Setup(m => m.CreateToken(It.IsAny<bool>()))
                .Callback(() => {
                    return;
                });

            mock.Setup(m => m.RefreshToken(It.IsAny<TokenDto>()))
                .Callback(() =>
                {
                    return;
                });

            return mock;
        }
    }
}
