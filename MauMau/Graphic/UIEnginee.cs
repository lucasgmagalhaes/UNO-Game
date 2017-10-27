using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MauMau.Graphic
{
    class UIEnginee
    {
        private List<UI_Player> playersIMG = new List<UI_Player>();
        private static Random ran = new Random();
        private UIElement element_colapse;
        private const int marginMin = -20;
        private const int marginMax = 50;
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
            playersIMG.Add(new UI_Player("Buzz", brush));

            brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/player/camb.jpg", UriKind.Absolute)));
            playersIMG.Add(new UI_Player("Cambit", brush));

            brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/player/cowboy-col.png", UriKind.Absolute)));
            playersIMG.Add(new UI_Player("CowBoy", brush));

            brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/player/magneto.jpg", UriKind.Absolute)));
            playersIMG.Add(new UI_Player("Magneto", brush));

            brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/player/mario.jpg", UriKind.Absolute)));
            playersIMG.Add(new UI_Player("Mario", brush));

            brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/player/stormtrooper.png", UriKind.Absolute)));
            playersIMG.Add(new UI_Player("StormTrooper", brush));

            brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/player/vader.png", UriKind.Absolute)));
            playersIMG.Add(new UI_Player("Vader", brush));

            brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/player/walle.png", UriKind.Absolute)));
            playersIMG.Add(new UI_Player("WallE", brush));
        }
        public UI_Player GetRandomImg()
        {
            int valran = ran.Next(0, playersIMG.Count - 1);
            return playersIMG[valran];
        }

        public List<UI_Player> GetRandom(int quant)
        {
            int[] aux = new int[quant];
            while (aux.Distinct().Count() != aux.Count())
            {
                aux[0] = ran.Next(0, playersIMG.Count - 1);
                aux[1] = ran.Next(0, playersIMG.Count - 1);
                aux[2] = ran.Next(0, playersIMG.Count - 1);
            }
            List<UI_Player> auxlist = new List<UI_Player>();
            foreach (int val in aux) auxlist.Add(playersIMG[val]);
            return auxlist;
        }

        public bool CancColapse(UIElement el)
        {
            double fixLEFT = Canvas.GetLeft(el);
            double fixTOP = Canvas.GetTop(el);

            double cardLEFT = Canvas.GetLeft(this.element_colapse);
            double cardTOP = Canvas.GetTop(this.element_colapse);

            if (fixLEFT - cardLEFT > marginMin && fixLEFT - cardLEFT < marginMax || fixLEFT - cardLEFT < marginMax && fixLEFT - cardLEFT > marginMin)
                if (fixTOP - cardTOP > marginMin && fixTOP - cardTOP < marginMax || fixTOP - cardTOP < marginMax && fixTOP - cardTOP > marginMin)
                {
                    return true;
                }
            return false;
        }
        public UIElement ColapseElement(UIElement el)
        {
            Canvas.SetLeft(el, Canvas.GetLeft(this.element_colapse));
            Canvas.SetTop(el, Canvas.GetTop(this.element_colapse));
            return el;
        }
    }
}
