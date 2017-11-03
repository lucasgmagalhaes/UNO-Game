using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauMau.Classes.Background.Estruturas
{
    class Pilha<T>
    {
        public Elemento root;
        private int indexall;
        public Pilha()
        {
            indexall = 0;
        }

        public T this[int index]
        {
            get { return (T)this.GetByIndex(index); }
        }

        public object GetByIndex(int val)
        {
            int count = 0;
            Elemento aux = root.Proximo;
            while (aux == null || count == val) aux = aux.Proximo;
            return aux.GetDado();
        }

        public void Add(object obj)
        {
            if (this.root == null)
            {
                this.root = new Elemento(obj);
                this.indexall++;
            }
        }
    }
}
