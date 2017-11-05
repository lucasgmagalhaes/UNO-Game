using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MauMau.Classes.Background
{
    abstract class Carta
    {
        protected ImageBrush source;
        protected Rectangle thisUI;

        public ImageBrush Source { get { return this.source; } }

        public Carta(ImageBrush image)
        {
            this.source = image;

            thisUI = new Rectangle();
            thisUI.Fill = source;
            thisUI.RadiusX = 10;
            thisUI.RadiusY = 10;
            thisUI.Height = 180;
            thisUI.Width = 114;
            thisUI.Name = "newcard";
        }

        public Rectangle GetCardUI()
        {
            return this.thisUI;
        }
    }
}
