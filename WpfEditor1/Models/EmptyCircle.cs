using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace WpfEditor1.Models
{
    public class EmptyCircle:Figure
    {

        public int Resolution { get; set; }
        public double LineWidth { get; set; }

        public override void Scale(double size) { }


        

        public override void Draw(MyImage image)
        {
            double r = Radius + LineWidth;
            double t = 2 * Math.PI / Resolution;
            for (int i = 0; i < Resolution; i++)
            {
                image.drawRect(
                    new Point3D(Position.X + Radius * Math.Cos(t * i), Position.Y - Radius * Math.Sin(t * i), Position.Z),
                    new Point3D(Position.X + r * Math.Cos(t * i), Position.Y - r * Math.Sin(t * i), Position.Z),
                    new Point3D(Position.X + r * Math.Cos(t * (i - 1)), Position.Y - r * Math.Sin(t * (i - 1)), Position.Z),
                    new Point3D(Position.X + Radius * Math.Cos(t * (i - 1)), Position.Y - Radius * Math.Sin(t * (i - 1)), Position.Z),
                    Color
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
                "Point of radius "
                + Radius.ToString()
                + " at "
                + Position.ToString()
                + ";";
        }

        
    }
}
