namespace MauMau.Classes.Background.Estruturas
{
    /// <summary>
    /// Lista de elementos do tipo T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class Lista<T>
    {
        private Elemento prim, ult;
        private int count;

        /// <summary>
        /// Retorna um elemento pelo seu index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get { return (T)this.GetByIndex(index); }
        }

        private object GetByIndex(int val)
        {
            int auxcount = 0;
            Elemento aux = prim.Prox;
            while (aux == null || count == val)
            {
                aux = aux.Prox;
                auxcount++;
            }
            return aux.GetDado();
        }
        /// <summary>
        /// Retorna o primeiro elemento na lista
        /// </summary>
        /// <returns></returns>
        public object Primeiro()
        {
            return this.prim.Prox.GetDado();
        }

        protected void ElementAdded()
        {
            count++;
        }

        protected void ElementDeleted()
        {
            count--;
        }
        /// <summary>
        /// Informa quantos elementos foram inseridos na lista
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return count;
        }

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
            Rebuild();
            Elemento aux = new Elemento(el);
            this.ult.Prox = aux;
            this.ult = aux;
            ElementAdded();
        }
        /// <summary>
        /// Remove um elemento da lista
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual object Remover(object obj)
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
                    return aux2.GetDado();
                }
                aux = aux.Prox;
            }
            return null;
        }
        /// <summary>
        /// Remove o primeiro elemento da lista
        /// </summary>
        /// <returns></returns>
        public virtual object RemoveFirst()
        {
            if (this.prim.Prox != null)
            {
                Elemento aux = this.prim.Prox;
                Elemento aux2 = aux;
                this.prim.Prox = aux.Prox;
                aux = null;
                ElementDeleted();
                Rebuild();
                return aux2.GetDado();
            }
            else return null;
        }
        /// <summary>
        /// Procura um elemento 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public object Find(object obj)
        {
            Elemento aux = this.prim;
            while (aux != null)
            {
                if (aux.GetDado().Equals(obj))
                {
                    return aux.GetDado();
                }
                aux = aux.Prox;
            }
            return null;
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
    }
}
