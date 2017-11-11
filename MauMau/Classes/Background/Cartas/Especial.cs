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

        public Especial(Efeito efeito, ImageBrush img, Cor cor, string id) : base(img, id )
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
                else return false;
            }
            else if (card is Especial)
            {
                Especial aux = (Especial)card;
                if (this.cor == aux.Cor) return true;
                else return false;
            }
            else return true;
        }
    }
}
