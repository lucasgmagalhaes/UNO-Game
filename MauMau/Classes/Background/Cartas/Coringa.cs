using System.Windows.Media;
using MauMau.Classes.Background.Enum;
using MauMau.Classes.Background.Interfaces;

namespace MauMau.Classes.Background.Cartas
{
    class Coringa : Carta
    {
        private Efeito efeito;

        public Efeito Efeito { get { return this.efeito; } }
        public Coringa(ImageBrush img, Efeito buycards, string id) : base(img, id)
        {
            base.frontImage = img;
            this.efeito = buycards;
        }

        public override bool Compatible(ICompatible card)
        {
            return true;
        }

        public override bool Compatible(ICompatible card, Cor color)
        {
            if (card is Normal)
            {
                Normal aux = (Normal)card;
                if (color != 0)
                {
                    if (color == aux.Cor) return true;
                    else return false;
                }
            }
            else if(card is Especial)
            {
                Especial aux = (Especial)card;
                if (color != 0)
                {
                    if (color == aux.Cor) return true;
                    else return false;
                }
            }
            return true;
        }
    }
}
