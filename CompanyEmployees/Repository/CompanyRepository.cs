using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {            
        }

        public IEnumerable<Company> GetAllCompanies(bool trackChange) =>
            FindAll(trackChange)
            .OrderBy(c => c.Name)
            .ToList();

        public Company GetCompany(Guid companyId, bool trackChange) =>
            FindByCondition(c => c.Id.Equals(companyId), trackChange)
            .SingleOrDefault();
    }
}
