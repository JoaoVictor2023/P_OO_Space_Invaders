using PlantsVsZombie.Helpers;

namespace PlantsVsZombie
{
    public partial class Zombie
    {
        private Image droneImage;
        private Pen droneBrush = new Pen(new SolidBrush(Color.Purple), 3);

        public Zombie()
        {
            // Charger l'image du drone (vous devez fournir le chemin de l'image)
            droneImage = Image.FromFile("C:\\Users\\pg05lby\\Documents\\GitHub\\P_OO_Space_Invaders\\Images PVZ\\walkMainZombie.gif");
        }

        public void Render(BufferedGraphics drawingSpace)
        {
            // Dessiner l'image du drone au lieu de l'ellipse
            if (droneImage != null)
            {
                drawingSpace.Graphics.DrawImage(droneImage, new Rectangle(x - 16, y - 16, 85, 128));
            }

            // Afficher le texte
            //drawingSpace.Graphics.DrawString($"{this}", TextHelpers.drawFont, TextHelpers.writingBrush, x + 5, y - 5);
        }

        //public override string ToString()
        //{
        //    return $"{name} ({((int)((double)charge / 1000 * 100)).ToString()}%)";
        //}
    }
}
