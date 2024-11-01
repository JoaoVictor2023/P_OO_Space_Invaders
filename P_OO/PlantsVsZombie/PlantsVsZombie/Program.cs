using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Timers; // Utilisation de System.Timers.Timer
using System.Windows.Forms;
namespace PlantsVsZombie
{
    internal static class Program
    {
        private static List<DrawZombie> hordeZombie = new List<DrawZombie>();
        private static List<(int x, int y)> positionsPredifinies;
        private static Random random = new Random();
        private static System.Timers.Timer zombieSpawnTimer;
        private static List<DrawPlants> plantes = new List<DrawPlants>();
        private static DrawBackgroundPlants fond = new DrawBackgroundPlants();


        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Configuration de l'application
            ApplicationConfiguration.Initialize();

            // D�finir les positions pr�d�finies pour leys zombies
            positionsPredifinies = new List<(int x, int y)>
            {
                (1200, 70),
                (1200, 170),
                (1200, 290),
                (1200, 370),
                (1200, 490)
            };

            // Initialiser le timer pour l'apparition des zombies
            zombieSpawnTimer = new System.Timers.Timer();
            zombieSpawnTimer.Elapsed += OnZombieSpawn;
            SetRandomTimerInterval(); // D�finit l'intervalle initial




            // D�marrage du jardin (sans zombies pour l'instant)
            Application.Run(new Garden(hordeZombie, fond));
        }

        // M�thode appel�e � chaque "tick" du Timer
        private static void OnZombieSpawn(object sender, ElapsedEventArgs e)
        {
            // Cr�er un zombie
            DrawZombie zombie = new DrawZombie();

            // S�lectionner une position al�atoire � partir de la liste
            var positionAleatoire = positionsPredifinies[random.Next(1, positionsPredifinies.Count)];
            zombie.x = positionAleatoire.x;
            zombie.y = positionAleatoire.y;

            // Ajouter le zombie � la horde
            hordeZombie.Add(zombie);

            // Afficher le nombre de zombies cr��s (pour d�bogage)
            Console.Clear();
            if (hordeZombie.Count < 2)
            {
                Console.WriteLine($"{hordeZombie.Count} zombie");

            }
            else
            {
                Console.WriteLine($"{hordeZombie.Count} zombies");
            }

            // D�finir un nouvel intervalle al�atoire pour le prochain zombie
            SetRandomTimerInterval();
        }

        
        // M�thode pour d�finir un intervalle al�atoire entre 5 et 10 secondes
        private static void SetRandomTimerInterval()
        {
            int interval = random.Next(5000, 10000); // Intervalle entre 5000 ms (5 secondes) et 10000 ms (10 secondes)
            zombieSpawnTimer.Interval = interval;
            zombieSpawnTimer.Start(); // Re-d�marrer le timer avec le nouvel intervalle
            Console.WriteLine("Prochain zombie dans " + interval + " ms");
        }
    }
}
