using lcmrecak__zadaca_3.Klase;
using lcmrecak__zadaca_3.Uzorci;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static lcmrecak__zadaca_3.Klase.Luka;

namespace lcmrecak__zadaca_3.UcitavanjeDatoteka
{
    public class UcitavanjePodaciLuke
    {
        public List<Luka> ucitajPodatkeLuke(string datoteka)
        {
            List<Luka> listaLuka = new List<Luka>();
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
                            Luka luka = new LukaBuilder(values[0].Trim(), double.Parse(values[1].Trim()), double.Parse(values[2].Trim()),
                                Int32.Parse(values[3].Trim()), Int32.Parse(values[4].Trim()), Int32.Parse(values[5].Trim()),
                                Int32.Parse(values[6].Trim()), DateTime.ParseExact(values[7].Trim(), "dd.MM.yyyy. HH:mm:ss", null))
                                .Build();

                            listaLuka.Add(luka);

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
                Console.WriteLine("Pogresna datoteka luke!");
                Environment.Exit(0);
            }
            
            return listaLuka;
        }
    }
}
