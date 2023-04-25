using lcmrecak__zadaca_3.Builder;
using lcmrecak__zadaca_3.Klase;
using lcmrecak__zadaca_3.Uzorci;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace lcmrecak__zadaca_3.UcitavanjeDatoteka
{
    public class UcitavanjePodaciZahtjevRezervacija
    {
        public List<ZahtjevRezervacije> ucitajZahtjeviDatoteku(string datoteka)
        {
            List<ZahtjevRezervacije> listaZahtjevaRezervacije = new List<ZahtjevRezervacije>();
            SingletonGreske greska = SingletonGreske.getInstanceGreska();
            String razlogGreske = "Razlog: Greska";

            try
            {
                using (var reader = new StreamReader(@datoteka))
                {
                    reader.ReadLine();
                    while (!reader.EndOfStream)
                    {

                        var line = reader.ReadLine();
                        var values = line.Split(';');

                        try
                        {
                            string[] datumVrijeme = values[1].Split(" ");
                            DateOnly datum = DateOnly.ParseExact(datumVrijeme[0].Trim(), "dd.MM.yyyy.");
                            TimeOnly vrijeme = TimeOnly.ParseExact(datumVrijeme[1].Trim(), "HH:mm:ss");

                            Boolean postoji = false;

                            ZahtjevRezervacije zahtjevRezervacije = new ZahtjevRezervacijeBuilder(Int32.Parse(values[0]), datum, vrijeme, Int32.Parse(values[2]))
                                                                    .Build();

                            listaZahtjevaRezervacije.Add(zahtjevRezervacije);

                        }
                        catch (Exception)
                        {
                            greska.povecajGresku();
                            for (int i = 0; i < values.Length; i++)
                            {
                                Console.Write(values[i] + " ");
                            }
                            Console.Write(" " + razlogGreske);
                            Console.WriteLine("");
                        }
                    }
                }
            }
            catch (Exception)
            {
                greska.povecajGresku();
                Console.WriteLine("Pogresna datoteka zahtjeva rezervacija!");
            }
            
            return listaZahtjevaRezervacije;
        }
    }
}
