﻿using Entities.LinkModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployees.Presentation.Controllers
{
    [Route("api")]
    [ApiController]
    public class RootController: ControllerBase
    {
        private readonly LinkGenerator _linkGenerator;

        public RootController(LinkGenerator linkGenerator) => 
            _linkGenerator = linkGenerator;

        [HttpGet(Name = "GetRoot")]
        public IActionResult GetRoot([FromHeader(Name = "Accept")] string mediaType)
        {
            if(mediaType.Contains("application/vnd.ydong.apiroot"))
            {
                var list = new List<Link>{
                    new Link{
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(GetRoot), new{}),
                        Rel = "self",
                        Method = "GET"
                    },
                    new Link{
                        Href = _linkGenerator.GetUriByName(HttpContext, "GetCompanies", new{}),
                        Rel = "companies",
                        Method = "GET"
                    },
                    new Link{
                        Href = _linkGenerator.GetUriByName(HttpContext, "CreateCompany", new{}),
                        Rel = "create_company",
                        Method = "POST"
                    }
                 };

                return Ok(list);
            }

            return NoContent();
        }

    }
}
