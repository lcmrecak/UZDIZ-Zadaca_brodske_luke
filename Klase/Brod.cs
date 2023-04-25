using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lcmrecak__zadaca_3.Klase
{
    public class Brod
    {
        public int Id { get; set; }
        public string OznakaBroda { get; set; }
        public string Naziv { get; set; }
        public string Vrsta { get; set; }
        public double Duljina { get; set; }
        public double Sirina { get; set; }
        public double Gaz { get; set; }
        public double MaksimalnaBrzina { get; set; }
        public int KapacitetPutnika { get; set; }
        public int KapacitetOsobnihVozila { get; set; }
        public int KapacitetTereta { get; set; }

        public Brod()
        {

        }
    }
}
