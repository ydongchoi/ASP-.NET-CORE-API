using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Responses
{
    public abstract class ApiBaseResponse
    {
        public bool Success { get; set; }

        protected ApiBaseResponse(bool success) => Success = success;
    }
}
