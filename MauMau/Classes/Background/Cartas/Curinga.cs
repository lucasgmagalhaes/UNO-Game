using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MauMau.Classes.Background.Cartas
{
    class Curinga : Carta
    {
        private bool comefeito;
        public Curinga(ImageBrush img, bool buycards) : base(img)
        {
            base.source = img;
            this.comefeito = buycards;
        }
    }
}
