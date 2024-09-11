using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace PlantsVsZombie
{
    public partial class Zombie : Form
    {
        private Image droneImage;
        private Timer animationTimer;

        public Zombie()
        {
            // Charger l'image animée du drone
            droneImage = Image.FromFile("C:\\Users\\pg05lby\\Documents\\GitHub\\P_OO_Space_Invaders\\Images PVZ\\walkMainZombie.gif");

            // Initialiser le Timer pour mettre à jour les frames
            animationTimer = new Timer();
            animationTimer.Interval = 100; // Ajustez l'intervalle en fonction de la vitesse de l'animation
            animationTimer.Tick += new EventHandler(OnAnimationTick);
            animationTimer.Start();

            // Démarrer l'animation
            ImageAnimator.Animate(droneImage, OnFrameChanged);
        }

        private void OnAnimationTick(object sender, EventArgs e)
        {
            // Mettre à jour les frames de l'animation
            ImageAnimator.UpdateFrames();
            // Redessiner le contrôle pour afficher la nouvelle frame
            this.Invalidate();
        }

        private void OnFrameChanged(object o, EventArgs e)
        {
            // Redessiner le contrôle pour afficher la nouvelle frame
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Dessiner l'image animée du drone
            if (droneImage != null)
            {
                // Mettre à jour l'animation avant de dessiner
                ImageAnimator.UpdateFrames();
                e.Graphics.DrawImage(droneImage, new Rectangle(0, 0, 75, 150)); // Ajustez la position et la taille selon vos besoins
            }
        }

        [STAThread]
        public static void Main()
        {
            Application.Run(new Zombie());
        }
    }
}
