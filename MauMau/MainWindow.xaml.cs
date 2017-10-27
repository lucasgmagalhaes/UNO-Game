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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MauMau
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private Point mousePosition = new Point();
        private UIElement element;
        private UIElement nowcreated;
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && element != null)
            {
                Point temp = e.GetPosition(this);
                Point to = new Point(mousePosition.X - temp.X, mousePosition.Y - temp.Y);

                Canvas.SetLeft(element, Canvas.GetLeft(element) - to.X);
                Canvas.SetTop(element, Canvas.GetTop(element) - to.Y);

                mousePosition = temp;
            }
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mouseWasDownOn = e.Source as FrameworkElement;

            if (mouseWasDownOn != null)
            {
                if (nowcreated != null)
                {
                    element = nowcreated;
                    mousePosition = e.GetPosition(this);
                    element.CaptureMouse();
                    nowcreated = null;
                }
                else
                {
                    if (mouseWasDownOn.Name != "Mont" && mouseWasDownOn.Name != "Played")
                    {
                        element = mouseWasDownOn;
                        mousePosition = e.GetPosition(this);
                        element.CaptureMouse();
                    }
                    else element = null;

                }
            }
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (element != null)
            {
                element.ReleaseMouseCapture();

                double fixLEFT = Canvas.GetLeft(Played as UIElement);
                double fixTOP = Canvas.GetTop(Played as UIElement);

                double cardLEFT = Canvas.GetLeft(element);
                double cardTOP = Canvas.GetTop(element);

                if(fixLEFT - cardLEFT > -30 && fixLEFT - cardLEFT < 50
                    || fixLEFT - cardLEFT < 50 && fixLEFT - cardLEFT > -30
                    && fixTOP - cardTOP > -30 && fixTOP - cardTOP < 50
                    || fixTOP - cardTOP < 50 && fixTOP - cardTOP > -30)
                {
                    Canvas.SetLeft(element, fixLEFT);
                    Canvas.SetTop(element, fixTOP);
                }
            }
        }
        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            var ee = Mouse.DirectlyOver as UIElement;
            if (ee.IsMouseOver)
            {
                Canvas.SetTop(ee, Canvas.GetTop(ee) + 40);
            }
        }
        private void btnUNO_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //btnUNO.Opacity = 0.5;
            DoubleAnimation da = new DoubleAnimation
                (0, 360, new Duration(TimeSpan.FromMilliseconds(400)));
            RotateTransform rt = new RotateTransform();
            btnUNO.RenderTransform = rt;
            btnUNO.RenderTransformOrigin = new Point(0.5, 0.5);
            rt.BeginAnimation(RotateTransform.AngleProperty, da);
        }

        private void Mont_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle getcard = new Rectangle();
            getcard.Fill = new ImageBrush
            {
               ImageSource = new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/azul/0blue.jpg", UriKind.Absolute))
            };
            getcard.RadiusX = 10;
            getcard.RadiusY = 10;
            getcard.Height = 180;
            getcard.Width = 114;
            getcard.Name = "newcard";

            Canvas.SetLeft(getcard as UIElement, Canvas.GetLeft(Mont));
            Canvas.SetTop(getcard as UIElement, Canvas.GetTop(Mont));
            root.Children.Add(getcard);
            nowcreated = getcard;
            Window_MouseLeftButtonDown(sender, e);
        }
    }
}
