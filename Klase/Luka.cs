using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lcmrecak__zadaca_3.Klase
{
    public class Luka
    {
        public string Naziv { get; set; }
        public double GpsSirina { get; set; }
        public double GpsVisina { get; set; }
        public int DubinaLuke { get; set; }
        public int UkupniBrojPutnickihVezova { get; set; }
        public int UkupniBrojPoslovnihVezova { get; set; }
        public int UkupniBrojOstalihVezova { get; set; }
        public DateTime VirtualnoVrijeme { get; set; }

        public Luka()
        {
        }
    }
}
