using lcmrecak__zadaca_3.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lcmrecak__zadaca_3.TemplateMethod
{
    public class ZauzetiVezovi : IAcceptVisitor
    {
        virtual public string Vrsta { get; set; } = "";
        public void Accept(IVisitor visitor, List<Vez> listaVezova, List<Raspored> listaRasporeda, DateOnly datum, TimeOnly vrijeme, bool redniBroj)
        {
            visitor.Visit(this, listaVezova, listaRasporeda, datum, vrijeme, redniBroj);
        }
    }
}
