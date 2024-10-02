using System.Drawing;
namespace PlantsVsZombie
{
    public partial class Garden : Form
    {
        private int argentJoueur = 100; // Le joueur commence avec 100 d'argent
        private int viesJoueur = 3; // Le joueur commence avec 3 vies
        private List<DrawZombie> fleet;
        private List<DrawPlants> fleetPlantes;
        private DrawBackgroundPlants fond;
        private Image[] plantImages;
        BufferedGraphicsContext currentContext;
        BufferedGraphics airspace;

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
                "100",
                "50",
                "50",
                "150",
                "200"
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

            fond.Render(airspace, plantImages, plantTexts);

            airspace.Render();
        }

        private bool PeutAcheterPlante(int coutPlante)
        {
            return argentJoueur >= coutPlante;
        }
        private bool PeutAcheterPlanteSolei(int coutPlanteSoleil)
        {
            return argentJoueur >= coutPlanteSoleil;
        }
        private bool PeutAcheterPlanteWallNut(int coutPlanteWallNut)
        {
            return argentJoueur >= coutPlanteWallNut;
        }
        private bool PeutAcheterPlanteGlace(int coutPlanteGlace)
        {
            return argentJoueur >= coutPlanteGlace;
        }
        private bool PeutAcheterPlanteDouble(int coutPlanteDouble)
        {
            return argentJoueur >= coutPlanteDouble;
        }

        private void EssayerPlacerPlante(int indicePlante)
        {
            int coutPlante = 100;            // Exemple de coût pour une plante
            int coutPlanteSoleil = 50;       // Exemple de coût pour une plante soleil
            int coutPlanteWallNut = 50;      // Exemple de coût pour une plante wallnut
            int coutPlanteGlace = 150;       // Exemple de coût pour une plante glace
            int coutPlanteDouble = 200;      // Exemple de coût pour une plante double

            if (PeutAcheterPlante(coutPlante))
            {
                // Réduire l'argent du joueur
                argentJoueur -= coutPlante;

                // Logique pour placer la plante
                // fleetPlantes.Add(new DrawPlants(...));

                MessageBox.Show("Plante placée !");
            }
            else if (PeutAcheterPlanteSolei(coutPlanteSoleil))
            {
                // Réduire l'argent du joueur
                argentJoueur -= coutPlanteSoleil;

                // Logique pour placer la plante
                // fleetPlantes.Add(new DrawPlants(...));

                MessageBox.Show("Plante placée !");
            }
            else if (PeutAcheterPlanteWallNut(coutPlanteWallNut))
            {
                // Réduire l'argent du joueur
                argentJoueur -= coutPlanteWallNut;

                // Logique pour placer la plante
                // fleetPlantes.Add(new DrawPlants(...));

                MessageBox.Show("Plante placée !");
            }
            else if (PeutAcheterPlanteGlace(coutPlanteGlace))
            {
                // Réduire l'argent du joueur
                argentJoueur -= coutPlanteGlace;

                // Logique pour placer la plante
                // fleetPlantes.Add(new DrawPlants(...));

                MessageBox.Show("Plante placée !");
            }
            else if (PeutAcheterPlanteDouble(coutPlanteDouble))
            {
                // Réduire l'argent du joueur
                argentJoueur -= coutPlanteDouble;

                // Logique pour placer la plante
                // fleetPlantes.Add(new DrawPlants(...));

                MessageBox.Show("Plante placée !");
            }
            else
            {
                MessageBox.Show("Argent insuffisant !");
            }
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (false)
            {
                // Vérifiez si une plante a été cliquée
                foreach (var plant in fleetPlantes)
                {
                    // Vérifiez si la souris a cliqué sur la plante
                    if (e.X >= plant.x - 16 && e.X <= plant.x + 69 && e.Y >= plant.y - 16 && e.Y <= plant.y + 112)
                    {
                        // Sélectionnez la plante
                        plant.Select();
                        break; // Sortez de la boucle après avoir sélectionné une plante
                    }
                }

                // Si une plante est sélectionnée, déplacez-la
                foreach (var plant in fleetPlantes)
                {
                    if (plant.IsSelected())
                    {
                        // Mettez à jour la position de la plante
                        plant.x = e.X; // Ajustez la position X
                        plant.y = e.Y; // Ajustez la position Y
                        plant.Deselect(); // Désélectionnez après le placement
                        break; // Sortez de la boucle après avoir déplacé la plante
                    }
                }

            }
        }
        // Calcul du nouvel état après que 'interval' millisecondes se sont écoulées
        private void Update(int interval)
        {
            List<DrawZombie> zombiesASupprimer = new List<DrawZombie>();
            var zombiesAMettreAJour = new List<DrawZombie>(fleet);

            foreach (DrawZombie zombie in zombiesAMettreAJour)
            {
                zombie.Update(interval);

                // Vérifiez si le zombie est hors limites
                if (zombie.IsOutOfBounds(300))
                {
                    zombiesASupprimer.Add(zombie);
                    viesJoueur--; // Réduire une vie lorsque le zombie dépasse les limites

                    if (viesJoueur <= 0)
                    {
                        MessageBox.Show("Vous avez perdu !");
                        Application.Exit(); // Ferme l'application
                    }
                }
            }

            // Supprimer les zombies hors limites
            foreach (DrawZombie zombie in zombiesASupprimer)
            {
                fleet.Remove(zombie);
            }
        } //fin Update


        // Méthode appelée à chaque frame
        private void NewFrame(object sender, EventArgs e)
        {
            this.Update(ticker.Interval);
            this.Render();
        }
    } //fin class garden
}
