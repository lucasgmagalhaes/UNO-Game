using System;
using System.Windows.Media;
using MauMau.Classes.Background.Enum;
using MauMau.Classes.Background.Interfaces;
namespace MauMau.Classes.Background.Cartas
{
    class Especial : Carta
    {
        private Efeito efeito;
        private Cor cor;

        public Efeito Efeito { get { return this.efeito; } }
        public Cor Cor { get { return this.cor; } }

        public Especial(Efeito efeito, ImageBrush img, Cor cor, string id) : base(img, id)
        {
            this.efeito = efeito;
            base.frontImage = img;
            this.cor = cor;
        }

        public override bool Compatible(ICompatible card)
        {
            if (card is Normal)
            {
                Normal aux = (Normal)card;
                if (aux.Cor == this.cor) return true;
            }
            else if (card is Especial)
            {
                Especial aux = (Especial)card;
                if (this.cor == aux.Cor || this.efeito == aux.efeito) return true;
            }
            else return true;
            return false;
        }

        public override bool Compatible(ICompatible card, Cor color)
        {
            if (card is Normal)
            {
                Normal aux = (Normal)card;
                if (color == 0 && aux.Cor == this.cor) return true;
                else if(color == aux.Cor) return true;
            }
            else if (card is Especial)
            {
                Especial aux = (Especial)card;
                if (color == 0 && this.cor == aux.Cor || this.efeito == aux.efeito) return true;
                else if (color == aux.cor) return true;
            }
            else return true;
            return false;
        }
    }
}
