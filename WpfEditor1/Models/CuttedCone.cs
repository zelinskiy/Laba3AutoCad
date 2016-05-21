using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace WpfEditor1.Models
{
    class CuttedCone : Cone
    {

        public double SmallRadius;

        public override void Scale(double size) { }




        public override void Draw(MyImage image)
        {
            double t = 2 * Math.PI / Resolution;

            //drawing Side
            for (int i = 0; i < Resolution; i++)
            {
                image.DrawTriangle(
                    new Point3D(Position.X + Radius * Math.Cos(t * i), Position.Y, Position.Z - Radius * Math.Sin(t * i)),
                    new Point3D(Position.X + SmallRadius * Math.Cos(t * i), Position.Y + Height, Position.Z - SmallRadius * Math.Sin(t * i)),
                    new Point3D(Position.X + Radius * Math.Cos(t * (i + 1)), Position.Y, Position.Z - Radius * Math.Sin(t * (i + 1))),
                    Color.ToString()
                );
                image.DrawTriangle(
                    new Point3D(Position.X + SmallRadius * Math.Cos(t * (i + 1)), Position.Y + Height, Position.Z - SmallRadius * Math.Sin(t * (i + 1))),
                    new Point3D(Position.X + Radius * Math.Cos(t * (i + 1)), Position.Y, Position.Z - Radius * Math.Sin(t * (i + 1))),
                    new Point3D(Position.X + SmallRadius * Math.Cos(t * i), Position.Y + Height, Position.Z - SmallRadius * Math.Sin(t * i)),

                    Color.ToString()
                );
            }

            for (int i = 0; i < Resolution; i++)
            {
                image.DrawTriangle(
                    new Point3D(Position.X + SmallRadius * Math.Cos(t * i), Position.Y + Height, Position.Z - SmallRadius * Math.Sin(t * i)),
                    new Point3D(Position.X, Position.Y + Height, Position.Z),
                    new Point3D(Position.X + SmallRadius * Math.Cos(t * (i + 1)), Position.Y + Height, Position.Z - SmallRadius * Math.Sin(t * (i + 1))),

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


        public override double Area()
        {
            return Math.PI*(Radius*Radius-SmallRadius*SmallRadius);
        }

        public override double FullArea()
        {
            return Math.PI * Height * (SmallRadius * SmallRadius + SmallRadius * Radius + Radius * Radius)/3;
        }

        public override string ToString()
        {
            return base.ToString() + ", r:" + SmallRadius.ToString();

        }


        public override bool Hitted(Point point)
        {
            Point3D v1, v2, v3;
            //Imagined qqual sided triangel 
            Point3D pt = point.Position;

            //lower left subtriangle
            v1 = new Point3D(Position.X - Radius, Position.Y, 0);
            v2 = Position;
            v3 = new Point3D(Position.X - SmallRadius, Position.Y + Height, 0);

            if (base.Barycentric(pt, v1, v2, v3))
            {
                return true;
            }

            //upper center subtriangle
            v1 = new Point3D(Position.X - SmallRadius, Position.Y + Height, 0);
            v2 = new Point3D(Position.X + SmallRadius, Position.Y + Height, 0);
            v3 = Position;

            if (base.Barycentric(pt, v1, v2, v3))
            {
                return true;
            }

            //lower right subtriangle
            v1 = Position;
            v2 = new Point3D(Position.X + SmallRadius, Position.Y + Height, 0);
            v3 = new Point3D(Position.X + Radius, Position.Y, 0);

            if (base.Barycentric(pt, v1, v2, v3))
            {
                return true;
            }

            return false;

        }


        public override List<Point3D> DropPointsOnPerimeter(double dl)
        {
            List<Point3D> R = new List<Point3D>();

            Point3D A = new Point3D(Position.X - Radius, Position.Y, 0);
            Point3D D = new Point3D(Position.X + Radius, Position.Y, 0);
            Point3D B = new Point3D(Position.X - SmallRadius, Position.Y + Height, 0);
            Point3D C = new Point3D(Position.X + SmallRadius, Position.Y + Height, 0);

            R.AddRange(Figure.PointsOnLine(A, B, dl));
            R.AddRange(Figure.PointsOnLine(B, C, dl));
            R.AddRange(Figure.PointsOnLine(C, D, dl));
            R.AddRange(Figure.PointsOnLine(D, A, dl));

            return R;

        }


    }
}
