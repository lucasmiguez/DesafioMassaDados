using Api.TratamentoDados.Business;
using CommonLibrary.Business;
using CommonLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.TratamentoDados.Controllers
{
    public class MassaDadosController : ApiController
    {
        // GET api/values
        [AcceptVerbs("GET")]
        [Route("GetFullDados")]
        public HttpResponseMessage Get()
        {
            try
            {
                TratamentoDadosBusiness _tratamentoDados = new TratamentoDadosBusiness();
                var retorno = _tratamentoDados.GetListMassaDadosModelFull();
                return Request.CreateResponse(HttpStatusCode.OK, retorno);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [AcceptVerbs("GET")]
        [Route("Agrupamento/{id}")]
        public HttpResponseMessage Get(AgrupamentoEnum id)
        {
            try
            {
                TratamentoDadosBusiness _tratamentoDados = new TratamentoDadosBusiness();
                var retorno = _tratamentoDados.Agrupamento(id);
                return Request.CreateResponse(HttpStatusCode.OK, retorno);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
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
