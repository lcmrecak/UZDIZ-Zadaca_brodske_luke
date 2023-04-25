using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Timers;
using lcmrecak__zadaca_3;
using lcmrecak__zadaca_3.Builder;
using lcmrecak__zadaca_3.ChainOfResponsibility;
using lcmrecak__zadaca_3.Klase;
using lcmrecak__zadaca_3.Metode;
using lcmrecak__zadaca_3.TemplateMethod;
using lcmrecak__zadaca_3.UcitavanjeDatoteka;
using lcmrecak__zadaca_3.Uzorci;
using lcmrecak_zadaca_3.Composite;
using lcmrecak_zadaca_3.Klase;
using Timer = System.Timers.Timer;

public class BrodskaLukaMain
{
    public static DateTime virtualnoVrijeme = DateTime.MinValue;

    public static UcitavanjePodaciLuke ucitajLuke = new UcitavanjePodaciLuke();
    public static UcitavanjePodaciVezova ucitajVez = new UcitavanjePodaciVezova();
    public static UcitavanjePodaciMol ucitajMol = new UcitavanjePodaciMol();
    public static UcitavavanjePodaciMolVez ucitajMolVez = new UcitavavanjePodaciMolVez();
    public static UcitavanjePodaciBrodovi ucitajBrod = new UcitavanjePodaciBrodovi();

    public static UcitavanjePodaciRaspored ucitajRaspored = new UcitavanjePodaciRaspored();
    public static UcitavanjePodaciZahtjevRezervacija ucitajZahtjevRezervacija = new UcitavanjePodaciZahtjevRezervacija();
    public static UcitavanjePodaciKanal ucitajKanal = new UcitavanjePodaciKanal();
    public static Metode metode = new Metode();

    public static List<Luka> listaLuka = new List<Luka>();
    public static List<Brod> listaBrodova = new List<Brod>();
    public static List<Vez> listaVezova = new List<Vez>();
    public static List<Raspored> listaRaspored = new List<Raspored>();
    public static List<Vez> zauzetiVezovi = new List<Vez>();
    public static List<ZahtjevRezervacije> listazahtjevRezervacija = new List<ZahtjevRezervacije>();
    public static List<ZahtjevRezervacije> odobreniZahtjevi = new List<ZahtjevRezervacije>();
    public static List<Kanal> listaKanala = new List<Kanal>();
    public static List<Mol> listaMolova = new List<Mol>();
    public static List<MolVez> listaMolVez = new List<MolVez>();
    public static List<SpremljenoStanje> listaSpremljenoStanje = new List<SpremljenoStanje>();

    public static SingletonGreske greska = SingletonGreske.getInstanceGreska();

    public static VezoviList listVezovi = new VezoviList(listaVezova);
    public static MoloviList listMolovi = new MoloviList(listaMolova);
    public static LukaComposite compositeLuka = new LukaComposite(listaLuka);

    public static bool zaglavlje = false;
    public static bool podnozje = false;
    public static bool redniBroj = false;
    public static int brojacIzmeduDana = 0;
    private static void Main(string[] args)
    {
        Timer timer;

        provjeraArgumenata(args);

        for (int i = 0; i < args.Length; i= i+2)
        {
            if (args[i] =="-l") listaLuka = ucitajLuke.ucitajPodatkeLuke(args[i+1]);
            if (args[i] =="-v") listaVezova = ucitajVez.ucitajPodatkeVezova(args[i+1]);
            if (args[i] == "-b") listaBrodova = ucitajBrod.ucitajBrodDatoteku(args[i+1]);
            if (args[i] =="-r") listaRaspored = ucitajRaspored.ucitajRasporedDatoteku(args[i+1]);
            if (args[i] == "-m") listaMolova = ucitajMol.ucitajPodatkeMola(args[i+1]);
            if (args[i] == "-k") listaKanala = ucitajKanal.ucitajPodatkeKanala(args[i+1]);
            if (args[i] == "-mv") listaMolVez = ucitajMolVez.ucitajPodatkeMolaVeza(args[i + 1], listaVezova);
        }

        compositeLuka.compositeLista.Add(listVezovi);
        compositeLuka.compositeLista.Add(listMolovi);

        Console.WriteLine("");
        Console.WriteLine("--COMPOSITE--"); //ISPIS KOJI SLUZI KAO PRIKAZ DJELOVANJA COMPOSITE UZORKA
        compositeLuka.kreirajLuku();

        virtualnoVrijeme = listaLuka[0].VirtualnoVrijeme;
        timer = new Timer(1000);
        timer.Elapsed += povecajVirtualniSat;
        timer.Enabled = true;
        Console.WriteLine("");
        Console.WriteLine("");

        Izbornik();
    }

    public static void provjeraArgumenata(string[] args)
    {
        List<string> komande = new List<string>();

        Console.WriteLine("Argument duljina: " + args.Length);

        for(int i=0; i < args.Length; i++)
        {
            if (i==0 || i % 2 == 0) komande.Add(args[i]);
        }
        if (args.Length==12 && (!komande.Contains("-l") || !komande.Contains("-v") || !komande.Contains("-m") || 
            !komande.Contains("-k") || !komande.Contains("-mv") || !komande.Contains("-b")))
        {
            greska.povecajGresku();
            Console.WriteLine("Nedostaje obavezna komanda");
            Environment.Exit(0);
        }
        else if (args.Length == 14 && (!komande.Contains("-l") || !komande.Contains("-v") || !komande.Contains("-m") ||
            !komande.Contains("-k") || !komande.Contains("-mv") || !komande.Contains("-b") || !komande.Contains("-r")))
        {
            greska.povecajGresku();
            Console.WriteLine("Nedostaje obavezna komanda");
            Environment.Exit(0);
        }
    }

