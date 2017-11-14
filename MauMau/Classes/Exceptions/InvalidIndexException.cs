using System;
using System.Windows;

namespace MauMau.Classes.Exceptions
{
    class InvalidIndexException : Exception
    {
        public InvalidIndexException() : base()
        {
            MessageBox.Show("Índice inválido para pesquisa em um vetor.", "InvalidIndexException", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public InvalidIndexException(string msg) : base()
        {
            MessageBox.Show("Índece inválido. \n" + msg, "InvalidIndexException", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
