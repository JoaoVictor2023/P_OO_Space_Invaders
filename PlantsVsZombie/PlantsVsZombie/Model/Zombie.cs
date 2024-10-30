using System;
using System.Drawing;

namespace PlantsVsZombie
{
    // Cette partie de la classe Zombie définit ce qu'est un zombie par un modèle numérique
    public partial class DrawZombie
    {
        Random alea = new Random();

        public string name;                             // Un nom
        public int x;                                   // Position en X depuis la gauche de l'espace aérien
        public int y;                                   // Position en Y depuis le haut de l'espace aérien
        private Rectangle collisionRectangle;           // Zone de collision du zombie
        public int speed = 2;                               // Vitesse du zombie

        // Constructeur
        public DrawZombie(string name, Point position, int speed)
        {
            this.name = name;
            this.x = position.X;
            this.y = position.Y;
            this.speed = speed;
            collisionRectangle = new Rectangle(x, y, 50, 100); // Ajustez la taille du rectangle de collision
        }

        // Cette méthode calcule le nouvel état dans lequel le zombie se trouve après
        // que 'interval' millisecondes se sont écoulées
        public void Update(int interval)
        {
            x -= speed;                                  // Il s'est déplacé vers la gauche selon sa vitesse

            // Met à jour la zone de collision
            collisionRectangle.Location = new Point(x, y);
        }

        public Rectangle GetCollisionRectangle()
        {
            return collisionRectangle; // Retourner la zone de collision
        }

        public void StopMoving()
        {
            speed = 0; // Arrête le mouvement du zombie
        }
    }
}
