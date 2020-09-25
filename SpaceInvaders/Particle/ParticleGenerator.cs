using SpaceInvaders.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
                        particleSet.Add(new Particle(new Vecteur2D(position.x + i, position.y + j),  (new Vecteur2D(position.x + i, position.y + j)- new Vecteur2D(middleX, middleY)), image.GetPixel(i, j),100,20,500));
                }
            }
            return particleSet;
        }
        public static HashSet<Particle> GenerateParticle(int nbParticle, Color color, Vecteur2D position, Vecteur2D direction,int dispersionX =0, int dispersionY = 0, int colorRandom = 0)
        {
            HashSet<Particle> particleSet = new HashSet<Particle>();
            for (int i = 0; i < nbParticle; i++)
            {
                particleSet.Add(new Particle(position, direction, color, dispersionX,dispersionY, colorRandom));
                
            }
            return particleSet;
        }

        public static HashSet<Particle> GenerateStars()
        {
            HashSet<Particle> particleSet = new HashSet<Particle>();
            
            if (Utils.rand.Next(0, 50) == 1)
            {
                Vecteur2D position = new Vecteur2D(Utils.rand.Next(0, 297), 0);
                Vecteur2D direction = new Vecteur2D(Utils.rand.Next(-1, 1), Utils.rand.Next(30, 80));
                particleSet.Add(new Particle(position, direction, Color.White,0,0,10000));
            }
            return particleSet;
        }

    }
}
