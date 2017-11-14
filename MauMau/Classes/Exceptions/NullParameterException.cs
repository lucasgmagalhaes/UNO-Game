using System;
using System.Windows;

namespace MauMau.Classes.Exceptions
{
    class NullParameterException : Exception
    {
        public NullParameterException() : base()
        {
            MessageBox.Show("Não é possível passar um valor null como parâmetro para o método.", "NullParameterException", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public NullParameterException(string msg) : base()
        {
            MessageBox.Show("Parâmetro null.: \n" + msg, "NullParameterException", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
