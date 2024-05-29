using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public sealed class CompanyNotFoundException : NotFoundException
    {
        public CompanyNotFoundException(Guid companyId)
            : base($"The company with id: {companyId} doesn't exist in the database.")
        {    
        }
    }
}
