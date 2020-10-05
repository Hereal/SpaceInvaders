using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SpaceInvaders.Manager;
using System.Threading;
using System.Media;
using System.Windows.Media;
using System.Diagnostics;
using SpaceInvaders.Particule;
using SpaceInvaders.Objects;

namespace SpaceInvaders
{
    /// <summary>
    /// Dummy class for demonstration
    /// </summary>
    class Player : MovingObject
    {
        #region Fields
        /// <summary>
        /// Position
        /// </summary>


        /// <summary>
        /// Ball speed in pixel/second
        /// </summary>
        private double playerSpeed = 500;

        private Missile missile = null;

        private MediaPlayer mediaShoot = new MediaPlayer();
        private MediaPlayer mediaExplosion = new MediaPlayer();
        private MediaPlayer mediaR2 = new MediaPlayer();


        #endregion

        #region Constructor
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="x">start position x</param>
        /// <param name="y">start position y</param>
        public Player(double x, double y, int pv) : base(pv, SpaceInvaders.Properties.Resources.player, new Vecteur2D(x, y))
        {
        }
        #endregion

        #region Methods

        public override void Update(Game gameInstance, double deltaT)
        {
            if (vector.y > gameInstance.gameSize.Height)
                alive = false;
            if (Utils.rand.Next(0, 4000) == 1)
            {
                mediaR2.Open(new Uri(Path.Combine(Environment.CurrentDirectory, @".\sound\R2\" + Utils.rand.Next(1, 7) + ".wav")));
                mediaR2.Volume = 0.7;
                mediaR2.Play();
            }

        }

        public override void Kill(int pv, Game gameInstance)
        {
            base.pv -= pv;
            if (base.pv <= 0)
            {
                alive = false;
                gameInstance.particles.UnionWith(ParticleGenerator.GenerateParticle(image, base.vector));
                mediaExplosion.Open(new Uri(Path.Combine(Environment.CurrentDirectory, @".\sound\explosion\" + Utils.rand.Next(1, 4) + ".wav")));
                mediaExplosion.Volume = 1.0;
                mediaExplosion.Play();
            }

        }

        public override void MoveRight(Game gameInstance, double deltaT)
        {
            if (!alive)
                return;
            vector.x += playerSpeed * deltaT;
            if (vector.x > gameInstance.gameSize.Width - image.Width)
                vector.x = gameInstance.gameSize.Width - image.Width;

        }
        public override void MoveLeft(Game gameInstance, double deltaT)
        {
            if (!alive)
                return;
            vector.x -= playerSpeed * deltaT;
            if (vector.x < 0)
                vector.x = 0;
        }

        public override void Shoot(Game gameInstance, double deltaT)
        {
            if (!alive)
                return;
            if (missile == null || missile.IsAlive() == false)
            {
                mediaShoot.Open(new Uri(Path.Combine(Environment.CurrentDirectory, @".\sound\shoot.wav")));
                mediaShoot.Play();
                missile = new Missile((int)vector.x - 5, (int)vector.y - 7, true, 10);
                gameInstance.AddNewGameObject(missile);
                gameInstance.AddNewGameObject(new Missile((int)vector.x + 40, (int)vector.y - 7, true, 10));

            }
        }
        #endregion
    }
}
