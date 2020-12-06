using mr.muscle;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

 
namespace Menut1
{
    class Program
    {
        static string kikkelipoika2 = @"..\..\..\kikkelipoika.json"; //Luodaan path
        static string ohje = "0.Lopetus\n1.Hae Asiakasta\n2.Lisää uusi Asiakas\n3.Asiakkaan uudelleen aktivointi tai muuta osoitetta/tasoa.\n4.Asiakas määrä ja tuotto\n5.Asiakkaan irtisanominen";
        static void Main(string[] args)
        {
            

            Dictionary<string, Asiakastiedot> asiakkaat = JsonConvert.DeserializeObject<Dictionary<string, Asiakastiedot>>(File.ReadAllText(kikkelipoika2));
            foreach (string id in asiakkaat.Keys){
                Console.WriteLine(id);
            }
            

            Console.WriteLine(ohje);
            string komento = Console.ReadLine();
            while (!komento.StartsWith("0"))   // ! tarkoittaa NOT kommentit 
            {
                
                switch (komento)
                {
                    case "1":


                        Console.Write("Etsi asiakasta sukunimellä: ");
                        string vastaus = Console.ReadLine();

                        if (asiakkaat.ContainsKey(vastaus))
                        {
                            Console.WriteLine(vastaus + " löydetty");
                            Console.WriteLine(asiakkaat[vastaus].palauta());
                            File.WriteAllText(kikkelipoika2, JsonConvert.SerializeObject(asiakkaat));
                        }
                        else
                        {
                            Console.WriteLine(vastaus + " Sukunimeä ei löydetty tietojärjestelmästä");
                        }

                        break;
                    case "2":
                        
                        Asiakastiedot asiakas = new Asiakastiedot();
                        
                        Console.Write("Syötä Etunimi: ");
                        asiakas.Nimi = Console.ReadLine().ToLower();
                        Console.Write("Syötä Sukunimi: ");
                        string sukunimi = Console.ReadLine().ToLower();
                        if (asiakkaat.ContainsKey(sukunimi))
                        {
                            Console.WriteLine("Sama sukunimi löydetty tietokannasta");
                            Console.WriteLine("Tämä asiakas löytyy jatkossa käyttäen etunimen ja sukunimen kombinaatiota");
                            sukunimi = sukunimi+asiakas.Nimi;
                            asiakas.Sukunimi = sukunimi;
                            Console.WriteLine("Hae jatkossa asiakasta: "+asiakas.Sukunimi);
                        }
                        asiakas.Sukunimi = sukunimi;
                        Console.Write("Syötä Ikä: ");
                        asiakas.Ikä = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Syötä Osoite: ");
                        asiakas.Osoite = Console.ReadLine().ToLower();
                        long now1 = DateTime.Now.Ticks;
                        asiakas.Luotu = now1;
                        Console.Write("Mikä Jäsenyys?\n1.Basic\n2.Hopea\n3.Kulta\n4.Kuningas: ");
                        asiakas.Taso = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Kesto kuukasina?: ");
                        asiakas.Kesto = Convert.ToInt32(Console.ReadLine());
                        asiakas.käytetty = false;
                        asiakas.Hintalaskuri();
                        asiakas.ikaalennus();
                        asiakkaat.Add(asiakas.Sukunimi, asiakas);
                        File.WriteAllText(kikkelipoika2, JsonConvert.SerializeObject(asiakkaat));

                        break;
                    case "3":

                        while (true)
                        {
                            Console.WriteLine("Kenen asiakkaan tietoja muutetaan, etsi sukunimellä: ");
                            string hauntulos = Console.ReadLine();

                            if (asiakkaat.ContainsKey(hauntulos))
                            {
                                Console.WriteLine(hauntulos + " löydetty");

                            }

                            else
                            {
                                Console.WriteLine(hauntulos + " Sukunimeä ei löydetty tietojärjestelmästä");
                                break;
                            }
                            

                            Console.WriteLine("Päivitetäänkö osoite vai taso? ");
                            string muutos = Console.ReadLine();
                            if (muutos.Equals("osoite"))
                            {
                                Console.WriteLine("Syötä uusi osoite: ");
                                string osoite = Console.ReadLine();
                                asiakkaat[hauntulos].Osoite = osoite;
                                File.WriteAllText(kikkelipoika2, JsonConvert.SerializeObject(asiakkaat));
                                Console.WriteLine(asiakkaat[hauntulos].palauta());
                                break;
                            }
                            else if (muutos.Equals("taso"))
                            {

                                Console.WriteLine("syötä uusi taso\n1.Basic\n2.Hopea\n3.Kulta\n4.Kuningas : ");
                                int tasonmuutos = Convert.ToInt32(Console.ReadLine());
                                asiakkaat[hauntulos].käytetty = false;
                                asiakkaat[hauntulos].Taso = tasonmuutos;
                                asiakkaat[hauntulos].Hinta = 0;
                                Console.WriteLine("Uusi kesto: ");
                                int kesto = Convert.ToInt32(Console.ReadLine());
                                asiakkaat[hauntulos].Kesto = kesto;
                                asiakkaat[hauntulos].Hintalaskuri();
                                asiakkaat[hauntulos].ikaalennus();
                                Console.WriteLine(asiakkaat[hauntulos].palauta());
                                File.WriteAllText(kikkelipoika2, JsonConvert.SerializeObject(asiakkaat));
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Tarkista kirjoitusasu");
                            }
                          
                        }
                   
                        break;
                    case "4":
                        double summa = 0;
                        int kuukaudetyhteensa = 0;
                        int maksavatasiakkaat = asiakkaat.Count;
                        Console.Write("Anna salasana: ");
                        String salis = Console.ReadLine();
                        while (true)
                        {
                            if (!(salis.Equals("sexyboy"))){
                                Console.WriteLine("Väärä salasana");
                                break;
                            } else
                            {
                                foreach (var ihminen in asiakkaat)

                                {
                                    if (ihminen.Value.Hinta == 0)
                                    {
                                        maksavatasiakkaat--;
                                    }

                                    if (ihminen.Value.Hinta > 0)
                                    {
                                        summa += (ihminen.Value.Hinta); //Haetaan jokaisen listalla olevan ihmisen Hinnan valueta
                                        kuukaudetyhteensa += (ihminen.Value.Kesto); // Jokainen sidottu kuukausi
                                    }
                                }
                                double kuukausitienesti = summa / kuukaudetyhteensa * maksavatasiakkaat;
                                Console.WriteLine("Asiakkaita yhteensä: " + asiakkaat.Count);
                                Console.WriteLine("Maksavia asiakkaita yhteensä: " + maksavatasiakkaat);
                                Console.WriteLine("Yhteensä tuloja: " + summa + " euroa.");
                                Console.WriteLine("Kuukausitulot yhteensä: " + kuukausitienesti + " euroa.");
                                break;

                            }

                        }

                        break;
                    case "5":
                        Console.WriteLine("Kenen asiakkuus mitätöidään, etsi sukunimellä? ");
                        string mitatointi = Console.ReadLine();

                        if (asiakkaat.ContainsKey(mitatointi))
                        {

                            asiakkaat[mitatointi].deaktivointi();
                            Console.WriteLine(asiakkaat[mitatointi].palauta());
                            asiakkaat[mitatointi].käytetty = false;
                            Console.WriteLine("Jos asiakas haluaa joskus uudellenliittyä, päivitä hänen tasonsa");
                            File.WriteAllText(kikkelipoika2, JsonConvert.SerializeObject(asiakkaat));

                        } else
                        {
                            Console.WriteLine("Asiakasta ei löytynyt,tarkista kirjoitusasu.");
                        }

                            break;
                }
                Console.WriteLine(ohje);
                komento = Console.ReadLine();

                
            }
        }

      
    }
}