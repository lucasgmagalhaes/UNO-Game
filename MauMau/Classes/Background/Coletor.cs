using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauMau.Classes.Background.Estruturas;

namespace MauMau.Classes.Background
{
    /// <summary>
    /// Cartas jogadas
    /// </summary>
    class Coletor
    {
        /// <summary>
        /// Pilha de todas as cartas do baralho
        /// </summary>
        private Pilha<Carta> cartas = new Pilha<Carta>();

        /// <summary>
        /// Recebe a primeira carta do baralho para ser a referência pro procedimento do jogo
        /// </summary>
        /// <param name="primeiranalista"></param>
        public Coletor(Carta primeiranalista)
        {
            this.cartas.Push(primeiranalista);
        }
        /// <summary>
        /// Pega todas as cartas da pilha
        /// </summary>
        /// <returns></returns>
        public Pilha<Carta> GetMonte()
        {
            return this.cartas;
        }
        /// <summary>
        /// Pega a carta no topo da pilha
        /// </summary>
        /// <returns></returns>
        public Carta GetCardOnTop()
        {
            return this.cartas.Peek();
        }
        //Insere na pilha uma carta jogada
        public void GetPlayedCard(Carta card)
        {
            this.cartas.Push(card);
        }
    }
}
