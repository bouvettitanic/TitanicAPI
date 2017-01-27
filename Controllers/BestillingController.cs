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
        public PersonligSannsynlighet[] Put([FromBody]Bestilling bestilling)
        {
            try
            {
                PersonligSannsynlighet[] sann = new PersonligSannsynlighet[bestilling.Passasjerer.Count()];
                DataTable dt;
                int i = 0;
                foreach(Passasjer pass in bestilling.Passasjerer)
                {
                     SqlParameter[] par = new SqlParameter[6];
                    par[0] = new SqlParameter("Klasse", SQLUtil.HentNullableParameter(pass.Klasse));
                    par[1] = new SqlParameter("Kjoenn", SQLUtil.HentNullableParameter(pass.Kjoenn));
                    par[2] = new SqlParameter("Alder", SQLUtil.HentNullableParameter(pass.Alder));
                    par[3] = new SqlParameter("Avreisested", SQLUtil.HentNullableParameter(bestilling.Avreisested));
                    par[4] = new SqlParameter("AntallReisende", SQLUtil.HentNullableParameter(bestilling.AntallReisende));
                    par[5] = new SqlParameter("AntallBarn", SQLUtil.HentNullableParameter(bestilling.AntallBarn));

                    dt = SQLUtil.ExecuteStoredProcedure("dbo.HentSannsynlighet", par);
                    if (dt.Rows.Count() = 0)
                    {
                        throw new Exception("Ingen rader funnet");
                    }
                    DataRow dr = dt.Rows[0];
                    PersonligSannsynlighet sannp = new PersonligSannsynlighet();

                    sannp.PassasjerID = pass.PassasjerID;
                    sannp.Sannsynlighet = (decimal)dr["Sannsynlighet"];

                    sann[i] = sannp;
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

         public Bestilling Get()
        {
            try
            {
                Bestilling best = new Bestilling();
                best.Passasjerer = new Passasjer[1];

                Passasjer pass = new Passasjer();
                pass.PassasjerID = 1;
                pass.Alder = 34;
                pass.Kjoenn = "Kvinne";
                pass.Klasse = "Klasse1";

                best.Passasjerer[0] = pass;
                return best;
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
}
