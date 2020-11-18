using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Objects
{
    class SpaceObject : GameObject
    {
        public Bitmap image { get; protected set; }
        public int pv { get; protected set; }
        public bool alive { get; protected set; }
        public Vecteur2D vector { get; protected set; }

        public SpaceObject(int pv, Bitmap image, Vecteur2D vector)
        {
            this.pv = pv;
            this.vector = vector;
            this.image = image;
            alive = true;
        }

        /// <summary>
        /// Function called to remove pv form the SpaceObject
        /// </summary>
        /// <param name="gameInstance"> The instance for the game </param>
        /// <param name="pv"> Number of pv to remove from the SpaceObject </param>
        public virtual void Kill(int pv, Game gameInstance) { }

        /// <summary>
        /// Function called on each update
        /// </summary>
        /// <param name="gameInstance"> The instance for the game </param>
        /// <param name="deltaT"> Value relative to the time required to render an image </param>
        public override void Update(Game gameInstance, double deltaT) { }

        /// <summary>
        /// Draw the image of the SpaceObject
        /// </summary>
        /// <param name="gameInstance"> The instance for the game </param>
        /// <param name="g"> The graphics instance of the game </param>
        public override void Draw(Game gameInstance, Graphics graphics)
        {
            graphics.DrawImage(image, (int)vector.x, (int)vector.y, image.Width, image.Height);
        }

        /// <summary>
        /// Determines if object is alive. If false, the object will be removed automatically.
        /// </summary>
        /// <returns>Am I alive ?</returns>
        public override bool IsAlive() { return alive; }
    }
}
