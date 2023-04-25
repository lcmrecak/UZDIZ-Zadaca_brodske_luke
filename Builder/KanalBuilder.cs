using lcmrecak__zadaca_3.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lcmrecak__zadaca_3.Uzorci
{
    public class KanalBuilder
    {
        private Kanal kanal = new Kanal();

        public KanalBuilder(int idKanal, int frekvencija, int maksimalanBroj, List<Brod> brod)
        {
            kanal.IdKanal = idKanal;
            kanal.Frekvencija = frekvencija;
            kanal.MaksimalanBroj = maksimalanBroj;
            kanal.spojeniBrodovi = brod;
        }

        public Kanal Build()
        {
            return kanal;
        }
    }
}
