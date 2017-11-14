using System;
using System.Windows;
using System.IO;

namespace MauMau.Classes.Exceptions
{
    class ImageLoadException : Exception
    {
        public ImageLoadException() : base()
        {

        }
        public ImageLoadException(string msg) : base(msg)
        {
            MessageBox.Show("Não foi possível carregar todas as imagens necessárias para o uso do sistema. O aplicativo será abortado. Erro interno: \n\n" + msg, "ImageLoadException", MessageBoxButton.OK, MessageBoxImage.Error);
            Environment.Exit(0);
        }
    }
}
