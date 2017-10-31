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
        private List<Carta> hand;
        private Profile infos;

        public Profile Infos { get { return this.infos; } set { this.infos = value; } }

        public Player(Profile infos)
        {
            this.hand = new List<Carta>();
            this.infos = infos;
        }

        public Player(List<Carta> hand, Profile infos)
        {
            this.hand = hand;
            this.infos = infos;
        }

        public void AddCardToHand(Carta card)
        {
            this.hand.Add(card);
        }

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
                if (this.hand[i] == card)
                {
                    hand.RemoveAt(i);
                    return card;
                }
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
