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
        
        public Monte(List<Carta> cartas)
        {
            foreach (Carta card in cartas) this.cartas.Push(card);
        }

        public Pilha<Carta> GetMonte()
        {
            return this.cartas;
        }

        public Carta GetCardOnTop()
        {
            return this.cartas.Pop();
        }
        
        public void PlayCard(Carta card)
        {
            this.cartas.Push(card);
        }
    }
}
