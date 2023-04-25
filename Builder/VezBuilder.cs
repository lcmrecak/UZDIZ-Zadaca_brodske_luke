using lcmrecak__zadaca_3.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lcmrecak__zadaca_3.Uzorci
{
    public class VezBuilder
    {
        public Vez vez = new Vez();
        public VezBuilder(int id, string oznakaVeza, string vrsta, int cijenaVezaPoSatu, double maksimalnaDuljina, double maksimalnaSirina, double maksimalnaDubina)
        {
            vez.Id = id;
            vez.OznakaVeza = oznakaVeza;
            vez.Vrsta = vrsta;
            vez.CijenaVezaPoSatu = cijenaVezaPoSatu;
            vez.MaksimalnaDuljina = maksimalnaDuljina;
            vez.MaksimalnaSirina = maksimalnaSirina;
            vez.MaksimalnaDubina = maksimalnaDubina;
        }

        public Vez Build()
        {
            return vez;
        }
    }
}
