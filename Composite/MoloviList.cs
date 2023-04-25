using lcmrecak__zadaca_3.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lcmrecak_zadaca_3.Composite
{
    public class MoloviList : ICompositeLuka
    {
        private List<Mol> ListaMolovi = new List<Mol>();

        public MoloviList(List<Mol> listaMolovi)
        {
            this.ListaMolovi = listaMolovi;
        }

        public void kreirajLuku()
        {
            Console.WriteLine("Molovi dodani u luku.");
        }
    }
}
