using lcmrecak__zadaca_3.Klase;
using lcmrecak__zadaca_3.Uzorci;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lcmrecak__zadaca_3.ChainOfResponsibility
{
    public class DirektorPutnicki : IManagerZapisa
    {
        private IManagerZapisa managerZapisa;
        public List<Brod> ukloniBrod(List<Brod> listaBrodova, Brod brod)
        {
            List<Brod> azuriranaListaBrodova = listaBrodova;
            if (brod.Vrsta == "TR" || brod.Vrsta == "KA" || brod.Vrsta == "KL" || brod.Vrsta == "KR")
            {
                azuriranaListaBrodova.Remove(brod);
                Console.WriteLine("Direktor uklonio brod: " + brod.Id + ": " + brod.Naziv);
                return azuriranaListaBrodova;
            }
            else
            {
                Console.WriteLine("Vrsta broda nije klasificirana");
                return listaBrodova;
            }

        }

        public void setNadredeni(IManagerZapisa managerZapisa)
        {
            this.managerZapisa = managerZapisa;
        }
    }
}
