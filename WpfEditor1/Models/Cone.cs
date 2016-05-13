using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace WpfEditor1.Models
{
    class Cone : Circle
    {
        public double Height;

        public override void Scale(double size) { }




        public override void Draw(MyImage image)
        {
            //drawing Side
            double t = 2 * Math.PI / Resolution;
            for (int i = 0; i < Resolution; i++)
            {
                image.DrawTriangle(
                    new Point3D(Position.X + Radius * Math.Cos(t * i),
                                Position.Y,
                                Position.Z - Radius * Math.Sin(t * i)),
                    new Point3D(Position.X,
                                Position.Y + Height,
                                Position.Z),
                    new Point3D(Position.X + Radius * Math.Cos(t * (i + 1)),
                                Position.Y,
                                Position.Z - Radius * Math.Sin(t * (i + 1))),
                    Color.ToString()
                );
            }


            //drawing Base
            for (int i = 0; i < Resolution; i++)
            {
                image.DrawTriangle(
                    new Point3D(Position.X + Radius * Math.Cos(t * i), Position.Y, Position.Z - Radius * Math.Sin(t * i)),
                    new Point3D(Position.X + Radius * Math.Cos(t * (i + 1)), Position.Y, Position.Z - Radius * Math.Sin(t * (i + 1))),
                    Position,
                    Color.ToString()
                );
            }
        }

        public virtual double SideArea()
        {
            return Math.PI * Radius * Math.Sqrt(Radius * Radius + Height * Height);
        }

        public virtual double BaseArea()
        {
            return base.Area();
        }

        public virtual double FullArea()
        {
            return BaseArea()+ SideArea();
        }

        public override double Area()
        {
            return Radius*Height;
        }

        public virtual double Volume()
        {
            return Math.PI * Radius*Radius*Height/3;
        }

        public override string ToString()
        {
            return $"{Name} at ({Position.ToString()}); "
                + $"R: {Radius}, "
                + $"H: {Height}, "
                + $"S: {Area()}, "
                + $"Sf: {FullArea()}, "
                + $"V: {Volume()}";

        }


        protected double sign(Point3D p1, Point3D p2, Point3D p3)
        {
            return (p1.X - p3.X) * (p2.Y - p3.Y) - (p2.X - p3.X) * (p1.Y - p3.Y);
        }

        public bool Barycentric(Point3D pt, Point3D v1, Point3D v2, Point3D v3)
        {
            bool b1, b2, b3;

            b1 = sign(pt, v1, v2) < 0.0f;
            b2 = sign(pt, v2, v3) < 0.0f;
            b3 = sign(pt, v3, v1) < 0.0f;

            return ((b1 == b2) && (b2 == b3));
        }

        public override bool Hitted(Point point)
        {
            Point3D pt = point.Position;
            Point3D v1 = new Point3D(Position.X - Radius, Position.Y, 0);
            Point3D v2 = new Point3D(Position.X + Radius, Position.Y, 0);
            Point3D v3 = new Point3D(Position.X, Position.Y + Height, 0);

            return Barycentric(pt, v1, v2, v3);
        }




    }
}
