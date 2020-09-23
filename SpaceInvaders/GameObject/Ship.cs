using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SpaceInvaders.Manager;
using System.Windows.Media;

namespace SpaceInvaders
{
    /// <summary>
    /// Dummy class for demonstration
    /// </summary>
    class Ship : GameObject
    {
        #region Fields
        /// <summary>
        /// Position
        /// </summary>
        public Vecteur2D vector = new Vecteur2D(0, 0);


        /// <summary>
        /// Ball speed in pixel/second
        /// </summary>
        private double shipSpeed = 100;

        private Int64 killTimer = 0;

        private bool alive = true;

        private Bitmap image;
        private Bitmap imageExplosion;

        private Missile missile = null;

        private MediaPlayer media = new MediaPlayer();

        #endregion

        #region Constructor
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="x">start position x</param>
        /// <param name="y">start position y</param>
        public Ship(double x, double y) : base()
        {
            image = SpaceInvaders.Properties.Resources.tieFighter;
            imageExplosion = SpaceInvaders.Properties.Resources.explosion;
            killTimer = 0;
            vector = new Vecteur2D(x, y);
        }
        #endregion

        #region Methods

        public override void Update(Game gameInstance, double deltaT)
        {
            Shoot(gameInstance, deltaT);
        }

        public override void Draw(Game gameInstance, Graphics graphics)
        {

            if (killTimer != 0)
            {
                GraphManager.DrawBufferedImage(gameInstance, imageExplosion, (int)vector.x, (int)vector.y);
            }
            else
            {
                GraphManager.DrawBufferedImage(gameInstance, image, (int)vector.x, (int)vector.y);
            }

        }

        public override bool IsAlive()
        {
            if (Int64.Parse(Utils.GetTimestamp(DateTime.Now)) >= killTimer + 1000 && killTimer != 0)
                alive = false;

            return alive;
        }

        public override void Kill()
        {
            killTimer = Int64.Parse(Utils.GetTimestamp(DateTime.Now));
        }

        public override void MoveRight(Game gameInstance, double deltaT)
        {
            vector.x += shipSpeed * deltaT;
            if (vector.x > GraphManager.bufferedImage.Width - image.Width)
                vector.x = GraphManager.bufferedImage.Width - image.Width;

        }
        public override void MoveLeft(Game gameInstance, double deltaT)
        {
            vector.x -= shipSpeed * deltaT;
            if (vector.x < 0)
                vector.x = 0;
        }

        public override void Shoot(Game gameInstance, double deltaT)
        {
            if (missile == null || missile.IsAlive() == false)
            {
                media.Open(new Uri(Path.Combine(Environment.CurrentDirectory, @"..\..\Resources\sound\shoot.wav")));
                media.Play();
                missile = new Missile((int)vector.x + 7, (int)vector.y + 16, false);
                gameInstance.AddNewGameObject(missile);
            }
        }
        public override Bitmap GetImage()
        {
            return image;
        }

        #endregion
    }
}
