using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

namespace Lecteur_ST_Console
{
    class Srt
    {
        private string srtPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private List<string> srtContent = new List<string>();

        private int ID;
        private string TempsDebut;
        private string PremierTexte;
        private string SecondTexte;

        private double heures = 0.0;
        private double minutes = 0.0;
        private double secondes = 0.0;
        private double milliseconds = 0.0;
        private double Temps;

        private int TempsA;
        private int TempsB = 0;

        public void SelectFile()
        {

            Console.WriteLine(@"Entrez le chemin du fichier de sous-titre depuis votre bureau : Desktop\");
            string fileName = Console.ReadLine();
            srtPath += @"\" + fileName;

        }

        public void RecoverySrt()
        {
            using (StreamReader outputFile = new StreamReader(srtPath))
            {
                string content = "";
                while ((content = outputFile.ReadLine()) != null)
                {
                    srtContent.Add(content);
                }
            }
        }

        private void ConvertTime(string Time)
        {
            string[] timeBar1 = Time.Split(':');
            heures = double.Parse(timeBar1[0]);
            minutes = double.Parse(timeBar1[1]);

            string timer = timeBar1[2];
            string[] timeBar2 = timer.Split(',');
            secondes = double.Parse(timeBar2[0]);
            milliseconds = double.Parse(timeBar2[1]);

            Temps = Math.Round((heures * 3600 * 1000 + minutes * 60 * 1000 + secondes * 1000 + milliseconds),0);

        }

        public async Task ReadSrt()
        {
            int i = 0;
            
            while (i < srtContent.Count)
            {


                //ID
                ID = int.Parse(srtContent[i]);
                i++;

                //Récupération et conversion de l'heure d'apparition du sous-titre
                string[] time = srtContent[i].Split(' ');
                TempsDebut = time[0];

                ConvertTime(TempsDebut);
                TempsA = Convert.ToInt32(Temps);


                i++;

                //Affichage de la première ligne du sous-titre
                PremierTexte = srtContent[i];

                await Task.Delay(TempsA - TempsB);

                Console.WriteLine(PremierTexte);

                i++;

                //Affichage de la deuxième ligne du sous-titre si elle existe
                if (srtContent[i] != "")
                {
                    SecondTexte = srtContent[i];
                    Console.WriteLine(SecondTexte);

                    Console.WriteLine("");
                    i++;
                }
                TempsB = TempsA;
                i++;

            }
        }
    }
}
