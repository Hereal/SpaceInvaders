using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SpaceInvaders.Manager;

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
        private double x, y;


        /// <summary>
        /// Ball speed in pixel/second
        /// </summary>
        private double shipSpeed = 100;

        private Int64 killTimer = 0;

        private bool alive = true;

        private Bitmap image ;
        private Bitmap imageExplosion;

        private Missile missile = null;

        private System.Media.SoundPlayer player;

        #endregion

        #region Constructor
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="x">start position x</param>
        /// <param name="y">start position y</param>
        public Ship(double x, double y) : base()
        {
            image = SpaceInvaders.Properties.Resources.ship20;
            imageExplosion = SpaceInvaders.Properties.Resources.explosion;
            player = new System.Media.SoundPlayer(@"..\..\Resources\sound\shoot.wav");
            killTimer = 0;
            this.x = x;
            this.y = y;
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
                GraphManager.DrawBufferedImage(gameInstance, imageExplosion, (int)x, (int)y);
            }
            else
            {
                GraphManager.DrawBufferedImage(gameInstance, image, (int)x, (int)y);
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
            x += shipSpeed * deltaT ;
            if (x > GraphManager.bufferedImage.Width - image.Width)
                x = GraphManager.bufferedImage.Width - image.Width;

        }
        public override void MoveLeft(Game gameInstance, double deltaT)
        {
            x -= shipSpeed * deltaT;
            if (x < 0)
                x= 0;
        }

        public override void Shoot(Game gameInstance, double deltaT)
        {
            if (missile == null || missile.IsAlive() == false)
            {
                missile = new Missile(x + 5, y, false);
                gameInstance.AddNewGameObject(missile);
                player.Play();
            }
        }
        public override Bitmap GetImage()
        {
            return image;
        }
        public override Point GetCoord()
        {
            return new Point((int)x, (int)y);
        }

        #endregion
    }
}
