using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TestML
{
    class Program
    {




        static void Main(string[] args)
        {
            MLWrapper wrap = new MLWrapper();
            Result res = wrap.GetTelthy("1", "female", 45, "C");
            Console.WriteLine(res.Lehtability);
            Console.ReadLine();
        }


    }

}
