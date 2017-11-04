using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MauMau.Classes.Exceptions;
=======
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MauMau.Classes.Exceptions;
using MauMau.Classes.Background.Cartas;
using MauMau.Classes.Background.Cartas.Composicao;
>>>>>>> master

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
<<<<<<< HEAD
            catch(Exception ee)
            {
               new ImageLoadException(ee.Message);
=======
            catch (Exception ee)
            {
                new ImageLoadException(ee.Message);
>>>>>>> master
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
<<<<<<< HEAD
            ImageBrush brush;
=======
>>>>>>> master
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
<<<<<<< HEAD

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
=======
                    for (int i = 1; i <= 4; i++)//Quatro cartas coringa
                    {
                        aux.Add(CreateCuringaComEfeito());
                        aux.Add(CreateCuringaSemEfeito());
                    }
                    return aux;
                default:
                    return null;
            }

            for (int i = 0; i != 9; i++) aux.Add(CreateCard(color, i.ToString(), corcarta));
            for (int i = 1; i <= 2; i++) //Duas cartas especiais de cada um desses tipos
            {
                aux.Add(CreateCardSpecial(color, corcarta, "Bloq"));
                aux.Add(CreateCardSpecial(color, corcarta, "Compra"));
                aux.Add(CreateCardSpecial(color, corcarta, "Inverte"));
            }
            return aux;
        }

        /// <summary>
        /// Referido as cartas normais
        /// </summary>
        /// <param name="foldercolor"></param>
        /// <param name="cardNumber"></param>
        /// <param name="cardcolor"></param>
        private Carta CreateCard(string foldercolor, string cardNumber, string cardcolor)
        {
            ImageBrush brush;

            try { brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/Cartas/" + foldercolor + "/" + cardNumber + cardcolor + ".jpg", UriKind.Absolute))); }
            catch { brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/Cartas/" + foldercolor + "/" + cardNumber + cardcolor + ".png", UriKind.Absolute))); }

            return new Normal(PaletaCor.GetCor(foldercolor), int.Parse(cardNumber), brush);
        }

        /// <summary>
        /// Cria as cartas especiais de uma cor
        /// </summary>
        /// <param name="foldercolor"></param>
        /// <param name="cardcolor"></param>
        private Carta CreateCardSpecial(string foldercolor, string cardcolor, string cardname)
        {
            ImageBrush brush;

            try { brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/Cartas/" + foldercolor + "/" + cardcolor + cardname + ".jpg", UriKind.Absolute))); }
            catch { brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/Cartas/" + foldercolor + "/" + cardcolor + cardname + ".png", UriKind.Absolute))); }

            Efeito efeitoaux;
            if (cardname.ToUpper() == "BLOQ") efeitoaux = Efeito.Bloquear;
            else if (cardname.ToUpper() == "COMPRA") efeitoaux = Efeito.Comprar2;
            else efeitoaux = Efeito.Inverter;

            return new Especial(efeitoaux, brush, PaletaCor.GetCor(foldercolor));
        }

        private Carta CreateCuringaSemEfeito()
        {
            ImageBrush brush;

            try { brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/Cartas/especial/coringa.jpg", UriKind.Absolute))); }
            catch { brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/Cartas/especial/coringa.png", UriKind.Absolute))); }
            return new Curinga(brush, Efeito.MudarCor);
        }

        private Carta CreateCuringaComEfeito()
        {
            ImageBrush brush;

            try { brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/Cartas/especial/coringaCompra.jpg", UriKind.Absolute))); }
            catch { brush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MauMau;component/Images/Cartas/especial/coringaCompra.png", UriKind.Absolute))); }
            return new Curinga(brush, Efeito.MudarCorEComprar4);
        }

        public List<Carta> GetCards()
        {
            return this.cartas;
        }
        /// <summary>
>>>>>>> master
        /// Reorganiza o baralho em uma nova ordem
        /// </summary>
        public void Embaralhar()
        {
<<<<<<< HEAD
            if(cartas.Count > 1)
            {
                for(int i = 0; i < cartas.Count; i++)
=======
            if (cartas.Count > 1)
            {
                for (int i = 0; i < cartas.Count; i++)
>>>>>>> master
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
