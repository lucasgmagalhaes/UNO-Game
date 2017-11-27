using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using MauMau.Classes.Background.Interfaces;
using MauMau.Classes.Background.Enum;

namespace MauMau.Classes.Background.Cartas
{
   public class Normal : Carta
    {
        private int numero;
        private Cor cor;

        public int Numero { get { return this.numero; } }
        public Cor Cor { get { return this.cor; } }

        public Normal(Cor cor, int numero, ImageBrush img, string id) : base(img, id)
        {
            this.numero = numero;
            this.cor = cor;
            base.frontImage = img;
        }

        public override bool Compatible(ICompatible card)
        {
            if (card is Normal)
            {
                Normal aux = (Normal)card;
                if (aux.Cor == this.cor || aux.numero == this.numero)
                {
                    return true;
                }
            }
            else if (card is Especial)
            {
                Especial aux = (Especial)card;
                if (this.cor == aux.Cor)
                {
                    return true;
                }
            }
            else return true;
            return false;
        }

        public override bool Compatible(ICompatible card, Cor color)
        {
            if (card is Normal)
            {
                Normal auxN = (Normal)card;
                if (color == 0 && this.cor == auxN.Cor || this.numero == auxN.numero)
                {
                    return true;
                }
                else if (auxN.Cor == color)
                {
                    return true;
                }
            }
            else if (card is Especial)
            {
                Especial auxE = (Especial)card;
                if (color == 0 && this.cor == auxE.Cor)
                {
                    return true;
                }
                else if (auxE.Cor == color)
                {
                    return true;
                }
            }
            else return true;
            return false;
        }
    }
}
