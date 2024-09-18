using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace PlantsVsZombie
{
    public partial class DrawZombie : Form
    {
        private Image droneImage;

        public DrawZombie()
        {
            // Charger l'image animée du drone
            droneImage = Image.FromFile("C:\\Users\\pg05lby\\Documents\\GitHub\\P_OO_Space_Invaders\\Images PVZ\\mainZombie.png");

        }
        public bool IsOutOfBounds(int widthThreshold)
        {
            return x <= widthThreshold;
        }
        public void Render(BufferedGraphics drawingSpace)
        {
            // Dessiner l'image du drone au lieu de l'ellipse
            if (droneImage != null)
                drawingSpace.Graphics.DrawImage(droneImage, new Rectangle(x - 16, y - 16, 85, 128));
            // Afficher le texte
            //drawingSpace.Graphics.DrawString($"{this}", TextHelpers.drawFont, TextHelpers.writingBrush, x + 5, y - 5);
        }

        private void OnFrameChanged(object o, EventArgs e)
        {
            // Redessiner le contrôle pour afficher la nouvelle frame
            this.Invalidate();
        }
    }
}
