using lcmrecak__zadaca_3.Klase;
using lcmrecak__zadaca_3.Uzorci;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lcmrecak__zadaca_3.UcitavanjeDatoteka
{
    public class UcitavanjePodaciMol
    {
        public List<Mol> ucitajPodatkeMola(string datoteka)
        {
            List<Mol> listaMol = new List<Mol>();
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

                            Mol mol = new MolBuilder(Int32.Parse(values[0].Trim()), values[1].Trim())
                                          .Build();

                            foreach (Mol mol1 in listaMol)
                            {
                                if (Int32.Parse(values[0]) == mol1.IdMol) postoji = true;
                            }

                            if (!postoji) listaMol.Add(mol);
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
                Console.WriteLine("Pogresna datoteka mol!");
                Environment.Exit(0);
            }
            
            return listaMol;
        }
    }
}
