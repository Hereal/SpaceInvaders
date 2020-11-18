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
    /// Missile
    /// </summary>
    class Missile : MovingObject
    {
        #region Fields
        private bool up;
        private double missileSpeed = 200;
        #endregion

        #region Constructor
        /// <summary>
        /// Missile
        /// </summary>
        /// <param name="x">start x position</param>
        /// <param name="y">start y position</param>
        /// <param name="up">direction of movement (up or down)</param>
        /// <param name="pv">start pv</param>
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
        /// <summary>
        /// Handle the collision with the missile
        /// </summary>
        /// <param name="gameInstance">The instance for the game </param>
        private void collisionHandler(Game gameInstance)
        {
            foreach (GameObject gm in gameInstance.gameObjects)
                if (gm is SpaceObject)
                {
                    SpaceObject so = (SpaceObject)gm;
                    bool collision = false;
                    if (!gm.Equals(this)) collision = IsCollision( gm);
                    if (collision)
                    {
                        int pv = Utils.Min(so.pv, this.pv);
                        so.Kill(1, gameInstance);
                        Kill(1, gameInstance);
                    }
                }
        }
        /// <summary>
        /// Tell if there is a collision between the missile and another gameobject
        /// </summary>
        /// <param name="gm"></param>
        /// <returns></returns>
        private bool IsCollision( GameObject gm)
        {
            SpaceObject so = (SpaceObject)gm;
            Rectangle missileRect = new Rectangle((int)vector.x, (int)vector.y, image.Width - 1, image.Height - 1);
            Rectangle gmRect = new Rectangle((int)so.vector.x, (int)so.vector.y, so.image.Width - 1, so.image.Height - 1);
            if (missileRect.IntersectsWith(gmRect) && gm.Equals(this) == false && gm.IsAlive() && ((up && so is Ship) || (!up && so is Player) || so is Missile || so is Bunker))
                return CollisionPoint(so, gmRect, missileRect);
            return false;
        }

        /// <summary>
        /// Change the pixels of the images is there is a collision
        /// </summary>
        /// <param name="gm">the gameobject to test</param>
        /// <param name="gmRect">the rectangle of the gameobjects</param>
        /// <param name="missileRect">the rectangle of the missile</param>
        /// <returns></returns>
        private bool CollisionPoint(SpaceObject gm, Rectangle gmRect, Rectangle missileRect)
        {
            bool collision = false;
            Rectangle r = Rectangle.Intersect(gmRect, missileRect);
            for (int i = r.X; i < r.X + r.Width; i++)
                for (int j = r.Y; j < r.Y + r.Height; j++)
                {
                    int gmx = i - gmRect.X, gmy = j - gmRect.Y, msx = i - missileRect.X, msy = j - missileRect.Y;
                    if (gm.image.GetPixel(gmx, gmy).A > 150 && image.GetPixel(msx, msy).A > 150)
                    {
                        gm.image.SetPixel(gmx, gmy, Color.Transparent);
                        collision = true;
                    }
                }
            return collision;
        }

        /// <summary>
        /// Function called on each update
        /// </summary>
        /// <param name="gameInstance"> The instance for the game </param>
        /// <param name="deltaT"> Value relative to the time required to render an image </param>
        public override void Update(Game gameInstance, double deltaT)
        {
            if (up) vector.y -= missileSpeed * deltaT;
            else vector.y += missileSpeed * deltaT;
            if (vector.y > gameInstance.gameSize.Height || vector.y < 0 - image.Height) alive = false;
            collisionHandler(gameInstance);
        }

        /// <summary>
        /// Function called to remove pv form the missile
        /// </summary>
        /// <param name="gameInstance"> The instance for the game </param>
        /// <param name="pv"> Number of pv to remove from the Ship </param>
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
