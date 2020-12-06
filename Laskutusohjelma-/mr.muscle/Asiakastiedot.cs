using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace mr.muscle
{
    public class Asiakastiedot
    {

        /*
        Etunimi
        Sukunimi
        Ikä
        Osoite
        */
        [JsonProperty("Nimi")]
        public string Nimi { get; set; }
        [JsonProperty("Sukunimi")]
        public string Sukunimi { get; set; }
        [JsonProperty("Ikä")]
        public int Ikä { get; set; }
        [JsonProperty("Osoite")]
        public string Osoite { get; set; }
        [JsonProperty("Asiakkuus solmittu")]
        public long Luotu { get; set; }
        private bool validointi;
        public bool käytetty;

        /* 1) Basic 2) Hopea 3) Kulta 4) Kuningas 
            Kesto 1kk 6kk 12kk
            Hinta Basic 1Kk = 30€
            Hinta Hopea 1kk = 40€
            Hinta Kulta 1kk = 60€
            Hinta Kuningas 1kk = 199€
        */

        //Jäsenyys vaatii nämä tiedot taso + kesto
        [JsonProperty("Asiakkuus-Taso")]
        public int Taso { get; set; }
        [JsonProperty("Sopimuksen Kesto")]
        public int Kesto { get; set; }
        //jäsenyys laskee itse hinnan//
        [JsonProperty("Hinta yhteensä")]
        public double Hinta { get; set; }

        //Julistetaan Metodit 
        //Laskee hinnan kesto * taso
        public void Hintalaskuri()
        {
            while (Hinta <= 0)
            {
                if (Kesto <= 0)
                {
                    Console.Write("Keston oltava suurempi kuin 0kk.\nKirjoita kesto: ");
                    Kesto = Convert.ToInt32(Console.ReadLine());
                }
                else if (Taso == 1)
                {

                    Hinta = 30 * Kesto;

                }
                else if (Taso == 2)
                {
                    Hinta = 40 * Kesto;
                }
                else if (Taso == 3)
                {
                    Hinta = 60 * Kesto;
                }
                else if (Taso == 4)
                {
                    Hinta = 199 * Kesto;
                }
                else
                {
                    Console.WriteLine("Hintaa ei voitu laskea tarkista taso ja kesto");
                    Console.Write("Taso: ");
                    Taso = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Kesto: ");
                    Kesto = Convert.ToInt32(Console.ReadLine());

                }
            }

        }
        IFormatProvider culture = new CultureInfo("fi-FI", true);
        public string palauta() ///Palauttaa oleelliset asiat työntekijälle
        {
            DateTime liittynyt = new DateTime(this.Luotu); //liittymisaika
            tarkistavalidointi();
            if (validointi == true && this.käytetty == false)
            {
                pitkaaikaisale();
                this.käytetty = true;
            }
            
            return this.Nimi+" "+this.Sukunimi+" Osoite: "+this.Osoite+" Liittynyt asiakkaaksi: "+liittynyt.Day+"."+liittynyt.Month+"."+liittynyt.Year+", Asiakkuus Taso: "+this.Taso+". Sopimuksen Arvo: "+this.Hinta+" euroa.\nSeuraava maksuerä: "+this.Hinta/this.Kesto+" euroa.";
        }

        // Antaa alennuksen eläkeläisille
        public double ikaalennus()
        {
            if (this.Ikä > 65)
            {
                double hinta = Convert.ToInt32(this.Hinta * 0.9);
                return this.Hinta = hinta;
            }
            else
            {
                return this.Hinta;
            }

        }

        //Antaa alennuksen pitkäaikaisille jäsenille
        public double pitkaaikaisale()
        {

            double uusi_hinta = this.Hinta * 0.85;
            this.validointi = false;
            return this.Hinta = uusi_hinta;
            

        }

        public bool tarkistavalidointi()
        {
            DateTime joined = new DateTime(this.Luotu);
            DateTime now = DateTime.Now;
            int ero = Convert.ToInt32((now - joined).TotalDays); //laskee nykyisen ja liittymisen päivien eron
            if (ero > 730)
            {
                return this.validointi = true;
                
            }
            else
            {
                return this.validointi = false;
            }
            
        }
        public double deaktivointi()
        {
            //this.käytetty = false;
            return this.Hinta = 0;
        }
      
    }
    
}
