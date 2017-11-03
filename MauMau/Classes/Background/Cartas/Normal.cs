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

        public Normal(Cor cor, int numero, ImageBrush img) : base(img)
        {
            this.numero = numero;
            this.cor = cor;
            base.source = img;
        }

        public bool Equals(IEquatable card)
        {
            throw new NotImplementedException();
        }
    }
}
