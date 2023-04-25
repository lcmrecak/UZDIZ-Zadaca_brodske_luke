using lcmrecak__zadaca_3.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lcmrecak__zadaca_3.ChainOfResponsibility
{
    public interface IManagerZapisa
    {
        void setNadredeni(IManagerZapisa managerZapisa);

        List<Brod> ukloniBrod(List<Brod> listaBrodova, Brod brod);
    }
}
