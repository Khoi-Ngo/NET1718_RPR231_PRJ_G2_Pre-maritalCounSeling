using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pre_maritalCounSeling.BAL.Auth;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pre_maritalCounSeling.API.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SampleControllers : ControllerBase
    {
        // GET: api/<SampleControllers>
        [HttpGet]
        [AuthenticateOnly]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
