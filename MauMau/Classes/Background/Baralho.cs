using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace MauMau.Classes.Background
{
    class Baralho
    {
        private List<Carta> cartas = new List<Carta>();

        public Baralho()
        {
            LoadCards();
        }
        private void LoadCards()
        {
            try
            {
                foreach (Carta card in GetCardsListOfEspecificColor("amarelo")) cartas.Add(card);
                foreach (Carta card in GetCardsListOfEspecificColor("azul")) cartas.Add(card);
                foreach (Carta card in GetCardsListOfEspecificColor("verde")) cartas.Add(card);
                foreach (Carta card in GetCardsListOfEspecificColor("vermelho")) cartas.Add(card);
                foreach (Carta card in GetCardsListOfEspecificColor("especial")) cartas.Add(card);
            }
            catch (System.Exception ee)
            {
                Exception.IOImage(ee.Message);
            }
        }
        private List<Carta> GetCardsListOfEspecificColor(string color)
        {
            string corcarta = "";
            ImageBrush brush;
            List<Carta> aux = new List<Carta>();
            switch (color)
            {
                case "amarelo":
                    corcarta = "yellow";
                    break;
                case "azul":
                    corcarta = "blue";
                    break;
                case "verde":
                    corcarta = "green";
                    break;
                case "vermelho":
                    corcarta = "red";
                    break;
                case "especial":

                    try { brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/Cartas/especial/coringa.jpg", UriKind.Absolute))); }
                    catch { brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/Cartas/especial/coringa.png", UriKind.Absolute))); }
                    aux.Add(new Carta(brush));

                    try { brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/Cartas/especial/coringaCompra.jpg", UriKind.Absolute))); }
                    catch { brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/Cartas/especial/coringaCompra.png", UriKind.Absolute))); }
                    aux.Add(new Carta(brush));

                    return aux;
                default:
                    return null;

            }

            for (int i = 0; i != 9; i++)
            {
                try { brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/Cartas/" + color + "/" + i + corcarta + ".jpg", UriKind.Absolute))); }
                catch { brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/Cartas/" + color + "/" + i + corcarta + ".png", UriKind.Absolute))); }
                aux.Add(new Carta(brush));
            }

            try { brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/Cartas/" + color + "/" + corcarta + "Bloq.jpg", UriKind.Absolute))); }
            catch { brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/Cartas/" + color + "/" + corcarta + "Bloq.png", UriKind.Absolute))); }
            aux.Add(new Carta(brush));

            try { brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/Cartas/" + color + "/" + corcarta + "Compra.jpg", UriKind.Absolute))); }
            catch { brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/Cartas/" + color + "/" + corcarta + "Compra.png", UriKind.Absolute))); }
            aux.Add(new Carta(brush));

            try { brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/Cartas/" + color + "/" + corcarta + "Inverte.jpg", UriKind.Absolute))); }
            catch { brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/Cartas/" + color + "/" + corcarta + "Inverte.png", UriKind.Absolute))); }
            aux.Add(new Carta(brush));
            return aux;
        }
    }
}