    public static void povecajVirtualniSat (Object source, ElapsedEventArgs e)
    {
        virtualnoVrijeme = virtualnoVrijeme.AddSeconds(1);
    }

    public static bool ProvjeraUcitanostiDatotekeRaspored(bool ucitano)
    {
        if (listaRaspored.Count == 0)
        {
            Console.WriteLine("Nije ucitana datoteka rasporeda, unesite naziv datoteke:");
            string datotekaRaspored = Console.ReadLine();

            try
            {
                listaRaspored = ucitajRaspored.ucitajRasporedDatoteku(datotekaRaspored);
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Pogresna datoteka!");
                return false;
            }
        }
        else return true;
    }

    public static void Izbornik()
    {
        string izbor;

        do
        {
            string formatUnosaVV = "^VR ([0-2][0-9]|(3)[0-1])(\\.)(((0)[0-9])|((1)[0-2]))(\\.)\\d{4}(\\.) (?:2[0-3]|[01][0-9]):[0-5][0-9]:[0-5][0-9]$";
            string formatUnosaZiliS = "^V [A-Z]{2} (S|Z) ([0-2][0-9]|(3)[0-1])(\\.)(((0)[0-9])|((1)[0-2]))(\\.)\\d{4}(\\.) " +
                "(?:2[0-3]|[01][0-9]):[0-5][0-9]:[0-5][0-9] ([0-2][0-9]|(3)[0-1])(\\.)(((0)[0-9])|((1)[0-2]))(\\.)\\d{4}(\\.) " +
                "(?:2[0-3]|[01][0-9]):[0-5][0-9]:[0-5][0-9]$";

            Console.WriteLine("OPIS                                            | KOMANDA");
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("Ispis statusa vezova                            | I");
            Console.WriteLine("Promjena virtualnog vremena                     | VR dd.mm.yyyy. hh:mm:ss");
            Console.WriteLine("Ispis vezova za trazeni status i vrijeme        | V [vrsta_veza] [status] [datum_vrijeme_od] [datum_vrijeme_do]");
            Console.WriteLine("Ucitavanje datoteke zahtjeva za rezervacijom    | UR [naziv_datoteke] *ne radi do kraja");
            Console.WriteLine("Spajanje broda na kanal                         | F [ID_brod] [kanal] [Q]");
            Console.WriteLine("Zahtjev za privez                               | ZD [ID_brod] *ne radi do kraja"); 
            Console.WriteLine("Uredenje ispisa unutar tablica                  | T [Z | P | RB] ");
            Console.WriteLine("Ispis ukupnog broja zauzetih vezove po vrstama  | ZA [vrijeme]");
            Console.WriteLine("Uklanjanje broda CoR                            | CR B [ID_brod]");
            Console.WriteLine("Spremanje trenutnog stanja vezova i VV          | SPS ''[naziv]''");
            Console.WriteLine("Postavljanje spremljenog stanja vezova i VV     | VPS ''[naziv]''");
            Console.WriteLine("--------------------------------------------------------");
            Console.Write("UNOS: ");
            izbor = Console.ReadLine();
            string[] izborArgumenti = izbor.Trim().Split(" ");
            string[] izborArgumenti2 = izbor.Trim().Split('"');

            Match matcherFormatVV = Regex.Match(izbor, formatUnosaVV);
            Match matcherFormatZiliS = Regex.Match(izbor, formatUnosaZiliS);

            izbori(izbor, izborArgumenti,izborArgumenti2, matcherFormatVV, matcherFormatZiliS);
            Console.WriteLine("");
            
        }while (izbor != "Q");
    }

    public static void izbori(string izbor, string[] izborArgumenti, string[] izborArgumenti2, Match matcherFormatVV, Match matcherFormatZiliS)
    {
        if (izbor == "I") IspisStatusaVezova();
        else if (matcherFormatVV.Success)
        {
            promjenaVirtualnogVremena(izbor);
        }
        else if (matcherFormatZiliS.Success)
        {
            ispisZauzetihIliSlobodnihVezova(izbor);
        }
        else if (izborArgumenti[0] == "UR" && izborArgumenti.Length == 2 && izborArgumenti[1] != "")
        {
            listazahtjevRezervacija = ucitajZahtjevRezervacija.ucitajZahtjeviDatoteku(izborArgumenti[1]);
            automatskoRezerviranje(listazahtjevRezervacija, listaRaspored, listaBrodova);

            Console.WriteLine("");
        }
        else if (izborArgumenti[0] == "ZD")
        {
            zahtjevZaPrivez(Int32.Parse(izborArgumenti[1]));
            Console.WriteLine("");
        }
        else if (izborArgumenti[0] == "F" && (izborArgumenti.Length == 3 || izborArgumenti.Length == 4))
        {
            if (izborArgumenti.Length == 3) spojiBrodKanal(izborArgumenti[1], izborArgumenti[2]);
            else if (izborArgumenti.Length == 4 && izborArgumenti[3] == "Q") odspojiBrodKanal(izborArgumenti[1], izborArgumenti[2]);
            else
            {
                greska.povecajGresku();
                Console.WriteLine("Pogrešan oblik naredbe");
            }

            Console.WriteLine("");
        }
        else if (izborArgumenti[0] == "T")
        {
            postavkeTablice(izborArgumenti);
        }
        else if (izborArgumenti[0] == "ZA")
        {
            ispisZauzetihVezovaUZadanoVrijeme(izborArgumenti);
        }
        else if (izborArgumenti[0] == "CR" && izborArgumenti[1] == "B" && izborArgumenti.Length == 3)
        {
            ukloniBrod(izborArgumenti);
        }
        else if (izborArgumenti[0] == "SPS" && izborArgumenti[1].StartsWith('"') && izborArgumenti2.Length==3) spremiStanje(izborArgumenti2[1]);
        else if (izborArgumenti[0] == "VPS" && izborArgumenti[1].StartsWith('"') && izborArgumenti2.Length == 3) postaviStanje(izborArgumenti2[1]);
        else if (izbor == "Q") Console.WriteLine("Zavrsen program.");
        else
        {
            greska.povecajGresku();
            Console.WriteLine("Pogresan oblik naredbe, pokusajte ponovo!");
            Console.WriteLine("");
        }
    }

