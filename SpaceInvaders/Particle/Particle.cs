using SpaceInvaders.Manager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;

namespace SpaceInvaders.Particule
{
    class Particle
    {
        private Vecteur2D position;
        private Vecteur2D direction;
        private Rectangle rectangle;
        private Color color;
        private static System.Timers.Timer aTimer;
        private bool alive = true;

        public Particle(Vecteur2D position, Vecteur2D direction, Color color,int dispersion=0, int colorRandom=0,int lifetime = 500)
        {
            this.position = position;
            this.direction = direction;
            this.color = color;

            dispersionManager(dispersion);

            this.color = colorManager(color, colorRandom);
            
            SetTimer(lifetime);
        }

        private void dispersionManager(int dispersion)
        {
            this.direction.x += Utils.rand.Next(-dispersion, dispersion);
            this.direction.y += Utils.rand.Next(-dispersion, dispersion);
        }

        private Color colorManager(Color color,int colorRandom)
        {
            
            int r = Utils.rand.Next(0, colorRandom);
            int g = Utils.rand.Next(0, colorRandom);
            int b = Utils.rand.Next(0, colorRandom);
            r = this.color.R + r <= 255 ? r + this.color.R : 255;
            g = this.color.G + g <= 255 ? g + this.color.G : 255;
            b = this.color.B + b <= 255 ? b + this.color.B : 255;
            this.color = Color.FromArgb(255, r, g, b);
            return color;
        }


        private  void SetTimer(int lifetime)
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(lifetime);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

        }

        private  void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            alive = false;
        }

        public void Update(Game game, double deltaT)
        {

            position = position + direction *deltaT;
            rectangle = new Rectangle((int)position.x, (int)position.y,1,1);


        }
        public void Draw(Graphics g)
        {
            //Pen p = new Pen(color, 1);
            //g.DrawRectangle(p, rectangle);
            Bitmap bm = new Bitmap(1, 1);

            bm.SetPixel(0, 0, color);

            g.DrawImageUnscaled(bm, (int)position.x, (int)position.y);
        }
        public bool IsAlive()
        {
            return alive;
        }
    }
}
