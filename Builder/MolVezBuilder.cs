using lcmrecak__zadaca_3.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lcmrecak__zadaca_3.Uzorci
{
    public class MolVezBuilder
    {
        private MolVez molVez = new MolVez();

        public MolVezBuilder(int idMol, string idVezovi)
        {
            molVez.IdMol = idMol;
            molVez.IdVezovi = idVezovi;
        }

        public MolVez Build()
        {
            return molVez;
        }
    }
}
