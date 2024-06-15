using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Responses
{
    public abstract class ApiBadRequestResponse : ApiBaseResponse
    {
        public string Message {  get; set; }

        public ApiBadRequestResponse(string message)
            : base(false)
        {
            Message = message;
        }
    }
}
