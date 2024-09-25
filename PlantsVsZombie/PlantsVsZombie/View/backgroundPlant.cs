using System.Drawing;
using System.Drawing.Drawing2D;

namespace PlantsVsZombie
{
    public partial class DrawBackgroundPlants
    {
        int x;
        int y;

        public bool IsOutOfBounds(int widthThreshold)
        {
            return x <= widthThreshold;
        }

        public void Render(BufferedGraphics drawingSpace, Image[] plantImages)
        {
            // Couleur de fond marron
            Color backgroundColor = ColorTranslator.FromHtml("#643410");

            // Dessiner le rectangle de fond marron
            Rectangle backgroundRect = new Rectangle(x - 16, y - 16, 197, 650);
            using (Brush backgroundBrush = new SolidBrush(backgroundColor))
            {
                drawingSpace.Graphics.FillRectangle(backgroundBrush, backgroundRect);
            }

            // Dessiner les petits rectangles orange à l'intérieur avec des coins arrondis
            int rectWidth = 165;
            int rectHeight = 90;
            int rectSpacing = 15; // Espacement ajusté entre les rectangles
            int startX = x; // Position X de départ des rectangles
            int startY = y + 75; // Ajuster l'espace du haut à 67 pixels
            int cornerRadius = 20; // Rayon des coins arrondis

            using (Brush rectangleBrush = new SolidBrush(ColorTranslator.FromHtml("#d96b1c")))
            {
                for (int i = 0; i < 5; i++) // Dessiner 5 petits rectangles
                {
                    // Calculer la position de chaque petit rectangle
                    int rectX = startX + (197 - rectWidth) / 2 - 10; // Déplacez de 20 pixels à gauche
                    int rectY = startY + i * (rectHeight + rectSpacing); // Espacement ajusté entre les rectangles

                    // Dessiner le rectangle avec des coins arrondis
                    GraphicsPath path = RoundedRectangle(rectX, rectY, rectWidth, rectHeight, cornerRadius);
                    drawingSpace.Graphics.FillPath(rectangleBrush, path);

                    // Dessiner l'image à l'intérieur du rectangle, à gauche
                    Image image = plantImages[i % plantImages.Length]; // Assurez-vous de ne pas dépasser la longueur du tableau
                    int imageX = rectX + 5; // Position X de l'image (décalé de 10 pixels à partir de la gauche)
                    int imageY = rectY + (rectHeight - image.Height) / 2; // Centrer verticalement
                    drawingSpace.Graphics.DrawImage(image, new Rectangle(imageX, imageY, image.Width, image.Height)); // Dessiner l'image
                }
            }
        }


        // Méthode pour dessiner un rectangle avec des coins arrondis
        private GraphicsPath RoundedRectangle(int x, int y, int width, int height, int radius)
        {
            GraphicsPath path = new GraphicsPath();

            // Coin supérieur gauche
            path.AddArc(x, y, radius, radius, 180, 90);
            // Bord supérieur
            path.AddLine(x + radius, y, x + width - radius, y);
            // Coin supérieur droit
            path.AddArc(x + width - radius, y, radius, radius, 270, 90);
            // Bord droit
            path.AddLine(x + width, y + radius, x + width, y + height - radius);
            // Coin inférieur droit
            path.AddArc(x + width - radius, y + height - radius, radius, radius, 0, 90);
            // Bord inférieur
            path.AddLine(x + width - radius, y + height, x + radius, y + height);
            // Coin inférieur gauche
            path.AddArc(x, y + height - radius, radius, radius, 90, 90);
            // Bord gauche
            path.AddLine(x, y + height - radius, x, y + radius);

            path.CloseFigure();
            return path;
        }
    }
}
