using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MauMau.Classes.Background
{
    class Profile
    {
        private string name;
        private ImageBrush source;
        Rectangle elementUI;

        public string Name { get { return this.name; } }
        public ImageBrush Source { get { return this.source; } }
        public Rectangle ElementUI { get { return elementUI; } set { elementUI = value; }
        }

        public Profile(string name, ImageBrush source)
        {
            this.name = name;
            this.source = source;

            this.elementUI = new Rectangle();
            this.elementUI.Fill = this.source;
            this.elementUI.RadiusX = 10;
            this.elementUI.RadiusY = 10;
            this.elementUI.Height = 55;
            this.elementUI.Width = 62;
            this.elementUI.Name = name;
        }
        /// <summary>
        /// Retorna o o path da imagem
        /// </summary>
        /// <returns></returns>
        public ImageBrush GetImageBrush()
        {
            return this.source;
        }
    }
}
