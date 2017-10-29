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

namespace MauMau.Classes.Graphics
{
    class UIEnginee
    {
        private List<UIPlayer> playersIMG = new List<UIPlayer>();
        private static Random ran = new Random();
        private UIElement element_colapse;
        private const int marginMin = -20;
        private const int marginMax = 50;
        private int zIndexElement = -99;

        public int ZIndexElement { get { this.zIndexElement++; return this.zIndexElement; } }

        public UIEnginee(UIElement colapse)
        {
            LoadImage();
            this.element_colapse = colapse;
        }
        /// <summary>
        /// Carrega as imagens dos jogadores
        /// </summary>
        private void LoadImage()
        {
            ImageBrush brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/player/buzz.png", UriKind.Absolute)));
            playersIMG.Add(new UIPlayer("Buzz", brush));

            brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/player/camb.jpg", UriKind.Absolute)));
            playersIMG.Add(new UIPlayer("Cambit", brush));

            brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/player/cowboy-col.png", UriKind.Absolute)));
            playersIMG.Add(new UIPlayer("CowBoy", brush));

            brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/player/magneto.jpg", UriKind.Absolute)));
            playersIMG.Add(new UIPlayer("Magneto", brush));

            brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/player/mario.jpg", UriKind.Absolute)));
            playersIMG.Add(new UIPlayer("Mario", brush));

            brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/player/stormtrooper.png", UriKind.Absolute)));
            playersIMG.Add(new UIPlayer("StormTrooper", brush));

            brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/player/vader.png", UriKind.Absolute)));
            playersIMG.Add(new UIPlayer("Vader", brush));

            brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/player/walle.png", UriKind.Absolute)));
            playersIMG.Add(new UIPlayer("WallE", brush));
        }
        public UIPlayer GetRandomImg()
        {
            int valran = ran.Next(0, playersIMG.Count - 1);
            return playersIMG[valran];
        }

        public List<UIPlayer> GetRandom(int quant)
        {
            int[] aux = new int[quant];
            while (aux.Distinct().Count() != aux.Count())
            {
                aux[0] = ran.Next(0, playersIMG.Count - 1);
                aux[1] = ran.Next(0, playersIMG.Count - 1);
                aux[2] = ran.Next(0, playersIMG.Count - 1);
            }
            List<UIPlayer> auxlist = new List<UIPlayer>();
            foreach (int val in aux) auxlist.Add(playersIMG[val]);
            return auxlist;
        }
        public UIElement ColapseElement(UIElement el)
        {
            Canvas.SetLeft(el, Canvas.GetLeft(this.element_colapse));
            Canvas.SetTop(el, Canvas.GetTop(this.element_colapse));
            return el;
        }

    }
}
