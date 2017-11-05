
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private List<Player> players;
        /// <summary>
        /// Lista dos perfis dos jogadores
        /// </summary>
        private List<Profile> allprofiles;
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
        /// Elemento usado como base para o "colapso" das cartas
        /// </summary>
        private UIElement element_colapse; //Elemento comparatório das cartas jogadas

        public Monte Monte { get { return this.monte; } }
        public Enginee(UIElement colapse)
        {
            this.players = new List<Player>();
            this.allprofiles = new List<Profile>();
            this.LoadImage();
            this.SetRandomPlayersProfile();
            this.baralho = new Baralho();
            this.baralho.Embaralhar();
            this.realOne = this.players[3];
            this.element_colapse = colapse;
            this.monte = new Monte(baralho.GetCards());
            this.roda = new Turno(this.players);
            this.DistributeCards();
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
            players.Add(new Player(allprofiles[aux[0]], Enum.PlayerPosition.Top));
            players.Add(new Player(allprofiles[aux[1]], Enum.PlayerPosition.Right));
            players.Add(new Player(allprofiles[aux[2]], Enum.PlayerPosition.Bottom));
            players.Add(new Player(allprofiles[aux[3]], Enum.PlayerPosition.Left));

//            for (int val = 0; val < aux.Length; val++) players.Add(new Player(allprofiles[aux[val]]));
        }

        public Player GetMainPlayer()
        {
            return this.realOne;
        }

        public List<Player> GetPlayers()
        {
            return this.players;
        }

        public Carta GetFromMonte()
        {
            return this.monte.GetCardOnTop();
        }

        public Player GetCurrentPlayer()
        {
            return this.roda.GetCurrentPlayer();
        }
        /// <summary>
        /// Distribui as cartas para os jogadores
        /// </summary>
        private void DistributeCards()
        {
            foreach(Player pl in this.players)
            {
                for (int i = 0; i < 7; i++) pl.AddCardToHand(this.monte.GetCardOnTop());
                this.AlignCardsToHand(pl);
            }
        }
        /// <summary>
        /// Alinha a posição das cartas na mão dos jogadores
        /// </summary>
        private void AlignCardsToHand(Player pl)
        {
            foreach(Carta card in pl.Hand)
            {
               //Canvas.SetLeft(card.GetCardUI(), )
            }
        }
        public UIElement ColapseElement(UIElement el)
        {
            Canvas.SetLeft(el, Canvas.GetLeft(this.element_colapse));
            Canvas.SetTop(el, Canvas.GetTop(this.element_colapse));
            return el;
        }
    }
}
