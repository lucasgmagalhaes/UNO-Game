using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace MauMau.Classes.Background
{
    class Animation
    {
        private DoubleAnimation moveanimX;
        private DoubleAnimation moveanimY;
        private int time;

        public DoubleAnimation PointX { get { return this.moveanimX; } }
        public DoubleAnimation PointY { get { return this.moveanimY; } }

        public Animation(int duration)
        {
            this.Init();
            this.time = duration;
        }

        public Animation()
        {
            this.Init();
        }

        private void Init()
        {
            this.moveanimX = new DoubleAnimation();
            this.moveanimY = new DoubleAnimation();
        }

        public void Begin(UIElement elementofrom, UIElement elementto)
        {
            this.moveanimX.From = Canvas.GetLeft(elementofrom);
            this.moveanimX.To = Canvas.GetLeft(elementto);
            this.moveanimX.Duration = new Duration(TimeSpan.FromMilliseconds(this.time));

            this.moveanimY.From = Canvas.GetTop(elementofrom);
            this.moveanimY.To = Canvas.GetTop(elementto);
            this.moveanimY.Duration = new Duration(TimeSpan.FromMilliseconds(this.time));

            moveanimX.FillBehavior = FillBehavior.Stop;
            moveanimY.FillBehavior = FillBehavior.Stop;

            elementofrom.BeginAnimation(Canvas.TopProperty, moveanimY);
            elementofrom.BeginAnimation(Canvas.LeftProperty, moveanimX);

            Canvas.SetLeft(elementofrom, Canvas.GetLeft(elementofrom));
            Canvas.SetTop(elementofrom, Canvas.GetTop(elementofrom));
        }

        public void Begin(UIElement elementofrom, UIElement elementto, int time)
        {
            this.moveanimX.From = Canvas.GetLeft(elementofrom);
            this.moveanimX.To = Canvas.GetLeft(elementto);
            this.moveanimX.Duration = new Duration(TimeSpan.FromMilliseconds(time));

            this.moveanimY.From = Canvas.GetTop(elementofrom);
            this.moveanimY.To = Canvas.GetTop(elementto);
            this.moveanimY.Duration = new Duration(TimeSpan.FromMilliseconds(time));

            moveanimX.FillBehavior = FillBehavior.Stop;
            moveanimY.FillBehavior = FillBehavior.Stop;

            elementofrom.BeginAnimation(Canvas.TopProperty, moveanimY);
            elementofrom.BeginAnimation(Canvas.LeftProperty, moveanimX);

            Canvas.SetLeft(elementofrom, Canvas.GetLeft(elementofrom));
            Canvas.SetTop(elementofrom, Canvas.GetTop(elementofrom));
        }

        public void Begin(UIElement elementofrom, int to)
        {
            this.moveanimX.From = Canvas.GetLeft(elementofrom);
            this.moveanimX.To = to;
            this.moveanimX.Duration = new Duration(TimeSpan.FromMilliseconds(this.time));

            this.moveanimY.From = Canvas.GetTop(elementofrom);
            this.moveanimY.To = to;
            this.moveanimY.Duration = new Duration(TimeSpan.FromMilliseconds(this.time));

            moveanimX.FillBehavior = FillBehavior.Stop;
            moveanimY.FillBehavior = FillBehavior.Stop;

            elementofrom.BeginAnimation(Canvas.TopProperty, moveanimY);
            elementofrom.BeginAnimation(Canvas.LeftProperty, moveanimX);

            Canvas.SetLeft(elementofrom, Canvas.GetLeft(elementofrom));
            Canvas.SetTop(elementofrom, Canvas.GetTop(elementofrom));
        }

        public void Begin(UIElement elementofrom, int to, int time)
        {
            this.moveanimX.From = Canvas.GetLeft(elementofrom);
            this.moveanimX.To = to;
            this.moveanimX.Duration = new Duration(TimeSpan.FromMilliseconds(time));

            this.moveanimY.From = Canvas.GetTop(elementofrom);
            this.moveanimY.To = to;
            this.moveanimY.Duration = new Duration(TimeSpan.FromMilliseconds(time));

            moveanimX.FillBehavior = FillBehavior.Stop;
            moveanimY.FillBehavior = FillBehavior.Stop;

            elementofrom.BeginAnimation(Canvas.TopProperty, moveanimY);
            elementofrom.BeginAnimation(Canvas.LeftProperty, moveanimX);

            Canvas.SetLeft(elementofrom, Canvas.GetLeft(elementofrom));
            Canvas.SetTop(elementofrom, Canvas.GetTop(elementofrom));
        }
    }
}
