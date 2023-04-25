using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lcmrecak__zadaca_3.Klase
{
    public class Raspored
    {
        public int IdVez { get; set; }
        public int IdBrod { get; set; }
        public string DaniUTjednu { get; set; }
        public TimeOnly VrijemeOd { get; set; }
        public TimeOnly VrijemeDo { get; set; }

        public Raspored()
        {
        }
    }
}
