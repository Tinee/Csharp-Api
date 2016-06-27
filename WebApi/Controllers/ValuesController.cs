using System.Collections.Generic;
using System.Web.Http;
using Business_Logic.Data_Manipulations.Database_Entites;
using Contracts;

namespace WebApi.Controllers
{
    public class ValuesController : ApiController
    {
        public IEnumerable<Office> Get()
        {
           var x = new OfficeHandler();
            return x.Get();
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
