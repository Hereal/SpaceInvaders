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
    class Player : GameObject
    {
        #region Fields
        /// <summary>
        /// Position
        /// </summary>
        private double x, y;


        /// <summary>
        /// Ball speed in pixel/second
        /// </summary>
        private double playerSpeed = 100;

        private bool alive = true;

        private Bitmap image ;

        private Missile missile = null;

        private System.Media.SoundPlayer player;

        #endregion

        #region Constructor
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="x">start position x</param>
        /// <param name="y">start position y</param>
        public Player(double x, double y) : base()
        {
            image = SpaceInvaders.Properties.Resources.player;
            player = new System.Media.SoundPlayer(@"..\..\Resources\sound\shoot.wav");
            
            this.x = x;
            this.y = y;
        }
        #endregion

        #region Methods

        public override void Update(Game gameInstance, double deltaT)
        {
            if (y > gameInstance.gameSize.Height)
                alive = false;
        }

        public override void Draw(Game gameInstance, Graphics graphics)
        {
            GraphManager.DrawBufferedImage(gameInstance, image, (int)x, (int)y);
            
        }

        public override bool IsAlive()
        {
            return alive;
        }

        public override void Kill()
        {
            alive = false;
        }

        public override void MoveRight(Game gameInstance, double deltaT)
        {
            x += playerSpeed * deltaT ;
            if (x > GraphManager.bufferedImage.Width - image.Width)
                x = GraphManager.bufferedImage.Width - image.Width;

        }
        public override void MoveLeft(Game gameInstance, double deltaT)
        {
            x -= playerSpeed * deltaT;
            if (x < 0)
                x= 0;
        }

        public override void Shoot(Game gameInstance, double deltaT)
        {
            if (missile == null || missile.IsAlive() == false)
            {
                missile = new Missile(x + 5, y, true);
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
