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
            if (up)
            {
                image = SpaceInvaders.Properties.Resources.shotUp;
            }
            else
            {
                image = SpaceInvaders.Properties.Resources.shotDown;
            }
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
                        bool colision = false;
                        for(int i= 0; i < image.Width; i++)
                        {
                            for(int j = 0; j < image.Height; j++)
                            {
                                if (image.GetPixel(i, j).A > 150 && gm.Equals(this) == false)
                                {
                                    /*if(gm.GetImage().GetPixel(((int)x+gm.GetCoord().X)-gm.GetCoord().X, ((int)x + gm.GetCoord().X) - gm.GetCoord().Y).A>150){
                                        colision = false;
                                        break;
                                       }*/
                                }
                            }
                        }











                        if (up && gm is Ship&& colision)
                        {


                            Color pixel = gm.GetImage().GetPixel((int)x-gm.GetCoord().X, (int)y - gm.GetCoord().Y);
                            if (pixel.A > 150) {
                                gm.Kill();
                                this.alive = false;
                            }
                            
                        }
                        else if (gm is Missile && colision)
                        {
                            Color pixel = gm.GetImage().GetPixel((int)x - gm.GetCoord().X, (int)y - gm.GetCoord().Y);
                            if (pixel.A > 150)
                            {
                                gm.Kill();
                                this.alive = false;
                            }
                        }
                        else if (!up && gm is Player && colision)
                        {
                            Color pixel = gm.GetImage().GetPixel((int)x - gm.GetCoord().X, (int)y - gm.GetCoord().Y);
                            if (pixel.A > 150)
                            {
                                gm.Kill();
                                this.alive = false;
                            }
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
