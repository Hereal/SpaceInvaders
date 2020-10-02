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
        MediaPlayer mediaExplosion = new MediaPlayer();

        public Bunker(double x, double y, int pv) : base(pv, SpaceInvaders.Properties.Resources.bunker, new Vecteur2D(x, y))
        { }

        public override void Kill(int pv, Game gameInstance)
        {
            base.pv -= pv;
            if (base.pv <= 0)
            {
                base.alive = false;
            }
            mediaExplosion.Open(new Uri(Path.Combine(Environment.CurrentDirectory, @"..\..\Resources\sound\explosion\" + Utils.rand.Next(1, 4) + ".wav")));
            mediaExplosion.Volume = 1.0;
            mediaExplosion.Play();

        }
    }
}
