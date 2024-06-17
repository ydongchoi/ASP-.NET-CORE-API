using Contracts;
using Entities.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Mocks
{
    internal class MockICompanyRepository
    {
        public static Mock<ICompanyRepository> GetMock()
        {
            var mock = new Mock<ICompanyRepository>();

            // Database Double
            var companies = new List<Company>()
            {
                new Company()
                {
                    Id = Guid.Parse("43585C00-2346-4FEA-AA74-08DC81A68D90"),
                    Name = "Electronics Solutions Ltd",
                    Address = "312 Deliver Street, F 234",
                    Country = "USA"
                },
                new Company()
                {
                    Id = Guid.Parse("02946162-6D18-4AED-45D6-08DC81B4C1C5"),
                    Name = "Branding Ltd",
                    Address = "255 Main Street, k 334",
                    Country = "USA"
                }
            };

            // Setup the mock
            mock.Setup(m => m.GetAllCompaniesAsync(It.IsAny<bool>()))
                .ReturnsAsync(() => companies);

            mock.Setup(m => m.GetCompanyAsync(It.IsAny<Guid>(), It.IsAny<bool>()))
                .ReturnsAsync((Guid id, bool trackChanges) =>
                {
                    return companies.FirstOrDefault(c => c.Id == id);
                });

            mock.Setup(m => m.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>(), It.IsAny<bool>()))
                .ReturnsAsync((IEnumerable<Guid> ids, bool trackChanges) =>
                {
                    List<Company> companiesWithId = new List<Company>();

                    foreach(var id in ids)
                    {
                        companiesWithId.Add(companies.FirstOrDefault(c => c.Id == id));
                    }

                    return companiesWithId;
                });

            mock.Setup(m => m.CreateCompany(It.IsAny<Company>()))
                .Callback(() => { return; });

            mock.Setup(m => m.DeleteCompany(It.IsAny<Company>()))
                .Callback(() => { return; });


            return mock;
        }
    }
}
