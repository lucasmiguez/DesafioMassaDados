using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Business
{
    public class RequestBusiness
    {
        public string RunRequest(string URLGetleads)
        {
            try
            {
                HttpWebRequest request = HttpWebRequest.CreateHttp(URLGetleads);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.PreAuthenticate = true;
                var Json = string.Empty;
                WebResponse response = request.GetResponse();
                Json = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return Json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
