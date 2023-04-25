using lcmrecak__zadaca_3.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lcmrecak_zadaca_3.Composite
{
    public class LukaComposite : ICompositeLuka
    {
        public List<ICompositeLuka> compositeLista = new List<ICompositeLuka>();

        private List<Luka> ListaLuka = new List<Luka>();

        public LukaComposite (List<Luka> listaLuka)
        {
            this.ListaLuka = listaLuka;
        }

        public void kreirajLuku()
        {
            Console.WriteLine("Kreirana luka.");

            foreach (var dioComposite in compositeLista)
            {
                dioComposite.kreirajLuku();
            }
        }
    }
}
