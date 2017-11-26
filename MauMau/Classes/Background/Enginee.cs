using MauMau.Classes.Background.Cartas;
using MauMau.Classes.Background.Estruturas;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MauMau.Classes.Background
{
    class Enginee
    {
        double screenSizeX;
        double screenSizeY;
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
        /// <summary>
        /// Elemento gráfico que representa o monte
        /// </summary>
        private UIElement monteUI;
        /// <summary>
        /// Define qual ação será executada ao jogar uma carta
        /// </summary>
        private Evento evento;
        /// <summary>
        /// Define se as cartas dos bots serão ou não exibidas
        /// </summary>
        private bool isplayerturn;
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
        public double ScreenSizeX { get { return this.screenSizeX; } }
        public double ScreenSizeY { get { return this.screenSizeY; } }

        public Enginee(UIElement colapse, Canvas env, UIElement monteUI)
        {
            this.screenSizeY = System.Windows.SystemParameters.PrimaryScreenHeight;
            this.screenSizeX = System.Windows.SystemParameters.PrimaryScreenWidth;

            //Não mude a ordem de iniciação desses elementos. A troca pode acarretar graves consequências físicas ao culpado.
            this.enviroment = env;
            this.monteUI = monteUI;
            this.element_colapse = colapse;

            this.PosicionarMonteUI();
            this.PosicionarUltimaCartaJogada();

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
            this.AddIconsOnInterface();

            this.descarte = new Coletor(GetValidCard());
            this.AddColetorCardOnInterface();
            this.DisableMoveIconsPlayers();

            this.AlignIconsPlayers();
            this.evento = new Evento(this);
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
            Lista<Carta> get = this.GetCurrentPlayer().Hand;
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

        private void PosicionarUltimaCartaJogada()
        {
            Canvas.SetLeft(this.element_colapse, this.screenSizeX / 2 + 40);
            Canvas.SetTop(this.element_colapse, this.screenSizeY / 2 - 114);
        }

        private void PosicionarMonteUI()
        {
            Canvas.SetLeft(this.monteUI, this.screenSizeX / 2 - 115);
            Canvas.SetTop(this.monteUI, this.screenSizeY / 2 - 114);
        }

        /// <summary>
        /// Alinha a posição das cartas na mão dos jogadores
        /// </summary>
        public void AlignCardsToHand(Player pl)
        {
            // Auxiliares criadas para não alterar valores das variáveis com os valores originais.
            double auxX, auxY;

            auxY = screenSizeY;
            auxX = screenSizeX;

            if (pl.Position == Enum.PlayerPosition.Left)
            {
                foreach (Carta card in pl.Hand)
                {
                    Canvas.SetLeft(card.ElementUI, 15);
                    Canvas.SetTop(card.ElementUI, auxY/2-100);
                    auxY += 80;
                }
            }
            else if (pl.Position == Enum.PlayerPosition.Right)
            {
                foreach (Carta card in pl.Hand)
                {
                    Canvas.SetLeft(card.ElementUI, auxX-15);
                    Canvas.SetTop(card.ElementUI, auxY/2-214);
                    auxY += 80;
                }
            }
            else if (pl.Position == Enum.PlayerPosition.Top)
            {
                
                foreach (Carta card in pl.Hand)
                {
                    Canvas.SetLeft(card.ElementUI, auxX/2-54);
                    Canvas.SetTop(card.ElementUI, 206);
                    auxX += 80;
                }
            }
            else
            {
                double Y = System.Windows.SystemParameters.PrimaryScreenHeight - 15;

                foreach (Carta card in pl.Hand)
                {
                    Canvas.SetLeft(card.ElementUI, auxX/2-164);
                    Canvas.SetTop(card.ElementUI, auxY - 235);
                    auxX += 80;
                }
            }
        }

        /// <summary>
        /// Torna o booleano "isEnabled" dos elementos UI falsos, para que não seja possível mover os ícones dos personagens.
        /// </summary>
        public void DisableMoveIconsPlayers()
        {
            foreach (Player i in this.players)
                i.Infos.ElementUI.IsEnabled = false;
        }

        /// <summary>
        /// Alinha a posição dos ícones dos jogadores
        /// </summary>
        public void AlignIconsPlayers()
        {
            // Auxiliares criadas para não alterar valores das variáveis com os valores originais.
            double auxX, auxY;

            auxX = screenSizeX;
            auxY = screenSizeY;

            for (int i = 0; i < this.players.Count; i++)
            {
                switch (this.players[i].Position)
                {
                    case Enum.PlayerPosition.Left:

                        Canvas.SetLeft(this.players[0].Infos.ElementUI, 75);
                        Canvas.SetTop(this.players[0].Infos.ElementUI, (auxY/2)-300);

                        break;

                    case Enum.PlayerPosition.Right:

                        Canvas.SetLeft(this.players[1].Infos.ElementUI, auxX - 132);
                        Canvas.SetTop(this.players[1].Infos.ElementUI, (auxY / 2) - 300);
                        break;

                    case Enum.PlayerPosition.Top:

                        Canvas.SetLeft(this.players[2].Infos.ElementUI, (auxX/2)-255);
                        Canvas.SetTop(this.players[2].Infos.ElementUI, 80);

                        break;

                    default:

                        Canvas.SetLeft(this.players[3].Infos.ElementUI, (auxX/2)-250);
                        Canvas.SetTop(this.players[3].Infos.ElementUI, auxY-115);
                        break;
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
        /// Adiciona os ícones dos jogadores na interface gráfica
        /// </summary>
        private void AddIconsOnInterface()
        {
            foreach (Player pl in this.players)
            {
                enviroment.Children.Add(pl.Infos.ElementUI);
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
            Player current = this.GetCurrentPlayer();
            Carta carta_jogada = current.PlayCard(card);
            this.evento.EventAtivado(carta_jogada);
            this.descarte.AddCard(carta_jogada);

            //Log.AdicionarEvento(current.Infos.Name, carta_jogada.FrontImage.
        }
        /// <summary>
        /// Não usado atualmente
        /// </summary>
        private void RealignPlayedCard()
        {
            Player pl = this.roda.GetCurrentPlayer();
            UIElement previucard = new UIElement();
            UIElement previucard2 = new UIElement();
            double distance = 0;
            double distance2 = 0;

            foreach (Carta card in pl.Hand)
            {
                distance = Canvas.GetLeft(previucard as FrameworkElement);

                distance2 = Canvas.GetLeft(previucard2) - Canvas.GetLeft(previucard);

                if (distance2 != distance)
                {
                    Canvas.SetLeft(card.ElementUI, Canvas.GetLeft(card.ElementUI) - distance);
                }
                previucard2 = previucard;
                previucard = card.ElementUI;
            }
        }
        public void RealignCards()
        {
            Player pl = this.roda.GetCurrentPlayer();
            int space = 0;
            int getsize = pl.Hand.Count;
            UIElement previucard = new UIElement();
            Lista<Carta> cartas = this.roda.GetCurrentPlayer().GetHand();

            if (getsize > 7)
            {
                if (pl.Position == Enum.PlayerPosition.Bottom || pl.Position == Enum.PlayerPosition.Top)
                {
                    foreach (Carta card in pl.Hand)
                    {
                        if (Canvas.GetLeft(card.ElementUI) - Canvas.GetLeft(previucard) > 50)
                        {
                            int indexstart = this.roda.GetCurrentPlayer().Hand.GetIndexOf(card);
                            int handsize = this.roda.GetCurrentPlayer().Hand.Count;

                            double diferencedistance = Canvas.GetLeft(card.ElementUI) - Canvas.GetLeft(previucard);

                            for (int i = indexstart; i < handsize; i++)
                            {
                                Canvas.SetLeft(cartas[i].ElementUI, Canvas.GetLeft(cartas[i].ElementUI) - diferencedistance + 30);
                            }
                            break;
                        }
                        Canvas.SetLeft(card.ElementUI, Canvas.GetLeft(card.ElementUI) - space);
                        previucard = card.ElementUI;
                        space++;
                    }
                }
                else
                {
                    foreach (Carta card in pl.Hand)
                    {
                        Canvas.SetTop(card.ElementUI, Canvas.GetTop(card.ElementUI) - space);
                        previucard = card.ElementUI;
                        space++;
                    }
                }
            }
            else
            {
                if (pl.Position == Enum.PlayerPosition.Bottom || pl.Position == Enum.PlayerPosition.Top)
                {
                    foreach (Carta card in pl.Hand)
                    {
                        Canvas.SetLeft(card.ElementUI, Canvas.GetLeft(card.ElementUI) + space);
                        previucard = card.ElementUI;
                        space++;
                    }
                }
                else
                {
                    foreach (Carta card in pl.Hand)
                    {
                        Canvas.SetTop(card.ElementUI, Canvas.GetTop(card.ElementUI) + space);
                        previucard = card.ElementUI;
                        space++;
                    }
                }
            }
        }
        /// <summary>
        /// Retorna a carta recebendo um UI element e um player no qual deve conter a carta
        /// </summary>
        /// <param name="pl"></param>
        /// <param name="card"></param>
        /// <returns></returns>
        public Carta GetCardFromUI(Player pl, UIElement card)
        {
            foreach(Carta cd in pl.Hand)
            {
                if (card == cd.ElementUI) return cd;
            }
            return null;
        }
        /// <summary>
        /// Procura pelo UIElement nas mãos dos jogadores, se algum deles conte-la, tal carta será retornada
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public Carta GetCardFromUI(UIElement card)
        {
            foreach(Carta cd in this.players[0].Hand)
            {
                if (cd.ElementUI == card) return cd;
            }
            foreach (Carta cd in this.players[1].Hand)
            {
                if (cd.ElementUI == card) return cd;
            }
            foreach (Carta cd in this.players[2].Hand)
            {
                if (cd.ElementUI == card) return cd;
            }
            foreach (Carta cd in this.players[3].Hand)
            {
                if (cd.ElementUI == card) return cd;
            }
            return null;
        }
        /// <summary>
        /// Valida a jogada
        /// </summary>
        /// <param name="played"></param>
        /// <returns></returns>
        public bool ValidatePlay(UIElement played)
        {
            Player auxplayer = this.GetCurrentPlayer();
            Lista<Carta> auxlista = auxplayer.GetHand();
            Carta aux = this.descarte.GetTopCard();

            foreach (Carta card in auxlista)
            {
                if (played.Uid == card.GetID())
                {
                    if (aux.Compatible(card))
                    {
                        PlayCard(card);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}