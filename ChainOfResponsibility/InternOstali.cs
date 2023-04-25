using lcmrecak__zadaca_3.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lcmrecak__zadaca_3.ChainOfResponsibility
{
    public class InternOstali : IManagerZapisa
    {
        private IManagerZapisa managerZapisa;
        public List<Brod> ukloniBrod(List<Brod> listaBrodova, Brod brod)
        {
            List<Brod> azuriranaListaBrodova = listaBrodova;
            if (brod.Vrsta == "JA" || brod.Vrsta == "BR" || brod.Vrsta == "RO")
            {
                azuriranaListaBrodova.Remove(brod);
                Console.WriteLine("Intern uklonio brod: " + brod.Id + ": " + brod.Naziv);
                return azuriranaListaBrodova;
            }
            else
            {
                managerZapisa.ukloniBrod(listaBrodova, brod);
                return listaBrodova;
            }

        }

        public void setNadredeni(IManagerZapisa managerZapisa)
        {
            this.managerZapisa = managerZapisa;
        }
    }
}
