using System;
using System.Collections.Generic;
using MauMau.Classes.Background.Cartas;

namespace MauMau.Classes.Background
{
    static class Log
    {
        static int aux;
        static List<String> listaEventos = new List<String>();

        public static List<String> ListaEventos { get => listaEventos; set => listaEventos = value; }

        /// <summary>
        /// Chamado sempre que uma carta for jogada.
        /// </summary>
        static public void AdicionarEvento(Object obj)
        {
            string horaAtual = DateTime.UtcNow.TimeOfDay.ToString().Substring(0, 8) + " - ";
            string textoPadrao = horaAtual + "Carta jogada: ";


            switch (obj.GetType().Name)
            {
                case "Normal":
                    Normal aux = (Normal)obj;
                    listaEventos.Add(textoPadrao + aux.Cor + " " + aux.Numero);
                    break;

                case "Bot":
                    Player bot = (Player)obj;
                    listaEventos.Add(horaAtual + "Player atual: " + bot.Infos.Name);
                    break;

                case "Player":
                    Player player = (Player)obj;
                    listaEventos.Add(horaAtual + "Player atual: " + player.Infos.Name);
                    break;

                case "Especial":
                    Especial especial = (Especial)obj;
                    listaEventos.Add(textoPadrao + especial.Cor + ", Efeito: " + especial.Efeito);
                    break;

                case "Coringa":
                    Coringa coringa = (Coringa)obj;
                    listaEventos.Add(textoPadrao + coringa.Efeito);
                    break;
            }

        }
    }
}
