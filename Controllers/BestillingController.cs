using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;

namespace TitanicWebService.Controllers
{
    public class BestillingController : ApiController
    {
        public decimal Get(string klasse, string kjoenn, int alder, string avreisesteg)
        {
            try
            {
                    MLWrapper wrap = new MLWrapper()
                    var sannsynlighet = wrap.GetTelthy(klasse, kjoenn, alder, avreisested).Lehtability.ToString("f2");
                    return sannsynlighet
                }
                return sann;
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

        // public Bestilling Get()
        //{
        //    try
        //    {
        //        Bestilling best = new Bestilling();
        //        best.Passasjerer = new Passasjer[1];

        //        Passasjer pass = new Passasjer();
        //        pass.PassasjerID = 1;
        //        pass.Alder = 34;
        //        pass.Kjoenn = "Kvinne";
        //        pass.Klasse = "Klasse1";
        //          MLWrapper wrap = new MLWrapper()
        //            var sannsynlighet = wrap.GetTelthy(pass.Klasse, pass.Kjoenn, Int.Parse(pass.Alder), pass.Avreisested).Lehtability.ToString("f2");
        //        pass.s
        //        best.Passasjerer[0] = pass;
        //        return best;
        //    }
        //    catch (Exception e)
        //    {
        //        var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
        //        {
        //            Content = new StringContent(e.Message),
        //            ReasonPhrase = "Server error."
        //        };
        //        throw new HttpResponseException(resp);
        //    }
        // }

    }
}
