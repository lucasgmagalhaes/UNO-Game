using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using MauMau.Classes.Background.Cartas.Composicao;
using MauMau.Classes.Background.Interfaces;

namespace MauMau.Classes.Background.Cartas
{
    class Curinga : Carta, IEquatable
    {
        private Efeito efeito;
        public Curinga(ImageBrush img, Efeito buycards) : base(img)
        {
            base.source = img;
            this.efeito = buycards;
        }

        public bool Equals(IEquatable card)
        {
            return true;
        }
    }
}
