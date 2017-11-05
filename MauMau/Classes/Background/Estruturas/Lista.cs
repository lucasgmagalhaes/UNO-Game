using System;
using System.Collections;

namespace MauMau.Classes.Background.Estruturas
{
    /// <summary>
    /// Lista de elementos do tipo T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class Lista<T> : IEnumerator, IEnumerable
    {
        private Elemento prim, ult;
        private int count;
        private int elementIndex = 0;
        private int actualIndexPosition = -1;
        public int Count { get { return this.count; } }
        public object Current
        {
            get
            {
                T val = this[actualIndexPosition];
                if (val == null) throw new InvalidOperationException();
                else return val;
            }
        }
        public Lista()
        {
            this.prim = new Elemento(null);
            this.ult = this.prim;
        }
        /// <summary>
        /// Retorna um elemento pelo seu index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get { return (T)this.GetByIndex(index); }
            set
            {
                Elemento val = this.GetElementoByIndex(index);
                val.SetDado(value);
            }
        }
        /// <summary>
        /// Posiciona o ponteiro da lista para o próximo elemento da mesma
        /// </summary>
        /// <returns></returns>
        private int GetNewIndex()
        {
            return this.elementIndex++;
        }
        /// <summary>
        /// Retorna um objeto pelo seu index
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private object GetByIndex(int val)
        {
            int auxcount = 0;
            Elemento aux = prim.Prox;
            while (aux != null && count == val || auxcount < actualIndexPosition || auxcount < val)
            {
                aux = aux.Prox;
                auxcount++;
            }
            return aux.GetDado();
        }
        /// <summary>
        /// Retorna um elemento pelo seu index
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private Elemento GetElementoByIndex(int val)
        {
            int auxcount = 0;
            Elemento aux = prim.Prox;
            while (aux == null || count == val)
            {
                aux = aux.Prox;
                auxcount++;
            }
            return aux;
        }
        /// <summary>
        /// Retorna o primeiro elemento na lista
        /// </summary>
        /// <returns></returns>
        public T Primeiro()
        {
            return (T)this.prim.Prox.GetDado();
        }
        /// <summary>
        /// Acrescenta o contador de elementos da lista
        /// </summary>
        protected void ElementAdded()
        {
            count++;
        }
        /// <summary>
        /// Decrescenta o contador de elementos da lista
        /// </summary>
        protected void ElementDeleted()
        {
            count--;
        }
        /// <summary>
        /// "Recria" a lista
        /// </summary>
        private void Rebuild()
        {
            if (this.prim.Prox == null)
            {
                object dad = null;
                this.prim = new Elemento(dad);
                this.ult = prim;
            }
        }
        /// <summary>
        /// Adiciona um elemento na lista
        /// </summary>
        /// <param name="el"></param>
        public virtual void Add(object el)
        {
            Elemento aux = new Elemento(el, GetNewIndex());
            this.ult.Prox = aux;
            this.ult = aux;
            ElementAdded();
        }
        /// <summary>
        /// Remove um elemento da lista
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual T Remover(object obj)
        {
            Elemento aux = this.prim;
            while (aux.Prox != null)
            {
                if (aux.Prox.GetDado().Equals(obj))
                {
                    Elemento aux2 = aux.Prox;
                    aux.Prox = aux.Prox.Prox;
                    aux2.Prox = null;
                    ElementDeleted();
                    Rebuild();
                    return (T)aux2.GetDado();
                }
                aux = aux.Prox;
            }
            return default(T);
        }
        /// <summary>
        /// Remove o primeiro elemento da lista
        /// </summary>
        /// <returns></returns>
        public virtual T RemoveFirst()
        {
            if (this.prim.Prox != null)
            {
                Elemento aux = this.prim.Prox;
                Elemento aux2 = aux;
                this.prim.Prox = aux.Prox;
                aux = null;
                ElementDeleted();
                Rebuild();
                return (T)aux2.GetDado();
            }
            else return default(T);
        }
        /// <summary>
        /// Procura um elemento 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public T Find(T obj)
        {
            Elemento aux = this.prim;
            while (aux != null)
            {
                if (aux.GetDado().Equals(obj))
                {
                    return (T)aux.GetDado();
                }
                aux = aux.Prox;
            }
            return default(T);
        }
        /// <summary>
        /// Limpa totalmente a lista
        /// </summary>
        public void Clear()
        {
            Elemento aux = this.prim.Prox;
            Elemento aux2;
            while (aux != null)
            {
                aux2 = aux;
                aux = null;
                aux = aux2.Prox;
            }
        }
        /// <summary>
        /// Setta para o proximo elmeento da lista
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            actualIndexPosition++;
            return (actualIndexPosition < this.count);
        }
        /// <summary>
        /// Retorna o ponteiro para o primeiro elemento da lista
        /// </summary>
        public void Reset()
        {
            this.actualIndexPosition = -1;
        }
        /// <summary>
        /// Pega o elemento
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }
        /// <summary>
        /// Retorna o index de um elemento
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetIndexOf(object obj)
        {
            int auxcount = 0;
            Elemento aux = prim.Prox;
            while (aux != null || aux.GetDado() == obj)
            {
                aux = aux.Prox;
                auxcount++;
            }
            return count;
        }
        /// <summary>
        /// Remove um elemento localizado em uma posição fornecida
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T RemoveAt(int index)
        {
            Elemento aux = this.prim;
            for (int i = 0; i < index; i++)
            {
                if(aux.Prox != null) aux = aux.Prox;
            }
            Elemento aux2 = aux.Prox;
            aux.Prox = aux.Prox.Prox;
            aux2.Prox = null;
            ElementDeleted();
            Rebuild();
            return (T)aux2.GetDado();
        }
    }
}
