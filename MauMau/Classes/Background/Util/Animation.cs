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

        private DoubleAnimation interanimation1 = new DoubleAnimation();
        private DoubleAnimation interanimation2 = new DoubleAnimation();
        private DoubleAnimation interanimation3 = new DoubleAnimation();
        private DoubleAnimation interanimation4 = new DoubleAnimation();

        private static Ellipse optionred;
        private static Ellipse optionyellow;
        private static Ellipse optionblue;
        private static Ellipse optiongreen;

        private Canvas container;
        private double animationTime = 100;
        private UIElement mont;
        private Enginee motor;

        public DoubleAnimation MoveAnimY { get { return this.moveAnimY; } }
        public DoubleAnimation MoveAnimX { get { return this.moveAnimY; } }
        public double Timer { get { return this.animationTime; } set { this.animationTime = value; } }
        public DoubleAnimation RotateAnimation { get { return this.rotateAnimation; } }
        public RotateTransform RotateAngle { get { return this.rotateAngle; } }

        private double optionColorTimer = 200;

        private void Init()
        {
            this.interanimation1.Completed += Interanimation1_Completed;
            this.interanimation2.Completed += Interanimation2_Completed;
            this.interanimation3.Completed += Interanimation3_Completed;
            this.interanimation4.Completed += Interanimation4_Completed;
        }

        private void ShowColorOptions()
        {
            this.interanimation1.From = this.interanimation2.From = this.interanimation3.From = this.interanimation4.From = 0;
            this.interanimation1.To = this.interanimation2.To = this.interanimation3.To = this.interanimation4.To = 300;

            this.interanimation1.Duration = this.interanimation2.Duration = this.interanimation3.Duration = this.interanimation4.Duration = new Duration(TimeSpan.FromMilliseconds(optionColorTimer));
        }

        private void ShowYellow()
        {
            optionyellow.BeginAnimation(Canvas.TopProperty, this.interanimation1);
        }

        private void ShowBlue()
        {
            optionblue.BeginAnimation(Canvas.TopProperty, this.moveAnimY);
        }

        private void ShowGreen()
        {
            optiongreen.BeginAnimation(Canvas.TopProperty, this.moveAnimY);
        }

        private void ShowRed()
        {
            optionred.BeginAnimation(Canvas.TopProperty, this.moveAnimY);
        }

        private void Interanimation4_Completed(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Interanimation3_Completed(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Interanimation2_Completed(object sender, EventArgs e)
        {
            this.ShowGreen();
        }

        private void Interanimation1_Completed(object sender, EventArgs e)
        {
            this.ShowBlue();
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
        private void ConvertCoordnatesToValueRight(ref double X, ref double Y, PlayerPosition destiny)
        {
            double auxX = this.motor.ScreenSizeX;
            double auxY = this.motor.ScreenSizeY;

            switch (destiny)
            {
                case PlayerPosition.Bottom:
                    X = 700+ this.bottomCardPointCount;
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
