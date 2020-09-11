using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Linq.Expressions;

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
        public Ship(double x, double y, int number) : base()
        {
            this.x = x;
            this.y = y;
            switch (number)
            {
                case 0:
                    image = SpaceInvaders.Properties.Resources.ship1;break;
                case 1:
                    image = SpaceInvaders.Properties.Resources.ship2; break;
                case 2:
                    image = SpaceInvaders.Properties.Resources.ship3; break;
                case 3:
                    image = SpaceInvaders.Properties.Resources.ship4; break;
                case 4:
                    image = SpaceInvaders.Properties.Resources.ship5; break;
                case 5:
                    image = SpaceInvaders.Properties.Resources.ship6; break;
                case 6:
                    image = SpaceInvaders.Properties.Resources.ship7; break;
                case 7:
                    image = SpaceInvaders.Properties.Resources.ship8; break;
            }
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
            ArrayGraphics.Draw(this, image, (int)x, (int)y);
        }

        public override bool IsAlive()
        {
            return alive;
        }
        public override void goRight(Game gameInstance)
        {
            y -= 1;
        }
        public override void goLeft(Game gameInstance)
        {
            y += 1;
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
