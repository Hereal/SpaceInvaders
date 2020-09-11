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
        private double playerSpeed = 10;

        /// <summary>
        /// A shared black pen for drawing
        /// </summary>
        private Bitmap image = SpaceInvaders.Properties.Resources.ship1;

        private bool alive = true;

        private Missile missile = null;

        #endregion

        #region Constructor
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="x">start position x</param>
        /// <param name="y">start position y</param>
        public Ship(double x, double y) : base()
        {
            this.x = x;
            this.y = y;
        }
        #endregion

        #region Methods

        public override void Update(Game gameInstance, double deltaT)
        {
            shoot(gameInstance);
        }

        public override void Draw(Game gameInstance, Graphics graphics)
        {
            graphics.DrawImage(image, (int)x, (int)y, image.Width, image.Height);
        }

        public override bool IsAlive()
        {
            return alive;
        }
        public override void goRight()
        {
        }
        public override void goLeft()
        {
        }

        public override void shoot(Game gameInstance)
        {
            if (missile == null) {
                missile = new Missile((int)(x + (image.Width / 2.0)), y, false);
                gameInstance.AddNewGameObject(missile);
            }
            else if (!missile.IsAlive())
            {
                missile = new Missile((int)(x + (image.Width / 2.0)), y, false);
                gameInstance.AddNewGameObject(missile);
            }
        }

        #endregion
    }
}
