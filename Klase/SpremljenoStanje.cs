using lcmrecak__zadaca_3.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lcmrecak_zadaca_3.Klase
{
    public class SpremljenoStanje
    {
        public string Naziv { get; set; }
        public DateTime VirtualnoVrijeme { get; set; }
        public List<Vez> Vezovi { get; set; }
        public List<Raspored> Raspored { get; set; }

        public SpremljenoStanje(string naziv, DateTime virtualnoVrijeme, List<Vez> vezovi, List<Raspored> raspored)
        {
            Naziv = naziv;
            VirtualnoVrijeme = virtualnoVrijeme;
            Vezovi = vezovi;
            Raspored = raspored;
        }

    }
}
