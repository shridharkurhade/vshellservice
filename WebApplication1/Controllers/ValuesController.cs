using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace WebApplication1.Controllers
{
    public class App
    {
        public string id { get; set; }
        [Required]
        public string url { get; set; }
        [Required]
        public string name { get; set; }
        public string icon { get; set; }
        public bool isFavorite { get; set; }
    }

    public class response
    {
        HttpStatusCode code { get; set; }

        public string status { get; set; }

        public object result { get; set; }

        public object error { get; set; }

        public response(HttpStatusCode c, object res, object err)
        {
            code = c;
            result = res;
            error = err;
            status = ((int)c).ToString();
        }
    }

    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<App> Get()
        {
            return new List<App>()
            {
                new App { name="SGMD", id="SGMD", url="/sgmd/index.html", isFavorite=false, icon="SGMD ICON"},
                new App { name="FAR", id="FAR", url="/far/index.html", isFavorite=true, icon="FAR ICON"}
            };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]App app)
        {
            if (ModelState.IsValid)
            {
                return Created("http://apps/", new response(HttpStatusCode.Created, app, null));
            }
            else
            {
                return BadRequest(new response(HttpStatusCode.BadRequest,null,new SerializableError(ModelState)));

            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
