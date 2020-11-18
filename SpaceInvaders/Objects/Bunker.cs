using SpaceInvaders.Manager;
using SpaceInvaders.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace SpaceInvaders
{
    class Bunker : SpaceObject
    {
        #region Fields
        MediaPlayer mediaExplosion = new MediaPlayer();
        #endregion

        #region Constructor
        /// <summary>
        /// Bunker
        /// </summary>
        /// <param name="x">start position x</param>
        /// <param name="y">start position y</param>
        /// <param name="pv">pv</param>
        public Bunker(double x, double y, int pv) : base(pv, SpaceInvaders.Properties.Resources.bunker, new Vecteur2D(x, y))
        { }
        #endregion


        #region Methods
        /// <summary>
        /// Function called to remove pv form the bunker
        /// </summary>
        /// <param name="gameInstance"> The instance for the game </param>
        /// <param name="pv"> Number of pv to remove from the Ship </param>
        public override void Kill(int pv, Game gameInstance)
        {
            base.pv -= pv;
            if (base.pv <= 0) base.alive = false;
            mediaExplosion.Open(new Uri(Path.Combine(Environment.CurrentDirectory, @"..\..\Resources\sound\explosion\" + Utils.rand.Next(1, 4) + ".wav")));
            mediaExplosion.Volume = 1.0;
            mediaExplosion.Play();
        }
        #endregion
    }
}
