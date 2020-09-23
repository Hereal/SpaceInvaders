﻿using System;
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

namespace SpaceInvaders
{
    /// <summary>
    /// Dummy class for demonstration
    /// </summary>
    class Player : GameObject
    {
        #region Fields
        /// <summary>
        /// Position
        /// </summary>


        /// <summary>
        /// Ball speed in pixel/second
        /// </summary>
        private double playerSpeed = 100;

        private bool alive = true;

        private Bitmap image;

        private Missile missile = null;


        private MediaPlayer media = new MediaPlayer();



        #endregion

        #region Constructor
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="x">start position x</param>
        /// <param name="y">start position y</param>
        public Player(double x, double y, int pv) : base()
        {
            base.vector = new Vecteur2D(x, y);
            image = SpaceInvaders.Properties.Resources.player;
            base.pv = pv;



        }
        #endregion

        #region Methods

        public override void Update(Game gameInstance, double deltaT)
        {
            if (vector.y > gameInstance.gameSize.Height)
                alive = false;

        }

        public override void Draw(Game gameInstance, Graphics graphics)
        {
            GraphManager.DrawBufferedImage(gameInstance, image, (int)vector.x, (int)vector.y);

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
            Debug.Print("pv: " + base.pv);

        }

        public override void MoveRight(Game gameInstance, double deltaT)
        {
            if (!alive)
                return;
            vector.x += playerSpeed * deltaT;
            if (vector.x > GraphManager.bufferedImage.Width - image.Width)
                vector.x = GraphManager.bufferedImage.Width - image.Width;

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
                media.Open(new Uri(Path.Combine(Environment.CurrentDirectory, @"..\..\Resources\sound\shoot.wav")));
                media.Play();
                missile = new Missile((int)vector.x + 7, (int)vector.y - 7, true, 10);
                gameInstance.AddNewGameObject(missile);

            }
        }
        public override Bitmap GetImage()
        {
            return image;
        }

        #endregion
    }
}
