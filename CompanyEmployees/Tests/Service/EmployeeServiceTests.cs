using AutoMapper;
using CompanyEmployees;
using CompanyEmployees.Utility;
using Contracts;
using LoggerService;
using Moq;
using Service;
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

        public IMapper GetMapper()
        {
            var mapplingProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mapplingProfile));
            return new Mapper(configuration);
        }
    }
}
