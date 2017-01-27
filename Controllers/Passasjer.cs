using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TitanicWebService.Controllers
{
    public class Passasjer
    {
        public int PassasjerID { get; set;}
        public string Klasse { get; set; }
        public string Kjoenn { get; set; }
        public int? Alder { get; set; }
    }
}