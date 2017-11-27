using MauMau.Classes.Background.Enum;
using MauMau.Classes.Background.Estruturas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MauMau.Classes.Background
{
   public class Player
    {
        protected Lista<Carta> hand;
        protected Profile infos;
        protected PlayerPosition position;
        protected bool uno;

        public bool Uno { get { return this.uno; } }
        public Profile Infos { get { return this.infos; } set { this.infos = value; } }
        public Lista<Carta> Hand { get { return this.hand; } }
        public PlayerPosition Position { get { return this.position; } }

        public Player(Profile infos)
        {
            this.hand = new Lista<Carta>();
            this.infos = infos;
        }

        public Player(Profile infos, PlayerPosition position)
        {
            this.hand = new Lista<Carta>();
            this.infos = infos;
            this.position = position;
        }

        public Player(Lista<Carta> hand, Profile infos)
        {
            this.hand = hand;
            this.infos = infos;
        }

        public Player(Lista<Carta> hand, Profile infos, PlayerPosition position)
        {
            this.hand = hand;
            this.infos = infos;
            this.position = position;
        }
        /// <summary>
        /// Adiciona uma carta a mão do jogador
        /// </summary>
        /// <param name="card"></param>
        public void AddCardToHand(Carta card)
        {
            this.hand.Add(SetCardAngle(card));
            if (uno) uno = false;
        }
        /// <summary>
        /// Retira uma carta da mão do jogador
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public Carta PlayCard(Carta card)
        {
            return Remover(card);
        }
        /// <summary>
        /// Define o angulo da carta com base no jogador
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        protected Carta SetCardAngle(Carta card)
        {
            switch (this.position)
            {
                case PlayerPosition.Left:
                    RotateTransform rotate = new RotateTransform(-90);
                    card.ElementUI.RenderTransform = rotate;
                    return card;

                case PlayerPosition.Right:
                    rotate = new RotateTransform(90);
                    card.ElementUI.RenderTransform = rotate;
                    return card;

                case PlayerPosition.Top:
                    rotate = new RotateTransform(180);
                    card.ElementUI.RenderTransform = rotate;
                    return card;

                default: return card;
            }
        }

        /// <summary>
        /// Remove uma carta do baralho
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        private Carta Remover(Carta card)
        {
            for (int i = 0; i < this.CountHand(); i++)
            {
                if (this.hand[i] == card)
                {
                    return hand.RemoveAt(i);
                }
            }
            return this.hand.Remover(card);
        }
        /// <summary>
        /// Retorna o número de cartas na mão do jogador
        /// </summary>
        /// <returns></returns>
        public int CountHand()
        {
            return this.hand.Count;
        }
        /// <summary>
        /// Retorna se o jogador pode ou não pedir UNO
        /// </summary>
        /// <returns></returns>
        public bool TimeToUNO()
        {
            if (CountHand() == 1) return true;
            return false;
        }
        /// <summary>
        /// Pega a coleção de cartas
        /// </summary>
        /// <returns></returns>
        public Lista<Carta> GetHand()
        {
            return this.hand;
        }
        /// <summary>
        /// Define a coleção de cartas
        /// </summary>
        /// <param name="hd"></param>
        public void SetHand(Lista<Carta> hd)
        {
            this.hand = hd;
        }
        /// <summary>
        /// Pega o perfil do jogador
        /// </summary>
        /// <returns></returns>
        public Profile GetProfile()
        {
            return this.infos;
        }
        /// <summary>
        /// Define o perfil do jogador
        /// </summary>
        /// <param name="prf"></param>
        public void SetProfile(Profile prf)
        {
            this.infos = prf;
        }
        /// <summary>
        /// Retorna a lista de cartas do jogador
        /// </summary>
        /// <returns></returns>
        public Carta GetLastCard()
        {
            return this.hand[this.hand.Count - 1];
        }
        /// <summary>
        /// Ao gritar uno, o jogador evita pegar uma carta em seu proximo turno, caso esse método não seja chamado, 
        /// No próximo turno, o jogador deve receber uma carta
        /// </summary>
        public void SayUno()
        {
            this.uno = true;
        }
    }
}
