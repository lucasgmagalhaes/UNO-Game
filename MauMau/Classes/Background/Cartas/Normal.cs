using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MauMau.Classes.Background.Cartas
{
    class Normal : Carta
    {
        private int numero;
        protected string cor;

        public Normal(string cor, int numero, ImageBrush img) : base(img)
        {
            this.numero = numero;
            this.cor = cor;
            base.source = img;
        }
    }
}
