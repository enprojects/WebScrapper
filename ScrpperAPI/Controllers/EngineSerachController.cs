using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ScrpperAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EngineSerachController : ControllerBase
    {
        

        private readonly IScrapperServiceManager _serv;

        public EngineSerachController(IScrapperServiceManager serv)
        {
            _serv = serv;
        }

        [HttpGet("get-results/{term}")]
        public async Task<IActionResult> GetSearchResult(string term)
        {

            var response = await _serv.GetQueryResult(term);
            return Ok(new {  Data = response});
        }
    }
}
