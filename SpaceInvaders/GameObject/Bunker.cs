using SpaceInvaders.Manager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace SpaceInvaders
{
    class Bunker : GameObject
    {
        private Bitmap image;
        private bool alive = true;
        MediaPlayer mediaExplosion = new MediaPlayer();




        public Bunker(double x, double y,int pv)
        {
            image = SpaceInvaders.Properties.Resources.bunker;
            base.vector = new Vecteur2D(x, y);
            base.pv = pv;
        }



        public override void Update(Game gameInstance, double deltaT)
        {


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
            }
            mediaExplosion.Open(new Uri(Path.Combine(Environment.CurrentDirectory, @"..\..\Resources\sound\explosion\" + Utils.rand.Next(1, 4) + ".wav")));
            mediaExplosion.Volume = 1.0;
            mediaExplosion.Play();

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
    }
}
