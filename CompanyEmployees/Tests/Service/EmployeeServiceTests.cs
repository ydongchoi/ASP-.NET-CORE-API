using AutoMapper;
using CompanyEmployees;
using CompanyEmployees.Utility;
using Contracts;
using Entities.Exceptions;
using LoggerService;
using Moq;
using Service;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Mocks;

namespace Tests.Service
{
    public class EmployeeServiceTests
    {
        private readonly EmployeeService _service;
        private readonly Mock<IRepositoryManager> _mockRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly Mock<IEmployeeLinks> _employeeLinks;

        public EmployeeServiceTests()
        {
            _mockRepository = MockIRepositoryManager.GetMock();
            _mapper = GetMapper();
            _logger = new LoggerManager();
            _employeeLinks = MockIEmployeeLinks.GetMock();

            _service = new EmployeeService(_mockRepository.Object, _logger, _mapper, _employeeLinks.Object);
        }

        [Fact]
        public async Task CreateEmployeeForCompanyAsync_Employee_ThenCreatedResult()
        {
            // Arrange
            var companyId = Guid.Parse("43585C00-2346-4FEA-AA74-08DC81A68D90");
            var employee = new EmployeeForCreationDto()
            {
                Name = "yeongdong",
                Age = 30,
                Position = "SW Developer"
            };
            bool trackChanges = false;

            // Act
            var result = await _service.CreateEmployeeForCompanyAsync(companyId, employee, trackChanges);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<EmployeeDto>(result);
        }

        [Fact]
        public async Task CreateEmployeeForCompanyAsync_NonCompany_ThenCreatedResult()
        {
            // Arrange
            var companyId = Guid.Parse("12585C00-2346-4FEA-AA74-08DC81A68D90");
            var employee = new EmployeeForCreationDto()
            {
                Name = "yeongdong",
                Age = 30,
                Position = "SW Developer"
            };
            bool trackChanges = false;

            // Act & Assert
            Assert.ThrowsAsync<CompanyNotFoundException>(async () => await _service.CreateEmployeeForCompanyAsync(companyId, employee, trackChanges));
        }

        [Fact]
        public async Task DeleteEmployeeForCompanyAsync_NonExistingCompany_CompanyNotFoundException()
        {
            // Arrange
            var companyId = Guid.Parse("12585C00-2346-4FEA-AA74-08DC81A68D90");
            var employeeId = Guid.Parse("80ABBCA8-664D-4B20-B5DE-024705497D4A");
            bool trackChanges = false;

            // Act & Assert
            Assert.ThrowsAsync<CompanyNotFoundException>(async () => await _service.DeleteEmployeeForCompanyAsync(companyId, employeeId, trackChanges));
        }

        [Fact]
        public async Task DeleteEmployeeForCompanyAsync_NonExistingEmployee_EmployeeNotFoundException()
        {
            // Arrange
            var companyId = Guid.Parse("43585C00-2346-4FEA-AA74-08DC81A68D90");
            var employeeId = Guid.Parse("77ABBCA8-664D-4B20-B5DE-024705497D4A");
            bool trackChanges = false;

            // Act & Assert
            Assert.ThrowsAsync<EmployeeNotFoundException>(async () => await _service.DeleteEmployeeForCompanyAsync(companyId, employeeId, trackChanges));
        }

        [Fact]
        public async Task UpdateEmployeeForCompanyAsync_NonExistingCompany_CompanyNotFoundException()
        {
            // Arrange
            var companyId = Guid.Parse("12585C00-2346-4FEA-AA74-08DC81A68D90");
            var employeeId = Guid.Parse("80ABBCA8-664D-4B20-B5DE-024705497D4A");
            var employeeForUpdate = new EmployeeForUpdateDto()
            {
                Name = "yeongdong",
                Age = 30,
                Position = "SW Developer"
            };
            bool compTrackChanges = false;
            bool empTrackChanges = false;

            // Act & Assert
            Assert.ThrowsAsync<CompanyNotFoundException>(async () => await _service.UpdateEmployeeForCompanyAsync(companyId, employeeId, employeeForUpdate, compTrackChanges, empTrackChanges));
        }

        [Fact]
        public async Task UpdateEmployeeForCompanyAsync_NonExistingEmployee_EmployeeNotFoundException()
        {
            // Arrange
            var companyId = Guid.Parse("43585C00-2346-4FEA-AA74-08DC81A68D90");
            var employeeId = Guid.Parse("77ABBCA8-664D-4B20-B5DE-024705497D4A");
            var employeeForUpdate = new EmployeeForUpdateDto()
            {
                Name = "yeongdong",
                Age = 30,
                Position = "SW Developer"
            };
            bool compTrackChanges = false;
            bool empTrackChanges = false;

            // Act & Assert
            Assert.ThrowsAsync<EmployeeNotFoundException>(async () => await _service.UpdateEmployeeForCompanyAsync(companyId, employeeId, employeeForUpdate, compTrackChanges, empTrackChanges));
        }

        public IMapper GetMapper()
        {
            var mapplingProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mapplingProfile));
            return new Mapper(configuration);
        }
    }
}
