using lcmrecak__zadaca_3.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lcmrecak__zadaca_3.Uzorci
{
    public class MolBuilder
    {
        private Mol mol = new Mol();

        public MolBuilder(int idMol, string naziv)
        {
            mol.IdMol = idMol;
            mol.Naziv = naziv;
        }

        public Mol Build()
        {
            return mol;
        }
    }
}
