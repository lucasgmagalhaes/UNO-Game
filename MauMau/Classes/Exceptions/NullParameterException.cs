using System;
using System.Windows;

namespace MauMau.Classes.Exceptions
{
    class NullParameterException : Exception
    {
        public NullParameterException() : base() { MessageBox.Show(base.Message); }
        public NullParameterException(string msg) : base() { MessageBox.Show(msg); }
    }
}
