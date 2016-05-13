using System;
using System.Windows.Media.Media3D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfEditor1.Models
{
    public abstract class Figure
    {
        public int Id;

        public virtual string Name
        {
            get
            {
                return GetType().ToString().Split('.').Last();
            }
        }

        public Point3D Position { get; set; }
        public double Radius { get; set; }
        public Color Color { get; set; }


        public Figure()
        {
        }

        public Figure(Point3D p, double r)
        {
            Position = p;
            Radius = r;
        }
        

        public abstract void Draw(MyImage image);
        public abstract void Scale(double size);
        public abstract override string ToString();
        public abstract bool Hitted(Point p);
        public abstract double Area();
        public abstract double Perimeter();

        public void Move(Point3D p)
        {
            this.Position = p;
        }

        public void Move(double dX, double dY, double dZ)
        {
            Position = new Point3D(
                Position.X + dX,
                Position.Y + dY,
                Position.Z + dZ
                );
        }

    }
}
