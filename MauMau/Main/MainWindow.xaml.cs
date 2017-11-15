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
        public static int count = -90;
        private UIElement next;
        //Variáveis usadas para voltar o elemento para a antiga posição
        private double backupleft;
        private double backuptop;
        private DoubleAnimation moveAnimY = new DoubleAnimation();
        DoubleAnimation moveAnimX = new DoubleAnimation();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.eng = new Enginee(this.played, this.root, this.Mont);

            moveAnimX.Completed += MoveAnimX_Completed;
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
                //else element = null;
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
                    this.CreatePositionBackup(this.element);
                    if (ValidarJogada(element))
                    {
                        this.moveAnimY.From = Canvas.GetTop(element);
                        this.moveAnimY.To = Canvas.GetTop(this.played);
                        this.moveAnimY.Duration = new Duration(TimeSpan.FromMilliseconds(100));

                        this.moveAnimX.From = Canvas.GetLeft(element);
                        this.moveAnimX.To = Canvas.GetLeft(this.played);
                        this.moveAnimX.Duration = new Duration(TimeSpan.FromMilliseconds(100));

                        this.moveAnimX.FillBehavior = FillBehavior.Stop;
                        this.moveAnimY.FillBehavior = FillBehavior.Stop;

                        this.element.BeginAnimation(Canvas.TopProperty, this.moveAnimY);
                        this.element.BeginAnimation(Canvas.LeftProperty, this.moveAnimX);

                        Canvas.SetLeft(this.element, Canvas.GetLeft(this.played));
                        Canvas.SetTop(this.element, Canvas.GetTop(this.played));

                        if (next != null) //Cambiarra :(
                        {
                            Thread.Sleep(100);
                            this.next.Visibility = Visibility.Collapsed;
                        }
                        if (eng.GetCurrentPlayer().Hand.Count == 0)
                        {
                            MessageBox.Show("VOCÊ GANHOU!!");
                        }                        
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
                    if (this.backupleft != Canvas.GetLeft(this.element) || this.backuptop != Canvas.GetTop(this.element))
                    {
                        var moveAnimY = new DoubleAnimation(Canvas.GetTop(this.element), this.backuptop, new Duration(TimeSpan.FromMilliseconds(100)));
                        var moveAnimX = new DoubleAnimation(Canvas.GetLeft(this.element), this.backupleft, new Duration(TimeSpan.FromMilliseconds(100)));

                        moveAnimX.FillBehavior = FillBehavior.Stop;
                        moveAnimY.FillBehavior = FillBehavior.Stop;

                        this.element.BeginAnimation(Canvas.TopProperty, moveAnimY);
                        this.element.BeginAnimation(Canvas.LeftProperty, moveAnimX);

                        Canvas.SetLeft(this.element, this.backupleft);
                        Canvas.SetTop(this.element, this.backuptop);
                    }
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
                Player current = eng.GetCurrentPlayer();
                current.AddCardToHand(getcard);
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
                if (ValidarJogada(element))
                {
                    eng.ColapseElement(element);
                    Canvas.SetZIndex(element, count++);
                    element = null;
                    this.eng.EndTurn();

                    if (eng.GetCurrentPlayer().Hand.Count == 0)
                    {
                        MessageBox.Show("VOCÊ GANHOU!!");
                    }
                }
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
            Carta aux = this.eng.GetCurrentPlayer().GetLastCard();

            DoubleAnimation moveAnimY = new DoubleAnimation(Canvas.GetTop(card.ElementUI), Canvas.GetTop(aux.ElementUI), new Duration(TimeSpan.FromMilliseconds(100)));
            DoubleAnimation moveAnimX = new DoubleAnimation(Canvas.GetLeft(card.ElementUI), Canvas.GetLeft(aux.ElementUI) + 40, new Duration(TimeSpan.FromMilliseconds(100)));

            moveAnimX.FillBehavior = FillBehavior.Stop;
            moveAnimY.FillBehavior = FillBehavior.Stop;

            card.ElementUI.BeginAnimation(Canvas.TopProperty, moveAnimY);
            card.ElementUI.BeginAnimation(Canvas.LeftProperty, moveAnimX);

            Canvas.SetLeft(card.ElementUI, Canvas.GetLeft(aux.ElementUI) + 40);
            Canvas.SetTop(card.ElementUI, Canvas.GetTop(aux.ElementUI));
        }

        private void MoveAnimX_Completed(object sender, EventArgs e)
        {
            eng.ColapseElement(element);
            Canvas.SetZIndex(element, ++count);
            element = null;
            this.eng.EndTurn();
            this.next = element;
        }
    }
}
