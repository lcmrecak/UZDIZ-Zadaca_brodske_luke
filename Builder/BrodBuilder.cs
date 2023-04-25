using lcmrecak__zadaca_3.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lcmrecak__zadaca_3.Uzorci
{
    public class BrodBuilder
    {
        private Brod brod = new Brod();

        public BrodBuilder(int id, string oznakaBroda, string naziv,
                           string vrsta, double duljina, double sirina, double gaz,
                           double maksimalnaBrzina, int kapacitetPutnika, int kapacitetOsobnihVozila,
                           int kapacitetTereta)
        {
            brod.Id = id;
            brod.OznakaBroda = oznakaBroda;
            brod.Naziv = naziv;
            brod.Vrsta = vrsta;
            brod.Duljina = duljina;
            brod.Sirina = sirina;
            brod.Gaz = gaz;
            brod.MaksimalnaBrzina = maksimalnaBrzina;
            brod.KapacitetPutnika = kapacitetPutnika;
            brod.KapacitetOsobnihVozila = kapacitetOsobnihVozila;
            brod.KapacitetTereta = kapacitetTereta;
        }

        public Brod Build()
        {
            return brod;
        }
    }
}
