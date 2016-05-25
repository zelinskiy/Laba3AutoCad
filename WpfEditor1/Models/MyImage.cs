using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
        public string Name = "Image";

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

        public List<Figure> Figures = new List<Figure>();
        


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


        public double DropRandomPoints(int n, bool show)
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
                if (show)
                {
                    Add(p);
                }
            }
            return ((double)good / n) * 100;
        }



        public override string ToString()
        {
            return Name;
            //return $"Image from ({Begin}) to ({End})";
        }


        public double Area()
        {
            return Math.Abs(End.X - Begin.X) * Math.Abs(End.Y - Begin.Y);
        }

        public double SumAreas()
        {
            return this.Figures.Select(f => f.Area()).Sum();
        }

        public double SumPerimeters()
        {
            return this.Figures.Select(f => f.Perimeter()).Sum();
        }




        public void Save(string filename)
        {
            using (TextWriter writer = new StreamWriter(File.Create(filename)))
            {
                foreach (var g in Figures.GroupBy(f => f.Name))
                {
                    writer.WriteLine("%%%" + g.First().GetType());
                    foreach (var fig in g)
                    {
                        writer.WriteLine(JsonConvert.SerializeObject(fig));
                    }
                }
            }
        }

        public void Load(string filename)
        {
            if (File.Exists(filename))
            {
                using (TextReader reader = new StreamReader(File.OpenRead(filename)))
                {
                    List<Models.Figure> figs = new List<Models.Figure>();
                    string line = "";
                    string type = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.StartsWith("%%%"))
                        {
                            type = line.Split('.').Last();
                        }
                        else
                        {
                            switch (type)
                            {
                                case "Ellipse":
                                    figs.Add(JsonConvert.DeserializeObject<Models.Ellipse>(line));
                                    break;
                                case "Cone":
                                    figs.Add(JsonConvert.DeserializeObject<Models.Cone>(line));
                                    break;
                                case "CuttedCone":
                                    figs.Add(JsonConvert.DeserializeObject<Models.CuttedCone>(line));
                                    break;
                                case "Point":
                                    figs.Add(JsonConvert.DeserializeObject<Models.Point>(line));
                                    break;
                                case "Circle":
                                    figs.Add(JsonConvert.DeserializeObject<Models.Circle>(line));
                                    break;
                                case "EmptyCircle":
                                    figs.Add(JsonConvert.DeserializeObject<Models.EmptyCircle>(line));
                                    break;
                            }
                        }
                    }
                    Figures = figs;
                }
            }
        }





        public double TotalPerimeter(double dl, double psize, bool show)
        {
            

            List<Models.Point> newPoints = new List<Models.Point>();


            foreach (Models.Figure myfig in Figures)
            {
                foreach (Point3D p in myfig.DropPointsOnPerimeter(dl))
                {
                    Models.Point newPoint = new Models.Point()
                    {
                        Id = -9,
                        Radius = psize,
                        Color = Colors.Green,
                        Position = new Point3D(p.X, p.Y, 0.01),
                    };

                    foreach (Models.Figure fig in Figures)
                    {
                        if (fig != myfig)
                        {
                            if (fig.Hitted(newPoint))
                            {
                                newPoint.Color = Colors.Orange;
                            }
                        }
                    }

                    newPoints.Add(newPoint);
                }
            }
            if(show)
                Figures.AddRange(newPoints);

            return newPoints.Where(p => p.Color == Colors.Green).Count() * dl;


        }






        public void Rescale(double size)
        {
            if(size <= 0)
            {
                return;
            }

            foreach(Figure fig in Figures)
            {
                fig.Scale(size);

                fig.Position = new Point3D(
                        fig.Position.X * size,
                        fig.Position.Y * size,
                        fig.Position.Z * size
                    );
            }
        }


        public void MoveAllFigures(double dX, double dY, double dZ)
        {
            foreach(Figure fig in Figures)
            {
                fig.Position = new Point3D
                    (                
                        fig.Position.X + dX,
                        fig.Position.Y + dY,
                        fig.Position.Z + dZ
                    );
            }
        }


        public void Move(double len, string dir)
        {
            switch (dir)
            {
                case "U":
                    MoveAllFigures(0, len, 0);
                    break;
                case "D":
                    MoveAllFigures(0, -len, 0);
                    break;
                case "L":
                    MoveAllFigures(-len, 0, 0);
                    break;
                case "R":
                    MoveAllFigures(len, 0, 0);
                    break;
                case "F":
                    MoveAllFigures(0, 0, -len);
                    break;
                case "B":
                    MoveAllFigures(0, 0, len);
                    break;

            }

        }




    }
}
