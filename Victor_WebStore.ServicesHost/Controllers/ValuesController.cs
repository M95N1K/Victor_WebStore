using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Victor_WebStore.ServicesHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static readonly List<string> _Values = Enumerable.Range(1, 10)
            .Select(i => $"Value {i}")
            .ToList();
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get() => _Values;

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id) 
        {
            if (id < 0)
                return BadRequest();
            if (id > _Values.Count)
                return NotFound();

            return _Values[id];
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            _Values.Add(value);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] string value)
        {
            if (id < 0)
                return BadRequest();
            if (id > _Values.Count)
                return NotFound();

            _Values[id] = value;

            return Ok();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id < 0)
                return BadRequest();
            if (id > _Values.Count)
                return NotFound();

            _Values.RemoveAt(id);

            return Ok();
        }
    }
}
