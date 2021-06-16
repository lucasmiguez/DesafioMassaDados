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
    }
}
