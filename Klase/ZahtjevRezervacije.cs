using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lcmrecak__zadaca_3.Klase
{
    public class ZahtjevRezervacije
    {
        public int IdBrod { get; set; }
        public DateOnly DatumOd { get; set; }
        public TimeOnly VrijemeOd { get; set; }
        public int TrajanjePrivezaUH { get; set; }
        public string Status { get; set; }

        public ZahtjevRezervacije()
        {
            
        }
    }
}
