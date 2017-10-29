using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MauMau.Classes.Graphics
{
    class UIPlayer
    {
        private string name;
        private ImageBrush source;

        public string Name { get { return this.name; } }
        public ImageBrush Source { get { return this.source; } }
        public UIPlayer(string name, ImageBrush source)
        {
            this.name = name;
            this.source = source;
        }
        /// <summary>
        /// Retorna o o path da imagem
        /// </summary>
        /// <returns></returns>
        public ImageSource GetImageSource()
        {
            return this.source.ImageSource;
        }
    }
}
