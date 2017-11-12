using System;
using System.Windows;

namespace MauMau.Classes.Exceptions
{
    class InvalidIndexException : Exception
    {
        public InvalidIndexException() : base() { MessageBox.Show(base.Message); }
        public InvalidIndexException(string msg) : base() { MessageBox.Show(msg); }
    }
}
