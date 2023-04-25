using lcmrecak__zadaca_3.Klase;
using lcmrecak__zadaca_3.Uzorci;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace lcmrecak__zadaca_3.UcitavanjeDatoteka
{
    public class UcitavanjePodaciKanal
    {
        public List<Kanal> ucitajPodatkeKanala(string datoteka)
        {
            List<Kanal> listaKanal = new List<Kanal>();
            List<Brod> spojeniBrodovi = new List<Brod>();
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
                            Boolean postojiId = false;
                            Boolean postojiFrekvencija = false;

                            Kanal kanal = new KanalBuilder(Int32.Parse(values[0].Trim()), Int32.Parse(values[1].Trim()),
                                          Int32.Parse(values[2].Trim()), spojeniBrodovi)
                                          .Build();

                            foreach (Kanal kanal1 in listaKanal)
                            {
                                if (Int32.Parse(values[0]) == kanal1.IdKanal) postojiId = true;
                            }
                            foreach (Kanal kanal1 in listaKanal)
                            {
                                if (Int32.Parse(values[1]) == kanal1.Frekvencija) postojiFrekvencija = true;
                            }

                            if (!postojiId && !postojiFrekvencija) listaKanal.Add(kanal);
                            if (postojiId || postojiFrekvencija)
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
                Console.WriteLine("Pogresna datoteka kanala!");
                Environment.Exit(0);
            }
            
            return listaKanal;
        }
    }
}
