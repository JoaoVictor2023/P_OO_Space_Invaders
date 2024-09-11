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
            List<Zombie> fleet= new List<Zombie>();
            Zombie drone = new Zombie();
            drone.x = Garden.WIDTH / 1;
            drone.y = Garden.HEIGHT / Garden.HEIGHT + 70;
            //drone.name = "Zombie";
            fleet.Add(drone);

            // Démarrage
            Application.Run(new Garden(fleet));
        }
    }
}