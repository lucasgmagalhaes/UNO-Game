using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MauMau.Classes.Exceptions;

namespace MauMau.Classes.Background
{
    class Baralho
    {
        /// <summary>
        /// Lista de todas as cartas do baralho
        /// </summary>
        private List<Carta> cartas = new List<Carta>();
        private Random RAM = new Random();

        public Baralho()
        {
            LoadCards();
        }
        /// <summary>
        /// Carrega todas as cartas do baralho
        /// </summary>
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
            catch(Exception ee)
            {
               new ImageLoadException(ee.Message);
            }
        }
        /// <summary>
        /// Retorna uma lista de cartas de acordo com a cor especificada
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Reorganiza o baralho em uma nova ordem
        /// </summary>
        public void Embaralhar()
        {
            if(cartas.Count > 1)
            {
                for(int i = 0; i < cartas.Count; i++)
                {
                    int newplace = RAM.Next(0, cartas.Count - 1);
                    Carta aux = cartas[i];
                    cartas[i] = cartas[newplace];
                    cartas[newplace] = aux;
                }
            }
        }
    }
}
