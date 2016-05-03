using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;



/*
    «Фигура», «Точка», «Окружность» «Круг закрашенный», «Эллипс», «Конус», «Усеченный конус» 



*/



namespace WpfEditor1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public double C12Speed = 0.1;


        #region Utility figures

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

        #endregion






        #region Main figures
        /////////////////////////////////////////////

        public void drawCircle(Point3D center, double R, int resolution, Color color)
        {
            //drawing
            double t = 2 * Math.PI / resolution;
            for (int i = 0; i < resolution; i++)
            {
                DrawTriangle(                    
                    new Point3D(center.X + R * Math.Cos(t * i), center.Y - R * Math.Sin(t * i), center.Z),
                    new Point3D(center.X + R * Math.Cos(t * (i+1)), center.Y - R * Math.Sin(t * (i+1)),center.Z),
                    center,
                    color.ToString()
                );
            }
            
        }
        
        ////////////////////////////////////////////

        public void drawCone(Point3D center, double R,double H, int resolution, Color color)
        {
            //drawing Side
            double t = 2 * Math.PI / resolution;
            for (int i = 0; i < resolution - 1; i++)
            {
                DrawTriangle(
                    new Point3D(center.X + R * Math.Cos(t * i),center.Y,  center.Z - R * Math.Sin(t * i)),
                    new Point3D(center.X, center.Y + H, center.Z),
                    new Point3D(center.X + R * Math.Cos(t * (i + 1)),center.Y,  center.Z - R * Math.Sin(t * (i + 1))),
                    color.ToString()
                );
            }
            //Final segment of Side
            DrawTriangle(
                    new Point3D(center.X + R * Math.Cos(t * (resolution-1)), center.Y, center.Z - R * Math.Sin(t * (resolution - 1))),
                    new Point3D(center.X, center.Y + H, center.Z),
                    new Point3D(center.X + R * Math.Cos(t * resolution), center.Y, center.Z - R * Math.Sin(t * resolution)),
                    color.ToString()
                );

            //drawing Base
            for (int i = 0; i < resolution - 1; i++)
            {
                DrawTriangle(
                    new Point3D(center.X + R * Math.Cos(t * i),center.Y,  center.Z - R * Math.Sin(t * i)),                    
                    new Point3D(center.X + R * Math.Cos(t * (i + 1)),center.Y, center.Z - R * Math.Sin(t * (i + 1))),
                    center,
                    color.ToString()
                );
            }
            //Final segment
            DrawTriangle(
                    new Point3D(center.X + R * Math.Cos(t * (resolution-1)), center.Y, center.Z - R * Math.Sin(t * (resolution - 1))),
                    new Point3D(center.X + R * Math.Cos(t * resolution), center.Y, center.Z - R * Math.Sin(t * resolution)),
                    center,
                    color.ToString()
                );
        }

        ////////////////////////////////////////

        public void drawCuttedCone(Point3D center, double R,double r, double H, int resolution, Color color)
        {
            double t = 2 * Math.PI / resolution;
            
            //drawing Side
            for (int i = 0; i < resolution; i++)
            {
                DrawTriangle(
                    new Point3D(center.X + R * Math.Cos(t * i), center.Y, center.Z - R * Math.Sin(t * i)),
                    new Point3D(center.X + r * Math.Cos(t * i), center.Y + H, center.Z - r * Math.Sin(t * i)),
                    new Point3D(center.X + R * Math.Cos(t * (i + 1)), center.Y, center.Z - R * Math.Sin(t * (i + 1))),
                    color.ToString()
                );
                DrawTriangle(
                    new Point3D(center.X + r * Math.Cos(t * (i + 1)), center.Y + H, center.Z - r * Math.Sin(t * (i + 1))),
                    new Point3D(center.X + R * Math.Cos(t * (i + 1)), center.Y, center.Z - R * Math.Sin(t * (i + 1))),                    
                    new Point3D(center.X + r * Math.Cos(t * i), center.Y + H, center.Z - r * Math.Sin(t * i)),
                    
                    color.ToString()
                );
            }
            












            for (int i = 0; i < resolution; i++)
            {
                DrawTriangle(
                    new Point3D(center.X + r * Math.Cos(t * i), center.Y + H, center.Z - r * Math.Sin(t * i)),
                    new Point3D(center.X, center.Y + H, center.Z),
                    new Point3D(center.X + r * Math.Cos(t * (i + 1)), center.Y + H, center.Z - r * Math.Sin(t * (i + 1))),
                    
                    color.ToString()
                );
            }



            //drawing Base
            for (int i = 0; i < resolution; i++)
            {
                DrawTriangle(
                    new Point3D(center.X + R * Math.Cos(t * i), center.Y, center.Z - R * Math.Sin(t * i)),
                    new Point3D(center.X + R * Math.Cos(t * (i + 1)), center.Y, center.Z - R * Math.Sin(t * (i + 1))),
                    center,
                    color.ToString()
                );
            }
            
        }

        /////////////////////////////////////////////  

        public void drawEmptyCircle(Point3D center, double R, int resolution,double width, Color color)
        {
            double r = R + width;
            //drawing
            double t = 2 * Math.PI / resolution;
            for (int i = 0; i < resolution ; i++)
            {
                drawRect(
                    new Point3D(center.X + R * Math.Cos(t * i), center.Y - R * Math.Sin(t * i), center.Z),
                    new Point3D(center.X + r * Math.Cos(t * i), center.Y - r * Math.Sin(t * i), center.Z),
                    new Point3D(center.X + r * Math.Cos(t * (i - 1)), center.Y - r * Math.Sin(t * (i - 1)), center.Z),
                    new Point3D(center.X + R * Math.Cos(t * (i-1)), center.Y - R * Math.Sin(t * (i - 1)), center.Z),
                    color
                    );
            }

        }
        
        /////////////////////////////////////////////

        public void drawEllipse(Point3D center, double R,double a, double b, int resolution, Color color)
        {
            double t = 2 * Math.PI / resolution;
            for (int i = 0; i < resolution; i++)
            {
                DrawTriangle(
                    new Point3D(center.X + a * R * Math.Cos(t * i), center.Y - b * R * Math.Sin(t * i), center.Z),
                    new Point3D(center.X + a * R * Math.Cos(t * (i + 1)), center.Y - b * R * Math.Sin(t * (i + 1)), center.Z),
                    center,
                    color.ToString()
                );
            }
        }

        /////////////////////////////////////////////
        #endregion







        public MainWindow()
        {
            InitializeComponent();
            drawPseudoLine(new Point3D(0, -100, 0), new Point3D(0, 100, 0), 0.01, Colors.Red);
            drawPseudoLine(new Point3D(-100, 0, 0), new Point3D(100, 0, 0), 0.01, Colors.Red);
            //drawCuttedCone(new Point3D(0, 0, 0), 0.5, 0.1, 2.0, 40, Colors.Aqua);
            //drawEllipse(new Point3D(0, 0, 0), 2, 3, 1, 40, Colors.Purple);
            
        }








        #region Controls
        

        private void C1UpButton_Click(object sender, RoutedEventArgs e)
        {
            MainCamera.Position = new Point3D(
                MainCamera.Position.X,
                MainCamera.Position.Y + C12Speed,
                MainCamera.Position.Z
            );
        }

        private void C1DownButton_Click(object sender, RoutedEventArgs e)
        {
            MainCamera.Position = new Point3D(
                MainCamera.Position.X,
                MainCamera.Position.Y - C12Speed,
                MainCamera.Position.Z
            );
        }

        private void C1LeftButton_Click(object sender, RoutedEventArgs e)
        {
            MainCamera.Position = new Point3D(
                MainCamera.Position.X - C12Speed,
                MainCamera.Position.Y,
                MainCamera.Position.Z
            );
        }

        private void C1RightButton_Click(object sender, RoutedEventArgs e)
        {
            MainCamera.Position = new Point3D(
                MainCamera.Position.X + C12Speed,
                MainCamera.Position.Y,
                MainCamera.Position.Z
            );
        }

        private void C1MZButton_Click(object sender, RoutedEventArgs e)
        {
            MainCamera.Position = new Point3D(
                MainCamera.Position.X,
                MainCamera.Position.Y,
                MainCamera.Position.Z - C12Speed
            );
        }

        private void C1LZButton_Click(object sender, RoutedEventArgs e)
        {
            MainCamera.Position = new Point3D(
                MainCamera.Position.X,
                MainCamera.Position.Y,
                MainCamera.Position.Z + C12Speed
            );
        }


        
        

        private void C12SpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            C12Speed = e.NewValue;
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MainCamera.LookDirection = new Vector3D(
                Math.Sin(slider.Value * Math.PI / 180),
                MainCamera.LookDirection.Y,
                MainCamera.LookDirection.Z);
        }

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MainCamera.LookDirection = new Vector3D(
                MainCamera.LookDirection.X,
                -Math.Cos(slider1.Value * Math.PI / 180),
                MainCamera.LookDirection.Z);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MainCamera.LookDirection = MainCamera.LookDirection * (-1);
        }



        #endregion

        private void AddFigureButton_Click(object sender, RoutedEventArgs e)
        {
            Point3D center;
            double R;
            double r;
            double H;
            double width;        
            int resolution;
            Color color;

            double a, b;


            try
            {
                center = new Point3D(
                    double.Parse(CenterXTextBox.Text),
                    double.Parse(CenterYTextBox.Text),
                    double.Parse(CenterZTextBox.Text)
                    );
                R = double.Parse(BigRadiusTextBox.Text);
                r = double.Parse(SmallRadiusTextBox.Text);
                H = double.Parse(HeightTextBox.Text);
                width = double.Parse(LineWidthTextBox.Text);
                resolution = int.Parse(ResolutionTextBox.Text);
                color = (Color)ColorsComboBox.SelectedItem;

                a = double.Parse(EllipseATextBox.Text);
                b = double.Parse(EllipseBTextBox.Text);

            }
            catch
            {
                MessageBox.Show("Sorry");
                return;
            }


            switch (FiguresComboBox.SelectedIndex)
            {
                case 0:
                    drawCircle(center, R, resolution, color);
                    break;
                case 1:
                    drawEmptyCircle(center, R, resolution,width, color);
                    break;
                case 2:
                    drawEllipse(center, R, a, b, resolution, color);
                    break;
                case 3:
                    drawCone(center, R, H, resolution, color);
                    break;
                case 4:
                    drawCuttedCone(center, R, r, H, resolution, color);
                    break;
            }
        }
    }

}
