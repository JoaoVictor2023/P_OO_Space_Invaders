using Microsoft.VisualBasic.Devices;
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
        List<DrawZombie> zombiesASupprimer = new List<DrawZombie>();


        private Image backgroundImage;
        private bool backgroundLoaded = false;


        public Garden(List<DrawZombie> fleet, DrawBackgroundPlants fond)
        {
            InitializeComponent();

            // Charger l'image d'arri�re-plan et d'autres initialisations
            backgroundImage = Image.FromFile(@"..\..\..\Images PVZ\backgroundJeu.png");
            backgroundLoaded = true;


            // Initialisation des images et des variables
            plantImages = new Image[]
            {
                Image.FromFile(@"..\..\..\Images PVZ\mainPlantPetit.png"),
                Image.FromFile(@"..\..\..\Images PVZ\sunFlowerPetit.png"),
                Image.FromFile(@"..\..\..\Images PVZ\wallNutPetit.png"),
                Image.FromFile(@"..\..\..\Images PVZ\blueMainPlantPetit.png"),
                Image.FromFile(@"..\..\..\Images PVZ\mainplants2xPetit.png"),
            };

            // Gestion du BufferedGraphics
            currentContext = BufferedGraphicsManager.Current;
            airspace = currentContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);

            this.fleet = fleet;
            this.fleetPlantes = new List<DrawPlants>();
            this.fond = fond;
            this.MouseClick += new MouseEventHandler(Garden_MouseClick);
        }
        //fin garden
        string[] plantTexts = new string[]
        {
                "100",
                "50",
                "50",
                "150",
                "200"
        };
        private void Garden_MouseClick(object sender, MouseEventArgs e)
        {
            // D�finir les dimensions de chaque rectangle orange
            int rectWidth = 165;
            int rectHeight = 90;

            // Positions Y des rectangles
            int startY = 67; // Position de d�part
            int spacing = 15; // Espacement entre les rectangles

            // Co�ts des plantes
            int[] coutPlantes = { 100, 50, 50, 150, 200 }; // Co�ts des plantes

            // V�rifiez si le clic de la souris est dans chaque rectangle
            for (int i = 0; i < coutPlantes.Length; i++)
            {
                int rectX = 50; // Position X fixe pour tous les rectangles
                int rectY = startY + i * (rectHeight + spacing); // Calcul de la position Y

                // V�rifiez si le clic de la souris est dans le rectangle
                if (e.X >= rectX && e.X <= rectX + rectWidth && e.Y >= rectY && e.Y <= rectY + rectHeight)
                {
                    // R�duire l'argent du joueur par le co�t de la plante
                    argentJoueur -= coutPlantes[i];
                    if (true)
                    {

                    }
                    // Quittez la boucle apr�s avoir trouv� le rectangle cliqu�
                    break;
                }
            }
        }
        // Affichage de la situation actuelle
        private void Render()
        {
            // Dessiner le fond
            if (backgroundLoaded && backgroundImage != null)
            {
                airspace.Graphics.DrawImage(backgroundImage, new Rectangle(0, 0, TextHelpers.WIDTH, TextHelpers.HEIGHT));
            }

            // Afficher l'argent et les vies du joueur
            string texteStatut = $"Argent: {argentJoueur} | Vies: {viesJoueur}";
            Font policeStatut = new Font("Arial", 20);
            Brush pinceauStatut = Brushes.Black;
            airspace.Graphics.DrawString(texteStatut, policeStatut, pinceauStatut, 10, 10); // 10,10 correspond � la position sur l'�cran

            // Dessiner les zombies
            foreach (DrawZombie zombie in fleet.ToList())
            {
                zombie.Render(airspace);
            }

            // Dessiner les plantes
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
            int coutPlante = 100;            // Exemple de co�t pour une plante
            int coutPlanteSoleil = 50;       // Exemple de co�t pour une plante soleil
            int coutPlanteWallNut = 50;      // Exemple de co�t pour une plante wallnut
            int coutPlanteGlace = 150;       // Exemple de co�t pour une plante glace
            int coutPlanteDouble = 200;      // Exemple de co�t pour une plante double

            if (PeutAcheterPlante(coutPlante))
            {
                // R�duire l'argent du joueur
                argentJoueur -= coutPlante;

                // Logique pour placer la plante
                // fleetPlantes.Add(new DrawPlants(...));

                MessageBox.Show("Plante plac�e !");
            }
            else if (PeutAcheterPlanteSolei(coutPlanteSoleil))
            {
                // R�duire l'argent du joueur
                argentJoueur -= coutPlanteSoleil;

                // Logique pour placer la plante
                // fleetPlantes.Add(new DrawPlants(...));

                MessageBox.Show("Plante plac�e !");
            }
            else if (PeutAcheterPlanteWallNut(coutPlanteWallNut))
            {
                // R�duire l'argent du joueur
                argentJoueur -= coutPlanteWallNut;

                // Logique pour placer la plante
                // fleetPlantes.Add(new DrawPlants(...));

                MessageBox.Show("Plante plac�e !");
            }
            else if (PeutAcheterPlanteGlace(coutPlanteGlace))
            {
                // R�duire l'argent du joueur
                argentJoueur -= coutPlanteGlace;

                // Logique pour placer la plante
                // fleetPlantes.Add(new DrawPlants(...));

                MessageBox.Show("Plante plac�e !");
            }
            else if (PeutAcheterPlanteDouble(coutPlanteDouble))
            {
                // R�duire l'argent du joueur
                argentJoueur -= coutPlanteDouble;

                // Logique pour placer la plante
                // fleetPlantes.Add(new DrawPlants(...));

                MessageBox.Show("Plante plac�e !");
            }
            else
            {
                MessageBox.Show("Argent insuffisant !");
            }
        }

        // Calcul du nouvel �tat apr�s que 'interval' millisecondes se sont �coul�es
        private void Update(int interval)
        {

            var zombiesAMettreAJour = new List<DrawZombie>(fleet);

            foreach (DrawZombie zombie in zombiesAMettreAJour)
            {
                zombie.Update(interval);

                // V�rifiez si le zombie est hors limites
                if (zombie.IsOutOfBounds(300))
                {
                    zombiesASupprimer.Add(zombie);
                    viesJoueur--; // R�duire une vie lorsque le zombie d�passe les limites
                    if (viesJoueur <= 0)
                    {
                        Environment.Exit(0);
                    }
                }
            }

            // Supprimer les zombies hors limites
            foreach (DrawZombie zombie in zombiesASupprimer)
            {
                fleet.Remove(zombie);
            }
        } //fin Update


        // M�thode appel�e � chaque frame
        private void NewFrame(object sender, EventArgs e)
        {
            this.Update(ticker.Interval);
            this.Render();
        }
    }//fin class garden
}//fin namespace
