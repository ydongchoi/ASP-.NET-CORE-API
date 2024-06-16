using CompanyEmployees.Presentation.Controllers;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Mocks;

namespace Tests.Controller
{
    public class AuthenticationControllerTests
    {
        private readonly AuthenticationController _controller;
        private readonly Mock<IServiceManager> _mockService;
        public AuthenticationControllerTests()
        {
            _mockService = MockServiceManager.GetMock();
            _controller = new AuthenticationController(_mockService.Object);
        }

        [Fact]
        public async Task RegisterUser_RegistrationDto_Created()
        {
            // Arrange
            var user = new UserForRegistrationDto()
            {
                FirstName = "yeongdong",
                LastName = "choi",
                UserName = "hello",
                Password = "hello1234",
                Email = "ydongdong@mail.com",
                PhoneNumber = "123-123-123",
                Roles = null
            };

            // Act
            var result = await _controller.RegisterUser(user) as StatusCodeResult;

            // Assert
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
        }

        [Fact]
        public async Task Authenticate_ValidatedUser_OkResponse()
        {
            // Arrage
            var user = new UserForAuthenticationDto()
            {
                UserName = "hello",
                Password = "hello1234",
            };

            // Act
            var result = await _controller.Authenticate(user) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Fact]
        public async Task Authenticate_UnValidatedUser_UnAuthorized()
        {
            // Arrage
            var user = new UserForAuthenticationDto()
            {
                UserName = "hello123",
                Password = "hello1234",
            };

            // Act
            var result = await _controller.Authenticate(user) as UnauthorizedResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status401Unauthorized, result.StatusCode);
        }

    }
}
