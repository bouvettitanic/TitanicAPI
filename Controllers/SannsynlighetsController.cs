using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Titanic_Web_Api.Controllers
{
    public class SannsynlighetsController : ApiController
    {
        [HttpGet]
        [Route("api/sant")]
        public dynamic Get(string klasse, string kjoenn, int alder, string avreisested)
        {
            try
            {
                MLWrapper wrap = new MLWrapper();
                var sannsynlighet = wrap.GetTelthy(klasse, kjoenn, alder, avreisested).Lehtability.ToString("f2");
                return new { probabilityForGruesomeAndAwefulDeath = sannsynlighet };
            }
            catch (Exception e)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(e.Message),
                    ReasonPhrase = "Server error."
                };
                throw new HttpResponseException(resp);
            }
        }
    }
    public class AnAnswer
    {
        public string Answer { get; set; }
    }
}
