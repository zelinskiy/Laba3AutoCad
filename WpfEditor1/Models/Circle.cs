using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace WpfEditor1.Models
{
    public class Circle:EmptyCircle
    {
        
        



        public override void Draw(MyImage image)
        {
            double t = 2 * Math.PI / Resolution;
            for (int i = 0; i < Resolution; i++)
            {
                image.DrawTriangle(
                    new Point3D(
                        Position.X + Radius * Math.Cos(t * i),
                        Position.Y - Radius * Math.Sin(t * i),
                        Position.Z),

                    new Point3D(Position.X + Radius * Math.Cos(t * (i + 1)),
                    Position.Y - Radius * Math.Sin(t * (i + 1)),
                    Position.Z),
                    Position,
                    Color.ToString()
                );
            }
        }

        public override bool Hitted(Point p)
        {
            return base.Hitted(p);
        }


        

        
    }
}
