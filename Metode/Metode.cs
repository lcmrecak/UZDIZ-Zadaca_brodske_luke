using lcmrecak__zadaca_3.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lcmrecak__zadaca_3.Metode
{
    public class Metode
    {
        public void zaglavljeIspisStatusa(bool redniBroj)
        {
            if (redniBroj) Console.WriteLine(String.Format("|{0,4}|{1,15}|{2,15}|{3,15}|{4,15}|{5,15}|{6,15}|{7,15}|",
                    "RB  ", "ID             ", "OZNAKA         ", "CIJENA PO SATU ", "MAKS. DULJINA  ", "MAKS. SIRINA   ", "MAKS. DUBINA   ", "STATUS        "));
            else Console.WriteLine(String.Format("|{0,15}|{1,15}|{2,15}|{3,15}|{4,15}|{5,15}|{6,15}|",
                    "ID             ", "OZNAKA         ", "CIJENA PO SATU ", "MAKS. DULJINA  ", "MAKS. SIRINA   ", "MAKS. DUBINA   ", "STATUS        "));
        }

        public void tablicaIspisStatusaZauzet(Vez vez, bool redniBroj, int brojac)
        {
            string redak = String.Format("|{0,15}|{1,15}|{2,15}|{3,15}|{4,15}|{5,15}|{6,15}|",
                    vez.Id, vez.OznakaVeza, vez.CijenaVezaPoSatu, vez.MaksimalnaDuljina, vez.MaksimalnaSirina, vez.MaksimalnaDubina, "ZAUZET         ");
            if (redniBroj) redak = String.Format("|{0,4}|{1,15}|{2,15}|{3,15}|{4,15}|{5,15}|{6,15}|{7,15}|",
                brojac + ".", vez.Id, vez.OznakaVeza+"          ", vez.CijenaVezaPoSatu, vez.MaksimalnaDuljina, vez.MaksimalnaSirina, vez.MaksimalnaDubina, "ZAUZET");
            Console.WriteLine(redak);
        }

        public void tablicaIspiStatusaSlobodan(Vez vez, bool redniBroj, int brojac)
        {
            string redak = String.Format("|{0,15}|{1,15}|{2,15}|{3,15}|{4,15}|{5,15}|{6,15}|",
                    vez.Id, vez.OznakaVeza + "          ", vez.CijenaVezaPoSatu, vez.MaksimalnaDuljina, vez.MaksimalnaSirina, vez.MaksimalnaDubina, "SLOBODAN       ");
            if (redniBroj) redak = String.Format("|{0,4}|{1,15}|{2,15}|{3,15}|{4,15}|{5,15}|{6,15}|{7,15}|",
                brojac + ".", vez.Id, vez.OznakaVeza + "          ", vez.CijenaVezaPoSatu, vez.MaksimalnaDuljina, vez.MaksimalnaSirina, vez.MaksimalnaDubina, "SLOBODAN       ");
            Console.WriteLine(redak);
        }

        public void podnozjeIspisStatusa(bool redniBroj, int brojac)
        {
            if (redniBroj) Console.WriteLine(String.Format("|{0,16}{1,100}|", "PODNOZJE:       ", brojac));
            else Console.WriteLine(String.Format("|{0,11}{1,100}|", "PODNOZJE:  ", brojac));
        }

        public void tablicaSlobodniVezovi(bool redniBroj, List<Vez> slobodniVezovi, bool zaglavlje, bool podnozje, int brojac)
        {
            if (zaglavlje)
            {
                if (redniBroj) Console.WriteLine(String.Format("|{0,4}|{1,10}|{2,10}|{3,10}|{4,10}|", "RB  ", "VEZ ID    ", "OZNAKA    ", "VRSTA     ", "STATUS    "));
                else Console.WriteLine(String.Format("|{0,10}|{1,10}|{2,10}|{3,10}|", "VEZ ID    ", "OZNAKA    ", "VRSTA     ", "STATUS    "));
            }
            foreach (Vez vez in slobodniVezovi)
            {
                brojac++;
                if (redniBroj) Console.WriteLine(String.Format("|{0,4}|{1,10}|{2,10}|{3,10}|{4,10}|", brojac + ".", vez.Id, vez.OznakaVeza + "     ", vez.Vrsta + "        ", "SLOBODAN  "));
                else Console.WriteLine(String.Format("|{0,10}|{1,10}|{2,10}|{3,10}|", vez.Id, vez.OznakaVeza + "     ", vez.Vrsta + "        ", "SLOBODAN  "));
            }
            if (podnozje)
            {
                if (redniBroj) Console.WriteLine(String.Format("|{0,16}{1,32}|", "PODNOZJE:       ", brojac));
                else Console.WriteLine(String.Format("|{0,11}{1,32}|", "PODNOZJE:  ", brojac));
            }
        }
    }
}
