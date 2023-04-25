using lcmrecak__zadaca_3.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lcmrecak__zadaca_3.Uzorci
{
    public class RasporedBuilder
    {
        public Raspored raspored = new Raspored();
        public RasporedBuilder(int idVez, int idBrod, string daniUTjednu, TimeOnly vrijemeOd, TimeOnly vrijemeDo)
        {
            raspored.IdVez = idVez;
            raspored.IdBrod = idBrod;
            raspored.DaniUTjednu = daniUTjednu;
            raspored.VrijemeOd = vrijemeOd;
            raspored.VrijemeDo = vrijemeDo;
        }

        public Raspored Build()
        {
            return raspored;
        }
    }
}
