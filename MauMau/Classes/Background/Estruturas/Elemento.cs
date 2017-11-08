using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauMau.Classes.Background.Estruturas
{
    class Elemento
    {
        private Dado dado;
        private Elemento proximo;
        private int index;
        public Elemento Prox { get { return this.proximo; } set { this.proximo = value; } }

        public Elemento(object dado, int index)
        {
            this.dado = new Dado(dado, index);
            this.index = index;
        }

        public Dado GetDado()
        {
            return this.dado;
        }
        
        public void SetDado(Dado dado)
        {
            this.dado = dado;
        }

        public void SetDado(object dado, int index)
        {
            this.dado.Info = dado;
            this.dado.Index = index;
        }
        public int GetIndex()
        {
            return this.index;
        }
        public void SetIndex(int val)
        {
            this.index = val;
        }
    }
}
