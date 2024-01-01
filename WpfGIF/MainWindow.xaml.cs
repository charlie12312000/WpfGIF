using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace WpfGIF
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    /// 



    public partial class MainWindow : Window
    {
        public List<System.Windows.Controls.Image> limg = new List<System.Windows.Controls.Image>();
        private string lpath = Directory.GetCurrentDirectory() + ConfigurationManager.AppSettings["picpath"];
        private double timepitch = double.Parse(ConfigurationManager.AppSettings["timepitch"]);
        private System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        int c = 0;

        public MainWindow()
        {
            InitializeComponent();

            //System.Windows.Controls.Image image = new System.Windows.Controls.Image();
            //image.Source = new BitmapImage(new Uri(@"D:\C\GifProgram\pics\frame0000.png", UriKind.Relative));

            //VImage.Source = new BitmapImage(new Uri(@"D:\C\GifProgram\pics\frame0000.png", UriKind.Absolute));

            DirectoryInfo di = new DirectoryInfo(lpath);
            FileInfo[] fis = di.GetFiles("*.png");
            foreach (FileInfo fi in fis)
            {
                System.Windows.Controls.Image image = new System.Windows.Controls.Image();
                image.Source = new BitmapImage(new Uri(fi.FullName, UriKind.Absolute));
                limg.Add(image);
            }



            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            dispatcherTimer.Start();
        }


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            VImage.Source = limg[c].Source;
            c++;
            c = c > 15 ? 0 : c;
        }

        private void VImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (e.Button == MouseButtons.Left)
            //{
            //    this.curr_x = e.X;
            //    this.curr_y = e.Y;
            //    this.isWndMove = true;
            //}
        }

        private void VImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //this.isWndMove = false;
        }

        private void VImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void VImage_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Vform_KeyDown(object sender, KeyEventArgs e)
        {
            var test = e.Key;
            if (e.Key.ToString() == "Add")
            {
                double mini = dispatcherTimer.Interval.TotalMilliseconds - timepitch;
                if (mini < 1)
                    dispatcherTimer.Interval = new TimeSpan(0,0,0,0, (int)timepitch);
                else
                    dispatcherTimer.Interval = new TimeSpan(0,0,0,0,(int)mini);
            }
            if (e.Key.ToString() == "Subtract")
            {
                double mini = dispatcherTimer.Interval.TotalMilliseconds + timepitch;
                if (mini > 1000)
                    dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
                else
                    dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, (int)mini);
            }

            if (e.Key.ToString() == "Escape")
            {
                this.Close();
            }
        }
    }




}
