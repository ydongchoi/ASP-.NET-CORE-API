using AutoMapper;
using CompanyEmployees;
using CompanyEmployees.Presentation.Extensions;
using Contracts;
using Entities.Models;
using Entities.Responses;
using LoggerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Service;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Mocks;

namespace Tests.Service
{
    public class CompanyServiceTests
    {
        private readonly Mock<IRepositoryManager> _mockRepository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly ICompanyService _service;


        public CompanyServiceTests()
        {
            _mockRepository = MockIRepositoryManager.GetMock();
            _logger = new LoggerManager();
            _mapper = GetMapper();

            _service = new CompanyService(_mockRepository.Object, _logger, _mapper);
        }

        [Fact]
        public async Task GetAllCompaniesAsync_Companies_OkResponse()
        {
            // Arrange
            bool trackChanges = false;

            // Act
            var result = await _service.GetAllCompaniesAsync(trackChanges);
            var typedResult = result.GetResult<IEnumerable<CompanyDto>>();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ApiOkResponse<IEnumerable<CompanyDto>>>(result);
            Assert.IsAssignableFrom<IEnumerable<CompanyDto>>(typedResult);
            Assert.NotEmpty(typedResult as IEnumerable<CompanyDto>);
        }

        [Fact]
        public async Task GetCompanyAsync_Company_OkResponse()
        {
            // Arrange
            var id = new Guid("43585C00-2346-4FEA-AA74-08DC81A68D90");
            bool trackChanges = false;

            // Act
            var result = await _service.GetCompanyAsync(id, trackChanges);
            var typedResult = result.GetResult<CompanyDto>();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ApiOkResponse<CompanyDto>>(result);
            Assert.IsAssignableFrom<CompanyDto>(typedResult);
            Assert.NotNull(typedResult as CompanyDto);
        }

        [Fact]
        public async Task GetCompanyAsync_Company_CompanyNotFoundResponse()
        {
            // Arrange
            var id = new Guid("43585C00-2346-4FEA-AA74-08DC81A68D98");
            bool trackChanges = false;

            // Act
            var result = await _service.GetCompanyAsync(id, trackChanges);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CompanyNotFoundResponse>(result);
        }

        public IMapper GetMapper()
        {
            var mapplingProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mapplingProfile));
            return new Mapper(configuration);
        }


    }
}
