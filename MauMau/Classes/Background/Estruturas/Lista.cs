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
            this.prim = new Elemento(null, 0);
            this.ult = this.prim;
        }
        /// <summary>
        /// Retorna um elemento pelo seu index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get
            {
                if (index > count) return default(T);
                return (T)this.GetByIndex(index);
            }
            set
            {
                Elemento val = this.GetElementoByIndex(index);
                val.SetDado(value);
            }
        }
        /// <summary>
        /// Retorna um objeto pelo seu index
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private object GetByIndex(int val)
        {
            Elemento aux = prim.Prox;
            if (val > this.count || val < 0) throw new IndexOutOfRangeException();
            else if (aux == null) throw new NullReferenceException();

            while (aux != null && val != aux.GetIndex()) aux = aux.Prox;
            return aux.GetDado();
        }
        /// <summary>
        /// Retorna um elemento pelo seu index
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private Elemento GetElementoByIndex(int val)
        {
            Elemento aux = this.prim.Prox;
            if (val > this.count || val < 0) throw new IndexOutOfRangeException();
            else if (aux == null) throw new NullReferenceException();

            while (val != aux.GetIndex()) aux = aux.Prox;
            return aux;
        }
        /// <summary>
        /// Através de um objeto, verifica se o mesmo existe na lista
        /// </summary>
        /// <param name="dado"></param>
        /// <returns></returns>
        private Elemento GetElementOnList(object dado)
        {
            Elemento aux = this.prim.Prox;
            while (aux.GetDado() != dado || aux == null) aux = aux.Prox;
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
                this.prim = new Elemento(dad, 0);
                this.ult = prim;
            }
        }
        /// <summary>
        /// Adiciona um elemento na lista
        /// </summary>
        /// <param name="el"></param>
        public virtual void Add(object el)
        {
            Elemento aux = new Elemento(el, this.count);
            this.ult.Prox = aux;
            this.ult = aux;
            this.ElementAdded();
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
                    this.ElementDeleted();
                    this.Rebuild();
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
                this.ElementDeleted();
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
            return new internClass(this);
        }
        /// <summary>
        /// Classe interna para foreach
        /// </summary>
        private class internClass : IEnumerator
        {
            private Lista<T> val;
            private int indexactual = -1;

            public internClass(Lista<T> val)
            {
                this.val = val;
            }
            public object Current
            {
                get
                {
                    return this.val[indexactual];
                }
            }

            public bool MoveNext()
            {
                indexactual++;
                return (indexactual < this.val.count);
            }

            public void Reset()
            {
                indexactual = -1;
            }
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
                if (aux.Prox != null) aux = aux.Prox;
            }
            Elemento aux2 = aux.Prox;
            aux.Prox = aux.Prox.Prox;
            aux2.Prox = null;
            ElementDeleted();
            Rebuild();
            return (T)aux2.GetDado();
        }
        /// <summary>
        /// troca a posições entre dois elementos da lista
        /// </summary>
        /// <param name="dadofrom"></param>
        /// <param name="dadoto"></param>
        public void Switch(object dadofrom, object dadoto)
        {

        }
        /// <summary>
        /// troca a posições entre dois elementos da lista
        /// </summary>
        /// <param name="dadofrom"></param>
        /// <param name="dadoto"></param>
        public void Switch(object dadofrom, int dadoto)
        {

        }
        /// <summary>
        /// troca a posições entre dois elementos da lista
        /// </summary>
        /// <param name="dadofrom"></param>
        /// <param name="dadoto"></param>
        public void Switch(int dadofrom, object dadoto)
        {
            this.TreatIndexException(dadofrom);
            this.TreatElementException(dadoto);

            object aux = this.GetElementoByIndex(dadofrom);
            ((Elemento)dadoto).SetDado(this.GetElementoByIndex(dadofrom).GetDado());
            this.GetElementoByIndex(dadofrom).SetDado(aux);
        }
        /// <summary>
        /// troca a posições entre dois elementos da lista
        /// </summary>
        /// <param name="dadofrom"></param>
        /// <param name="dadoto"></param>
        public void Switch(int dadofrom, int dadoto)
        {
            this.TreatIndexException(dadofrom);
            this.TreatIndexException(dadoto);

            object aux = this.GetByIndex(dadoto);
            this.GetElementoByIndex(dadoto).SetDado(this.GetElementoByIndex(dadofrom).GetDado());
            this.GetElementoByIndex(dadofrom).SetDado(aux);
        }
        /// <summary>
        /// Embaralha a posição de todos os elementos da lista
        /// </summary>
        /// <param name="RAM"></param>
        public void SwitchAll(Random RAM)
        {
            if (this.count > 1)
            {
                for (int i = 0; i < this.count; i++)
                {
                    int newplace = RAM.Next(0, this.count - 1);
                    object obj = this[i];
                    this[i] = this[newplace];
                    this[newplace] = (T)obj;
                }
            }
        }

        private void TreatIndexException(int forsearch)
        {
            if (forsearch < 0 || forsearch > this.count) throw new IndexOutOfRangeException();
        }

        private void TreatElementException(Elemento el)
        {
            Elemento aux;
            if (el == null) throw new ArgumentNullException();
            else if ((aux = GetElementOnList(el.GetDado())) == null) throw new NullReferenceException();
        }
        private void TreatElementException(object obj)
        {
            Elemento aux;
            if (obj == null) throw new ArgumentNullException();
            else if ((aux = this.GetElementOnList(obj)) == null) throw new NullReferenceException();
        }
    }
}
