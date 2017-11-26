using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauMau.Classes.Background.Cartas;

namespace MauMau.Classes.Background
{
    static class Log
    {
        static List<String> listaEventos = new List<string>();

        public static List<String> ListaEventos { get => listaEventos; set => listaEventos = value; }

        /// <summary>
        /// Chamado sempre que uma carta for jogada, armazenando na lista o nome do jogador e a(s) carta(s) jogada(s) por ele.
        /// </summary>
        static public void AdicionarEvento(Player player, Carta card)
        {
            string textoEfeito;
            string horaAtual = DateTime.UtcNow.TimeOfDay.ToString().Substring(0, 8) + " - ";
            string textoPadrao = "O jogador " + player.Infos.Name + " jogou a carta ";

            switch (card.GetType().Name)
            {
                case "Normal":
                    Normal normal = (Normal)card;
                    listaEventos.Add(horaAtual + textoPadrao + normal.Cor + " " + normal.Numero + ".");
                    break;

                case "Especial":
                    Especial especial = (Especial)card;

                    textoEfeito = especial.Efeito.ToString();

                    // Se for uma carta do tipo "comprar 2", será resolvida no bloco try. Se não (inverter), será no catch.
                    try
                    {
                        int auxiliar = int.Parse(textoEfeito.Substring(textoEfeito.Length - 1));
                        textoEfeito = textoEfeito.Remove(textoEfeito.Length - 1);
                        textoEfeito += " " + auxiliar.ToString();
                    }
                    catch { }

                    listaEventos.Add(horaAtual + textoPadrao + especial.Cor + " " + textoEfeito + ".");
                    break;

                case "Coringa":
                    Coringa coringa = (Coringa)card;

                    textoEfeito = coringa.Efeito.ToString();

                    // Se for um tipo "coringa comprar 4", será resolvida no try. Se não, será no catch.
                    try
                    {
                        int aux = int.Parse(textoEfeito.Substring(textoEfeito.Length-1));
                        textoEfeito = "Coringa Comprar " + aux.ToString();
                    }
                    catch
                    {
                        textoEfeito = "Coringa";
                    }

                    listaEventos.Add(horaAtual + textoPadrao + textoEfeito + ".");


                    break;
            }    
        }
    }
}
