using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SpaceInvaders.Manager;
using System.Diagnostics;
using System.Security.Cryptography;

namespace SpaceInvaders
{
    /// <summary>
    /// Dummy class for demonstration
    /// </summary>
    class Missile : GameObject
    {
        #region Fields
        private bool up;


        /// <summary>
        /// Ball speed in pixel/second
        /// </summary>
        private double missileSpeed = 100;

        private bool alive = true;

        private Bitmap image;

        #endregion

        #region Constructor
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="x">start position x</param>
        /// <param name="y">start position y</param>
        public Missile(double x, double y, bool up) : base()
        {
            if (up)
            {
                image = SpaceInvaders.Properties.Resources.shotUp;
            }
            else
            {
                image = SpaceInvaders.Properties.Resources.shotDown;
            }
            base.vector = new Vecteur2D(x, y);
            this.up = up;
        }
        #endregion

        #region Methods
        private void collisionHandler(Game gameInstance)
        {
            foreach (GameObject gm in gameInstance.gameObjects)
            {
                if (!gm.Equals(this))
                {
                    Rectangle missileRect = new Rectangle((int)vector.x, (int)vector.y, image.Width - 1, image.Height - 1);
                    Rectangle gmRect = new Rectangle((int)gm.vector.x, (int)gm.vector.y, gm.GetImage().Width - 1, gm.GetImage().Height - 1);

                    /*Graphics g = Graphics.FromImage(GraphManager.bufferedImage);
                    g.DrawRectangle(new Pen(Color.Red), missileRect);
                    g.DrawRectangle(new Pen(Color.Red), gmRect);*/
                    if (missileRect.IntersectsWith(gmRect) && gm.Equals(this) == false)
                    {
                        getCollisionPoint(gm, gmRect, missileRect);
                        //this.alive = true;
                        //gm.Kill();

                    }
                }
            }
        }

        private Point getCollisionPoint(GameObject gm, Rectangle gmRect, Rectangle missileRect)
        {
            for (int i = 0; i < missileRect.Width; i++)
            {
                for (int j = 0; j < missileRect.Width; j++)
                {
                    int gmx = i + (int)vector.x - gmRect.X;
                    int gmy = j + (int)vector.y - gmRect.Y;
                    if (gmx > 0 && gmy > 0)
                        if (gm.GetImage().GetPixel(gmx, gmy).A > 150 && image.GetPixel(i, j).A > 150)
                            gm.GetImage().SetPixel(gmx, gmy, Color.Transparent);
                }
            }
            return Point.Empty;
        }


        public override void Update(Game gameInstance, double deltaT)
        {
            if (up)
                vector.y -= missileSpeed * deltaT;
            else
                vector.y += missileSpeed * deltaT;

            if (vector.y > GraphManager.bufferedImage.Height || vector.y < 0 - image.Height)
                alive = false;

            collisionHandler(gameInstance);

        }

        public override void Draw(Game gameInstance, Graphics graphics)
        {
            GraphManager.DrawBufferedImage(gameInstance, image, (int)vector.x, (int)vector.y);

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

        #endregion
    }
}