    public static void IspisStatusaVezova()
    {
        bool ucitano = true;
        do
        {
            ucitano = ProvjeraUcitanostiDatotekeRaspored(ucitano);
        } while (ucitano == false);

        int brojac = 0;
        DateTime trenutnoVirtualnoVrijeme = virtualnoVrijeme;
        int trenutniDan = (int)trenutnoVirtualnoVrijeme.DayOfWeek;
        TimeOnly trenutnoVrijeme = TimeOnly.FromDateTime(trenutnoVirtualnoVrijeme);

        Console.WriteLine("Trenutno virutalno vrijeme: " + trenutnoVirtualnoVrijeme.ToString());
        Console.WriteLine("");
        if (zaglavlje)
        {
            metode.zaglavljeIspisStatusa(redniBroj);
        }

        foreach (Vez vez in listaVezova)
        {
            Boolean pogodak = false;

            foreach (Raspored raspored in listaRaspored)
            {
                
                if (vez.Id.Equals(raspored.IdVez) && raspored.DaniUTjednu.Contains(trenutniDan.ToString())
                    && trenutnoVrijeme.IsBetween(raspored.VrijemeOd, raspored.VrijemeDo))
                {
                    pogodak = true;
                }
            }

            if (pogodak)
            {
                brojac++;
                metode.tablicaIspisStatusaZauzet(vez, redniBroj, brojac);
            }
            else
            {
                brojac++;
                metode.tablicaIspiStatusaSlobodan(vez, redniBroj, brojac);  
            }
            pogodak = false;
        }
        if (podnozje)
        {
            metode.podnozjeIspisStatusa(redniBroj, brojac);
        }
    }
    public static void promjenaVirtualnogVremena(string izbor)
    {
        DateTime trenutnoVirtualnoVrijeme = virtualnoVrijeme;
        Console.WriteLine("Trenutno virutalno vrijeme: " + trenutnoVirtualnoVrijeme.ToString());
        Console.WriteLine("");

        string novoVV = "";
        var novoVVPolje = izbor.Split(' ');
        
        novoVV = novoVVPolje[1] + " " + novoVVPolje[2];

        virtualnoVrijeme = DateTime.ParseExact(novoVV, "dd.MM.yyyy. HH:mm:ss", null);
        Console.WriteLine("Novo virtualno vrijeme: " + virtualnoVrijeme.ToString());
        Console.WriteLine("");
    }

    public static void ispisZauzetihIliSlobodnihVezova(string izbor) 
    {
        DateTime trenutnoVirtualnoVrijeme = virtualnoVrijeme;
        Console.WriteLine("Trenutno virutalno vrijeme: " + trenutnoVirtualnoVrijeme.ToString());
        Console.WriteLine("");
        bool ucitano = true;
        do
        {
            ucitano = ProvjeraUcitanostiDatotekeRaspored(ucitano);
        } while (ucitano == false);

        var splitPolje = izbor.Split(" ");

        string vrsta = splitPolje[1];
        string status = splitPolje[2];
        DateOnly datumOd = DateOnly.ParseExact(splitPolje[3], "dd.MM.yyyy.");
        DateOnly datumDo = DateOnly.ParseExact(splitPolje[5], "dd.MM.yyyy.");
        TimeOnly vrijemeOd = TimeOnly.ParseExact(splitPolje[4], "H:mm:ss");
        TimeOnly vrijemeDo = TimeOnly.ParseExact(splitPolje[6], "H:mm:ss");
        int brojac = 0;

        if (status.Equals("Z"))
        {
            ispisZauzetihVezova(vrsta, datumOd, datumDo, vrijemeOd, vrijemeDo, status);
            Console.WriteLine("");
        }
        if (status.Equals("S"))
        {
            List<Vez> slobodniVezovi = listaSlobodnihVezova(vrsta, datumOd, datumDo, vrijemeOd, vrijemeDo, status);
            Console.WriteLine("");
            metode.tablicaSlobodniVezovi(redniBroj, slobodniVezovi, zaglavlje, podnozje, brojac);
            Console.WriteLine("");
        }
    }

