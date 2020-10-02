using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceInvaders
{
    class Vecteur2D
    {
        public double x { get; set; }
        public double y { get; set; }
        public double Norme { get; private set; }

        public Vecteur2D() : this(0, 0) { }

        public Vecteur2D(double x, double y)
        {
            this.x = x;
            this.y = y;
            this.Norme = Math.Sqrt(x * x + y * y);
        }

        public static Vecteur2D operator +(Vecteur2D a, Vecteur2D b)
        {
            if (a == null || b == null)
                throw new Exception("RequireNonNull");
            return new Vecteur2D(a.x + b.x, a.y + b.y);

        }

        public static Vecteur2D operator -(Vecteur2D a, Vecteur2D b)
        {
            if (a == null || b == null)
                throw new Exception("RequireNonNull");
            return new Vecteur2D(a.x - b.x, a.y - b.y);

        }

        public static Vecteur2D operator -(Vecteur2D a)
        {
            if (a == null)
                throw new Exception("RequireNonNull");
            return new Vecteur2D(-a.x, -a.y);

        }

        public static Vecteur2D operator *(Vecteur2D a, double b)
        {
            if (a == null)
                throw new Exception("RequireNonNull");
            return new Vecteur2D(a.x * b, a.y * b);
        }

        public static Vecteur2D operator *(double a, Vecteur2D b)
        {
            return b * a;
        }

        public static Vecteur2D operator /(Vecteur2D a, double b)
        {
            if (a == null || b == 0)
                throw new Exception("RequireNonNull");
            return new Vecteur2D(a.x / b, a.y / b);
        }

        public override string ToString()
        {
            return "Vecteur x:" + x + "  y:" + y;
        }
    }
}
