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
        private int lifeTime = 1000;
        private bool alive = true;

        public Particle(Vecteur2D position, Vecteur2D direction, Color color)
        {
            this.position = position;
            this.direction = direction;
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            this.direction.x += rnd.Next(-2,2);
            this.direction.y += rnd.Next(-2, 2);

            this.color = color;
            int r = rnd.Next(0, 20);
            int g = rnd.Next(0, 20);
            int b = rnd.Next(0, 20);
            r = this.color.R + r <= 255 ? r+this.color.R : 255;
            g = this.color.G + g <= 255 ? g + this.color.G : 255;
            b = this.color.B + b <= 255 ? b + this.color.B : 255;
            this.color = Color.FromArgb(255, r, g, b);
            SetTimer();
        }


        private  void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(lifeTime);
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

            position = position + (direction / 50.0);
            rectangle = new Rectangle((int)position.x, (int)position.y, 1, 1);


        }
        public void Draw(Graphics g)
        {
            g.DrawRectangle(new Pen(color), rectangle);
        }
        public bool IsAlive()
        {
            return alive;
        }
    }
}
