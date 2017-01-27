using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TitanicWebService.Controllers
{
    public class Bestilling
    {
        public Passasjer[] Passasjerer { get; set; }
        public string Avreisested { get; set; }
        public int? AntallReisende { get; set;}
        public int? AntallBarn { get; set; }
    }
}