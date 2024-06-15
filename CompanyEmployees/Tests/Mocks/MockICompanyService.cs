using Entities.Models;
using Entities.Responses;
using Moq;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Mocks
{
    internal class MockICompanyService
    {
        public static Mock<ICompanyService> GetMock()
        {
            var mock = new Mock<ICompanyService>();

            var companies = new List<CompanyDto>()
            {
                new CompanyDto()
                {
                    Id = new Guid("43585C00-2346-4FEA-AA74-08DC81A68D90"),
                    Name = "Electronics Solutions Ltd",
                    FullAddress = "312 Deliver Street, F 234 USA"
                },
                new CompanyDto()
                {
                    Id = new Guid("02946162-6D18-4AED-45D6-08DC81B4C1C5"),
                    Name = "Branding Ltd",
                    FullAddress = "255 Main Street, k 334 USA"
                }
            };

            // Setup the mock
            mock.Setup(m => m.GetAllCompaniesAsync(It.IsAny<bool>()))
                .ReturnsAsync(() => new ApiOkResponse<IEnumerable<CompanyDto>>(companies));

            mock.Setup(m => m.GetCompanyAsync(It.IsAny<Guid>(), It.IsAny<bool>()))
                .ReturnsAsync((Guid id, bool trackChanges) => {
                    var company = companies.FirstOrDefault(o => o.Id == id);

                    if (company is null)
                        return new CompanyNotFoundResponse(id);

                    return new ApiOkResponse<CompanyDto>(company);
                });

            mock.Setup(m => m.CreateCompanyAsync(It.IsAny<CompanyForCreationDto>()))
                .ReturnsAsync((CompanyForCreationDto companyForCreationDto) => new CompanyDto
                {
                    Id = Guid.NewGuid(),
                    Name = companyForCreationDto.Name,
                    FullAddress = string.Join(" ", companyForCreationDto.Address, companyForCreationDto.Country)
                });

            mock.Setup(m => m.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>(), It.IsAny<bool>()))
                .ReturnsAsync((IEnumerable<Guid> ids, bool trackChanges) =>
                {
                    return companies.Where(company => ids.Contains(company.Id)).ToList();
                });

            mock.Setup(m => m.CreateCompanyCollectionAsync(It.IsAny<IEnumerable<CompanyForCreationDto>>()))
                .ReturnsAsync((IEnumerable<CompanyForCreationDto> companyCollection) =>
                {
                    List<CompanyDto> companyDtoList = new List<CompanyDto>();

                    foreach(var company in companyCollection)
                    {
                        companyDtoList.Add(new CompanyDto()
                        {
                            Id = Guid.NewGuid(),
                            Name = company.Name,
                            FullAddress = String.Join(" ", company.Address, company.Country)
                        });
                    }
                    var ids = string.Join(",", companyDtoList.Select(c => c.Id));

                    return (companies: companyDtoList, ids: ids);
                });

            mock.Setup(m => m.DeleteCompanyAsync(It.IsAny<Guid>(), It.IsAny<bool>()))
                .Callback(() => { return; });

            mock.Setup(m => m.UpdateCompanyAsync(It.IsAny<Guid>(), It.IsAny<CompanyForUpdateDto>(), It.IsAny<bool>()))
                .Callback(() => { return; });
            return mock;
        }
    }
}
