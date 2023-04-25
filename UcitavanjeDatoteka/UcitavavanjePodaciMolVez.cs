using lcmrecak__zadaca_3.Klase;
using lcmrecak__zadaca_3.Uzorci;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lcmrecak__zadaca_3.UcitavanjeDatoteka
{
    public class UcitavavanjePodaciMolVez
    {
        public List<MolVez> ucitajPodatkeMolaVeza(string datoteka, List<Vez> listaVezova)
        {
            List<MolVez> listaMolVez = new List<MolVez>();
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
                            Boolean postoji = false;

                            string[] vezovi = values[1].Split(',');

                            string postojeciVezovi = provjeraVezova(vezovi, listaVezova);

                            MolVez molVez = new MolVezBuilder(Int32.Parse(values[0].Trim()), postojeciVezovi)
                                          .Build();

                            listaMolVez.Add(molVez);
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
                Console.WriteLine("Pogresna datoteka mol_vez!");
                Environment.Exit(0);
            }
            
            return listaMolVez;
        }

        private string provjeraVezova(string[] vezovi, List<Vez> listaVezova)
        {
            string provjereniVezovi = "";
            bool postoji = false;

            for (int i=0; i < vezovi.Length; i++)
            {
                postoji = false;
                foreach (Vez vez in listaVezova)
                {
                    if (Int32.Parse(vezovi[i]) == vez.Id) postoji = true;
                }

                if (postoji) provjereniVezovi = String.Join(",", vezovi[i]);
                else
                {
                    SingletonGreske greska = SingletonGreske.getInstanceGreska();
                    greska.povecajGresku();
                    Console.Write(" " + "Ne postoji vez " + vezovi[i]);
                    Console.WriteLine("");
                }
            }

            return provjereniVezovi;
        }
    }
}
