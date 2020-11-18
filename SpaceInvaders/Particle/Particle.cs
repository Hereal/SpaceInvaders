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
    // <summary>
    /// Class for managing a particle 
    /// </summary>
    class Particle
    {
        private Vecteur2D position;
        private Vecteur2D direction;
        private Rectangle rectangle;
        private Color color;
        private static System.Timers.Timer timer;
        private bool alive = true;
        private int size;

        #region Constructor
        /// <summary>
        /// Particle
        /// </summary>
        /// <param name="position">Vecteur2D for the position</param>
        /// <param name="direction">Vecteur2D for the direction</param>
        /// <param name="color">The color of the particle</param>
        /// <param name="dispersion">Value to a applie a dispersion to the particle</param>
        /// <param name="colorRandom">Value for randomize the color of a particle</param>
        /// <param name="lifetime">How many milliseconds the particle is displayed</param>
        /// <param name="size">The size of the particle in px</param>
        public Particle(Vecteur2D position, Vecteur2D direction, Color color, int dispersion = 0, int colorRandom = 0, int lifetime = 500, int size = 1)
        {
            this.position = position;
            this.direction = direction;
            this.color = color;
            dispersionManager(dispersion);
            this.color = colorManager(color, colorRandom);
            this.size = size;
            SetTimer(lifetime);
        }
        #endregion

        #region methods
        /// <summary>
        /// Disperse the particle
        /// </summary>
        /// <param name="dispersion">Value to a applie a dispersion to the particle</param>
        private void dispersionManager(int dispersion)
        {
            this.direction.x += Utils.rand.Next(-dispersion, dispersion);
            this.direction.y += Utils.rand.Next(-dispersion, dispersion);
        }


        // <summary>
        /// Randomise the color
        /// </summary>
        /// <param name="color">The color of the particle</param>
        /// <returns>
        /// The method returns an color.
        /// </returns>
        private Color colorManager(Color color, int colorRandom)
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

        // <summary>
        /// Set the timer for the life of the particle
        /// </summary>
        /// <param name="lifetime">How many milliseconds the particle is displayed</param>
        private void SetTimer(int lifetime)
        {
            timer = new System.Timers.Timer(lifetime);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;

        }
        // <summary>
        /// Event for the timer
        /// </summary>
        /// <param name="source">Object</param>
        /// <param name="e">ElapsedEventArgs</param>
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            alive = false;
        }

        /// <summary>
        /// Function called on each update
        /// </summary>
        /// <param name="gameInstance"> The instance for the game </param>
        /// <param name="deltaT"> Value relative to the time required to render an image </param>
        public void Update(Game gameInstance, double deltaT)
        {
            position = position + direction * deltaT;
            rectangle = new Rectangle((int)position.x, (int)position.y, 1, 1);
            if (position.x < 0 || position.x > gameInstance.gameSize.Width || position.y > gameInstance.gameSize.Height) alive = false;

        }
        /// <summary>
        /// Function called to draw the particle
        /// </summary>
        /// <param name="g"> The graphics instance of the game </param>
        public void Draw(Graphics g)
        {
            Pen p = new Pen(color, size);
            g.DrawRectangle(p, rectangle);
        }

        // <summary>
        /// Is the particle still displayed?
        /// </summary>
        /// <returns>
        /// The method returns if the particle is alive or not
        /// </returns>
        public bool IsAlive()
        {
            return alive;
        }
        #endregion
    }
}
