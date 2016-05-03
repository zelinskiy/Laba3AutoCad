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
            return 0;
        }
        public override string ToString()
        {
            return
                "Cutted Cone of Radius: "
                + Radius.ToString()
                + ", Small Radius: "
                + SmallRadius.ToString()
                + ", Height: "
                + Height.ToString()
                + " at "
                + Position.ToString()
                + ";";
        }


    }
}
