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
using MauMau.Classes.Graphics;
using MauMau.Classes.Background;

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
        private UIEnginee eng;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            eng = new UIEnginee(Played);
            List<UIPlayer> img = eng.GetRandom(3);
            player1.Fill = new ImageBrush(img[0].GetImageSource());
            player1name.Content = img[0].Name;

            player2.Fill = new ImageBrush(img[1].GetImageSource());
            player2name.Content = img[1].Name;

            player4.Fill = new ImageBrush(img[2].GetImageSource());
            player4name.Content = img[2].Name;

            Baralho B = new Baralho();
        }

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
                if (mouseWasDownOn.Name != "Mont" && mouseWasDownOn.Name != "Played")
                {
                    element = mouseWasDownOn;
                    mousePosition = e.GetPosition(this);
                    element.CaptureMouse();
                    Canvas.SetZIndex(element, -1);
                }
                else element = null;
            }
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (element != null)
            {
                element.ReleaseMouseCapture();
                element = null;
            }
        }
        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {

        }
        private void btnUNO_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
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
                ImageSource = new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/Cartas/azul/0blue.jpg", UriKind.Absolute))
            };
            getcard.RadiusX = 10;
            getcard.RadiusY = 10;
            getcard.Height = 180;
            getcard.Width = 114;
            getcard.Name = "newcard";

            //getcard.MouseEnter += Getcard_MouseEnter;
            //getcard.MouseLeave += Getcard_MouseLeave;
            Canvas.SetLeft(getcard as UIElement, Canvas.GetLeft(Mont));
            Canvas.SetTop(getcard as UIElement, Canvas.GetTop(Mont));
            Canvas.SetZIndex(getcard as UIElement, 0);
            root.Children.Add(getcard);
        }

        //private void Getcard_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    var ee = Mouse.DirectlyOver as UIElement;
        //    if (ee.IsMouseOver && element != null)
        //    {
        //        ElementAnimationLeave(element);
        //        element = null;
        //    }
        //}

        private void Getcard_MouseEnter(object sender, MouseEventArgs e)
        {
            //var ee = Mouse.DirectlyOver as UIElement;
            //element = ee;
            // if (ee.IsMouseOver) ElementAnimationHover(element);
        }

        private void blockplayedborder_MouseEnter(object sender, MouseEventArgs e)
        {
            if (element != null)
            {
                eng.ColapseElement(element);
                Canvas.SetZIndex(element,-3);
                element = null;
            }
        }
        public void ElementAnimationHover(UIElement element)
        {
            var moveAnimY = new DoubleAnimation(Canvas.GetTop(element), Canvas.GetTop(element) - 40, new Duration(TimeSpan.FromMilliseconds(100)));
            element.BeginAnimation(Canvas.TopProperty, moveAnimY);
        }
        public void ElementAnimationLeave(UIElement element)
        {
            var moveAnimY = new DoubleAnimation(Canvas.GetTop(element), Canvas.GetTop(element) + 40, new Duration(TimeSpan.FromMilliseconds(100)));
            element.BeginAnimation(Canvas.TopProperty, moveAnimY);
        }
    }
}
