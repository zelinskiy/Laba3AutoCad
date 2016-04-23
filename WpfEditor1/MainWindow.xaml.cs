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




        public void DrawCircle(Point3D center, Vector3D normal, double radius, int resolution, string color)
        {

            ModelVisual3D m = new ModelVisual3D();
            MeshGeometry3D mg = new MeshGeometry3D();


            //Adding points to Mesh
            double t = 2 * Math.PI / resolution;
            for (int i = 0; i < resolution; i++)
            {
                mg.Positions.Add(new Point3D(radius * Math.Cos(t * i), 0, -radius * Math.Sin(t * i)));
            }
            
            for (int i = 0; i < resolution; i++)
            {
                var a = 0;
                var d = i + 1;
                var c = (i < (resolution - 1)) ? i + 2 : 1;

                mg.TriangleIndices.Add(a);
                mg.TriangleIndices.Add(d);
                mg.TriangleIndices.Add(c);
            }

            //Coloring
            BrushConverter bc = new BrushConverter();
            Brush b = (Brush)bc.ConvertFromString(color);
            DiffuseMaterial mat = new DiffuseMaterial(b);
            m.Content = new GeometryModel3D(mg, mat);
            

            //Rotating
            var trn = new Transform3DGroup();           
            var up = new Vector3D(0, 1, 0);
            normal.Normalize();
            var axis = Vector3D.CrossProduct(up, normal);
            var angle = Vector3D.AngleBetween(up, normal);
            trn.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(axis, 2*angle)));
            trn.Children.Add(new TranslateTransform3D(new Vector3D(center.X, center.Y, center.Z)));

            m.Transform = trn;
            MainViewPort.Children.Add(m);
            
        }


        


        



        public MainWindow()
        {
            InitializeComponent();
            
            
        }



        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double x1 = double.Parse(C1XInput.Text);
                double y1 = double.Parse(C1YInput.Text);
                double z1 = double.Parse(C1ZInput.Text);

                MainCamera.Position = new Point3D(x1, y1, z1);

                double x2 = double.Parse(C2XInput.Text);
                double y2 = double.Parse(C2YInput.Text);
                double z2 = double.Parse(C2ZInput.Text);

                MainCamera.LookDirection = new Vector3D(x2, y2, z2);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wrong input");
            }
        }
        
     


        #region C1 Controls

        private void C1UpButton_Click(object sender, RoutedEventArgs e)
        {
            MainCamera.Position = new Point3D(
                MainCamera.Position.X,
                MainCamera.Position.Y + C12Speed,
                MainCamera.Position.Z
            );
            C1YInput.Text = MainCamera.Position.Y.ToString();
        }

        private void C1DownButton_Click(object sender, RoutedEventArgs e)
        {
            MainCamera.Position = new Point3D(
                MainCamera.Position.X,
                MainCamera.Position.Y - C12Speed,
                MainCamera.Position.Z
            );
            C1YInput.Text = MainCamera.Position.Y.ToString();
        }

        private void C1LeftButton_Click(object sender, RoutedEventArgs e)
        {
            MainCamera.Position = new Point3D(
                MainCamera.Position.X - C12Speed,
                MainCamera.Position.Y,
                MainCamera.Position.Z
            );
            C1XInput.Text = MainCamera.Position.X.ToString();
        }

        private void C1RightButton_Click(object sender, RoutedEventArgs e)
        {
            MainCamera.Position = new Point3D(
                MainCamera.Position.X + C12Speed,
                MainCamera.Position.Y,
                MainCamera.Position.Z
            );
            C1XInput.Text = MainCamera.Position.X.ToString();
        }

        private void C1MZButton_Click(object sender, RoutedEventArgs e)
        {
            MainCamera.Position = new Point3D(
                MainCamera.Position.X,
                MainCamera.Position.Y,
                MainCamera.Position.Z - C12Speed
            );
            C1ZInput.Text = MainCamera.Position.Z.ToString();
        }

        private void C1LZButton_Click(object sender, RoutedEventArgs e)
        {
            MainCamera.Position = new Point3D(
                MainCamera.Position.X,
                MainCamera.Position.Y,
                MainCamera.Position.Z + C12Speed
            );
            C1ZInput.Text = MainCamera.Position.Z.ToString();
        }


        #endregion



        //TODO: fix C2
        #region C2 controls
        private void C2UpButton_Click(object sender, RoutedEventArgs e)
        {
            MainCamera.LookDirection = new Vector3D(
                MainCamera.LookDirection.X,
                MainCamera.LookDirection.Y - C12Speed,
                MainCamera.LookDirection.Z
            );
            C2YInput.Text = MainCamera.LookDirection.Y.ToString();
        }

        private void C2DownButton_Click(object sender, RoutedEventArgs e)
        {
            MainCamera.LookDirection = new Vector3D(
                MainCamera.LookDirection.X,
                MainCamera.LookDirection.Y + C12Speed,
                MainCamera.LookDirection.Z
            );
            C2YInput.Text = MainCamera.LookDirection.Y.ToString();
        }

        private void C2LeftButton_Click(object sender, RoutedEventArgs e)
        {
            MainCamera.LookDirection = new Vector3D(
                MainCamera.LookDirection.X + C12Speed,
                MainCamera.LookDirection.Y,
                MainCamera.LookDirection.Z
            );
            C2XInput.Text = MainCamera.LookDirection.X.ToString();
        }

        private void C2RightButton_Click(object sender, RoutedEventArgs e)
        {
            MainCamera.LookDirection = new Vector3D(
                MainCamera.LookDirection.X - C12Speed,
                MainCamera.LookDirection.Y,
                MainCamera.LookDirection.Z
            );
            C2XInput.Text = MainCamera.LookDirection.X.ToString();
        }

        private void C2MZButton_Click(object sender, RoutedEventArgs e)
        {
            MainCamera.LookDirection = new Vector3D(
                MainCamera.LookDirection.X,
                MainCamera.LookDirection.Y,
                MainCamera.LookDirection.Z + C12Speed
            );
            C2ZInput.Text = MainCamera.LookDirection.Z.ToString();
        }

        private void C2LZButton_Click(object sender, RoutedEventArgs e)
        {
            MainCamera.LookDirection = new Vector3D(
                MainCamera.LookDirection.X,
                MainCamera.LookDirection.Y,
                MainCamera.LookDirection.Z - C12Speed
            );
            C2ZInput.Text = MainCamera.LookDirection.Z.ToString();
        }




        #endregion

        private void C12SpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            C12Speed = e.NewValue;
            C12SpeedInput.Text = e.NewValue.ToString();
        }

        private void C12SpeedInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                C12Speed = double.Parse(C12SpeedInput.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wrong input");
            }

        }





        
        private void AddTriangleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Point3D p1 = new Point3D(
                    double.Parse(NTX1Input.Text),
                    double.Parse(NTY1Input.Text),
                    double.Parse(NTZ1Input.Text)
                    );

                Point3D p2 = new Point3D(
                    double.Parse(NTX2Input.Text),
                    double.Parse(NTY2Input.Text),
                    double.Parse(NTZ2Input.Text)
                    );

                Point3D p3 = new Point3D(
                    double.Parse(NTX3Input.Text),
                    double.Parse(NTY3Input.Text),
                    double.Parse(NTZ3Input.Text)
                    );

                string color = NTColorInput.Text;

                DrawTriangle(p1, p2, p3, color);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wrong input");
            }
        }

        private void NTX2Input1_Copy1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NCircleAddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Point3D center = new Point3D(
                double.Parse(NCircleXInput.Text),
                double.Parse(NCircleYInput.Text),
                double.Parse(NCircleZInput.Text)
                );

                Vector3D normal = new Vector3D(
                    double.Parse(NCircleXNormInput.Text),
                    double.Parse(NCircleYNormInput.Text),
                    double.Parse(NCircleZNormInput.Text)
                    );

                double radius = double.Parse(NCircleRadInput.Text);
                int resolution = int.Parse(NCircleResInput.Text);
                string color = NCircleColInput.Text;

                DrawCircle(center, normal, radius, resolution, color);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Wrong input");
            }
            

        }
    }
}
