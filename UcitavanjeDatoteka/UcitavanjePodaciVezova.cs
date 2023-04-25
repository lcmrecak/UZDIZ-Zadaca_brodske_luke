using lcmrecak__zadaca_3.Klase;
using lcmrecak__zadaca_3.Uzorci;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static lcmrecak__zadaca_3.Klase.Vez;

namespace lcmrecak__zadaca_3.UcitavanjeDatoteka
{
    public class UcitavanjePodaciVezova
    {
        public List<Vez> ucitajPodatkeVezova(string datoteka)
        {
            List<Vez> listaVez = new List<Vez>();
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

                            Vez vez = new VezBuilder(Int32.Parse(values[0].Trim()), values[1].Trim(), values[2].Trim(), Int32.Parse(values[3].Trim()),
                                      double.Parse(values[4].Trim()), double.Parse(values[5].Trim()), double.Parse(values[6].Trim()))
                                      .Build();

                            foreach (Vez vez1 in listaVez)
                            {
                                if (Int32.Parse(values[0]) == vez1.Id) postoji = true;
                            }

                            if (!postoji) listaVez.Add(vez);
                            if (postoji)
                            {
                                throw new Exception();
                            }

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
                Console.WriteLine("Pogresna datoteka podaci_vezova!");
                Environment.Exit(0);
            }
            
            return listaVez;
        }
    }
}
