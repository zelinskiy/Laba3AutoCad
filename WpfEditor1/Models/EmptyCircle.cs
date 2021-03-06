﻿using System;
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

        public override void Scale(double size)
        {
            Radius *= size;
        }



        public override bool Hitted(Point p)
        {
            return 
                Math.Sqrt(
                Math.Pow(p.Position.X - Position.X, 2)+
                Math.Pow(p.Position.Y - Position.Y, 2))
                <= Radius;
        }


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
            return Math.PI * Radius * Radius;
        }

        public override double Perimeter()
        {
            return 2 * Math.PI * Radius;
        }

        public override string ToString()
        {
            return $"{Name} at ({Position.ToString()}) "
                +$"R: {Radius}, S:{Area()}, P: {Perimeter()}";                
        }



        public override List<Point3D> DropPointsOnPerimeter(double dl)
        {
            List<Point3D> R = new List<Point3D>();

            int resolution = (int)(this.Perimeter() / dl);
            double t = 2 * Math.PI / resolution; ;
            for (int i = 0; i < resolution; i++)
            {
                R.Add(new Point3D(Position.X + Radius * Math.Cos(t * i), Position.Y - Radius * Math.Sin(t * i), Position.Z + 0.05));
            }
            return R;
        }

    }
}
