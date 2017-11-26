using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MauMau.Classes.Background;
using System.Threading;
using MauMau.Windows;
using MauMau.Classes.Background.Util;
using MauMau.Classes.Background.Enum;

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

            this.selectblue.Opacity = 0;
            this.selectgreen.Opacity = 0;
            this.selectred.Opacity = 0;
            this.selectyellow.Opacity = 0;
        }

        private Point mousePosition = new Point();
        private UIElement element;
        private Enginee eng;
        public static int count = -90;
        private UIElement next;
        private Evento evento;
        //Variáveis usadas para voltar o elemento para a antiga posição
        private double backupleft;
        private double backuptop;
        private DoubleAnimation moveAnimY = new DoubleAnimation();
        private DoubleAnimation moveAnimX = new DoubleAnimation();
        private Animation anim;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.eng = new Enginee(this.played, this.root, this.Mont);
            this.evento = new Evento(this.eng);
            Animation.SetColorOptionEllipses(this.selectred, this.selectgreen, this.selectblue, this.selectyellow);
            moveAnimX.Completed += MoveAnimX_Completed;
            this.anim = new Animation(this.eng);
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
                    if (this.eng.ValidatePlay(element))
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
                    else
                    {
                        this.BackCardToHand(element);
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

            this.eng.GetCurrentPlayer().SayUno();
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
                this.eng.RealignCards();
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
                if (this.eng.GetCurrentPlayer().GetHand().Count > 1)
                {
                    Carta card = this.eng.GetCardFromUI(this.eng.GetCurrentPlayer(), element);
                    if (eng.ValidatePlay(element))
                    {
                        eng.ColapseElement(element);
                        Canvas.SetZIndex(element, count++);
                        this.eng.RealignCards();                     
                        element = null;
                        if (card != null)
                        {
                            if (this.evento.EventAtivado(card))
                            {
                                this.eng.EndTurn();
                            }

                        } 
                    }
                    else
                    {
                        this.BackCardToHand(element);
                    }
                }
                else if (this.eng.GetCurrentPlayer().Uno)
                {
                    MessageBox.Show("VOCÊ GANHOU!!");
                    eng.ColapseElement(element);
                    Canvas.SetZIndex(element, count++);
                    element = null;
                }
                else //!this.eng.GetCurrentPlayer().Uno
                {
                    this.BackCardToHand(element);
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

        private void BackCardToHand(UIElement cardJogada) // true se pode fazer a jogada, ou seja , compara carta que está jogando com a carta do top do coletor
        {
            if (this.backupleft != Canvas.GetLeft(cardJogada) || this.backuptop != Canvas.GetTop(cardJogada))
            {
                this.moveAnimY.From = Canvas.GetTop(cardJogada);
                this.moveAnimY.To = this.backuptop;
                this.moveAnimY.Duration = new Duration(TimeSpan.FromMilliseconds(100));

                this.moveAnimX.From = Canvas.GetLeft(cardJogada);
                this.moveAnimX.To = this.backupleft;
                this.moveAnimX.Duration = new Duration(TimeSpan.FromMilliseconds(100));

                cardJogada.BeginAnimation(Canvas.TopProperty, moveAnimY);
                cardJogada.BeginAnimation(Canvas.LeftProperty, moveAnimX);
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

        private void SendCardToHand(Carta card, double left, double top)
        {
            DoubleAnimation moveAnimY = new DoubleAnimation(Canvas.GetTop(card.ElementUI), top, new Duration(TimeSpan.FromMilliseconds(100)));
            DoubleAnimation moveAnimX = new DoubleAnimation(Canvas.GetLeft(card.ElementUI), left + 40, new Duration(TimeSpan.FromMilliseconds(100)));

            moveAnimX.FillBehavior = FillBehavior.Stop;
            moveAnimY.FillBehavior = FillBehavior.Stop;

            card.ElementUI.BeginAnimation(Canvas.TopProperty, moveAnimY);
            card.ElementUI.BeginAnimation(Canvas.LeftProperty, moveAnimX);

            Canvas.SetLeft(card.ElementUI, left + 40);
            Canvas.SetTop(card.ElementUI, top);
        }

        private void MoveAnimX_Completed(object sender, EventArgs e)
        {
            if (this.eng.GetCurrentPlayer().GetHand().Count > 1)
            {
                if (this.element != null)
                {
                    eng.ColapseElement(element);
                    Canvas.SetZIndex(element, ++count);
                    element = null;
                    this.eng.EndTurn();
                    this.next = element;
                }
            }
            else if (!this.eng.GetCurrentPlayer().Uno)
            {
                Carta getcard = eng.RemoveFromMonte();
                Rectangle cardUI = getcard.ElementUI;
                Canvas.SetLeft(cardUI as UIElement, Canvas.GetLeft(this.Mont));
                Canvas.SetTop(cardUI as UIElement, Canvas.GetTop(this.Mont));

                this.root.Children.Add(cardUI);
                this.SendCardToHand(getcard, this.backupleft, this.backuptop);
                Player current = eng.GetCurrentPlayer();
                current.AddCardToHand(getcard);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.L)
            {
                ScreenLog log = new ScreenLog();
                log.Show();
            }
        }
        private void SetColor(Cor color)
        {
            this.eng.ColorChosen = color;
            this.anim.HidePaletColors();
            this.eng.EndTurn();
        }
        private void selectyellow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.SetColor(Cor.Amarelo);
        }

        private void selectblue_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.SetColor(Cor.Azul);
        }

        private void selectgreen_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.SetColor(Cor.Verde);
        }

        private void selectred_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.SetColor(Cor.Vermelho);
        }
    }
}
