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


        public override void Draw(MyImage image)
        {
            double t = 2 * Math.PI / Resolution;
            for (int i = 0; i < Resolution; i++)
            {
                image.DrawTriangle(
                    new Point3D(Position.X + A * Radius * Math.Cos(t * i), Position.Y - B * Radius * Math.Sin(t * i), Position.Z),
                    new Point3D(Position.X + A * Radius * Math.Cos(t * (i + 1)), Position.Y - B * Radius * Math.Sin(t * (i + 1)), Position.Z),
                    Position,
                    Color.ToString()
                );
            }
        }
        public override string ToString()
        {
            return
                "Ellipse of radius "
                + Radius.ToString()
                + " at "
                + Position.ToString()
                + ";";
        }
    }
}
