using lcmrecak__zadaca_3.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lcmrecak__zadaca_3.Uzorci
{
    public class LukaBuilder
    {
        public Luka luka = new Luka();

        public LukaBuilder(string naziv, double gpsSirina, double gpsVisina, int dubinaLuke, int ukupniBrojPutnickihVezova, int ukupniBrojPoslovnihVezova, int ukupniBrojOstalihVezova, DateTime virtualnoVrijeme)
        {
            luka.Naziv = naziv;
            luka.GpsSirina = gpsSirina;
            luka.GpsVisina = gpsVisina;
            luka.DubinaLuke = dubinaLuke;
            luka.UkupniBrojPutnickihVezova = ukupniBrojPutnickihVezova;
            luka.UkupniBrojPoslovnihVezova = ukupniBrojPoslovnihVezova;
            luka.UkupniBrojOstalihVezova = ukupniBrojOstalihVezova;
            luka.VirtualnoVrijeme = virtualnoVrijeme;
        }

        public Luka Build()
        {
            return luka;
        }
    }
}
