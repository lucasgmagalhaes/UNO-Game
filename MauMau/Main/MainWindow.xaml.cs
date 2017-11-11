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
using System.Threading;

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
        private int count = -90;
        private UIElement next;
        //Variáveis usadas para voltar o elemento para a antiga posição
        private double backupleft;
        private double backuptop;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            eng = new Enginee(played, root);
            Lista<Player> img = eng.GetPlayers();

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
                if (mouseWasDownOn is Rectangle && mouseWasDownOn.IsEnabled)
                {
                    this.element = mouseWasDownOn;
                    this.CreatePositionBackup(element);
                    mousePosition = e.GetPosition(this);
                    this.element.CaptureMouse();
                    Canvas.SetZIndex(element, 3);
                }
                else element = null;
            }
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var mouseon = e.OriginalSource as FrameworkElement;
            if (mouseon != null && mouseon.Name != "btnUNO")
            {
                this.element = mouseon as UIElement;
                if (this.element.IsEnabled && mouseon is Rectangle)
                {
                    Canvas.SetZIndex(this.element, count++);
                    this.CreatePositionBackup(this.element);
                    if (ValidarJogada(element))
                    {
                        var moveAnimY = new DoubleAnimation(Canvas.GetTop(element), Canvas.GetTop(this.played), new Duration(TimeSpan.FromMilliseconds(100)));
                        var moveAnimX = new DoubleAnimation(Canvas.GetLeft(element), Canvas.GetLeft(this.played), new Duration(TimeSpan.FromMilliseconds(100)));
                        this.element.BeginAnimation(Canvas.TopProperty, moveAnimY);
                        this.element.BeginAnimation(Canvas.LeftProperty, moveAnimX);
                        if (next != null) //Cambiarra :(
                        {
                            Thread.Sleep(100);
                            this.next.Visibility = Visibility.Collapsed;
                        }
                        this.next = element;
                        this.element = null;
                    }
                }
            }
        }
        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.element != null)
            {
                this.element.ReleaseMouseCapture();
                if (this.element != null)
                {
                    Canvas.SetZIndex(element, 0);
                }
                this.element = null;
            }
        }

        private void btnUNO_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation
                (0, 360, new Duration(TimeSpan.FromMilliseconds(400)));
            RotateTransform rt = new RotateTransform();
            this.btnUNO.RenderTransform = rt;
            this.btnUNO.RenderTransformOrigin = new Point(0.5, 0.5);
            rt.BeginAnimation(RotateTransform.AngleProperty, da);
        }

        private void Mont_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.eng.Monte.Count() > 0)
            {
                Carta getcard = eng.RemoveFromMonte();
                Rectangle cardUI = getcard.ElementUI;
                Canvas.SetLeft(cardUI as UIElement, Canvas.GetLeft(this.Mont));
                Canvas.SetTop(cardUI as UIElement, Canvas.GetTop(this.Mont));

                this.root.Children.Add(cardUI);
                this.SendCardToHand(getcard);
                eng.GetCurrentPlayer().AddCardToHand(getcard);
            }
            else //será chamado o método para reembaralhar
            {
                this.Mont.IsEnabled = false;
                this.Mont.Fill = null;
            }
        }

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

        private void CreatePositionBackup(UIElement element)
        {
            this.backupleft = Canvas.GetLeft(element);
            this.backuptop = Canvas.GetTop(element);
        }

        private bool ValidarJogada(UIElement cardJogada) // true se pode fazer a jogada, ou seja , compara carta que está jogando com a carta do top do coletor
        {
            if (eng.ValidatePlay(cardJogada))
            {
                eng.EndTurn();
                return true;
            }
            else
            {
                if (this.backupleft != Canvas.GetLeft(cardJogada) || this.backuptop != Canvas.GetTop(cardJogada))
                {
                    var moveAnimY = new DoubleAnimation(Canvas.GetTop(cardJogada), this.backuptop, new Duration(TimeSpan.FromMilliseconds(100)));
                    var moveAnimX = new DoubleAnimation(Canvas.GetLeft(cardJogada), this.backupleft, new Duration(TimeSpan.FromMilliseconds(100)));
                    cardJogada.BeginAnimation(Canvas.TopProperty, moveAnimY);
                    cardJogada.BeginAnimation(Canvas.LeftProperty, moveAnimX);
                }
                return false;
            }
        }

        private void SendCardToHand(Carta card)
        {
            Carta aux = eng.GetCurrentPlayer().GetLastCard();
            var moveAnimY = new DoubleAnimation(Canvas.GetTop(card.ElementUI), Canvas.GetTop(aux.ElementUI), new Duration(TimeSpan.FromMilliseconds(100)));
            var moveAnimX = new DoubleAnimation(Canvas.GetLeft(card.ElementUI), Canvas.GetLeft(aux.ElementUI) + 40, new Duration(TimeSpan.FromMilliseconds(100)));
            card.ElementUI.BeginAnimation(Canvas.TopProperty, moveAnimY);
            card.ElementUI.BeginAnimation(Canvas.LeftProperty, moveAnimX);
        }
    }
}
