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
        
        public MovingObject(int pv, Bitmap image,Vecteur2D vector) : base(pv,image,vector)
        {
        }
        public virtual void MoveRight(Game gameInstance, double deltaT) {}

        public virtual void MoveLeft(Game gameInstance, double deltaT) { }

        public virtual void Shoot(Game gameInstance, double deltaT) { }


        public override bool IsAlive() { return alive; }
    }
}
