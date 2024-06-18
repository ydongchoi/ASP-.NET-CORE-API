using AutoMapper;
using CompanyEmployees;
using CompanyEmployees.Presentation.Extensions;
using Contracts;
using Entities.Exceptions;
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

        [Fact]
        public async Task GetByIdsAsync_ExisitingIds_Companies()
        {
            // Arrange
            var ids = new List<Guid>()
            {
                new Guid("43585C00-2346-4FEA-AA74-08DC81A68D90"),
                new Guid("02946162-6D18-4AED-45D6-08DC81B4C1C5")
            };
            bool trackChanges = false;

            // Act
            var result = await _service.GetByIdsAsync(ids, trackChanges);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<CompanyDto>>(result);
            Assert.IsAssignableFrom<List<CompanyDto>>(result);
            Assert.NotEmpty(result as List<CompanyDto>);
        }

        [Fact]
        public async Task GetByIdsAsync_NullIds_IdPrametersBadRequestException()
        {
            // Arrange
            List<Guid> ids = null;
            bool trackChanges = false;

            // Act & Assert
            Assert.ThrowsAsync<IdParametersBadRequestException>(async () => await _service.GetByIdsAsync(ids, trackChanges));
        }

        [Fact]
        public async Task GetByIdsAsync_BadIds_CollectionByIdsBadRequestException()
        {
            // Arrange
            var ids = new List<Guid>()
            {
                new Guid("43585C00-2346-4FEA-AA74-08DC81A68D90"),
                new Guid("02946162-6D18-4AED-45D6-08DC81B4C1F0")
            };
            bool trackChanges = false;

            // Act & Assert
            Assert.ThrowsAsync<CollectionByIdsBadRequestException>(async () => await _service.GetByIdsAsync(ids, trackChanges));
        }

        [Fact]
        public async Task CreateCompanyAsync_Company_ThenCreatedReturns()
        {
            // Arrange
            var company = new CompanyForCreationDto()
            {
                Name = "Happy",
                Address = "Surrey",
                Country = "Canada"
            };

            // Act
            var result = await _service.CreateCompanyAsync(company);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CompanyDto>(result);
            Assert.IsAssignableFrom<CompanyDto>(result);
            Assert.NotNull(result as CompanyDto);
        }

        [Fact]
        public async Task CreateCompanyCollectionAsync_Companies_ThenCreatedResults()
        {
            // Arrange
            var companies = new List<CompanyForCreationDto>(){
                new CompanyForCreationDto(){
                    Name = "Happy",
                    Address = "Surrey",
                    Country = "Canada"
                },
                new CompanyForCreationDto(){
                    Name = "Exciting",
                    Address = "Vancouver",
                    Country = "Canada"
                }
            };

            // Act
            var result = await _service.CreateCompanyCollectionAsync(companies);

            // Assert
            Assert.NotNull(result.companies);
            Assert.NotNull(result.ids);
            Assert.IsAssignableFrom<IEnumerable<CompanyDto>>(result.companies);
            Assert.IsAssignableFrom<string>(result.ids);
        }

        [Fact]
        public async Task CreateCompanyCollectionAsync_NullCompanies_CompanyCollectionBadRequest()
        {
            // Arrange
            List<CompanyForCreationDto> companies = null;

            // Act & Assert
            Assert.ThrowsAsync<CompanyCollectionBadRequest>(async () => await _service.CreateCompanyCollectionAsync(companies));
        }

        [Fact]
        public async Task DeleteCompanyAsync_NonExistingCompanyId_CompanyNotFoundException()
        {
            // Arrange
            Guid companyId = new Guid("43585C00-2346-4FEA-AA74-08DC81A68D23");
            bool trackChanges = false;

            // Act & Assert
            Assert.ThrowsAsync<CompanyNotFoundException>(async () => await _service.DeleteCompanyAsync(companyId, trackChanges));
        }

        [Fact]
        public async Task UpdateCompanyAsync_NonExistingCompanyId_CompanyNotFoundException()
        {
            // Arrange
            Guid companyId = new Guid("43585C00-2346-4FEA-AA74-08DC81A68D23");
            var companyForUpdate = new CompanyForUpdateDto("Happy", "Surrey", "Canada", null);
            bool trackChanges = false;

            // Act & Assert
            Assert.ThrowsAsync<CompanyNotFoundException>(async () => await _service.UpdateCompanyAsync(companyId, companyForUpdate, trackChanges));
        }

        public IMapper GetMapper()
        {
            var mapplingProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mapplingProfile));
            return new Mapper(configuration);
        }


    }
}
