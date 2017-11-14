using MauMau.Classes.Exceptions;

namespace MauMau.Classes.Background.Estruturas
{
    class Pilha<T>
    {
        public Elemento topo, fundo;
        private int count = 0;

        public int Count { get { return this.count; } }

        public Pilha()
        {
            this.topo = new Elemento(null);
            this.fundo = this.topo;
        }

        /// <summary>
        /// Insere um elemento no topo da pilha
        /// </summary>
        /// <param name="novo"></param>
        public void Push(object novo)
        {
            if (novo == null) throw new NullParameterException(this.ToString() + ", line 24");
            Elemento elemento = new Elemento(novo);
            elemento.Prox = this.topo.Prox;
            this.topo.Prox = elemento;
            if (this.topo == this.fundo) this.fundo = elemento;
            this.count++;
        }
        /// <summary>
        /// Pega o elemento no topo da pilha, removendo-
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            Elemento aux = this.topo.Prox;

            if (aux != null)
            {
                this.topo.Prox = aux.Prox;
                aux.Prox = null;
                if (aux == this.fundo)
                    this.fundo = this.topo;
                this.count--;
                return (T)aux.GetDado();
            }
            else return default(T);
        }
        /// <summary>
        /// Pega o primeiro elemento da pilha sem remove-lo
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            return (T)this.topo.Prox.GetDado();
        }
        /// <summary>
        /// Verifica se a pilha está vazia
        /// </summary>
        /// <returns></returns>
        public bool PilhaVazia()
        {
            return (this.topo == this.fundo);
        }
        /// <summary>
        /// Limpa a pilha
        /// </summary>
        public void Clear()
        {
            this.topo = this.fundo = null;
        }
    }
}
