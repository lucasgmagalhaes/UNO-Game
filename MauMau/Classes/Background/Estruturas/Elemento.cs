using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauMau.Classes.Background.Estruturas
{
    class Elemento
    {
        private object dado;
        private Elemento proximo;

        public Elemento Prox { get { return this.proximo; } set { this.proximo = value; } }

        public Elemento(object dado)
        {
            this.dado = dado;
        }

        public object GetDado()
        {
            return this.dado;
        }
    }
}
