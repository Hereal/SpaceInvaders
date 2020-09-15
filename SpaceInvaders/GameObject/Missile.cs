using System;
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
    class Missile : GameObject
    {
        #region Fields
        /// <summary>
        /// Position
        /// </summary>
        private double x, y;
        private bool up; 


        /// <summary>
        /// Ball speed in pixel/second
        /// </summary>
        private double missileSpeed = 100;

        private bool alive = true;

        private Bitmap image ;

        #endregion

        #region Constructor
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="x">start position x</param>
        /// <param name="y">start position y</param>
        public Missile(double x, double y,bool up) : base()
        {
            image = SpaceInvaders.Properties.Resources.shot30;
            this.x = x;
            this.y = y;
            this.up = up;
        }
        #endregion

        #region Methods

        public override void Update(Game gameInstance, double deltaT)
        {
            if (up)
            {
                y -= missileSpeed * deltaT;
                
            }
            else
                y += missileSpeed * deltaT;
            
            if (y > GraphManager.bufferedImage.Height || y<0-image.Height)
                alive = false;
            foreach (GameObject gm in gameInstance.gameObjects)
            {
                if (x > gm.GetCoord().X && x <= gm.GetImage().Width + gm.GetCoord().X)
                {
                    if (y > gm.GetCoord().Y && y <= gm.GetImage().Height + gm.GetCoord().Y)
                    {
                        if (up && gm.GetType().ToString().Equals("SpaceInvaders.Ship"))
                        {
                            gm.Kill();
                            this.alive = false;
                        }
                        else if (gm.GetType().ToString().Equals("SpaceInvaders.Missile") && gm.Equals(this)==false)
                        {
                            gm.Kill();
                            this.alive = false;
                        }
                        else if (!up && gm.GetType().ToString().Equals("SpaceInvaders.Player"))
                        {
                            //gm.Kill();
                            this.alive = false;
                        }
                    }
                }
            }
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
            

        }
        public override void MoveLeft(Game gameInstance, double deltaT)
        {
            
        }

        public override void Shoot(Game gameInstance, double deltaT)
        {

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
