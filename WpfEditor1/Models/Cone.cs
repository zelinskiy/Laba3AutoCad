using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace WpfEditor1.Models
{
    class Cone : Figure
    {

        public int Resolution;
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
        public override double Area()
        {
            return 0;
        }
        public override string ToString()
        {
            return
                "Cone of Radius: "
                + Radius.ToString()
                + ", Height: "
                + Height.ToString()
                + " at "
                + Position.ToString()
                + ";";
        }


    }
}
