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
    /// Player class
    /// </summary>
    class Player : MovingObject
    {
        #region Fields
        private double playerSpeed = 500;
        private Missile missile = null;
        private MediaPlayer mediaShoot = new MediaPlayer();
        private MediaPlayer mediaExplosion = new MediaPlayer();
        private MediaPlayer mediaR2 = new MediaPlayer();
        #endregion

        #region Constructor
        /// <summary>
        /// Player
        /// </summary>
        /// <param name="x">start x position</param>
        /// <param name="y">start y position</param>
        /// <param name="pv">start pv</param>
        public Player(double x, double y, int pv) : base(pv, SpaceInvaders.Properties.Resources.player, new Vecteur2D(x, y))
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
            if (vector.y > gameInstance.gameSize.Height)
                alive = false;
            if (Utils.rand.Next(0, 4000) == 1)
            {
                mediaR2.Open(new Uri(Path.Combine(Environment.CurrentDirectory, @".\sound\R2\" + Utils.rand.Next(1, 7) + ".wav")));
                mediaR2.Volume = 0.7;
                mediaR2.Play();
            }

        }

        /// <summary>
        /// Function called to remove pv form the player
        /// </summary>
        /// <param name="gameInstance"> The instance for the game </param>
        /// <param name="pv"> Number of pv to remove from the Player </param>
        public override void Kill(int pv, Game gameInstance)
        {
            base.pv -= pv;
            if (base.pv <= 0)
            {
                gameInstance.nbLife--;
                alive = false;
                gameInstance.particles.UnionWith(ParticleGenerator.GenerateParticle(image, base.vector));
                mediaExplosion.Open(new Uri(Path.Combine(Environment.CurrentDirectory, @".\sound\explosion\" + Utils.rand.Next(1, 4) + ".wav")));
                mediaExplosion.Volume = 1.0;
                mediaExplosion.Play();
                if (gameInstance.nbLife > 0)
                {
                    Game.player = new Player(650, 600, 10);
                    gameInstance.AddNewGameObject(Game.player);
                }
                else Game.pause = true;
            }
        }

        /// <summary>
        /// Move the player on the right
        /// </summary>
        /// <param name="gameInstance"> The instance for the game </param>
        /// <param name="deltaT"> Value relative to the time required to render an image </param>
        public override void MoveRight(Game gameInstance, double deltaT)
        {
            if (!alive)
                return;
            vector.x += playerSpeed * deltaT;
            if (vector.x > gameInstance.gameSize.Width - image.Width)
                vector.x = gameInstance.gameSize.Width - image.Width;

        }

        /// <summary>
        /// Move the player on the left
        /// </summary>
        /// <param name="gameInstance"> The instance for the game </param>
        /// <param name="deltaT"> Value relative to the time required to render an image </param>
        public override void MoveLeft(Game gameInstance, double deltaT)
        {
            if (!alive)
                return;
            vector.x -= playerSpeed * deltaT;
            if (vector.x < 0)
                vector.x = 0;
        }

        /// <summary>
        /// Function called to shoot a missile
        /// </summary>
        /// <param name="gameInstance"> The instance for the game </param>
        /// <param name="deltaT"> Value relative to the time required to render an image </param>
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
