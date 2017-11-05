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
using MauMau.Classes.Background;
using MauMau.Classes.Background.Estruturas;

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
        private Enginee eng;
        private bool cardsExpanded;
        int count = -100;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            eng = new Enginee(Played);
            List<Player> img = eng.GetPlayers();

            player1.Fill = img[0].Infos.GetImageBrush();
            player1name.Content = img[0].Infos.Name;

            player2.Fill = img[1].Infos.GetImageBrush();
            player2name.Content = img[1].Infos.Name;

            player3.Fill = img[2].Infos.GetImageBrush();
            player3name.Content = img[2].Infos.Name;

            player4.Fill = img[3].Infos.GetImageBrush();
            player4name.Content = img[3].Infos.Name;
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
                    if (cardsExpanded == true)
                    {
                        element = mouseWasDownOn;
                        mousePosition = e.GetPosition(this);
                        element.CaptureMouse();
                        Canvas.SetZIndex(element, 3);
                    }
                    else ExpandPlayerCards();
                }
                else element = null;
            }
        }

        private void ExpandPlayerCards()
        {
            cardsExpanded = true;
        }
        UIElement nexttohide;
        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var mouseon = e.OriginalSource as FrameworkElement;
            if (mouseon != null && mouseon.Name != "btnUNO")
            {
                element = mouseon as UIElement;
                if (element.IsEnabled == true && mouseon.Name != "root" && mouseon.Name != "worldgrid")
                {
                    Canvas.SetZIndex(element, count++);
                    var moveAnimY = new DoubleAnimation(Canvas.GetTop(element), Canvas.GetTop(this.Played), new Duration(TimeSpan.FromMilliseconds(100)));
                    var moveAnimX = new DoubleAnimation(Canvas.GetLeft(element), Canvas.GetLeft(this.Played), new Duration(TimeSpan.FromMilliseconds(100)));
                    element.BeginAnimation(Canvas.TopProperty, moveAnimY);
                    element.BeginAnimation(Canvas.LeftProperty, moveAnimX);
                    if (nexttohide != null) nexttohide.Visibility = Visibility.Collapsed;
                    nexttohide = element;
                    element = null;
                }
            }
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (element != null)
            {
                element.ReleaseMouseCapture();
                if (element != null)
                {
                    Canvas.SetZIndex(element, 0);
                }
                element = null;
            }
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
            if (eng.Monte.Count() > 0)
            {
                Rectangle getcard = eng.GetFromMonte().ElementUI;

                ////getcard.MouseEnter += Getcard_MouseEnter;
                ////getcard.MouseLeave += Getcard_MouseLeave;
                Canvas.SetLeft(getcard as UIElement, Canvas.GetLeft(Mont));
                Canvas.SetTop(getcard as UIElement, Canvas.GetTop(Mont));
                root.Children.Add(getcard);
            }
            else //será chamado o método para reembaralhar
            {
                Mont.IsEnabled = false;
                Mont.Fill = null;
            }
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
                Canvas.SetZIndex(element, count++);
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
