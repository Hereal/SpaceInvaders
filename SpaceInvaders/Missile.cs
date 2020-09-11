using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SpaceInvaders
{
    /// <summary>
    /// Dummy class for demonstration
    /// </summary>
    class Missile : GameObject
    {
        #region Fields
        /// <summary>
        /// Position
        /// </summary>
        private double x, y;

        /// <summary>
        /// Ball speed in pixel/second
        /// </summary>
        private double missileSpeed = 1;

        /// <summary>
        /// A shared black pen for drawing
        /// </summary>
        private Bitmap image = SpaceInvaders.Properties.Resources.shoot1;

        private bool alive = true;

        private bool up = true;

        #endregion

        #region Constructor
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="x">start position x</param>
        /// <param name="y">start position y</param>
        public Missile(double x, double y,bool up) : base()
        {
            this.x = x;
            this.y = y;
            this.up = up;
        }
        #endregion

        #region Methods

        public override void Update(Game gameInstance, double deltaT)
        {
            //Deplacement du missile
            if (up) {
                y -= missileSpeed; 
            }
            else { 
                y += missileSpeed; 
            }
            //Destruction du missile s'il sort de la fenetre
            if (y < 0 - image.Height || y > gameInstance.gameSize.Height)
            {
                alive = false;
            }
        }

        public override void Draw(Game gameInstance, Graphics graphics)
        {
            graphics.DrawImage(image, (int)x, (int)y, image.Width, image.Height);
            ArrayGraphics.Draw(this, image, (int)x, (int)y);
        }

        public override bool IsAlive()
        {
            return alive;
        }
        public override void goRight(Game gameInstance)
        {
        }
        public override void goLeft(Game gameInstance)
        {
        }

        public override void shoot(Game gameInstance)
        {
        }

        #endregion
    }
}
