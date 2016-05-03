using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace WpfEditor1.Models
{
    public class Point:Figure
    {

        public override void Scale(double size) { }
        public override void Draw(MyImage image)
        {
            image.drawRect(
                Position,
                new Point3D(Position.X, Position.Y + Radius, Position.Z),
                new Point3D(Position.X + Radius, Position.Y + Radius, Position.Z),                
                new Point3D(Position.X + Radius, Position.Y, Position.Z),
                this.Color
                );
        }

        public override bool Hitted(Point p)
        {
            return false;
        }

        public override double Area()
        {
            return 0;
        }

        public override string ToString()
        {
            return
                Color.ToString()
                + " (Pseudo) Point at "
                + Position.ToString()
                + ";";
        }
    }
}
