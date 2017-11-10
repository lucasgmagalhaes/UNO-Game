using MauMau.Classes.Background.Estruturas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauMau.Classes.Background
{
    class Monte
    {
        /// <summary>
        /// Lista de todas as cartas do restantes
        /// </summary>
        private Pilha<Carta> cartas = new Pilha<Carta>();
        
        public Monte(Lista<Carta> cartas)
        {
            foreach (Carta card in cartas) this.cartas.Push(card);
        }

        public Pilha<Carta> GetMonte()
        {
            return this.cartas;
        }
        /// <summary>
        /// Pega a primeira carta do topo removendo-a
        /// </summary>
        /// <returns></returns>
        public Carta RemoveTopCard()
        {
            return this.cartas.Pop();
        }
        /// <summary>
        /// Pega a primeira carta do topo sem remove-la
        /// </summary>
        /// <returns></returns>
        public Carta GetTopCard()
        {
            return this.cartas.Peek();
        }

        public void PlayCard(Carta card)
        {
            this.cartas.Push(card);
        }

        public int Count()
        {
            return this.cartas.Count;
        }
    }
}
