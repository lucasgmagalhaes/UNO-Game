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
        public Rectangle ElementUI { get { return this.elementUI; } set { this.elementUI = value; } }
        public ImageBrush BackImage { get { return this.backImage; } }

        public Carta(ImageBrush image, string id)
        {
            this.frontImage = image;

            this.elementUI = new Rectangle();
            this.elementUI.Fill = this.frontImage;
            this.elementUI.RadiusX = 10;
            this.elementUI.RadiusY = 10;
            this.elementUI.Height = 180;
            this.elementUI.Width = 114;
            this.elementUI.Name = "newcard";
            this.elementUI.Uid = id;
        }
        public string GetID()
        {
            return this.elementUI.Uid;
        }
    }
}