    public static void ispisZauzetihVezova(string vrsta, DateOnly datumOd, DateOnly datumDo, TimeOnly vrijemeOd, TimeOnly vrijemeDo, string status)
    {
        DateOnly pocetniDatum = datumOd;
        bool dodajNaListu = false;
        Console.WriteLine("");

        if (datumOd.Equals(datumDo))
        {
            istiDan(vrsta, datumOd, datumDo, vrijemeOd, vrijemeDo, dodajNaListu, status);
        }
        else
        {
            if (datumOd<datumDo)
            {
                do
                {
                    if (datumOd.Equals(pocetniDatum))
                    {
                        prviDan(vrsta, datumOd, datumDo, vrijemeOd, vrijemeDo, dodajNaListu, status);
                    }
                    else if ((datumOd < datumDo) && (datumOd != pocetniDatum))
                    {
                        ostaliDani(vrsta, datumOd, datumDo, vrijemeOd, vrijemeDo, dodajNaListu);
                    }
                    else
                    {
                        zadnjiDan(vrsta, datumOd, datumDo, vrijemeOd, vrijemeDo, dodajNaListu, status);
                    }
                    datumOd = datumOd.AddDays(1);
                    
                } while (datumOd <= datumDo);
            }
        }
    }

    public static List<Vez> listaSlobodnihVezova(string vrsta, DateOnly datumOd, DateOnly datumDo, TimeOnly vrijemeOd, TimeOnly vrijemeDo, string status)
    {
        List<Vez> slobodniVezovi = new List<Vez>();
        bool postoji = false;
        listaZauzetihVezova(vrsta, datumOd, datumDo, vrijemeOd, vrijemeDo, status);

        foreach (Vez vez in listaVezova)
        {
            foreach (Vez zauzetiVez in zauzetiVezovi)
            {
                if(vez.Equals(zauzetiVez)) postoji = true;
            }
            if(postoji==false && vez.Vrsta.Equals(vrsta)) slobodniVezovi.Add(vez);
            postoji = false;
        }
        return slobodniVezovi;
    }

    public static void listaZauzetihVezova(string vrsta, DateOnly datumOd, DateOnly datumDo, TimeOnly vrijemeOd, TimeOnly vrijemeDo, string status)
    {  
        DateOnly pocetniDatum = datumOd;
        bool dodajNaListu = true;

        if (datumOd.Equals(datumDo))
        {
            istiDan(vrsta, datumOd, datumDo, vrijemeOd, vrijemeDo, dodajNaListu, status);
        }
        else
        {
            if (datumOd < datumDo)
            {
                do
                {
                    if (datumOd.Equals(pocetniDatum))
                    {
                        prviDan(vrsta, datumOd, datumDo, vrijemeOd, vrijemeDo, dodajNaListu, status);
                    }
                    else if ((datumOd < datumDo) && (datumOd != pocetniDatum))
                    {
                        ostaliDani(vrsta, datumOd, datumDo, vrijemeOd, vrijemeDo, dodajNaListu);
                    }
                    else
                    {
                        zadnjiDan(vrsta, datumOd, datumDo, vrijemeOd, vrijemeDo, dodajNaListu, status);
                    }
                    datumOd = datumOd.AddDays(1);

                } while (datumOd <= datumDo);
            }
        } 
    }

    public static void istiDan(string vrsta, DateOnly datumOd, DateOnly datumDo, TimeOnly vrijemeOd, TimeOnly vrijemeDo, bool dodajNaListu, string status)
    {
        int brojac = 0;
        if (zaglavlje && status== "Z")
        {
            if (redniBroj) Console.WriteLine(String.Format("|{0,4}|{1,10}|{2,10}|{3,10}|{4,10}|{5,10}|{6,15}|", "RB  ", "ID        ", "OZNAKA    ", "VRSTA     ", "VRIJEME OD", "VRIJEME DO", "DATUM          "));
            else Console.WriteLine(String.Format("|{0,10}|{1,10}|{2,10}|{3,10}|{4,10}|{5,15}|", "ID        ", "OZNAKA    ", "VRSTA     ", "VRIJEME OD", "VRIJEME DO", "DATUM          "));
        }
        foreach (Vez vez in listaVezova)
        {
            foreach (Raspored raspored in listaRaspored)
            {
                if (raspored.IdVez.Equals(vez.Id) && vez.Vrsta.Equals(vrsta) && raspored.DaniUTjednu.Contains(((int)datumOd.DayOfWeek).ToString()))
                {
                    if (raspored.VrijemeOd.IsBetween(vrijemeOd, vrijemeDo) ||
                        raspored.VrijemeDo.IsBetween(vrijemeOd, vrijemeDo))
                    {
                        if (!dodajNaListu)
                        {
                           brojac++;
                           if(redniBroj) Console.WriteLine(String.Format("|{0,4}|{1,10}|{2,10}|{3,10}|{4,10}|{5,10}|{6,15}|", brojac+".", vez.Id, vez.OznakaVeza + "     ", vez.Vrsta + "        ",
                            raspored.VrijemeOd, raspored.VrijemeDo, datumOd.ToString()));
                           else Console.WriteLine(String.Format("|{0,10}|{1,10}|{2,10}|{3,10}|{4,10}|{5,15}|", vez.Id, vez.OznakaVeza + "     ", vez.Vrsta +"        ",
                            raspored.VrijemeOd, raspored.VrijemeDo, datumOd.ToString()));
                        }
                        if(dodajNaListu) zauzetiVezovi.Add(vez);
                    }
                }
            }
        }
        if (podnozje && status=="Z")
        {
            if (redniBroj) Console.WriteLine(String.Format("|{0,15}{1,60}|", "PODNOZJE:      ", brojac));
            else Console.WriteLine(String.Format("|{0,11}{1,59}|", "PODNOZJE:  ", brojac));
        }
    }

