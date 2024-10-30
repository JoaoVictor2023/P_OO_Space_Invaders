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
        private BufferedGraphicsContext currentContext;
        private BufferedGraphics airspace;
        private List<DrawZombie> zombiesASupprimer = new List<DrawZombie>();
        private int clicsItem2 = 0;

        private Image backgroundImage;
        private bool backgroundLoaded = false;
        private int? planteSelectionnee = null; // Index de la plante sélectionnée, ou null si aucune sélection

        public Garden(List<DrawZombie> fleet, DrawBackgroundPlants fond)
        {
            InitializeComponent();

            // Charger l'image d'arrière-plan et d'autres initialisations
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

        string[] plantTexts = new string[]
        {
            "100", "50", "50", "150", "200"
        };

        private void Garden_MouseClick(object sender, MouseEventArgs e)
        {
            if (planteSelectionnee.HasValue)
            {
                // Créer et ajouter la plante à la position du clic dans le cadre de jeu
                var nouvellePlante = new DrawPlants(plantImages[planteSelectionnee.Value], e.Location);
                fleetPlantes.Add(nouvellePlante);

                // Déselectionner la plante
                planteSelectionnee = null;

                // Redessiner pour afficher la nouvelle plante
                Render();
            }
            else
            {
                // Définir les dimensions de chaque rectangle orange
                int rectWidth = 165;
                int rectHeight = 90;

                // Positions Y des rectangles
                int startY = 67; // Position de départ
                int spacing = 15; // Espacement entre les rectangles

                // Coûts des plantes
                int[] coutPlantes = { 100, 50, 50, 150, 200 };

                // Vérifiez si le clic de la souris est dans chaque rectangle
                for (int i = 0; i < coutPlantes.Length; i++)
                {
                    int rectX = 50; // Position X fixe pour tous les rectangles
                    int rectY = startY + i * (rectHeight + spacing);

                    // Vérifiez si le clic de la souris est dans le rectangle
                    if (e.X >= rectX && e.X <= rectX + rectWidth && e.Y >= rectY && e.Y <= rectY + rectHeight)
                    {
                        // Réduire l'argent du joueur par le coût de la plante
                        argentJoueur -= coutPlantes[i];

                        // Enregistrer l’index de la plante sélectionnée
                        planteSelectionnee = i;

                        // Si c'est l'item 2 (index 1 du tableau), après 2 secondes, augmenter l'argent du joueur de 25
                        if (i == 1) // Index 1 correspond à l'item 2
                        {
                            clicsItem2++; // Incrémenter le nombre de clics sur l'item 2

                            // Démarrer une tâche qui va continuer à augmenter l'argent toutes les 2 secondes
                            Task.Run(async () =>
                            {
                                while (true)
                                {
                                    await Task.Delay(2000); // Attendre 2 secondes

                                    // Mettre à jour l'argent dans le thread principal de l'UI
                                    this.Invoke((Action)(() =>
                                    {
                                        argentJoueur += 25; // Ajouter le montant calculé

                                        // Mettre à jour l'affichage de l'argent et des vies
                                        string texteStatut = $"Argent: {argentJoueur} | Vies: {viesJoueur}";
                                        Font policeStatut = new Font("Arial", 20);
                                        Brush pinceauStatut = Brushes.Black;
                                        airspace.Graphics.DrawString(texteStatut, policeStatut, pinceauStatut, 10, 10); // Position sur l'écran
                                        airspace.Render(); // Rafraîchir l'affichage
                                    }));
                                }
                            });
                        }
                        break;
                    }
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
            airspace.Graphics.DrawString(texteStatut, policeStatut, pinceauStatut, 10, 10);

            // Dessiner les zombies
            foreach (DrawZombie zombie in fleet.ToList())
            {
                zombie.Render(airspace);
            }

            // Dessiner les plantes
            fond.Render(airspace, plantImages, plantTexts);

            // Dessiner les plantes placées
            foreach (DrawPlants plante in fleetPlantes)
            {
                plante.Render(airspace);
            }

            airspace.Render();
        }

        // Calcul du nouvel état après que 'interval' millisecondes se sont écoulées
        private void Update(int interval)
        {
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
                        Environment.Exit(0);
                    }
                }
            }

            // Supprimer les zombies hors limites
            foreach (DrawZombie zombie in zombiesASupprimer)
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
