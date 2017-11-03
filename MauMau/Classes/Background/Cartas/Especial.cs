using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MauMau.Classes.Background.Cartas
{
    class Especial : Carta
    {
        private string efeito;
        protected string cor;

        public Especial(string efeito, ImageBrush img, string cor) : base(img)
        {
            this.efeito = efeito;
            base.source = img;
            this.cor = cor;
        }
    }
}
