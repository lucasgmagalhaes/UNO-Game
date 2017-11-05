using MauMau.Classes.Background.Estruturas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
namespace MauMau.Classes.Background
{
    class Player
    {
        private Lista<Carta> hand;
        private Profile infos;

        public Profile Infos { get { return this.infos; } set { this.infos = value; } }
        public Lista<Carta> Hand { get { return this.hand; } }

        public Player(Profile infos)
        {
            this.hand = new Lista<Carta>();
            this.infos = infos;
        }

        public Player(Lista<Carta> hand, Profile infos)
        {
            this.hand = hand;
            this.infos = infos;
        }

        /// <summary>
        /// Adiciona uma carta a mão do jogador
        /// </summary>
        /// <param name="card"></param>
        public void AddCardToHand(Carta card)
        {
            this.hand.Add(card);
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
        /// Remove uma carta do baralho
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        private Carta Remover(Carta card)
        {
            for (int i = 0; i < this.hand.Count; i++)
            {
                if (this.hand[i] == card) return hand.RemoveAt(i);
            }
            return null;
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
            if (this.hand.Count == 1) return true;
            return false;
        }
    }
}
