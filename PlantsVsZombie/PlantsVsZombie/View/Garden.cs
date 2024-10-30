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
        private int? planteSelectionnee = null; // Index de la plante s�lectionn�e, ou null si aucune s�lection

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

        string[] plantTexts = new string[]
        {
            "100", "50", "50", "150", "200"
        };

        private void Garden_MouseClick(object sender, MouseEventArgs e)
        {
            if (planteSelectionnee.HasValue)
            {
                // Cr�er et ajouter la plante � la position du clic dans le cadre de jeu
                var nouvellePlante = new DrawPlants(plantImages[planteSelectionnee.Value], e.Location);
                fleetPlantes.Add(nouvellePlante);

                // D�selectionner la plante
                planteSelectionnee = null;

                // Redessiner pour afficher la nouvelle plante
                Render();
            }
            else
            {
                // D�finir les dimensions de chaque rectangle orange
                int rectWidth = 165;
                int rectHeight = 90;

                // Positions Y des rectangles
                int startY = 67; // Position de d�part
                int spacing = 15; // Espacement entre les rectangles

                // Co�ts des plantes
                int[] coutPlantes = { 100, 50, 50, 150, 200 };

                // V�rifiez si le clic de la souris est dans chaque rectangle
                for (int i = 0; i < coutPlantes.Length; i++)
                {
                    int rectX = 50; // Position X fixe pour tous les rectangles
                    int rectY = startY + i * (rectHeight + spacing);

                    // V�rifiez si le clic de la souris est dans le rectangle
                    if (e.X >= rectX && e.X <= rectX + rectWidth && e.Y >= rectY && e.Y <= rectY + rectHeight)
                    {
                        // R�duire l'argent du joueur par le co�t de la plante
                        argentJoueur -= coutPlantes[i];

                        // Enregistrer l�index de la plante s�lectionn�e
                        planteSelectionnee = i;

                        // Si c'est l'item 2 (index 1 du tableau), apr�s 2 secondes, augmenter l'argent du joueur de 25
                        if (i == 1) // Index 1 correspond � l'item 2
                        {
                            clicsItem2++; // Incr�menter le nombre de clics sur l'item 2

                            // D�marrer une t�che qui va continuer � augmenter l'argent toutes les 2 secondes
                            Task.Run(async () =>
                            {
                                while (true)
                                {
                                    await Task.Delay(2000); // Attendre 2 secondes

                                    // Mettre � jour l'argent dans le thread principal de l'UI
                                    this.Invoke((Action)(() =>
                                    {
                                        argentJoueur += 25; // Ajouter le montant calcul�

                                        // Mettre � jour l'affichage de l'argent et des vies
                                        string texteStatut = $"Argent: {argentJoueur} | Vies: {viesJoueur}";
                                        Font policeStatut = new Font("Arial", 20);
                                        Brush pinceauStatut = Brushes.Black;
                                        airspace.Graphics.DrawString(texteStatut, policeStatut, pinceauStatut, 10, 10); // Position sur l'�cran
                                        airspace.Render(); // Rafra�chir l'affichage
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

            // Dessiner les plantes plac�es
            foreach (DrawPlants plante in fleetPlantes)
            {
                plante.Render(airspace);
            }

            airspace.Render();
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
        }

        // M�thode appel�e � chaque frame
        private void NewFrame(object sender, EventArgs e)
        {
            this.Update(ticker.Interval);
            this.Render();
        }
    }
}
