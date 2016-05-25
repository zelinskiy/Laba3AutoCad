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
using System.IO;
using Newtonsoft.Json;



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

        public List<MyImage> Images = new List<MyImage>();


        


        public MainWindow()
        {
            InitializeComponent();
            myImage = new MyImage(MainViewPort);
            Images.Add(myImage);

            foreach(MyImage im in Images)
            {
                ImagesListBox.Items.Add(im);
            }

            RefreshImagesListBox();

            


            string[] names = new string[]
            {
                "Circle",
                "EmptyCircle",
                "Ellipse",
                "Cone",
                "CuttedCone",
                "Point",                
            };

            FiguresComboBox.ItemsSource = names;
            


            myImage.Add(new Circle()
            {
                Id = 11,
                Position = new Point3D(-2, 0, 0),
                Radius = 3,
                Resolution = 30,
                LineWidth = 0.05,
                Color = Colors.Turquoise,
            });

            myImage.Add(new Circle()
            {
                Id = 12,
                Position = new Point3D(2, 0, 0),
                Radius = 3,
                Resolution = 30,
                LineWidth = 0.05,
                Color = Colors.Turquoise,
            });



            /*
            myImage.Add(new Models.Ellipse()
            {
                Id = 0,
                Position = new Point3D(-2, 0, 0),
                Resolution = 30,
                A = 1,
                B = 2,
                Color = Colors.Red,
            });

            myImage.Add(new Models.Ellipse()
            {
                Id = 1,
                Position = new Point3D(2, 0, 0),
                Resolution = 30,
                A = 1,
                B = 2,
                Color = Colors.Red,
            });

            myImage.Add(new Models.Ellipse()
            {
                Id = 2,
                Position = new Point3D(0, 2, 0),
                Resolution = 30,
                A = 2,
                B = 1,
                Color = Colors.Red,
            });

            myImage.Add(new Models.Ellipse()
            {
                Id = 3,
                Position = new Point3D(0, -2, 0),
                Resolution = 30,
                A = 2,
                B = 1,
                Color = Colors.Red,
            });

            myImage.Add(new Cone()
            {
                Id = 10,
                Position = new Point3D(0, -5, -3),
                Radius = 3,
                Height = 6,
                Resolution = 30,
                Color = Colors.Fuchsia,
            });

            
            myImage.Add(new Circle()
            {
                Id = 11,
                Position = new Point3D(-3, -4, -3),
                Radius = 2,
                Resolution = 30,
                LineWidth = 0.05,
                Color = Colors.Turquoise,
            });

            myImage.Add(new Circle()
            {
                Id = 12,
                Position = new Point3D(3, -4,-3),
                Radius = 2,
                Resolution = 30,
                LineWidth = 0.05,
                Color = Colors.Turquoise,
            });


            myImage.Add(new CuttedCone()
            {
                Id = 4,
                Position = new Point3D(0, -6, -15),
                Radius = 10,
                SmallRadius = 8,
                Height = 10,
                Resolution = 50,
                Color = Colors.Gainsboro,
            });
            

            /*
            myImage.Add(new EmptyCircle()
            {
                Id = 1,
                Position = new Point3D(5, 0, 0),
                Radius = 2,
                Resolution = 30,
                LineWidth = 0.05,
                Color = Colors.Red,
            });

            myImage.Add(new Models.Ellipse()
            {
                Id = 2,
                Position = new Point3D(10, 0, 0),
                Resolution = 30,
                A = 1,
                B = 2,
                Color = Colors.Red,
            });
            myImage.Add(new Cone()
            {
                Id = 3,
                Position = new Point3D(-5, 0, 0),
                Radius = 3,
                Height = 6,
                Resolution = 30,
                Color = Colors.Red,
            });

            myImage.Add(new CuttedCone()
            {
                Id = 4,
                Position = new Point3D(-10, 0, 0),
                Radius = 3,
                SmallRadius = 0.5,
                Height = 5,
                Resolution = 50,
                Color = Colors.Red,
            });            
            */



            RefreshFiguresView();

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
            if(FiguresListBox.SelectedIndex == -1)
            {
                return;
            }
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
                HeightTextBox.Text = ((Cone)MyFigure).Height.ToString();
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
            FiguresComboBox.SelectedItem = MyFigure.Name;
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
            double x = myImage.DropRandomPoints(n, true);
            myImage.RedrawAll();
            MessageBox.Show("Area: " + (x / myImage.Area()).ToString()  );
            
        }

        private void ImagesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ImagesListBox.SelectedIndex == -1 || ImagesListBox.SelectedItems.Count > 1)
            {
                return;
            }

            this.myImage = (MyImage)ImagesListBox.SelectedItem;

            RefreshFiguresView();
            myImage.RedrawAll();


        }

        private void ColorsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FiguresComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ShowAreaButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(myImage.SumAreas().ToString());
        }

        private void AhowPerimeterButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(myImage.SumPerimeters().ToString());
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".txt";
            dlg.Filter = "Json (.txt)|*.txt";

            bool? result = dlg.ShowDialog();


            if (result == true)
            {
                string filename = dlg.FileName;
                MyImage newImg = new MyImage(this.MainViewPort);
                newImg.Name = filename.Split('\\').Last();
                newImg.Load(filename);
                Images.Add(newImg);

                this.myImage = newImg;

            }

            myImage.RedrawAll();
            RefreshImagesListBox();
            RefreshFiguresView();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".txt";
            dlg.Filter = "Json (.txt)|*.txt";

            bool? result = dlg.ShowDialog();


            if (result == true)
            {
                string filename = dlg.FileName;
                myImage.Save(filename);
            }
        }

        private void JoinButtonClick(object sender, RoutedEventArgs e)
        {
            if(ImagesListBox.SelectedItems.Count != 2)
            {
                return;
            }

            MyImage fst = (MyImage)ImagesListBox.SelectedItems[0];
            MyImage snd = (MyImage)ImagesListBox.SelectedItems[1];

            foreach(Models.Figure fig in snd.Figures)
            {
                fst.Add(fig);
            }

            Images.Remove(snd);
            
            myImage.RedrawAll();
            RefreshFiguresView();
            RefreshImagesListBox();
            

            
        }



        public void RefreshImagesListBox()
        {
            ImagesListBox.Items.Clear();

            foreach (MyImage img in Images)
            {
                ImagesListBox.Items.Add(img);
            }

            RefreshFiguresView();
        }


        private void ClearPointsButtonClick(object sender, RoutedEventArgs e)
        {
            myImage.Figures = myImage.Figures
                        .Where(f => f.Id != -9)
                        .ToList();
            myImage.RedrawAll();
            RefreshFiguresView();
        }
        





        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            double dl = 0.02;
            double psize = 0.05;

            ClearPointsButtonClick(null, null);

            try
            {
                dl = double.Parse(dlInputTextBox.Text);
            }
            catch { }
           
            

            double perimeter = myImage.TotalPerimeter(dl, psize, (bool)verboseTotalPerimeterCheckBox.IsChecked);
            

            myImage.RedrawAll();
            MessageBox.Show(perimeter.ToString());
        }

        private void ScaleSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            double size;

            if (FiguresListBox.SelectedIndex == -1)
            {
                return;
            }            

            double.TryParse(ScaleTextBox.Text, out size);

            if (size == 0)
            {
                return;
            }

            ((Models.Figure)FiguresListBox.SelectedItem).Scale(size);

            myImage.RedrawAll();
        }




        private void ScaleImageButton_Click(object sender, RoutedEventArgs e)
        {
            double size;

            double.TryParse(ScaleTextBox.Text, out size);

            if(size == 0)
            {
                return;
            }

            myImage.Rescale(size);
            myImage.RedrawAll();
        }

        private void MoveImageButton_Click(object sender, RoutedEventArgs e)
        {
            MoveImage(((Button)sender).Content.ToString());
            myImage.RedrawAll();
            RefreshFiguresView();
        }
        


        private void MoveImage(string dir)
        {
            myImage.Move(MoveImageSpeedSlider.Value, dir);
        }


    }

}
