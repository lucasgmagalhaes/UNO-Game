using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace MauMau.Classes.Background
{
    class Animation
    {
        private Turno turno;

        public Animation(Turno turno_obj)
        {
            this.turno = turno_obj;
        }
        private Action method;
        public void SetTopAnimationCompletEvent(Action method)
        {

        }
        public void SendCardToHand(Carta card)
        {
            Carta aux = this.turno.GetCurrentPlayer().GetLastCard();

            DoubleAnimation moveAnimY = new DoubleAnimation(Canvas.GetTop(card.ElementUI), Canvas.GetTop(aux.ElementUI), new Duration(TimeSpan.FromMilliseconds(100)));
            DoubleAnimation moveAnimX = new DoubleAnimation(Canvas.GetLeft(card.ElementUI), Canvas.GetLeft(aux.ElementUI) + 40, new Duration(TimeSpan.FromMilliseconds(100)));

            moveAnimX.FillBehavior = FillBehavior.Stop;
            moveAnimY.FillBehavior = FillBehavior.Stop;

            card.ElementUI.BeginAnimation(Canvas.TopProperty, moveAnimY);
            card.ElementUI.BeginAnimation(Canvas.LeftProperty, moveAnimX);

            Canvas.SetLeft(card.ElementUI, Canvas.GetLeft(aux.ElementUI) + 40);
            Canvas.SetTop(card.ElementUI, Canvas.GetTop(aux.ElementUI));
        }

        public void BackCardToHand(UIElement cardJogada, double backupleft, double backuptop) // true se pode fazer a jogada, ou seja , compara carta que está jogando com a carta do top do coletor
        {
            if (backupleft != Canvas.GetLeft(cardJogada) || backuptop != Canvas.GetTop(cardJogada))
            {
                var moveAnimY = new DoubleAnimation(Canvas.GetTop(cardJogada), backuptop, new Duration(TimeSpan.FromMilliseconds(100)));
                var moveAnimX = new DoubleAnimation(Canvas.GetLeft(cardJogada), backupleft, new Duration(TimeSpan.FromMilliseconds(100)));
                cardJogada.BeginAnimation(Canvas.TopProperty, moveAnimY);
                cardJogada.BeginAnimation(Canvas.LeftProperty, moveAnimX);
            }
        }
    }
}
