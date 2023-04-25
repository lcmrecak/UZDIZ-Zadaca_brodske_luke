using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lcmrecak__zadaca_3.Klase
{
    public class Vez
    {
        public int Id { get; set; }
        public string OznakaVeza { get; set; }
        public string Vrsta { get; set; }
        public int CijenaVezaPoSatu { get; set; }
        public double MaksimalnaDuljina { get; set; }
        public double MaksimalnaSirina { get; set; }
        public double MaksimalnaDubina { get; set; }

        public Vez()
        {

        }
    }
}
