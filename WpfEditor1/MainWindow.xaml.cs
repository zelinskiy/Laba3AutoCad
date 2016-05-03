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
using WpfEditor1.Models;
using System.Reflection;



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
        public Random rand = new Random();

        public double C12Speed = 0.1;

        public MyImage myImage;


        public MainWindow()
        {
            InitializeComponent();
            myImage = new MyImage(MainViewPort);
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


        public void RefreshFiguresView()
        {
            FiguresListBox.Items.Clear();
            foreach(Models.Figure f in myImage.Figures)
            {
                FiguresListBox.Items.Add(f);
            }
            
        }


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
                if(ColorsComboBox.SelectedIndex == -1)
                {
                    color = (Color)ColorsComboBox.Items[rand.Next(ColorsComboBox.Items.Count-1)];
                }
                else
                {
                    color = (Color)ColorsComboBox.SelectedItem;
                }
                

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
                    myImage.Add(new Circle() {
                        Position = center,
                        Radius = R,
                        Resolution = resolution,
                        Color = color
                    });
                    break;
                case 1:
                    myImage.Add(new EmptyCircle()
                    {
                        Position = center,
                        Radius = R,
                        Resolution = resolution,
                        LineWidth = width,
                        Color = color
                    });
                    break;
                case 2:
                    myImage.Add(new Models.Ellipse()
                    {
                        Position = center,
                        Radius = R,
                        Resolution = resolution,
                        A = a,
                        B = b,
                        Color = color
                    });
                    break;
                case 3:
                    myImage.Add(new Cone()
                    {
                        Position = center,
                        Radius = R,
                        Height = H,
                        Resolution = resolution,
                        Color = color
                    });
                    break;
                case 4:
                    myImage.Add(new CuttedCone()
                    {
                        Position = center,
                        Radius = R,
                        SmallRadius = r,
                        Height = H,
                        Resolution = resolution,
                        Color = color
                    });
                    break;
                case 5:
                    myImage.Add(new Models.Point()
                    {
                        Position = center,
                        Radius = R,
                        Color = color
                    });
                    break;
            }
            RefreshFiguresView();
        }

        private void DeleteFigureButton_Click(object sender, RoutedEventArgs e)
        {
            if(FiguresListBox.SelectedIndex == -1)
            {
                return;
            }
            myImage.Remove(((Models.Figure)FiguresListBox.SelectedItem).Id);
            RefreshFiguresView();

        }

        


        private void SaveFigureButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteFigureButton_Click(null, null);
            AddFigureButton_Click(null, null);
        }




        private void LoadDataToForms(Models.Figure MyFigure)
        {            
            CenterXTextBox.Text = MyFigure.Position.X.ToString();
            CenterYTextBox.Text = MyFigure.Position.Y.ToString();
            CenterZTextBox.Text = MyFigure.Position.Z.ToString();

            BigRadiusTextBox.Text = MyFigure.Radius.ToString();

            if (MyFigure is CuttedCone)
            {
                SmallRadiusTextBox.Text = ((CuttedCone)MyFigure).SmallRadius.ToString();
            }

            if (MyFigure is Cone)
            {
                SmallRadiusTextBox.Text = ((CuttedCone)MyFigure).Height.ToString();
            }

            if (MyFigure is EmptyCircle)
            {
                ResolutionTextBox.Text = ((EmptyCircle)MyFigure).Resolution.ToString();
            }

            if (MyFigure is EmptyCircle)
            {
                LineWidthTextBox.Text = ((EmptyCircle)MyFigure).LineWidth.ToString();
            }
            ColorsComboBox.SelectedItem = MyFigure.Color;
        }


        private void FiguresListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Models.Figure MyFigure = (Models.Figure)FiguresListBox.SelectedItem;
                LoadDataToForms(MyFigure);
            }
            catch(NullReferenceException nullex)
            {
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            myImage.Remove(-9);
            int n;
            try
            {
                n = int.Parse(MonteCarlotNumTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Hello from monte carlo");
                return;
            }
            double x = myImage.DropRandomPoints(n);
            myImage.RedrawAll();
            MessageBox.Show(x.ToString() + "%" );
        }
    }

}
