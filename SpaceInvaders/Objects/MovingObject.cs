using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceInvaders.Objects;
using System.Drawing;

namespace SpaceInvaders.Objects
{
    class MovingObject : SpaceObject
    {

        public MovingObject(int pv, Bitmap image, Vecteur2D vector) : base(pv, image, vector)
        { }

        /// <summary>
        /// Move the player on the right
        /// </summary>
        /// <param name="gameInstance"> The instance for the game </param>
        /// <param name="deltaT"> Value relative to the time required to render an image </param>
        public virtual void MoveRight(Game gameInstance, double deltaT) { }

        /// <summary>
        /// Move the player on the left
        /// </summary>
        /// <param name="gameInstance"> The instance for the game </param>
        /// <param name="deltaT"> Value relative to the time required to render an image </param>
        public virtual void MoveLeft(Game gameInstance, double deltaT) { }

        /// <summary>
        /// Function called to shoot a missile
        /// </summary>
        /// <param name="gameInstance"> The instance for the game </param>
        /// <param name="deltaT"> Value relative to the time required to render an image </param>
        public virtual void Shoot(Game gameInstance, double deltaT) { }

        /// <summary>
        /// Determines if object is alive. If false, the object will be removed automatically.
        /// </summary>
        /// <returns>Am I alive ?</returns>
        public override bool IsAlive() { return alive; }
    }
}
