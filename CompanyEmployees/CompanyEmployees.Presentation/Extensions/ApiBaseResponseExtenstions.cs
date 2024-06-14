using Entities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployees.Presentation.Extensions
{
    public static class ApiBaseResponseExtenstions
    {
        public static TResultType GetResult<TResultType>(this ApiBaseResponse apiBaseResponse) =>
            ((ApiOkResponse<TResultType>)apiBaseResponse).Result;
    }
}