    public static void prviDan(string vrsta, DateOnly datumOd, DateOnly datumDo, TimeOnly vrijemeOd, TimeOnly vrijemeDo, bool dodajNaListu, string status)
    {
        brojacIzmeduDana = 0;
        if (zaglavlje && status=="Z")
        {
            if (redniBroj) Console.WriteLine(String.Format("|{0,4}|{1,10}|{2,10}|{3,10}|{4,10}|{5,10}|{6,15}|", "RB  ", "ID        ", "OZNAKA    ", "VRSTA     ", "VRIJEME OD", "VRIJEME DO", "DATUM          "));
            else Console.WriteLine(String.Format("|{0,10}|{1,10}|{2,10}|{3,10}|{4,10}|{5,15}|", "ID        ", "OZNAKA    ", "VRSTA     ", "VRIJEME OD", "VRIJEME DO", "DATUM          "));
        }
        foreach (Vez vez in listaVezova)
        {
            foreach (Raspored raspored in listaRaspored)
            {
                if (raspored.IdVez.Equals(vez.Id) && vez.Vrsta.Equals(vrsta) && raspored.DaniUTjednu.Contains(((int)datumOd.DayOfWeek).ToString()))
                {
                    if (raspored.VrijemeOd.IsBetween(vrijemeOd, TimeOnly.MaxValue) ||
                        raspored.VrijemeDo.IsBetween(vrijemeOd, TimeOnly.MaxValue))
                    {

                        if (!dodajNaListu)
                        {
                            brojacIzmeduDana++;
                            if (redniBroj) Console.WriteLine(String.Format("|{0,4}|{1,10}|{2,10}|{3,10}|{4,10}|{5,10}|{6,15}|", brojacIzmeduDana + ".", vez.Id, vez.OznakaVeza + "     ", vez.Vrsta + "        ",
                             raspored.VrijemeOd, raspored.VrijemeDo, datumOd.ToString()));
                            else Console.WriteLine(String.Format("|{0,10}|{1,10}|{2,10}|{3,10}|{4,10}|{5,15}|", vez.Id, vez.OznakaVeza + "     ", vez.Vrsta + "        ",
                             raspored.VrijemeOd, raspored.VrijemeDo, datumOd.ToString()));
                        }

                        if(dodajNaListu) zauzetiVezovi.Add(vez);
                    }
                }
            }
        }
    }

    public static void ostaliDani(string vrsta, DateOnly datumOd, DateOnly datumDo, TimeOnly vrijemeOd, TimeOnly vrijemeDo, bool dodajNaListu)
    {
        foreach (Vez vez in listaVezova)
        {
            foreach (Raspored raspored in listaRaspored)
            {
                if (raspored.IdVez.Equals(vez.Id) && vez.Vrsta.Equals(vrsta) && raspored.DaniUTjednu.Contains(((int)datumOd.DayOfWeek).ToString()))
                {
                    if (!dodajNaListu)
                    {
                        brojacIzmeduDana++;
                        if (redniBroj) Console.WriteLine(String.Format("|{0,4}|{1,10}|{2,10}|{3,10}|{4,10}|{5,10}|{6,15}|", brojacIzmeduDana + ".", vez.Id, vez.OznakaVeza + "     ", vez.Vrsta + "        ",
                         raspored.VrijemeOd, raspored.VrijemeDo, datumOd.ToString()));
                        else Console.WriteLine(String.Format("|{0,10}|{1,10}|{2,10}|{3,10}|{4,10}|{5,15}|", vez.Id, vez.OznakaVeza + "     ", vez.Vrsta + "        ",
                         raspored.VrijemeOd, raspored.VrijemeDo, datumOd.ToString()));
                    }
                    
                    if(dodajNaListu)zauzetiVezovi.Add(vez);
                }
            }
        }
    }

    public static void zadnjiDan(string vrsta, DateOnly datumOd, DateOnly datumDo, TimeOnly vrijemeOd, TimeOnly vrijemeDo, bool dodajNaListu, string status)
    {
        foreach (Vez vez in listaVezova)
        {
            foreach (Raspored raspored in listaRaspored)
            {
                if (raspored.IdVez.Equals(vez.Id) && vez.Vrsta.Equals(vrsta) && raspored.DaniUTjednu.Contains(((int)datumOd.DayOfWeek).ToString()))
                {
                    if (raspored.VrijemeOd.IsBetween(TimeOnly.MinValue, vrijemeOd) ||
                        raspored.VrijemeDo.IsBetween(TimeOnly.MinValue, vrijemeDo))
                    {
                        if (!dodajNaListu)
                        {
                            brojacIzmeduDana++;
                            if (redniBroj) Console.WriteLine(String.Format("|{0,4}|{1,10}|{2,10}|{3,10}|{4,10}|{5,10}|{6,15}|", brojacIzmeduDana + ".", vez.Id, vez.OznakaVeza  + "     ", vez.Vrsta + "        ",
                             raspored.VrijemeOd, raspored.VrijemeDo, datumOd.ToString()));
                            else Console.WriteLine(String.Format("|{0,10}|{1,10}|{2,10}|{3,10}|{4,10}|{5,15}|", vez.Id, vez.OznakaVeza + "     ", vez.Vrsta + "        ",
                             raspored.VrijemeOd, raspored.VrijemeDo, datumOd.ToString()));
                        }
                        
                        if(dodajNaListu) zauzetiVezovi.Add(vez);
                    }
                }
            }
        }
        if (podnozje && status == "Z")
        {
            if (redniBroj) Console.WriteLine(String.Format("|{0,15}{1,60}|", "PODNOZJE:      ", brojacIzmeduDana));
            else Console.WriteLine(String.Format("|{0,11}{1,59}|", "PODNOZJE:  ", brojacIzmeduDana));
        }
    }

