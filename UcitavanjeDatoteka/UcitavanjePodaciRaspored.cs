using lcmrecak__zadaca_3.Klase;
using lcmrecak__zadaca_3.Uzorci;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static lcmrecak__zadaca_3.Klase.Raspored;

namespace lcmrecak__zadaca_3.UcitavanjeDatoteka
{
    public class UcitavanjePodaciRaspored
    {
        public List<Raspored> ucitajRasporedDatoteku(string datoteka)
        {

            List<Raspored> listaRaspored = new List<Raspored>();
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
                            Raspored raspored = new RasporedBuilder(Int32.Parse(values[0].Trim()), Int32.Parse(values[1].Trim()),
                                                values[2].Trim(), TimeOnly.ParseExact(values[3].Trim(), "H:mm"), TimeOnly.ParseExact(values[4].Trim(), "H:mm"))
                                                .Build();
                            if ((values[3].Length < 4 && values[4].Length < 4) ||
                                (values[3].Length > 5 && values[4].Length > 5) || values.Length > 5) throw new Exception();
                            listaRaspored.Add(raspored);
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
                Console.WriteLine("Pogresna datoteka raspored!");
                Environment.Exit(0);
            }
            
            return listaRaspored;
        }

    }
}
