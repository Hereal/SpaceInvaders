using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SpaceInvaders.Manager;
using System.Windows.Media;
using System.Diagnostics;
using SpaceInvaders.Particule;
using SpaceInvaders.Objects;

namespace SpaceInvaders
{
    /// <summary>
    /// Ship class
    /// </summary>
    class Ship : MovingObject
    {
        #region Fields
        private double shipSpeed = 100;
        private Missile missile = null;
        private MediaPlayer mediaShoot = new MediaPlayer();
        private MediaPlayer mediaExplosion = new MediaPlayer();
        #endregion

        #region Constructor
        /// <summary>
        /// Ship
        /// </summary>
        /// <param name="x">start position x</param>
        /// <param name="y">start position y</param>
        /// <param name="pv">pv</param>
        /// <param name="image">Image of th ship</param>
        public Ship(double x, double y, int pv, Bitmap image) : base(pv, image, new Vecteur2D(x, y))
        { }
        #endregion

        #region Methods

        /// <summary>
        /// Function called on each update
        /// </summary>
        /// <param name="gameInstance"> The instance for the game </param>
        /// <param name="deltaT"> Value relative to the time required to render an image </param>
        public override void Update(Game gameInstance, double deltaT)
        {
            Shoot(gameInstance, deltaT);
            if (vector.y + image.Height >= 600)
            {
                Game.pause = true;
                gameInstance.nbLife = 0;
            }
        }

        /// <summary>
        /// Function called to remove pv form the ship
        /// </summary>
        /// <param name="gameInstance"> The instance for the game </param>
        /// <param name="pv"> Number of pv to remove from the Ship </param>
        public override void Kill(int pv, Game gameInstance)
        {
            base.pv -= pv;
            if (base.pv <= 0)
            {
                gameInstance.particles.UnionWith(ParticleGenerator.GenerateParticle(image, base.vector));
                alive = false;
                mediaExplosion.Open(new Uri(Path.Combine(Environment.CurrentDirectory, @".\sound\explosion\" + Utils.rand.Next(1, 4) + ".wav")));
                mediaExplosion.Volume = 1.0;
                mediaExplosion.Play();
                gameInstance.score += 10;
            }
        }

        /// <summary>
        /// Move the ship on the right
        /// </summary>
        /// <param name="gameInstance"> The instance for the game </param>
        /// <param name="deltaT"> Value relative to the time required to render an image </param>
        public override void MoveRight(Game gameInstance, double deltaT)
        {
            vector.x += shipSpeed * deltaT;

        }
        /// <summary>
        /// Move the ship on the left
        /// </summary>
        /// <param name="gameInstance"> The instance for the game </param>
        /// <param name="deltaT"> Value relative to the time required to render an image </param>
        public override void MoveLeft(Game gameInstance, double deltaT)
        {
            vector.x -= shipSpeed * deltaT;
        }

        /// <summary>
        /// Function called to shoot a missile
        /// </summary>
        /// <param name="gameInstance"> The instance for the game </param>
        /// <param name="deltaT"> Value relative to the time required to render an image </param>
        public override void Shoot(Game gameInstance, double deltaT)
        {
            if (missile == null || missile.IsAlive() == false)
                if (Utils.rand.Next(0, 10000) == 1)
                {
                    mediaShoot.Open(new Uri(Path.Combine(Environment.CurrentDirectory, @".\sound\shoot.wav")));
                    mediaShoot.Play();
                    missile = new Missile((int)vector.x + 7, (int)vector.y + 16, false, 10);
                    gameInstance.AddNewGameObject(missile);
                }

        }

        /// <summary>
        /// Speed up the ship
        /// </summary>
        public void Accelerate()
        {
            int maxSpeed = 500;
            shipSpeed += 10;
            if (shipSpeed > maxSpeed)
                shipSpeed = maxSpeed;
        }

        /// <summary>
        /// Move down the ship
        /// </summary>
        public void MoveDown()
        {
            vector.y += 10;
        }

        #endregion
    }
}
