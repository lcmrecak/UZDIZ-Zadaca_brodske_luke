using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lcmrecak__zadaca_3.Klase
{
    public class Kanal
    {
        public int IdKanal { get; set; }
        public int Frekvencija { get; set; }
        public int MaksimalanBroj { get; set; }
        public List<Brod> spojeniBrodovi { get; set; }

        public Kanal()
        {

        }
    }
}
