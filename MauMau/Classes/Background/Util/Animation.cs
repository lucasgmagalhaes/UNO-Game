using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace MauMau.Classes.Background.Util
{
    class Animation
    {
        private DoubleAnimation moveAnimY = new DoubleAnimation();
        private DoubleAnimation moveAnimX = new DoubleAnimation();
        private Canvas container;
        private double animationTime = 100;
        private UIElement mont;
        private Enginee motor;
        public DoubleAnimation MoveAnimY { get { return this.moveAnimY; } }
        public DoubleAnimation MoveAnimX { get { return this.moveAnimY; } }
        public double Timer { get { return this.animationTime; } set { this.animationTime = value; } }
        public Animation(Canvas container)
        {
            this.container = container;
        }
        public Animation(Enginee motor)
        {
            this.motor = motor;
            this.container = motor.Enviroment;
            this.mont = motor.MonteUI;
        }
        public Animation(Canvas container, UIElement mont)
        {
            this.container = container;
            this.mont = mont;
        }
        public Animation(double timer)
        {
            this.animationTime = timer;
        }
        public Animation(Canvas container, double timer)
        {
            this.container = container;
            this.animationTime = timer;
        }

        public void MontToHand(UIElement elementfrom)
        {
            Canvas.SetLeft(elementfrom as UIElement, Canvas.GetLeft(this.mont));
            Canvas.SetTop(elementfrom as UIElement, Canvas.GetTop(this.mont));

            this.container.Children.Add(elementfrom);

            this.moveAnimY.From = Canvas.GetTop(elementfrom);
            this.moveAnimY.To = this.GetCardY();
            this.moveAnimY.Duration = new Duration(TimeSpan.FromMilliseconds(animationTime));

            this.moveAnimX.From = Canvas.GetLeft(elementfrom);
            this.moveAnimX.To = this.GetCardX();
            this.moveAnimX.Duration = new Duration(TimeSpan.FromMilliseconds(animationTime));

            elementfrom.BeginAnimation(Canvas.TopProperty, moveAnimY);
            elementfrom.BeginAnimation(Canvas.LeftProperty, moveAnimX);
        }

        private double GetCardX()
        {
            Player auxplayer = this.motor.GetCurrentPlayer();
            return Canvas.GetLeft(auxplayer.GetLastCard().ElementUI);
        }
        private double GetCardY()
        {
            Player auxplayer = this.motor.GetCurrentPlayer();
            return Canvas.GetTop(auxplayer.GetLastCard().ElementUI);
        }
    }
}