    public static void automatskoRezerviranje(List<ZahtjevRezervacije> listaZahtjevaRezervacije, List<Raspored> listaRasporeda, List<Brod> listaBrodova)
    {
        List<Vez> listaSlobodnih = kreirajListuSlobodnihZaRezervaciju(listaZahtjevaRezervacije);

        foreach (ZahtjevRezervacije zahtjevRezervacije in listaZahtjevaRezervacije)
        {
            bool potvrdi = false;
            foreach (Brod brod in listaBrodova)
            {
                if (brod.Id.Equals(zahtjevRezervacije.IdBrod) && (brod.Vrsta == "TR" || brod.Vrsta == "KA" || brod.Vrsta == "KL" || brod.Vrsta == "KR"))
                {
                    foreach (Vez vez in listaSlobodnih)
                    {
                        if (vez.Vrsta=="PU" && brod.Sirina <= vez.MaksimalnaSirina && brod.Duljina <= vez.MaksimalnaDuljina && brod.Gaz <= vez.MaksimalnaDubina)
                        {
                            potvrdi = true;
                        }
                    }
                }
                else if (brod.Id.Equals(zahtjevRezervacije.IdBrod) && (brod.Vrsta == "RI" || brod.Vrsta == "TE"))
                {
                    foreach (Vez vez in listaSlobodnih)
                    {
                        if (vez.Vrsta == "PO" && brod.Sirina <= vez.MaksimalnaSirina && brod.Duljina <= vez.MaksimalnaDuljina && brod.Gaz <= vez.MaksimalnaDubina)
                        {
                            potvrdi = true;
                        }
                    }
                }
                else if (brod.Id.Equals(zahtjevRezervacije.IdBrod) && (brod.Vrsta == "JA" || brod.Vrsta == "BR" || brod.Vrsta == "RO"))
                {
                    foreach (Vez vez in listaSlobodnih)
                    {
                        if (vez.Vrsta == "OS" && brod.Sirina <= vez.MaksimalnaSirina && brod.Duljina <= vez.MaksimalnaDuljina && brod.Gaz <= vez.MaksimalnaDubina)
                        {
                            potvrdi = true;
                        }
                    }
                }
            }
            if (potvrdi)
            {
                ZahtjevRezervacije noviUpis = new ZahtjevRezervacijeBuilder(zahtjevRezervacije.IdBrod, zahtjevRezervacije.DatumOd,
                    zahtjevRezervacije.VrijemeOd, zahtjevRezervacije.TrajanjePrivezaUH)
                    .setStatus("ODOBREN")
                    .Build();
                odobreniZahtjevi.Add(noviUpis);
                Console.WriteLine("Zahtjev odobren: " + zahtjevRezervacije.IdBrod);
            }
        }
    }
    private static List<Vez> kreirajListuSlobodnihZaRezervaciju(List<ZahtjevRezervacije> listaZahtjevaRezervacije)
    {
        List<Vez> listaSlobodnih = new List<Vez>();

        foreach (ZahtjevRezervacije zahtjevRezervacije in listaZahtjevaRezervacije)
        {
            foreach (Brod brod in listaBrodova)
            {
                if (brod.Id.Equals(zahtjevRezervacije.IdBrod))
                {
                    try
                    {
                        if ((brod.Vrsta == "TR" || brod.Vrsta == "KA" || brod.Vrsta == "KL" || brod.Vrsta == "KR"))
                        {
                            listaSlobodnih = listaSlobodnihVezova("PU", zahtjevRezervacije.DatumOd, zahtjevRezervacije.DatumOd, zahtjevRezervacije.VrijemeOd, zahtjevRezervacije.VrijemeOd.AddHours(zahtjevRezervacije.TrajanjePrivezaUH), "S");
                        }
                        else if (brod.Vrsta == "RI" || brod.Vrsta == "TE")
                        {
                            listaSlobodnih = listaSlobodnihVezova("PO", zahtjevRezervacije.DatumOd, zahtjevRezervacije.DatumOd, zahtjevRezervacije.VrijemeOd, zahtjevRezervacije.VrijemeOd.AddHours(zahtjevRezervacije.TrajanjePrivezaUH), "S");
                        }
                        else if ((brod.Vrsta == "JA" || brod.Vrsta == "BR" || brod.Vrsta == "RO"))
                        {
                            listaSlobodnih = listaSlobodnihVezova("OS", zahtjevRezervacije.DatumOd, zahtjevRezervacije.DatumOd, zahtjevRezervacije.VrijemeOd, zahtjevRezervacije.VrijemeOd.AddHours(zahtjevRezervacije.TrajanjePrivezaUH), "S");
                        }
                        else Console.WriteLine("Nema");
                    }
                    catch (Exception)
                    {
                        greska.povecajGresku();
                        Console.WriteLine("Neispravan unos");
                    }
                }
            }
        }
        return listaSlobodnih;
    }
    private static void zahtjevZaPrivez(int idBrod)
    {
        DateTime trenutnoVirtualnoVrijeme = virtualnoVrijeme;
        string datumVrijeme = trenutnoVirtualnoVrijeme.ToString();
        string[] poljeDV = datumVrijeme.Split(" ");

        DateOnly datum = DateOnly.ParseExact(poljeDV[0].Trim(), "dd.M.yyyy.");
        TimeOnly vrijeme = TimeOnly.ParseExact(poljeDV[1].Trim(), "H:mm:ss");

        Console.WriteLine("Trenutno virutalno vrijeme: " + trenutnoVirtualnoVrijeme.ToString());
        Console.WriteLine("");

        Console.WriteLine("Brod " + idBrod + " trazi dozvolu za privez");
    }
    private static void spojiBrodKanal(string brod, string kanal)
    {
        DateTime trenutnoVirtualnoVrijeme = virtualnoVrijeme;
        Console.WriteLine("Trenutno virutalno vrijeme: " + trenutnoVirtualnoVrijeme.ToString());
        Console.WriteLine("");

        Kanal trazeniKanal = listaKanala.SingleOrDefault(kanalFrekvencija => kanalFrekvencija.Frekvencija == Int32.Parse(kanal));
        Brod trazeniBrod = listaBrodova.SingleOrDefault(brodId => brodId.Id == Int32.Parse(brod));

        if (!listaKanala.Contains(trazeniKanal))
        {
            greska.povecajGresku();
            Console.WriteLine("Ne postoji kanal s tom frekvencijom.");
        }

        else if (!listaBrodova.Contains(trazeniBrod))
        {
            greska.povecajGresku();
            Console.WriteLine("Ne postoji brod s tim ID brojem");
        }
        else
        {
            foreach (Kanal kanal1 in listaKanala)
            {
                if (kanal1.Frekvencija == Int32.Parse(kanal))
                {
                    if (!kanal1.spojeniBrodovi.Contains(trazeniBrod))
                    {
                        if (kanal1.spojeniBrodovi.Count < kanal1.MaksimalanBroj)
                        {
                            kanal1.spojeniBrodovi.Add(trazeniBrod);
                            Console.WriteLine("Brod s id " + trazeniBrod.Id + " spaja se na kanalu " + trazeniKanal.Frekvencija);
                        }
                        else
                        {
                            greska.povecajGresku();
                            Console.WriteLine(" Dosegnut maksimalan broj spojenih brodova na ovom kanalu");
                        }
                    }
                    else
                    {
                        greska.povecajGresku();
                        Console.WriteLine("Ovaj brod je vec spojen na kanal");
                    }
                }
            }
        }
    }

