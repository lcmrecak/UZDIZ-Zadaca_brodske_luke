using lcmrecak__zadaca_3.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lcmrecak_zadaca_3.Composite
{
    public class VezoviList : ICompositeLuka
    {
        private List<Vez> ListaVezovi = new List<Vez>();

        public VezoviList(List<Vez> listaVezovi)
        {
            this.ListaVezovi = listaVezovi;
        }

        public void kreirajLuku()
        {
            Console.WriteLine("Vezovi dodani u luku.");
        }
    }
}
