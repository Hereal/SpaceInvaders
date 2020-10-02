using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SpaceInvaders.Manager;
using System.Diagnostics;
using System.Security.Cryptography;
using SpaceInvaders.Particule;
using SpaceInvaders.Objects;

namespace SpaceInvaders
{
    /// <summary>
    /// Dummy class for demonstration
    /// </summary>
    class Missile : MovingObject
    {
        #region Fields
        private bool up;


        /// <summary>
        /// Ball speed in pixel/second
        /// </summary>
        private double missileSpeed = 200;

        #endregion

        #region Constructor
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="x">start position x</param>
        /// <param name="y">start position y</param>
        public Missile(double x, double y, bool up, int pv) : base(pv, SpaceInvaders.Properties.Resources.shotUp, new Vecteur2D(x, y))
        {
            if (up)
                base.image = SpaceInvaders.Properties.Resources.shotUp;
            else
                base.image = SpaceInvaders.Properties.Resources.shotDown;
            this.up = up;
        }
        #endregion

        #region Methods
        private void collisionHandler(Game gameInstance)
        {
            foreach (GameObject gm in gameInstance.gameObjects)
            {
                if (gm is SpaceObject)
                {
                    SpaceObject so = (SpaceObject)gm;

                    bool collision = false;
                    if (!gm.Equals(this))
                    {
                        Rectangle missileRect = new Rectangle((int)vector.x, (int)vector.y, image.Width - 1, image.Height - 1);
                        Rectangle gmRect = new Rectangle((int)so.vector.x, (int)so.vector.y, so.image.Width - 1, so.image.Height - 1);


                        if (missileRect.IntersectsWith(gmRect) && gm.Equals(this) == false && gm.IsAlive() && ((up && so is Ship) || (!up && so is Player) || so is Missile || so is Bunker))
                        {
                            collision = CollisionPoint(so, gmRect, missileRect, gameInstance);
                        }
                    }
                    if (collision)
                    {
                        int pv = Utils.min(so.pv, this.pv);
                        so.Kill(1, gameInstance);
                        Kill(1, gameInstance);
                    }
                }
            }
        }

        private bool CollisionPoint(SpaceObject gm, Rectangle gmRect, Rectangle missileRect, Game gameInstance)
        {
            bool collision = false;
            Rectangle r = Rectangle.Intersect(gmRect, missileRect);
            for (int i = r.X; i < r.X + r.Width; i++)
            {
                for (int j = r.Y; j < r.Y + r.Height; j++)
                {
                    int gmx = i - gmRect.X;
                    int gmy = j - gmRect.Y;
                    int msx = i - missileRect.X;
                    int msy = j - missileRect.Y;
                    if (gm.image.GetPixel(gmx, gmy).A > 150 && image.GetPixel(msx, msy).A > 150)
                    {
                        gm.image.SetPixel(gmx, gmy, Color.Transparent);
                        collision = true;
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


        public override void Kill(int pv, Game gameInstance)
        {
            base.pv -= pv;
            if (base.pv <= 0)
            {
                alive = false;
                gameInstance.particles.UnionWith(ParticleGenerator.GenerateParticle(image, base.vector));
            }
        }
        #endregion
    }
}