    private static void odspojiBrodKanal(string brod, string kanal)
    {
        DateTime trenutnoVirtualnoVrijeme = virtualnoVrijeme;
        Console.WriteLine("Trenutno virutalno vrijeme: " + trenutnoVirtualnoVrijeme.ToString());
        Console.WriteLine("");

        Kanal trazeniKanal = listaKanala.SingleOrDefault(kanalFrekvencija => kanalFrekvencija.Frekvencija == Int32.Parse(kanal));
        Brod trazeniBrod = listaBrodova.SingleOrDefault(brodId => brodId.Id == Int32.Parse(brod));

        if (!listaKanala.Contains(trazeniKanal))
        {
            greska.povecajGresku();
            Console.WriteLine("Ne postoji kanal na toj frekvenciji.");
        }

        else if (!listaBrodova.Contains(trazeniBrod))
        {
            greska.povecajGresku();
            Console.WriteLine("Ne postoji brod s tim ID brojem");
        }
        else
        {
            foreach (Kanal kanal1 in listaKanala)
            {
                if (kanal1.Frekvencija == Int32.Parse(kanal))
                {
                    if (kanal1.spojeniBrodovi.Contains(trazeniBrod))
                    {
                        if (kanal1.spojeniBrodovi.Count > 0)
                        {
                            kanal1.spojeniBrodovi.Remove(trazeniBrod);
                            Console.WriteLine("Brod s id " + trazeniBrod.Id + " se odjavljuje s kanala " + trazeniKanal.Frekvencija);
                        }
                    }
                    else
                    {
                        greska.povecajGresku();
                        Console.WriteLine("Ovaj brod nije spojen na kanal");
                    }
                }
            }
        }
    }
    private static void postavkeTablice(string[] izborArgumenti)
    {
        DateTime trenutnoVirtualnoVrijeme = virtualnoVrijeme;
        Console.WriteLine("Trenutno virutalno vrijeme: " + trenutnoVirtualnoVrijeme.ToString());
        Console.WriteLine("");

        if (izborArgumenti.Contains("Z")) zaglavlje = true;
        else zaglavlje = false;
        if (izborArgumenti.Contains("P")) podnozje = true;
        else podnozje = false;
        if (izborArgumenti.Contains("RB")) redniBroj = true;
        else redniBroj = false;
    }
    private static void ispisZauzetihVezovaUZadanoVrijeme(string[] argumentiIzbor)
    {
        DateTime trenutnoVirtualnoVrijeme = virtualnoVrijeme;
        Console.WriteLine("Trenutno virutalno vrijeme: " + trenutnoVirtualnoVrijeme.ToString());
        Console.WriteLine("");

        TimeOnly vrijeme = new TimeOnly();
        DateOnly datum = new DateOnly();
        try
        {
            datum = DateOnly.ParseExact(argumentiIzbor[1].Trim(), "dd.MM.yyyy.");
            vrijeme = TimeOnly.ParseExact(argumentiIzbor[2].Trim(), "H:mm");

            var putnicki = new Putnicki();
            var poslovni = new Poslovni();
            var ostali = new Ostali();

            var brojanje = new Brojanje();

            if (zaglavlje)
            {
                if (redniBroj) Console.WriteLine(String.Format("|{0,4}|{1,10}|{2,10}|","RB  ", "VRSTA     ", "BROJ     "));
                else Console.WriteLine(String.Format("|{0,10}|{1,10}|", "VRSTA     ", "BROJ     "));
            }
            putnicki.Accept(brojanje, listaVezova, listaRaspored, datum, vrijeme, redniBroj);
            poslovni.Accept(brojanje, listaVezova, listaRaspored, datum, vrijeme, redniBroj);
            ostali.Accept(brojanje, listaVezova, listaRaspored, datum, vrijeme, redniBroj);
            if (podnozje)
            {
                if (redniBroj)
                {
                    Console.WriteLine(String.Format("|{0,10}{1,16}|", "PODNOZJE  ", "               3"));
                }
                else Console.WriteLine(String.Format("|{0,10} {1,10}|", "PODNOZJE  ", "         3"));
            }
        }
        catch (Exception)
        {
            greska.povecajGresku();
            Console.WriteLine("Pogresan oblik datuma ili vremena");
        }
    }
    private static void ukloniBrod(string[] argumentiIzbor)
    {
        var intern = new InternOstali();
        var upravitelj = new UpraviteljPoslovni();
        var direktor = new DirektorPutnicki();

        intern.setNadredeni(upravitelj);
        upravitelj.setNadredeni(direktor);

        Brod zadaniBrod = new Brod();
        foreach (Brod brod1 in listaBrodova)
        {
            if (brod1.Id == Int32.Parse(argumentiIzbor[2])) zadaniBrod = brod1;
        }

        bool postoji = false;

        Console.WriteLine("");
        foreach (Brod brod in listaBrodova.ToList())
        {
            if (zadaniBrod.Id == brod.Id)
            {
                listaBrodova = intern.ukloniBrod(listaBrodova, brod);
                postoji = true;
            }
        }

        if (!listaBrodova.Contains(zadaniBrod))
        {
            azurirajListe(zadaniBrod);
        }

        if (!postoji)
        {
            greska.povecajGresku();
            Console.WriteLine("Ne postoji brod s tim ID brojem");
        }
    }

