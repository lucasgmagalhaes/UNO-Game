using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MauMau.Classes.Background
{
    abstract class Carta
    {
        protected ImageBrush frontImage;
        protected ImageBrush backImage = new ImageBrush(((ImageSource)Application.Current.Resources["Card_other_back"]));
        protected Rectangle elementUI;
        public ImageBrush FrontImage { get { return this.frontImage; } }

        public Carta(ImageBrush image)
        {
            this.frontImage = image;

            this.elementUI = new Rectangle();
            this.elementUI.Fill = this.frontImage;
            this.elementUI.RadiusX = 10;
            this.elementUI.RadiusY = 10;
            this.elementUI.Height = 180;
            this.elementUI.Width = 114;
            this.elementUI.Name = "newcard";
        }

        public Rectangle GetCardUI()
        {
            return this.elementUI;
        }
    }
}
