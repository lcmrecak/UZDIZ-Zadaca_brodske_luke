using lcmrecak__zadaca_3.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lcmrecak__zadaca_3.TemplateMethod
{
    public class Brojanje : IVisitor
    {
        public void Visit(ZauzetiVezovi zauzetiVezoviVrstaTemplate, List<Vez> listaVezova, List<Raspored> listaRasporeda, DateOnly datum, TimeOnly vrijeme, bool redniBroj)
        {
            int brojac = 0;
            bool zauzet = false;
            string postojeci = "";
            foreach (Vez vez in listaVezova)
            {
                zauzet = false;
                foreach (Raspored raspored in listaRasporeda)
                {
                    if (vez.Id == raspored.IdVez && vez.Vrsta == zauzetiVezoviVrstaTemplate.Vrsta && raspored.DaniUTjednu.Contains(((int)datum.DayOfWeek).ToString())
                        && vrijeme.IsBetween(raspored.VrijemeOd, raspored.VrijemeDo) && !postojeci.Contains(vez.Id.ToString()))
                    {
                        postojeci = String.Join(",", vez.Id.ToString());
                        zauzet = true;
                    }
                }
                if (zauzet) brojac++;
            }

            if (zauzetiVezoviVrstaTemplate.Vrsta == "PU")
            {
                if (redniBroj)
                {
                    Console.WriteLine(String.Format("|{0,4}|{1,10}|{2,10}|", "1.", zauzetiVezoviVrstaTemplate.Vrsta + "        ", brojac));
                }
                else Console.WriteLine(String.Format("|{0,10}|{1,10}|", zauzetiVezoviVrstaTemplate.Vrsta + "        ", brojac));
            }
            else if (zauzetiVezoviVrstaTemplate.Vrsta == "PO")
            {
                if (redniBroj)
                {
                    Console.WriteLine(String.Format("|{0,4}|{1,10}|{2,10}|", "2.", zauzetiVezoviVrstaTemplate.Vrsta + "        ", brojac));
                }
                else Console.WriteLine(String.Format("|{0,10}|{1,10}|", zauzetiVezoviVrstaTemplate.Vrsta + "        ", brojac));
            }
            else if (zauzetiVezoviVrstaTemplate.Vrsta == "OS")
            {
                if (redniBroj)
                {
                    Console.WriteLine(String.Format("|{0,4}|{1,10}|{2,10}|", "3.", zauzetiVezoviVrstaTemplate.Vrsta + "        ", brojac));
                }
                else Console.WriteLine(String.Format("|{0,10}|{1,10}|", zauzetiVezoviVrstaTemplate.Vrsta + "        ", brojac));
            }
            Console.WriteLine("");
        }
    }
}
