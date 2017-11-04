namespace MauMau.Classes.Background.Estruturas
{
    class Pilha<T>
    {
        public Elemento topo, fundo;
        private int indexall;
        public Pilha()
        {
            this.topo = new Elemento(null);
            this.fundo = this.topo;
        }

        public T this[int index]
        {
            get { return (T)this.GetByIndex(index); }
        }

        public object GetByIndex(int val)
        {
            int count = 0;
            Elemento aux = topo.Proximo;
            while (aux == null || count == val) aux = aux.Proximo;
            return aux.GetDado();
        }

        public void Push(object novo)
        {
            Elemento elemento = new Elemento(novo);
            elemento.Proximo = this.topo.Proximo;
            this.topo.Proximo = elemento;
            if (this.topo == this.fundo) this.fundo = elemento;
        }
        /// <summary>
        /// Pega o elemento no topo da pilha, removendo-
        /// </summary>
        /// <returns></returns>
        public object Pop()
        {
            Elemento aux = this.topo.Proximo;

            if (aux != null)
            {
                this.topo.Proximo = aux.Proximo;
                aux.Proximo = null;
                if (aux == this.fundo)
                    this.fundo = this.topo;
                return aux.GetDado();
            }
            else return null;
        }
        /// <summary>
        /// Pega o primeiro elemento da pilha sem remove-lo
        /// </summary>
        /// <returns></returns>
        public object Peek()
        {
            return this.topo.Proximo.GetDado();
        }

        /// <summary>
        /// Verifica se a pilha está vazia
        /// </summary>
        /// <returns></returns>
        public bool PilhaVazia()
        {
            return (this.topo == this.fundo);
        }
    }
}
