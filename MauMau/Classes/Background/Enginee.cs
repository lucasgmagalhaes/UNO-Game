using MauMau.Classes.Background.Cartas;
using MauMau.Classes.Background.Estruturas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MauMau.Classes.Background
{
    class Enginee
    {
        /// <summary>
        /// Lista com todos os jogadores
        /// </summary>
        private Lista<Player> players;
        /// <summary>
        /// Lista dos perfis dos jogadores
        /// </summary>
        private Lista<Profile> allprofiles;
        /// <summary>
        /// Cartas para saque
        /// </summary>
        private Monte monte;
        /// <summary>
        /// Cartas descartadas
        /// </summary>
        private Coletor descarte;
        /// <summary>
        /// Elemento que contem todas as cartas;
        /// </summary>
        private Baralho baralho;
        /// <summary>
        /// Elemento base para o get dos perfils
        /// </summary>
        private static Random ran = new Random();
        /// <summary>
        /// Jogador real
        /// </summary>
        private Player realOne;
        /// <summary>
        /// Turno dos jogadores
        /// </summary>
        private Turno roda;
        /// <summary>
        /// Objeto que contem todos os outros
        /// </summary>
        private Canvas enviroment;
        /// <summary>
        /// Elemento usado como base para o "colapso" das cartas
        /// </summary>
        private UIElement element_colapse; //Elemento comparatório das cartas jogadas
        /// <summary>
        /// Define se as cartas dos BOTs vao ou não serem exibidas
        /// </summary>
        private bool showBotCards;
        private UIElement monteUI;
        public bool ShowBotCards
        {
            get { return this.showBotCards; }
            set
            {
                this.showBotCards = value;
                if (this.showBotCards == true) ShowBOTCards();
                else HideBOTCards();
            }
        }
        public Monte Monte { get { return this.monte; } }
        public UIElement Element_colapse { get { return this.element_colapse; } }
        public Canvas Enviroment { get { return this.enviroment; } }
        public Turno Roda { get { return this.roda; } }
        public Player RealOne { get { return this.realOne; } }
        public Baralho Baralho { get { return this.baralho; } }
        public Coletor Descarte { get { return this.descarte; } }
        public UIElement MonteUI { get { return this.monteUI; } }

        public Enginee(UIElement colapse, Canvas env, UIElement monteUI)
        {
            //Não mude a ordem de iniciação desses elementos. A troca pode acarretar em falhas no programa
            this.enviroment = env;
            this.monteUI = monteUI;
            this.element_colapse = colapse;
            this.players = new Lista<Player>();
            this.allprofiles = new Lista<Profile>();
            this.LoadImage();
            this.SetRandomPlayersProfile();
            this.baralho = new Baralho();
            this.baralho.Embaralhar();
            this.realOne = this.players[2];
            this.monte = new Monte(baralho.GetCards());
            this.DistributeCards();
            this.roda = new Turno(this.players);
            this.AddCardsOnInterface();
            this.descarte = new Coletor(GetValidCard());
            this.AddColetorCardOnInterface();
        }

        /// <summary>
        /// Carrega as imagens dos jogadores
        /// </summary>
        private void LoadImage()
        {
            allprofiles.Add(new Profile("Buzz", new ImageBrush(((ImageSource)Application.Current.Resources["Card_player_buzz"]))));
            allprofiles.Add(new Profile("Gambit", new ImageBrush(((ImageSource)Application.Current.Resources["Card_player_camb"]))));
            allprofiles.Add(new Profile("CowBoy", new ImageBrush(((ImageSource)Application.Current.Resources["Card_player_cowboy-cool"]))));
            allprofiles.Add(new Profile("Magneto", new ImageBrush(((ImageSource)Application.Current.Resources["Card_player_magneto"]))));
            allprofiles.Add(new Profile("Mario", new ImageBrush(((ImageSource)Application.Current.Resources["Card_player_mario"]))));
            allprofiles.Add(new Profile("StormTrooper", new ImageBrush(((ImageSource)Application.Current.Resources["Card_player_stormtrooper"]))));
            allprofiles.Add(new Profile("Vader", new ImageBrush(((ImageSource)Application.Current.Resources["Card_player_vader"]))));
            allprofiles.Add(new Profile("WallE", new ImageBrush(((ImageSource)Application.Current.Resources["Card_player_walle"]))));
        }
        private void SetRandomPlayersProfile()
        {
            int[] aux = new int[4];
            while (aux.Distinct().Count() != aux.Count())
            {
                aux[0] = ran.Next(0, this.allprofiles.Count - 1);
                aux[1] = ran.Next(0, this.allprofiles.Count - 1);
                aux[2] = ran.Next(0, this.allprofiles.Count - 1);
                aux[3] = ran.Next(0, this.allprofiles.Count - 1);
            }

            players.Add(new Bot(allprofiles[aux[0]], this, Enum.PlayerPosition.Top));
            players.Add(new Bot(allprofiles[aux[1]], this, Enum.PlayerPosition.Right));
            players.Add(new Player(allprofiles[aux[2]], Enum.PlayerPosition.Bottom));
            players.Add(new Bot(allprofiles[aux[3]], this, Enum.PlayerPosition.Left));
        }
        public Player GetMainPlayer()
        {
            return this.realOne;
        }
        public Lista<Player> GetPlayers()
        {
            return this.players;
        }
        /// <summary>
        /// Pega a carta do monte sem remove-la
        /// </summary>
        /// <returns></returns>
        public Carta GetFromMonte()
        {
            return this.monte.GetTopCard();
        }
        public Carta RemoveFromMonte()
        {
            return this.monte.RemoveTopCard();
        }
        public Player GetCurrentPlayer()
        {
            return this.roda.GetCurrentPlayer();
        }
        public void EndTurn()
        {
            this.roda.EndPLayerTurn();
            if (this.GetCurrentPlayer() is Bot) ((Bot)this.GetCurrentPlayer()).Jogar();
        }
        /// <summary>
        /// Distribui as cartas para os jogadores
        /// </summary>
        private void DistributeCards()
        {
            foreach (Player pl in this.players)
            {
                for (int i = 0; i < 7; i++) pl.AddCardToHand(this.monte.RemoveTopCard());
                this.AlignCardsToHand(pl);
            }
        }
        /// <summary>
        /// Alinha a posição das cartas na mão dos jogadores
        /// </summary>
        private void AlignCardsToHand(Player pl)
        {
            if (pl.Position == Enum.PlayerPosition.Left)
            {
                double X = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
                foreach (Carta card in pl.Hand)
                {
                    Canvas.SetLeft(card.ElementUI, 15);
                    Canvas.SetTop(card.ElementUI, X);
                    X += 30;
                }
            }
            else if (pl.Position == Enum.PlayerPosition.Right)
            {
                double Y = (System.Windows.SystemParameters.PrimaryScreenHeight / 2) - 114;
                double X = System.Windows.SystemParameters.PrimaryScreenWidth - 15;

                foreach (Carta card in pl.Hand)
                {
                    Canvas.SetLeft(card.ElementUI, X);
                    Canvas.SetTop(card.ElementUI, Y);
                    Y += 30;
                }
            }
            else if (pl.Position == Enum.PlayerPosition.Top)
            {
                int X = 621;
                foreach (Carta card in pl.Hand)
                {
                    Canvas.SetLeft(card.ElementUI, X);
                    Canvas.SetTop(card.ElementUI, 200);
                    X += 40;
                }
            }
            else
            {
                int X = 501;
                double Y = System.Windows.SystemParameters.PrimaryScreenHeight - 15;

                foreach (Carta card in pl.Hand)
                {
                    Canvas.SetLeft(card.ElementUI, X);
                    Canvas.SetTop(card.ElementUI, Y - 165);
                    X += 40;
                }
            }
        }
        /// <summary>
        /// Exibe as cartas dos BOTs
        /// </summary>
        private void ShowBOTCards()
        {
            foreach (Player pl in this.players)
            {
                if (pl.Position != Enum.PlayerPosition.Bottom)
                {
                    foreach (Carta card in pl.Hand) card.ElementUI.Fill = card.FrontImage;
                }
            }
        }
        /// <summary>
        /// Esconde as cartas dos BOTs
        /// </summary>
        private void HideBOTCards()
        {
            foreach (Player pl in this.players)
            {
                if (pl.Position != Enum.PlayerPosition.Bottom)
                {
                    foreach (Carta card in pl.Hand) card.ElementUI.Fill = card.BackImage;
                }
            }
        }
        /// <summary>
        /// Adiciona as cartas dos jogadores na interface gráfica
        /// </summary>
        private void AddCardsOnInterface()
        {
            foreach (Player pl in this.players)
            {
                foreach (Carta card in pl.Hand)
                {
                    enviroment.Children.Add(card.ElementUI);
                    if (pl.Position != Enum.PlayerPosition.Bottom) card.ElementUI.IsEnabled = false;
                }
            }
        }
        /// <summary>
        /// Junta a carta do jogador com o Monte caso esteja próximo
        /// </summary>
        /// <param name="el"></param>
        /// <returns></returns>
        public UIElement ColapseElement(UIElement el)
        {
            Canvas.SetLeft(el, Canvas.GetLeft(this.element_colapse));
            Canvas.SetTop(el, Canvas.GetTop(this.element_colapse));
            return el;
        }

        private Carta GetValidCard()
        {
            Carta aux = this.monte.RemoveTopCard();
            Pilha<Carta> pilhaaux = new Pilha<Carta>();
            while (aux is Especial || aux is Coringa)
            {
                pilhaaux.Push(aux);
                aux = this.monte.RemoveTopCard();
            }
            while (pilhaaux.Count > 0) this.monte.Add(pilhaaux.Pop());
            return aux;
        }

        private void AddColetorCardOnInterface()
        {
            UIElement aux = this.descarte.GetTopCard().ElementUI;
            Canvas.SetLeft(aux, Canvas.GetLeft(this.element_colapse));
            Canvas.SetTop(aux, Canvas.GetTop(this.element_colapse));
            Canvas.SetZIndex(aux, -100);
            this.enviroment.Children.Add(aux);
        }
        public void PlayCard(Carta card)
        {
            this.descarte.AddCard(this.GetCurrentPlayer().PlayCard(card));
        }
        public bool ValidatePlay(UIElement played)
        {
            Player auxplayer = this.GetCurrentPlayer();
            foreach (Carta card in auxplayer.GetHand())
            {
                if (played.Uid == card.GetID())
                {
                    Carta aux = this.descarte.GetTopCard();
                    if (aux.Compatible(card))
                    {
                        PlayCard(card);
                        return true;
                    }
                    return false;
                }
            }
            return false;
        }
    }
}
