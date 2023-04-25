using lcmrecak__zadaca_3.Klase;
using lcmrecak__zadaca_3.Uzorci;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static lcmrecak__zadaca_3.Klase.Brod;

namespace lcmrecak__zadaca_3.UcitavanjeDatoteka
{
    public class UcitavanjePodaciBrodovi
    {
        public List<Brod> ucitajBrodDatoteku(string datoteka)
        {
            
            List<Brod> listaBrod = new List<Brod>();
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

                            if (values[4].Equals("0") || values[5].Equals("0") || values[6].Equals("0") || values[7].Equals("0")) throw new Exception();

                            Brod brod = new BrodBuilder(Int32.Parse(values[0].Trim()), values[1].Trim(), values[2].Trim(),
                                        values[3].Trim(), double.Parse(values[4].Trim()), double.Parse(values[5].Trim()), double.Parse(values[6].Trim()),
                                        double.Parse(values[7].Trim()), Int32.Parse(values[8].Trim()), Int32.Parse(values[9].Trim()), Int32.Parse(values[10].Trim()))
                                        .Build();

                            foreach (Brod brod1 in listaBrod)
                            {
                                if (Int32.Parse(values[0]) == brod1.Id) postoji = true;
                            }

                            if (!postoji) listaBrod.Add(brod);
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
                Console.WriteLine("Pogresna datoteka brod!");
                Environment.Exit(0);
            }
            
            return listaBrod;
        }
    }
}
