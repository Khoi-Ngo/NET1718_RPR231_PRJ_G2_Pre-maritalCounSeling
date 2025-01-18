using Microsoft.AspNetCore.Mvc;
using Pre_maritalCounSeling.BAL.Auth;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pre_maritalCounSeling.API.Controllers
{
    [Route("api/[controller]")]
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

        // GET api/<SampleControllers>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SampleControllers>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SampleControllers>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SampleControllers>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
