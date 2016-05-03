using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace WpfEditor1.Models
{
    public class Ellipse:Circle
    {
        public double A, B;

        public override void Scale(double size) { }

        public override bool Hitted(Point p)
        {
            double x = p.Position.X;
            double x0 = Position.Y;
            double y = p.Position.Y;
            double y0 = Position.Y;
            return Math.Pow((x - x0) / A, 2) + Math.Pow((y - y0) / B, 2) <= 1;
        }


        public override void Draw(MyImage image)
        {
            double t = 2 * Math.PI / Resolution;
            for (int i = 0; i < Resolution; i++)
            {
                image.DrawTriangle(
                    new Point3D(Position.X + A *  Math.Cos(t * i), Position.Y - B *  Math.Sin(t * i), Position.Z),
                    new Point3D(Position.X + A *  Math.Cos(t * (i + 1)), Position.Y - B *  Math.Sin(t * (i + 1)), Position.Z),
                    Position,
                    Color.ToString()
                );
            }
        }

        public override double Area()
        {
            return Math.PI * A * B;
        }

        public override string ToString()
        {
            return
                Color.ToString()
                + " Ellipse at "
                + Position.ToString()
                + ";";
        }
    }
}
