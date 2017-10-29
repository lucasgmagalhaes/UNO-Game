using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MauMau.Classes.Background
{
    static class Exception
    {
        /// <summary>
        /// Falha no carregamento de uma imagem
        /// </summary>
       public static void IOImage(string msg)
        {
            MessageBox.Show("Não foi possível carregar todas as imagens necessárias para o uso do sistema. O aplicativo será abortado. Erro interno: \n\n" +msg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Environment.Exit(0);
        }
    }
}
