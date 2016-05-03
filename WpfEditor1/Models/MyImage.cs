using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace WpfEditor1.Models
{
    public class MyImage
    {
        public Random rand = new Random();

        private bool _xyaxis = true;
        public bool XYAxis
        {
            get
            {
                return _xyaxis;
            }
            set
            {
                _xyaxis = value;
                RedrawAll();
            }
        }

        private bool _borders = true;
        public bool Borders
        {
            get
            {
                return _borders;
            }
            set
            {
                _borders = value;
                RedrawAll();
            }
        }



        public Point3D Begin { get; set; } = new Point3D(-10, -10, -10);
        public Point3D End { get; set; } = new Point3D(10, 10, 10);

        public List<Figure> Figures = new List<Figure>() {
            /*
            new Circle()
            {
                Id = 1,
                Position = new Point3D(0,0,0),
                Radius = 2,
                Resolution = 40,
                Color = Colors.Blue
            },
            new Circle()
            {
                Id = 2,
                Position = new Point3D(4,0,0),
                Radius = 2,
                Resolution = 40,
                Color = Colors.Blue
            },
            new Circle()
            {
                Id = 3,
                Position = new Point3D(0,4,0),
                Radius = 2,
                Resolution = 40,
                Color = Colors.Blue
            },
            new Circle()
            {
                Id = 4,
                Position = new Point3D(4,4,0),
                Radius = 2,
                Resolution = 40,
                Color = Colors.Blue
            },
            

            new Ellipse()
            {
                Id = 1,
                Position = new Point3D(4,4,0),
                A = 2, B = 1,
                Radius = 2,
                Resolution = 40,
                Color = Colors.Blue
            },*/

            new CuttedCone()
            {
                Id = 1,
                Position = new Point3D(0,0,-1),
                Radius = 2,
                SmallRadius = 0.5,
                Height = 5,
                Resolution = 40,
                Color = Colors.Blue
            },
        };
        


        public void AddLights()
        {
            MainViewPort.Children.Add(
                new ModelVisual3D()
                {
                    Content = new DirectionalLight(Colors.White, new Vector3D(-1, -1, -2))
                });
            MainViewPort.Children.Add(
                new ModelVisual3D()
                {
                    Content = new DirectionalLight(Colors.White, new Vector3D(1, 1, 2))
                });
        }

        public void RedrawAll()
        {
            MainViewPort.Children.Clear();


            AddLights();

            if (_xyaxis)
            {
                drawPseudoLine(new Point3D(0, -100, 0), new Point3D(0, 100, 0), 0.01, Colors.Red);
                drawPseudoLine(new Point3D(-100, 0, 0), new Point3D(100, 0, 0), 0.01, Colors.Red);
            }

            if (_borders)
            {
                //blah blah blah
                drawPseudoLine(new Point3D(-100, -100, 100), new Point3D(100, -100, 100), 5, Colors.Red);
                drawPseudoLine(new Point3D(100, -100, 100), new Point3D(100, -100, 100), 5, Colors.Red);
            }

            foreach (Figure f in Figures)
            {
                f.Draw(this);
            }
        }


        public MyImage(Viewport3D mainViewPort)
        {
            MainViewPort = mainViewPort;
            RedrawAll();
        }

        public Viewport3D MainViewPort { get; set; }

        public void DrawTriangle(Point3D p1, Point3D p2, Point3D p3, string color)
        {
            /* Usage:
                p2
               /  \ 
              /    \
             /      \
            /        \
           p1--------p3

    */
            ModelVisual3D m = new ModelVisual3D();

            MeshGeometry3D mg = new MeshGeometry3D();
            mg.Positions = new Point3DCollection(new List<Point3D>() { p1, p2, p3 });
            mg.TriangleIndices = new Int32Collection(new List<int>() { 0, 2, 1 });
            BrushConverter bc = new BrushConverter();
            Brush b = (Brush)bc.ConvertFromString(color);
            Material mat = new DiffuseMaterial(b);
            m.Content = new GeometryModel3D(mg, mat);


            MainViewPort.Children.Add(m);
        }

        public void drawRect(Point3D a, Point3D b, Point3D c, Point3D d, Color color)
        {

            DrawTriangle(a, b, c, color.ToString());
            DrawTriangle(a, c, d, color.ToString());

            DrawTriangle(a, c, b, color.ToString());
            DrawTriangle(a, d, c, color.ToString());
        }

        public void drawPseudoLine(Point3D a, Point3D b, double w, Color color)
        {
            if (b.X < a.X)
            {
                Point3D t = a;
                a = b;
                b = t;
            }


            drawRect(
                new Point3D(a.X + w, a.Y - w, a.Z),
                new Point3D(a.X + w, a.Y + w, a.Z),
                new Point3D(b.X + w, b.Y + w, b.Z),
                new Point3D(b.X + w, b.Y - w, b.Z),
                color);


            drawRect(
                new Point3D(a.X - w, a.Y - w, a.Z),
                new Point3D(a.X + w, a.Y + w, a.Z),
                new Point3D(b.X + w, b.Y + w, b.Z),
                new Point3D(b.X - w, b.Y - w, b.Z),
                color);

        }




        public void Add(Figure f)
        {
            f.Id = rand.Next(100000);
            Figures.Add(f);
            RedrawAll();
        }
        public void Remove(int id)
        {
            Figures = Figures.Where(f=>f.Id != id).ToList();
            RedrawAll();
        }




        public double SimpleSumSquares()
        {
            double S = 0;
            foreach(Figure f in Figures)
            {
                S += f.Area();
            }
            return S;
        }


        public double DropRandomPoints(int n)
        {
            int good = 0;

            for(int i=0; i< n; i++)
            {
                Point3D pos = new Point3D(
                        rand.Next((int)Begin.X, (int)End.X),
                        rand.Next((int)Begin.Y, (int)End.Y),
                        0.5
                        );

                Point p = new Point() {
                    Id = -9,
                    Radius = 0.2,
                    Color = Colors.Green,
                    Position = pos
                };
                foreach(Figure f in Figures)
                {
                    if (f.Hitted(new Point() { Position = pos }))
                    {
                        p.Color = Colors.Orange;
                        good++;
                        break;
                    }
                }
                Add(p);
            }
            return ((double)good / n) * 100;
        }






    }
}
