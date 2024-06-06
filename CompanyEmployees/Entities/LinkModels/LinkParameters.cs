using Microsoft.AspNetCore.Http;
using Shared.RequestFeature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.LinkModels
{
    public record LinkParameters(EmployeeParameters EmployeeParameters, HttpContext Context);
}
