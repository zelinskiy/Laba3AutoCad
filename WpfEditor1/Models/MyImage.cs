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

        public Point3D Begin { get; set; }
        public Point3D End { get; set; }

        public List<Figure> Figures = new List<Figure>()
        {
            /*
            new EmptyCircle() {
                Radius = 0.5,
                Position = new Point3D(0,0,0),
                Color = Colors.Gold,
                Resolution = 20,
                LineWidth = 0.05
            },
            new EmptyCircle() {
                Radius = 0.75,
                Position = new Point3D(0,0,0),
                Color = Colors.Red,
                Resolution = 20,
                LineWidth = 0.05
            },
            new EmptyCircle() {
                Radius = 1,
                Position = new Point3D(0,0,0),
                Color = Colors.Green,
                Resolution = 20,
                LineWidth = 0.05
            },
            new Cone() {
                Radius = 1,
                Height = 3,
                Position = new Point3D(4,1,5),
                Color = Colors.Cyan,
                Resolution = 50,
            },
            new CuttedCone() {
                Radius = 1,
                SmallRadius = 0.2,
                Height = 3,
                Position = new Point3D(2,2,0),
                Color = Colors.Lime,
                Resolution = 50,
            },
            new CuttedCone() {
                Radius = 1,
                SmallRadius = 0.2,
                Height = 3,
                Position = new Point3D(-2,2,0),
                Color = Colors.BlueViolet,
                Resolution = 50,
            },
            */
            new Ellipse() {
                Radius = 1,
                A = 0.5,
                B = 0.3,
                Position = new Point3D(2,2,0),
                Color = Colors.Firebrick,
                Resolution = 50,
            },
            new Ellipse() {
                Radius = 1,
                A = 0.3,
                B = 0.5,
                Position = new Point3D(-2,-2,0),
                Color = Colors.AliceBlue,
                Resolution = 50,
            },
            new Ellipse() {
                Radius = 3,
                A = 0.5,
                B = 0.4,
                Position = new Point3D(-2,2,0),
                Color = Colors.Violet,
                Resolution = 50,
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
            foreach (Figure f in Figures)
            {
                f.Draw(this);
            }
        }


        public MyImage(Viewport3D mainViewPort)
        {
            MainViewPort = mainViewPort;
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







    }
}
