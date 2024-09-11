namespace PlantsVsZombie
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Création de la flotte de drones
            List<Zombie> hordeZombie= new List<Zombie>();
            Zombie zombie = new Zombie();
            zombie.x = Garden.WIDTH / 1;
            zombie.y = Garden.HEIGHT / Garden.HEIGHT + 50;
            //drone.name = "Zombie";
            hordeZombie.Add(zombie);

            // Démarrage
            Application.Run(new Garden(hordeZombie));
        }
    }
}