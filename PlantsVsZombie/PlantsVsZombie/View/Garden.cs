namespace PlantsVsZombie
{
    public partial class Garden : Form
    {
        public static readonly int WIDTH = 1200;        
        public static readonly int HEIGHT = 600;

        // La flotte est l'ensemble des zombies dans notre jardin
        private List<Zombie> fleet;

        BufferedGraphicsContext currentContext;
        BufferedGraphics airspace;

        // Image d'arrière-plan
        private Image backgroundImage;
        private bool backgroundLoaded = false;

        // Initialisation du jardin avec un certain nombre de zombies
        public Garden(List<Zombie> fleet)
        {
            InitializeComponent();

            // Charger l'image d'arrière-plan
            backgroundImage = Image.FromFile("C:\\Users\\pg05lby\\Documents\\GitHub\\P_OO_Space_Invaders\\Images PVZ\\backgroundJeu.png");
            backgroundLoaded = true;

            // Gets a reference to the current BufferedGraphicsContext
            currentContext = BufferedGraphicsManager.Current;
            // Creates a BufferedGraphics instance associated with this form, and with
            // dimensions the same size as the drawing surface of the form.
            airspace = currentContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);
            this.fleet = fleet;
        }

        // Affichage de la situation actuelle
        private void Render()
        {
            // Dessiner l'image d'arrière-plan une seule fois et la conserver en mémoire
            if (backgroundLoaded && backgroundImage != null)
            {
                airspace.Graphics.DrawImage(backgroundImage, new Rectangle(0, 0, WIDTH, HEIGHT));
            }

            // Dessiner les zombies
            foreach (Zombie zombie in fleet)
            {
                zombie.Render(airspace);
            }

            airspace.Render();
        }

        // Calcul du nouvel état après que 'interval' millisecondes se sont écoulées
        private void Update(int interval)
        {
            foreach (Zombie zombie in fleet)
            {
                zombie.Update(interval);
            }
        }

        // Méthode appelée à chaque frame
        private void NewFrame(object sender, EventArgs e)
        {
            this.Update(ticker.Interval);
            this.Render();
        }
    }
}
