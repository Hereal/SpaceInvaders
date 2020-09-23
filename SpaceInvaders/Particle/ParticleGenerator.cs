using SpaceInvaders.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;

namespace SpaceInvaders.Particule
{
    class ParticleGenerator
    {

        public static HashSet<Particle> GenerateParticle(Bitmap image, Vecteur2D position)
        {
            HashSet<Particle> particleSet = new HashSet<Particle>();
            int width = image.Width;
            int height = image.Height;
            int middleX = (int)(width / 2.0) + (int)position.x;
            int middleY = (int)(height / 2.0) + (int)position.y;
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    if (image.GetPixel(i, j).A > 150 && i % 2 == 0 && j % 2 == 0)
                        particleSet.Add(new Particle(new Vecteur2D(position.x + i, position.y + j),  new Vecteur2D(position.x + i, position.y + j)- new Vecteur2D(middleX, middleY), image.GetPixel(i, j)));
                }
            }
            return particleSet;
        }

    }
}
