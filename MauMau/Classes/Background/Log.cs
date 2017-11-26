using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauMau.Classes.Background
{
    static class Log
    {
        static List<String> listaEventos = new List<string>();

        public static List<string> ListaEventos { get => listaEventos; set => listaEventos = value; }

        /// <summary>
        /// Método que será chamado sempre que um novo evento acontecer durante o jogo.
        /// </summary>
        static public void AdicionarEvento(string jogadorAtual, string cartaJogada, DateTime tempoJogada)
        {
            
        }

    }
}
