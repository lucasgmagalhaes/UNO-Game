using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauMau.Classes.Background
{
    /// <summary>
    /// Cartas jogadas
    /// </summary>
    class Descarte
    {
        /// <summary>
        /// Lista de todas as cartas do baralho
        /// </summary>
        private Stack<Carta> cartas = new Stack<Carta>();

        /// <summary>
        /// Recebe a primeira carta do baralho para ser a referência pro procedimento do jogo
        /// </summary>
        /// <param name="primeiranalista"></param>
        public Descarte(Carta primeiranalista)
        {
            this.cartas.Push(primeiranalista);
        }

        public Stack<Carta> GetMonte()
        {
            return this.cartas;
        }

        public Carta GetCardOnTop()
        {
            return this.cartas.Peek();
        }

        public void PlayCard(Carta card)
        {
            this.cartas.Push(card);
        }
    }
}
