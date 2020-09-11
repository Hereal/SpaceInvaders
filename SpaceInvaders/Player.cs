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
        private double playerSpeed = 1;

        /// <summary>
        /// A shared black pen for drawing
        /// </summary>
        private Bitmap image = SpaceInvaders.Properties.Resources.player;

        private bool alive = true;

        private Missile missile = null;

        private Random rnd = new Random();

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
            ArrayGraphics.Draw(this, image, (int)x, (int)y);
        }

        public override bool IsAlive()
        {
            return alive;
        }
        public override void goRight(Game gameInstance)
        {
            if ( x+image.Width+playerSpeed>gameInstance.gameSize.Width)
                return;
            x += playerSpeed;
        }
        public override void goLeft(Game gameInstance)
        {
            if (x + playerSpeed < 0 )
                return;
            x -= playerSpeed;
        }

        public override void shoot(Game gameInstance)
        {
            if (missile == null) {
                missile = new Missile((int)(x + (image.Width / 2.0)), y, true);
                gameInstance.AddNewGameObject(missile);
            }
            else if (!missile.IsAlive())
            {
                missile = new Missile((int)(x + (image.Width / 2.0)), y, true);
                gameInstance.AddNewGameObject(missile);
            }
        }

        #endregion
    }
}
