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


        /// <summary>
        /// Ball speed in pixel/second
        /// </summary>
        private double shipSpeed = 100;



        private bool alive = true;

        private Bitmap image;

        private Missile missile = null;

        private MediaPlayer mediaShoot = new MediaPlayer();

        private MediaPlayer mediaExplosion = new MediaPlayer();
        

        #endregion

        #region Constructor
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="x">start position x</param>
        /// <param name="y">start position y</param>
        public Ship(double x, double y, int pv,Bitmap image) : base()
        {
            this.image = image;
            base.vector = new Vecteur2D(x, y);
            base.pv = pv;
        }
        #endregion

        #region Methods

        public override void Update(Game gameInstance, double deltaT)
        {
            Shoot(gameInstance, deltaT);
        }

        public override void Draw(Game gameInstance, Graphics graphics)
        {
            graphics.DrawImage(image, (int)vector.x, (int)vector.y, image.Width, image.Height);
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
                gameInstance.particles.UnionWith(ParticleGenerator.GenerateParticle(image, base.vector));
                alive = false;

                mediaExplosion.Open(new Uri(Path.Combine(Environment.CurrentDirectory, @"..\..\Resources\sound\explosion\" + Utils.rand.Next(1, 4) + ".wav")));
                mediaExplosion.Volume = 1.0;
                mediaExplosion.Play();
                gameInstance.score += 10;
            }
        }

        public override void MoveRight(Game gameInstance, double deltaT)
        {
            vector.x += shipSpeed * deltaT;

        }
        public override void MoveLeft(Game gameInstance, double deltaT)
        {
            vector.x -= shipSpeed * deltaT;
        }

        public override void Shoot(Game gameInstance, double deltaT)
        {
            if (missile == null || missile.IsAlive() == false)
            {
                if (Utils.rand.Next(0, 10000) == 1)
                {

                    mediaShoot.Open(new Uri(Path.Combine(Environment.CurrentDirectory, @"..\..\Resources\sound\shoot.wav")));
                    mediaShoot.Play();
                    missile = new Missile((int)vector.x + 7, (int)vector.y + 16, false, 10);
                    gameInstance.AddNewGameObject(missile);
                }
            }
        }
        public override Bitmap GetImage()
        {
            return image;
        }

        public void accelerate()
        {
            int maxSpeed = 500;
            shipSpeed += 10;
            if (shipSpeed > maxSpeed)
                shipSpeed = maxSpeed;
        }
        public void moveDown()
        {
            vector.y += 10;
        }

        #endregion
    }
}
