using MauMau.Classes.Background.Enum;
using MauMau.Classes.Background.Estruturas;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MauMau.Classes.Background.Util
{
    class Animation
    {
        private DoubleAnimation moveAnimY = new DoubleAnimation();
        private DoubleAnimation moveAnimX = new DoubleAnimation();
        private DoubleAnimation rotateAnimation = new DoubleAnimation();
        private RotateTransform rotateAngle = new RotateTransform();

        private int leftCardPointCount = 30;
        private int topCardPointCount = 30;
        private int rightCardPointCount = 30;
        private int bottomCardPointCount = 30;

        private static Ellipse optionred;
        private static Ellipse optionyellow;
        private static Ellipse optionblue;
        private static Ellipse optiongreen;

        private bool optionredIsVisible;
        private bool optionyellowIsVisible;
        private bool optionblueIsVisible;
        private bool optiongreenIsVisible;

        private Canvas container;
        private double animationTime = 100;
        private UIElement mont;
        private Enginee motor;

        public DoubleAnimation MoveAnimY { get { return this.moveAnimY; } }
        public DoubleAnimation MoveAnimX { get { return this.moveAnimY; } }
        public double Timer { get { return this.animationTime; } set { this.animationTime = value; } }
        public DoubleAnimation RotateAnimation { get { return this.rotateAnimation; } }
        public RotateTransform RotateAngle { get { return this.rotateAngle; } }

        private void Init()
        {
            this.moveAnimY.Completed += MoveAnimY_Completed;
        }

        private void MoveAnimY_Completed(object sender, EventArgs e)
        {
            if (!optionyellowIsVisible)
            {
                this.ShowEllipeColor(optionyellow);
                this.optionyellowIsVisible = true;
            }
            else if (!optionblueIsVisible)
            {
                this.ShowEllipeColor(optionblue);
                this.optionblueIsVisible = true;
            }
            else if (!optiongreenIsVisible)
            {
                this.ShowEllipeColor(optiongreen);
                this.optiongreenIsVisible = true;
            }
            else if (!optionredIsVisible)
            {
                this.ShowEllipeColor(optionred);
                this.optionredIsVisible = true;
            }
        }

        public Animation(Canvas container)
        {
            this.container = container;
            this.Init();
        }

        public Animation(Enginee motor)
        {
            this.motor = motor;
            this.container = motor.Enviroment;
            this.mont = motor.MonteUI;
            this.Init();
        }
        public Animation(Enginee motor, double timer)
        {
            this.motor = motor;
            this.container = motor.Enviroment;
            this.mont = motor.MonteUI;
            this.animationTime = timer;
            this.Init();
        }

        public Animation(Canvas container, UIElement mont)
        {
            this.container = container;
            this.mont = mont;
            this.Init();
        }

        public Animation(Canvas container, double timer)
        {
            this.container = container;
            this.animationTime = timer;
            this.Init();
        }

        public Animation(double timer)
        {
            this.animationTime = timer;
        }

        public static void SetColorOptionEllipses(Ellipse red, Ellipse green, Ellipse blue, Ellipse yellow)
        {
            optionblue = blue;
            optiongreen = green;
            optionred = red;
            optionyellow = yellow;
        }

        private double GetCardX(Player who)
        {
            Carta aux = who.GetLastCard();
            return Canvas.GetLeft(aux.ElementUI);
        }

        private double GetCardY(Player who)
        {
            Carta aux = who.GetLastCard();
            return Canvas.GetTop(aux.ElementUI);
        }
        /// <summary>
        /// Cria a animação da carta indo do 'monte' até a mão do jogador
        /// </summary>
        /// <param name="elementfrom"></param>
        public void MontToHand(UIElement elementfrom, Player who)
        {
            Canvas.SetLeft(elementfrom, Canvas.GetLeft(this.mont));
            Canvas.SetTop(elementfrom, Canvas.GetTop(this.mont));

            this.container.Children.Add(elementfrom);

            double X = this.GetCardX(who);
            double Y = this.GetCardY(who);
            this.ConvertCoordnatesToValueRight(ref X, ref Y, who.Position);

            this.moveAnimY.From = Canvas.GetTop(elementfrom);
            this.moveAnimY.To = Y;
            this.moveAnimY.Duration = new Duration(TimeSpan.FromMilliseconds(animationTime));

            this.moveAnimX.From = Canvas.GetLeft(elementfrom);
            this.moveAnimX.To = X;
            this.moveAnimX.Duration = new Duration(TimeSpan.FromMilliseconds(animationTime));

            this.moveAnimY.FillBehavior = FillBehavior.Stop;
            this.moveAnimX.FillBehavior = FillBehavior.Stop;

            elementfrom.BeginAnimation(Canvas.TopProperty, this.moveAnimY);
            elementfrom.BeginAnimation(Canvas.LeftProperty, this.moveAnimX);

            Canvas.SetLeft(elementfrom, X);
            Canvas.SetTop(elementfrom, Y);
        }
        public void ShowPaletColors()
        {
            this.ShowEllipeColor(optionyellow);
        }
        public void HidePaletColors()
        {
            this.HideEllipseColors(optionyellow);
        }
        private void HideEllipseColors(UIElement elipse)
        {
            double auxY = this.motor.ScreenSizeY;
            elipse.Opacity = 1;
            Canvas.SetZIndex(elipse, 10);
            this.moveAnimY.From = Canvas.GetTop(elipse);
            this.moveAnimY.To = auxY;
            this.moveAnimY.Duration = new Duration(TimeSpan.FromMilliseconds(300));

            this.moveAnimY.FillBehavior = FillBehavior.Stop;

            elipse.BeginAnimation(Canvas.TopProperty, this.moveAnimY);

            Canvas.SetTop(elipse, auxY);
        }
        private void ShowEllipeColor(UIElement elipse)
        {
            double auxY = this.motor.ScreenSizeY;
            auxY = auxY - 300;
            elipse.Opacity = 1;
            Canvas.SetZIndex(elipse, 10);
            this.moveAnimY.From = Canvas.GetTop(elipse);
            this.moveAnimY.To = auxY;
            this.moveAnimY.Duration = new Duration(TimeSpan.FromMilliseconds(300));

            this.moveAnimY.FillBehavior = FillBehavior.Stop;

            elipse.BeginAnimation(Canvas.TopProperty, this.moveAnimY);

            Canvas.SetTop(elipse, auxY);
        }
        private void ConvertCoordnatesToValueRight(ref double X, ref double Y, PlayerPosition destiny)
        {
            double auxX = this.motor.ScreenSizeX;
            double auxY = this.motor.ScreenSizeY;

            switch (destiny)
            {
                case PlayerPosition.Bottom:
                    X = 710 + this.bottomCardPointCount;
                    Y = auxY - 233;
                    this.bottomCardPointCount += 30;
                    break;
                case PlayerPosition.Left:
                    X = 50;
                    Y = 380 + this.leftCardPointCount;
                    this.leftCardPointCount += 30;
                    break;
                case PlayerPosition.Right:
                    X = auxX - 161;
                    Y = 380 + this.rightCardPointCount;
                    this.rightCardPointCount += 30;
                    break;
                case PlayerPosition.Top:
                    X = 710 + this.topCardPointCount;
                    Y = 26;
                    this.topCardPointCount += 30;
                    break;
            }
        }
        /// <summary>
        /// Cria a animação da carta rodando até o angulo correspondente ao da posição do jogador no jogo
        /// </summary>
        /// <param name="card"></param>
        /// <param name="to"></param>
        public void RotateToHand(Carta card, Player to)
        {
            switch (to.Position)
            {
                case PlayerPosition.Left:
                    this.rotateAnimation.From = 0;
                    this.rotateAnimation.To = -90;
                    this.rotateAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(100));

                    card.ElementUI.RenderTransform = this.rotateAngle;
                    card.ElementUI.RenderTransformOrigin = new Point(0.5, 0.5);
                    this.rotateAngle.BeginAnimation(RotateTransform.AngleProperty, this.RotateAnimation);
                    break;

                case PlayerPosition.Right:
                    this.rotateAnimation.From = 0;
                    this.rotateAnimation.To = 90;
                    this.rotateAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(100));

                    card.ElementUI.RenderTransform = this.rotateAngle;
                    card.ElementUI.RenderTransformOrigin = new Point(0.5, 0.5);
                    this.rotateAngle.BeginAnimation(RotateTransform.AngleProperty, this.RotateAnimation);
                    break;

                case PlayerPosition.Top:
                    this.rotateAnimation.From = 0;
                    this.rotateAnimation.To = 180;
                    this.rotateAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(100));

                    card.ElementUI.RenderTransform = this.rotateAngle;
                    card.ElementUI.RenderTransformOrigin = new Point(0.5, 0.5);
                    this.rotateAngle.BeginAnimation(RotateTransform.AngleProperty, this.RotateAnimation);
                    break;
            }
        }
    }
}
