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


        public abstract List<Point3D> DropPointsOnPerimeter(double dl);


        public static List<Point3D> PointsOnLine(Point3D A, Point3D B, double dl)
        {
            List<Point3D> R = new List<Point3D>();

            double len = Math.Sqrt((B.X - A.X) * (B.X - A.X) + (B.Y - A.Y) * (B.Y - A.Y));

            for (int i = 0; i * dl < len; i++) {
                double t = dl * i / len;
                R.Add(new Point3D(
                    (1 - t) * A.X + t * B.X,
                    (1 - t) * A.Y + t * B.Y,
                    0
                ));
            }

            return R;
        }
        
    }
}
