using CommonLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Api.Base.Business
{
    public class MassaDadosBusiness
    {
        public MassaDadosBusiness()
        {

        }

        public List<MassaDadosModel> GetMassaDados() 
        {
            try 
            {
                var fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/MassaDeDados.json");
                using (StreamReader r = new StreamReader(fullPath))
                {
                    string json = r.ReadToEnd();
                    var _massa = JsonConvert.DeserializeObject<List<MassaDadosModel>>(json);
                    return _massa;
                }
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }
    }
}