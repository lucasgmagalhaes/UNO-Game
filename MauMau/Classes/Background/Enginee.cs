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
        /// Elemento usado como base para o "colapso" das cartas
        /// </summary>
        private UIElement element_colapse; //Elemento comparatório das cartas jogadas

        public Enginee(UIElement colapse)
        {
            this.players = new List<Player>();
            this.allprofiles = new List<Profile>();
            this.baralho = new Baralho();
            baralho.Embaralhar();
            LoadImage();
            SetRandomPlayersProfile();
            this.realOne = this.players[3];
            this.element_colapse = colapse;
            this.monte = new Monte(baralho.GetCards());
        }
        /// <summary>
        /// Carrega as imagens dos jogadores
        /// </summary>
        private void LoadImage()
        {
            ImageBrush brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/player/buzz.png", UriKind.Absolute)));
            allprofiles.Add(new Profile("Buzz", brush));

            brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/player/camb.jpg", UriKind.Absolute)));
            allprofiles.Add(new Profile("Cambit", brush));

            brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/player/cowboy-col.png", UriKind.Absolute)));
            allprofiles.Add(new Profile("CowBoy", brush));

            brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/player/magneto.jpg", UriKind.Absolute)));
            allprofiles.Add(new Profile("Magneto", brush));

            brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/player/mario.jpg", UriKind.Absolute)));
            allprofiles.Add(new Profile("Mario", brush));

            brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/player/stormtrooper.png", UriKind.Absolute)));
            allprofiles.Add(new Profile("StormTrooper", brush));

            brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/player/vader.png", UriKind.Absolute)));
            allprofiles.Add(new Profile("Vader", brush));

            brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/player/walle.png", UriKind.Absolute)));
            allprofiles.Add(new Profile("WallE", brush));
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
            for (int val = 0; val < aux.Length; val++) players.Add(new Player(allprofiles[aux[val]]));
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

        public UIElement ColapseElement(UIElement el)
        {
            Canvas.SetLeft(el, Canvas.GetLeft(this.element_colapse));
            Canvas.SetTop(el, Canvas.GetTop(this.element_colapse));
            return el;
        }
    }
}
