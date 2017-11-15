using MauMau.Classes.Background.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using MauMau.Classes.Background.Cartas;
using System.Windows.Media.Animation;
using System.Windows.Controls;

namespace MauMau.Classes.Background
{
    abstract class Evento
    {
        protected Enginee eng;
        protected MainWindow mWindow; 

        public Evento(Enginee e, MainWindow mw)
        {
            this.eng = e;
            this.mWindow = mw;
        }
        /// <summary>
        /// Faz o evento conforme carta jogada 
        /// </summary>
        /// <param name="cardJogada"></param>
        public void EventAtivado(Carta cardJogada)
        {
            if (cardJogada is Especial)
            {
                if (cardJogada.ElementUI.Uid.Contains("Comprar"))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (this.eng.Monte.Count() > 0)
                        {
                            Carta getcard = eng.RemoveFromMonte();
                            Rectangle cardUI = getcard.ElementUI;
                            Canvas.SetLeft(cardUI as UIElement, Canvas.GetLeft(mWindow.Mont));
                            Canvas.SetTop(cardUI as UIElement, Canvas.GetTop(mWindow.Mont));

                            mWindow.root.Children.Add(cardUI);
                            this.CartaParaMao(getcard);
                            Player current = eng.GetCurrentPlayer();
                            current.AddCardToHand(getcard);
                        }
                    }
                    this.eng.EndTurn();
                }
                if (cardJogada.ElementUI.Uid.Contains("Bloq"))
                {
                    this.eng.EndTurn();
                }
                if (cardJogada.ElementUI.Uid.Contains("Inverte"))
                {
                    this.eng.Roda.SetSentido((this.eng.Roda.GetSentido() * (-1)));
                    this.eng.EndTurn();
                }
                if (cardJogada.ElementUI.Uid.Contains("cEfeito")) //coringa troca cor
                {
                    //trocar cor - a fazer
                }
                if (cardJogada.ElementUI.Uid.Contains("sEfeito")) // coringa compra 4
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (this.eng.Monte.Count() > 0)
                        {
                            Carta getcard = eng.RemoveFromMonte();
                            Rectangle cardUI = getcard.ElementUI;
                            Canvas.SetLeft(cardUI as UIElement, Canvas.GetLeft(mWindow.Mont));
                            Canvas.SetTop(cardUI as UIElement, Canvas.GetTop(mWindow.Mont));

                            mWindow.root.Children.Add(cardUI);
                            this.CartaParaMao(getcard);
                            Player current = eng.GetCurrentPlayer();
                            current.AddCardToHand(getcard);
                        }
                    } 
                    //trocar cor - a fazer
                    this.eng.EndTurn();
                }
            }
        }
        private void CartaParaMao(Carta card)
        {
            Carta aux = eng.GetCurrentPlayer().GetLastCard();

            DoubleAnimation moveAnimY = new DoubleAnimation(Canvas.GetTop(card.ElementUI), Canvas.GetTop(aux.ElementUI), new Duration(TimeSpan.FromMilliseconds(100)));
            DoubleAnimation moveAnimX = new DoubleAnimation(Canvas.GetLeft(card.ElementUI), Canvas.GetLeft(aux.ElementUI) + 40, new Duration(TimeSpan.FromMilliseconds(100)));

            moveAnimX.FillBehavior = FillBehavior.Stop;
            moveAnimY.FillBehavior = FillBehavior.Stop;

            card.ElementUI.BeginAnimation(Canvas.TopProperty, moveAnimY);
            card.ElementUI.BeginAnimation(Canvas.LeftProperty, moveAnimX);

            Canvas.SetLeft(card.ElementUI, Canvas.GetLeft(aux.ElementUI) + 40);
            Canvas.SetTop(card.ElementUI, Canvas.GetTop(aux.ElementUI));
        }
    }
}

