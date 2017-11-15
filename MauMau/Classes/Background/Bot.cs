using MauMau.Classes.Background.Cartas;
using MauMau.Classes.Background.Enum;
using MauMau.Classes.Background.Estruturas;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MauMau.Classes.Background
{
    class Bot : Player
    {
        /// <summary>
        /// Elemento gráfico que contem todos os outros (Referência dele)
        /// </summary>
        private Canvas ambiente;
        /// <summary>
        /// Estrutura gerenciadora do jogo. (Referência dela)
        /// </summary>
        private Enginee eng;

        /// <summary>
        /// Elemento de auxilio para animação referente ao AnimationHandToMont() no ponto Y
        /// </summary>
        private DoubleAnimation moveAnimY;
        /// <summary>
        /// Elemento de auxilio para animação referente ao AnimationHandToMont() no ponto X
        /// </summary>
        private DoubleAnimation moveAnimX;
        /// <summary>
        /// Elemento de auxilio para animação referente ao AnimationMontToHand() no ponto Y
        /// </summary>
        private DoubleAnimation moveAnimY2;
        /// <summary>
        /// Elemento de auxilio para animação referente ao AnimationMontToHand() no ponto X
        /// </summary>
        private DoubleAnimation moveAnimX2;
        /// <summary>
        /// Verificador de quantas vezes a animação AnimationHandToColetor() foi executada
        /// </summary>
        private int anim1loop;
        /// <summary>
        /// Verificador de quantas vezes a animação AnimationMontToHand() foi executada
        /// </summary>
        private int anim2loop;
        /// <summary>
        /// Carta que está no topo do Coletor
        /// </summary>
        private Carta cdTop;
        /// <summary>
        /// Carda pegada do monte
        /// </summary>
        private Carta getcard;
        /// <sum            
        /// Lista de todas as cartas do baralho
        /// </summary>
        public Bot(Profile prf) : base(prf)
        {
            this.SetHand(new Lista<Carta>());
            SetProfile(prf);
        }
        /// <summary>
        /// Construtor do BOT com a mão ja definida
        /// </summary>
        /// <param name="hand"></param>
        /// <param name="infos"></param>
        /// <param name="eng"></param>
        /// <param name="position"></param>
        public Bot(Lista<Carta> hand, Profile infos, Enginee eng, PlayerPosition position) : base(hand, infos, position)
        {
            this.eng = eng;
            this.ambiente = this.eng.Enviroment;
            this.SetHand(hand);
            SetProfile(infos);
        }
        /// <summary>
        /// Construtor do BOT sem as cartas da mão definidas
        /// </summary>
        /// <param name="infos"></param>
        /// <param name="eng"></param>
        /// <param name="position"></param>
        public Bot(Profile infos, Enginee eng, PlayerPosition position) : base(infos, position)
        {
            this.eng = eng;
            this.ambiente = this.eng.Enviroment;
            this.SetHand(new Lista<Carta>());
            SetProfile(infos);
            this.moveAnimX = new DoubleAnimation();
            this.moveAnimY = new DoubleAnimation();
            this.moveAnimX.Completed += MoveAnimX_Completed;

            this.moveAnimX2 = new DoubleAnimation();
            this.moveAnimY2 = new DoubleAnimation();
            this.moveAnimX2.Completed += MoveAnimX2_Completed;
        }
        /// <summary>
        /// Simula jogadas de um player
        /// </summary>
        /// <param name="cardMonte">
        /// Recebe a carta do topo do coletor e o Monte</param> 
        public void Jogar()
        {
            // pega carta do top do coletor como referencia
            cdTop = this.eng.Descarte.GetTopCard();
            Lista<Carta> listaaux = new Lista<Carta>();
            if (this.hand.Count == 1)
            {
                base.TimeToUNO();
            }
            //prioridade Normal(menor numero) > especial
            foreach (Carta cardMao in this.hand)
            {
                if (cardMao.Compatible(cdTop))
                {
                    listaaux.Add(cardMao);
                }
            }
            if (listaaux.Count > 1)
            {
                foreach (Carta card in listaaux)
                {
                    if (card is Coringa)
                    {
                        this.getcard = card;
                    }

                    if (card is Especial)
                    {
                        this.getcard = card;
                    }

                    if (card is Normal)
                    {
                        this.getcard = card;
                        break;
                    }
                }
            }
            else if (listaaux.Count == 1)
            {
                this.getcard = listaaux[0];
            }
            else
            {
                this.anim2loop++;
                if (this.eng.Monte.Count() > 0)
                {
                    this.getcard = eng.RemoveFromMonte();
                    Rectangle cardUI = getcard.ElementUI;
                    Canvas.SetLeft(cardUI as UIElement, Canvas.GetLeft(this.eng.MonteUI));
                    Canvas.SetTop(cardUI as UIElement, Canvas.GetTop(this.eng.MonteUI));

                    this.eng.Enviroment.Children.Add(cardUI);
                    this.AnimationMontToHand(this.getcard);
                    this.AddCardToHand(getcard);
                }
            }

            if (this.anim2loop == 0)
            {
                this.eng.PlayCard(this.getcard);
                this.AnimationHandToColetor(this.getcard);
            }
        }
        /// <summary>
        /// Animação da retirada da carta da mão do jogador para o Coletor
        /// </summary>
        /// <param name="card"></param>
        private void AnimationHandToColetor(Carta card)
        {
            RotateToColetor(card);

            this.moveAnimY.From = Canvas.GetTop(card.ElementUI);
            this.moveAnimY.To = Canvas.GetTop(this.eng.Element_colapse);
            this.moveAnimY.Duration = new Duration(TimeSpan.FromMilliseconds(100));

            this.moveAnimX.From = Canvas.GetLeft(card.ElementUI);
            this.moveAnimX.To = Canvas.GetLeft(this.eng.Element_colapse);
            this.moveAnimX.Duration = new Duration(TimeSpan.FromMilliseconds(100));

            card.ElementUI.BeginAnimation(Canvas.TopProperty, moveAnimY);
            card.ElementUI.BeginAnimation(Canvas.LeftProperty, moveAnimX);
            Canvas.SetZIndex(card.ElementUI, ++MainWindow.count);
        }
        /// <summary>
        /// Animação da retirada da carta do monte para a mão do jogador
        /// </summary>
        /// <returns></returns>
        private void AnimationMontToHand(Carta card)
        {
            this.RotateToHand(getcard);

            Carta aux = this.GetLastCard();

            this.moveAnimY2.From = Canvas.GetTop(card.ElementUI);
            this.moveAnimY2.To = Canvas.GetTop(aux.ElementUI);
            this.moveAnimY2.Duration = new Duration(TimeSpan.FromMilliseconds(100));

            this.moveAnimX2.From = Canvas.GetLeft(card.ElementUI);
            this.moveAnimY2.To = Canvas.GetLeft(aux.ElementUI) + 40;
            this.moveAnimY2.Duration = new Duration(TimeSpan.FromMilliseconds(100));

            moveAnimY2.FillBehavior = FillBehavior.Stop;
            moveAnimX2.FillBehavior = FillBehavior.Stop;

            card.ElementUI.BeginAnimation(Canvas.TopProperty, moveAnimY2);
            card.ElementUI.BeginAnimation(Canvas.LeftProperty, moveAnimX2);

            Canvas.SetLeft(card.ElementUI, Canvas.GetLeft(aux.ElementUI));
            Canvas.SetTop(card.ElementUI, Canvas.GetTop(aux.ElementUI) + 40);
        }
        /// <summary>
        /// Roda a carta para o angulo de 0º. Tornando-a alinhada com o Coletor
        /// </summary>
        /// <param name="card"></param>
        private void RotateToColetor(Carta card)
        {
            DoubleAnimation da;
            RotateTransform rt;

            switch (this.position)
            {
                case PlayerPosition.Left:
                    da = new DoubleAnimation(-90, 0, new Duration(TimeSpan.FromMilliseconds(100)));
                    rt = new RotateTransform();
                    card.ElementUI.RenderTransform = rt;
                    card.ElementUI.RenderTransformOrigin = new Point(0.5, 0.5);
                    rt.BeginAnimation(RotateTransform.AngleProperty, da);
                    break;

                case PlayerPosition.Right:
                    da = new DoubleAnimation(90, 0, new Duration(TimeSpan.FromMilliseconds(100)));
                    rt = new RotateTransform();
                    card.ElementUI.RenderTransform = rt;
                    card.ElementUI.RenderTransformOrigin = new Point(0.5, 0.5);
                    rt.BeginAnimation(RotateTransform.AngleProperty, da);
                    break;

                case PlayerPosition.Top:
                    da = new DoubleAnimation(180, 0, new Duration(TimeSpan.FromMilliseconds(200)));
                    rt = new RotateTransform();
                    card.ElementUI.RenderTransform = rt;
                    card.ElementUI.RenderTransformOrigin = new Point(0.5, 0.5);
                    rt.BeginAnimation(RotateTransform.AngleProperty, da);
                    break;
            }
        }

        private void RotateToHand(Carta card)
        {
            DoubleAnimation da;
            RotateTransform rt;

            switch (this.position)
            {
                case PlayerPosition.Left:
                    da = new DoubleAnimation(0, -90, new Duration(TimeSpan.FromMilliseconds(100)));
                    rt = new RotateTransform();
                    card.ElementUI.RenderTransform = rt;
                    card.ElementUI.RenderTransformOrigin = new Point(0.5, 0.5);
                    rt.BeginAnimation(RotateTransform.AngleProperty, da);
                    break;

                case PlayerPosition.Right:
                    da = new DoubleAnimation(0, 90, new Duration(TimeSpan.FromMilliseconds(100)));
                    rt = new RotateTransform();
                    card.ElementUI.RenderTransform = rt;
                    card.ElementUI.RenderTransformOrigin = new Point(0.5, 0.5);
                    rt.BeginAnimation(RotateTransform.AngleProperty, da);
                    break;

                case PlayerPosition.Top:
                    da = new DoubleAnimation(0, 180, new Duration(TimeSpan.FromMilliseconds(200)));
                    rt = new RotateTransform();
                    card.ElementUI.RenderTransform = rt;
                    card.ElementUI.RenderTransformOrigin = new Point(0.5, 0.5);
                    rt.BeginAnimation(RotateTransform.AngleProperty, da);
                    break;
            }
        }
        /// <summary>
        /// Evento de animação completa para o método AnimationMontToHand()
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveAnimX2_Completed(object sender, EventArgs e)
        {
            if (this.getcard.Compatible(cdTop))
            {
                this.AnimationHandToColetor(this.getcard);
            }
            else
            {
                this.cdTop = null;
                this.anim1loop = this.anim2loop = 0;
                this.eng.EndTurn();
            }
        }
        /// <summary>
        /// Evento de animação completa para o método AnimationHandToColetor()
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveAnimX_Completed(object sender, EventArgs e)
        {
            this.cdTop = null;
            this.anim1loop = this.anim2loop = 0;
            this.eng.EndTurn();
        }
    }
}
