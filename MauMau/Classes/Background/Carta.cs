using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MauMau.Classes.Background
{
<<<<<<< HEAD
    class Carta
    {
        private ImageBrush source;
=======
    abstract class Carta
    {
        protected ImageBrush source;

        public ImageBrush Source { get { return this.source; } }
>>>>>>> master

        public Carta(ImageBrush image)
        {
            this.source = image;
        }
    }
}
