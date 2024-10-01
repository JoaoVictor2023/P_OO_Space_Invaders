using System.Drawing;
namespace PlantsVsZombie
{
    public partial class Garden : Form
    {

        // La flotte est l'ensemble des zombies dans notre jardin
        private List<DrawZombie> fleet;
        private List<DrawPlants> fleetPlantes;
        private DrawBackgroundPlants fond;
        private Image[] plantImages;

        BufferedGraphicsContext currentContext;
        BufferedGraphics airspace;

        // Image d'arrière-plan
        private Image backgroundImage;
        private bool backgroundLoaded = false;

        // Initialisation du jardin avec un certain nombre de zombies
        public Garden(List<DrawZombie> fleet, DrawBackgroundPlants fond)
        {
            InitializeComponent();

            // Charger l'image d'arrière-plan
            backgroundImage = Image.FromFile("C:\\Users\\pg05lby\\Documents\\GitHub\\P_OO_Space_Invaders\\Images PVZ\\backgroundJeu.png");
            backgroundLoaded = true;

            // Charger les images pour les petits rectangles
            plantImages = new Image[]
            {
                Image.FromFile("C:\\Users\\pg05lby\\Documents\\GitHub\\P_OO_Space_Invaders\\Images PVZ\\mainPlantPetit.png"),
                Image.FromFile("C:\\Users\\pg05lby\\Documents\\GitHub\\P_OO_Space_Invaders\\Images PVZ\\sunFlowerPetit.png"),
                Image.FromFile("C:\\Users\\pg05lby\\Documents\\GitHub\\P_OO_Space_Invaders\\Images PVZ\\wallNutPetit.png"),
                Image.FromFile("C:\\Users\\pg05lby\\Documents\\GitHub\\P_OO_Space_Invaders\\Images PVZ\\blueMainPlantPetit.png"),
                Image.FromFile("c:\\users\\pg05lby\\documents\\github\\p_oo_space_invaders\\images pvz\\mainplants2xPetit.png"),
            };

            


            // Gets a reference to the current BufferedGraphicsContext
            currentContext = BufferedGraphicsManager.Current;
            // Creates a BufferedGraphics instance associated with this form, and with
            // dimensions the same size as the drawing surface of the form.
            airspace = currentContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);
            this.fleet = fleet;
            this.fleetPlantes = fleetPlantes;
            this.fond = fond;
        }
            string[] plantTexts = new string[]
            {
                $"50 {Image.FromFile("C:\\Users\\pg05lby\\Documents\\GitHub\\P_OO_Space_Invaders\\Images PVZ\\wallNutPetit.png")}",
                "Sunflower",
                "Wall-Nut",
                "Ice Shooter",
                "Cherry Bomb"
            };

        // Affichage de la situation actuelle
        private void Render()
        {
            // Dessiner l'image d'arrière-plan une seule fois et la conserver en mémoire
            if (backgroundLoaded && backgroundImage != null)
            {
                airspace.Graphics.DrawImage(backgroundImage, new Rectangle(0, 0, TextHelpers.WIDTH, TextHelpers.HEIGHT));
            }

            // Dessiner les zombies
            foreach (DrawZombie zombie in fleet.ToList())
            {
                zombie.Render(airspace);
            }

            // Dessiner les plantes (utiliser le tableau d'images pour les petits rectangles)
            fond.Render(airspace, plantImages, plantTexts);

            airspace.Render();
        }

        // Calcul du nouvel état après que 'interval' millisecondes se sont écoulées
        private void Update(int interval)
        {
            // Créer une liste temporaire pour stocker les zombies à retirer
            List<DrawZombie> zombiesToRemove = new List<DrawZombie>();

            // Utiliser une copie de la liste pour éviter les modifications concurrentes
            var zombiesToUpdate = new List<DrawZombie>(fleet);

            foreach (DrawZombie zombie in zombiesToUpdate)
            {
                zombie.Update(interval);

                // Vérifier si le zombie est hors des limites et doit être retiré
                if (zombie.IsOutOfBounds(300)) // 300 pixels de largeur comme seuil
                {
                    zombiesToRemove.Add(zombie);
                }
            }

            // Supprimer les zombies hors des limites
            foreach (DrawZombie zombie in zombiesToRemove)
            {
                fleet.Remove(zombie);
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
