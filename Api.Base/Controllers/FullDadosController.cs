using Api.Base.Business;
using CommonLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Base.Controllers
{
    public class FullDadosController : ApiController
    {
        // GET api/values
        [AcceptVerbs("GET")]
        [Route("GetFullDados")]
        public HttpResponseMessage Get()
        {
            try
            {
                MassaDadosBusiness _massaDadosBusiness = new MassaDadosBusiness();
                _massaDadosBusiness.GetMassaDados(); 
                return Request.CreateResponse(HttpStatusCode.OK, _massaDadosBusiness.GetMassaDados());
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
