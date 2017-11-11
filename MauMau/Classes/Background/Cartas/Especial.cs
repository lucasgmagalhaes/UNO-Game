using System;
using System.Windows.Media;
using MauMau.Classes.Background.Cartas.Composicao;
using MauMau.Classes.Background.Interfaces;
namespace MauMau.Classes.Background.Cartas
{
    class Especial : Carta, IEquatable
    {
        private Efeito efeito;
        private Cor cor;

        public Efeito Efeito { get { return this.efeito; } }
        public Cor Cor { get { return this.cor; } }

        public Especial(Efeito efeito, ImageBrush img, Cor cor, string id) : base(img, id )
        {
            this.efeito = efeito;
            base.frontImage = img;
            this.cor = cor;
        }

        public bool Equals(IEquatable card)
        {
            if (Normal.ReferenceEquals(this, card))
            {
                Normal aux = (Normal)card;
                if (aux.Cor == this.cor) return true;
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
