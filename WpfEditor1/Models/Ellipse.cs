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
            double x0 = Position.X;
            double y = p.Position.Y;
            double y0 = Position.Y;
            return Math.Pow(x - x0, 2) / Math.Pow(A, 2) 
                + Math.Pow(y - y0, 2) / Math.Pow(B, 2) < 0.99;
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

        public override double Perimeter()
        {
            return 4*(Math.PI * A * B + A - B)/(A + B);
        }

        public override string ToString()
        {
            return $"{Name} at ({Position.ToString()}) "
                + $"A: {A}, B:{B}, S: {Area()}, P: P{Perimeter()}";
        }



        public override List<Point3D> DropPointsOnPerimeter(double dl)
        {
            List<Point3D> R = new List<Point3D>();

            int resolution = (int)( this.Perimeter() / dl);
            double t = 2 * Math.PI / resolution; ;
            for (int i = 0; i < resolution; i++)
            {
                R.Add(new Point3D(Position.X + A*Math.Cos(t * i), Position.Y - B*Math.Sin(t * i), Position.Z + 0.05));
            }
            return R;
        }


    }
}