    public static void azurirajListe(Brod zadaniBrod)
    {
        foreach (Raspored raspored in listaRaspored.ToList())
        {
            if (!listaBrodova.Contains(zadaniBrod))
            {
                if (raspored.IdBrod == zadaniBrod.Id) listaRaspored.Remove(raspored);
            }
        }

        foreach (Kanal kanal in listaKanala.ToList())
        {
            if (!listaBrodova.Contains(zadaniBrod))
            {
                if (kanal.spojeniBrodovi.Contains(zadaniBrod)) kanal.spojeniBrodovi.Remove(zadaniBrod);
            }
        }
        foreach (ZahtjevRezervacije zahtjevRezervacije in odobreniZahtjevi.ToList())
        {
            if (!listaBrodova.Contains(zadaniBrod))
            {
                if (zahtjevRezervacije.IdBrod == zadaniBrod.Id) odobreniZahtjevi.Remove(zahtjevRezervacije);
            }
        }
    }

    public static void spremiStanje(string naziv)
    {
        DateTime trenutnoVirtualnoVrijeme = virtualnoVrijeme;
        Console.WriteLine("Trenutno virutalno vrijeme: " + trenutnoVirtualnoVrijeme.ToString());
        Console.WriteLine("");

        SpremljenoStanje spremljenoStanje = new SpremljenoStanje(naziv, virtualnoVrijeme, listaVezova, listaRaspored);

        bool postoji = false;

        foreach (SpremljenoStanje stanje in listaSpremljenoStanje)
        {
            if (stanje.Naziv.Equals(spremljenoStanje.Naziv)) postoji = true;
        }

        if (!postoji) listaSpremljenoStanje.Add(spremljenoStanje);
        else
        {
            greska.povecajGresku();
            Console.WriteLine("Vec postoji zapis s tim nazivom!");
        }
    }

    public static void postaviStanje(string naziv)
    {
        DateTime trenutnoVirtualnoVrijeme = virtualnoVrijeme;
        Console.WriteLine("Trenutno virutalno vrijeme: " + trenutnoVirtualnoVrijeme.ToString());
        Console.WriteLine("");

        SpremljenoStanje zadanoStanje = null;

        bool postoji = false;   
        foreach (SpremljenoStanje stanje in listaSpremljenoStanje)
        {
            if (stanje.Naziv.Equals(naziv))
            {
                zadanoStanje = stanje;
                postoji = true;
            }
        }

        if (postoji)
        {

            virtualnoVrijeme = zadanoStanje.VirtualnoVrijeme;
            listaVezova = zadanoStanje.Vezovi;
            listaRaspored = zadanoStanje.Raspored;

            Console.WriteLine("Stanje vezova vraćeno je na spremljeno stanje.");
            DateTime novoVirtualnoVrijeme = virtualnoVrijeme;
            Console.WriteLine("Novo virutalno vrijeme: " + novoVirtualnoVrijeme.ToString());
            Console.WriteLine("");
        }
        else
        {
            greska.povecajGresku();
            Console.WriteLine("Ne postoji spremljeno stanje s tim nazivom!");
        }

        
    }
}