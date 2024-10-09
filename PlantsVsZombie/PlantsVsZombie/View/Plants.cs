using System;
using System.Drawing;

namespace PlantsVsZombie
{
    public partial class DrawPlants
    {
        private Image droneImage;
        private bool isSelected; // Indicateur de sélection

        public DrawPlants()
        {
            // Charger l'image animée de la plante
            droneImage = Image.FromFile(@"..\..\..\Images PVZ\mainPlant.png");
            isSelected = false; // Initialiser la sélection à faux
        }

        public void Select()
        {
            isSelected = true; // Marquer comme sélectionnée
        }

        public void Deselect()
        {
            isSelected = false; // Marquer comme désélectionnée
        }

        public bool IsSelected()
        {
            return isSelected; // Retourne si la plante est sélectionnée
        }

        public void Render(BufferedGraphics drawingSpace)
        {
            // Dessiner l'image de la plante
            if (droneImage != null)
                drawingSpace.Graphics.DrawImage(droneImage, new Rectangle(x - 16, y - 16, 85, 128));

            // Dessiner un contour si la plante est sélectionnée
            if (isSelected)
            {
                Pen pen = new Pen(Color.Red, 2); // Contour rouge
                drawingSpace.Graphics.DrawRectangle(pen, new Rectangle(x - 16, y - 16, 85, 128));
            }
        }
    }

}
