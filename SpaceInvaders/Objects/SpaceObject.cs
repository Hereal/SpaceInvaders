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

        public SpaceObject(int pv, Bitmap image,Vecteur2D vector)
        {
            this.pv = pv;
            this.vector = vector;
            this.image = image;
            alive = true;
        }
        public virtual void Kill(int pv, Game gameInstance) { }
        public override void Update(Game gameInstance, double deltaT) { }

        public override void Draw(Game gameInstance, Graphics graphics) {
            graphics.DrawImage(image, (int)vector.x, (int)vector.y, image.Width, image.Height);
        }

        public override bool IsAlive() { return alive; }
    }
}
