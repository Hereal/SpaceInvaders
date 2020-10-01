using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SpaceInvaders.Manager;
using System.Diagnostics;
using System.Security.Cryptography;
using SpaceInvaders.Particule;

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
        private double missileSpeed = 200;

        private bool alive = true;

        private Bitmap image;

        #endregion

        #region Constructor
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="x">start position x</param>
        /// <param name="y">start position y</param>
        public Missile(double x, double y, bool up, int pv) : base()
        {
            if (up)
                image = SpaceInvaders.Properties.Resources.shotUp;
            else
                image = SpaceInvaders.Properties.Resources.shotDown;
            base.vector = new Vecteur2D(x, y);
            base.pv = pv;
            this.up = up;
        }
        #endregion

        #region Methods
        private void collisionHandler(Game gameInstance)
        {
            foreach (GameObject gm in gameInstance.gameObjects)
            {
                bool collision = false;
                if (!gm.Equals(this))
                {
                    Rectangle missileRect = new Rectangle((int)vector.x, (int)vector.y, image.Width - 1, image.Height - 1);
                    Rectangle gmRect = new Rectangle((int)gm.vector.x, (int)gm.vector.y, gm.GetImage().Width - 1, gm.GetImage().Height - 1);

                    
                    if (missileRect.IntersectsWith(gmRect) && gm.Equals(this) == false && gm.IsAlive() && ((up && gm is Ship) || (!up && gm is Player) || gm is Missile || gm is Bunker))
                    {
                        collision = CollisionPoint(gm, gmRect, missileRect, gameInstance);
                    }
                }
                if (collision )
                {
                    int pv = Utils.min(gm.pv, this.pv);
                    gm.Kill(1, gameInstance);
                    Kill(1, gameInstance);
                }
            }
        }

        private bool CollisionPoint(GameObject gm, Rectangle gmRect, Rectangle missileRect, Game gameInstance)
        {
            bool collision = false;
            Rectangle r = Rectangle.Intersect(gmRect, missileRect);
            for(int i = r.X; i < r.X+r.Width; i++)
            {
                for (int j = r.Y; j < r.Y + r.Height; j++)
                {
                    int gmx = i  - gmRect.X;
                    int gmy = j - gmRect.Y;
                    int msx = i - missileRect.X;
                    int msy = j - missileRect.Y;
                        if (gm.GetImage().GetPixel(gmx,gmy).A > 150 && image.GetPixel(msx,msy).A > 150)
                        {
                            
                            gm.GetImage().SetPixel(gmx, gmy, Color.Transparent);
                            collision= true;
                       }
                    
                }
            }
            return collision;
        }

        public override void Update(Game gameInstance, double deltaT)
        {
            if (up)
                vector.y -= missileSpeed * deltaT;
            else
                vector.y += missileSpeed * deltaT;

            if (vector.y > gameInstance.gameSize.Height || vector.y < 0 - image.Height)
                alive = false;

            collisionHandler(gameInstance);

        }

        public override void Draw(Game gameInstance, Graphics graphics)
        {
            graphics.DrawImage(image, (int)vector.x, (int)vector.y,image.Width,image.Height);

        }

        public override bool IsAlive()
        {
            return alive;
        }

        public override void Kill(int pv, Game gameInstance)
        {
            base.pv -= pv;
            if (base.pv <= 0)
            {
                alive = false;
                gameInstance.particles.UnionWith(ParticleGenerator.GenerateParticle(image, base.vector));
            }
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
