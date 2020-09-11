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
        private double playerSpeed = 10;

        /// <summary>
        /// A shared black pen for drawing
        /// </summary>
        private Bitmap image = SpaceInvaders.Properties.Resources.ship3;

        private bool alive = true;

        #endregion

        #region Constructor
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="x">start position x</param>
        /// <param name="y">start position y</param>
        public Player(double x, double y) : base()
        {
            this.x = x;
            this.y = y;
        }
        #endregion

        #region Methods

        public override void Update(Game gameInstance, double deltaT)
        {
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
            x += 10;
        }
        public override void goLeft()
        {
            x -= 10;
        }

        #endregion
    }
}
