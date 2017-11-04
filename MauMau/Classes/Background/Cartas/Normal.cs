using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using MauMau.Classes.Background.Interfaces;
using MauMau.Classes.Background.Cartas.Composicao;

namespace MauMau.Classes.Background.Cartas
{
    class Normal : Carta, IEquatable
    {
        private int numero;
        private Cor cor;

        public int Numero { get { return this.numero; } }
        public Cor Cor { get{ return this.cor; } }

        public Normal(Cor cor, int numero, ImageBrush img) : base(img)
        {
            this.numero = numero;
            this.cor = cor;
            base.source = img;
        }

        public bool Equals(IEquatable card)
        {
            if(Normal.ReferenceEquals(this, card))
            {
                Normal aux = (Normal)card;
                if (aux.numero == this.numero || aux.cor == this.cor) return true;
                else return false;
            }
            else
            {
                Especial aux = (Especial)card;
                if (this.cor == aux.Cor) return true;
                else return false;
            }
        }
    }
}
