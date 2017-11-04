using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MauMau.Classes.Background
{
    abstract class Carta
    {
        protected ImageBrush source;

        public ImageBrush Source { get { return this.source; } }

        public Carta(ImageBrush image)
        {
            this.source = image;
        }
    }
}
