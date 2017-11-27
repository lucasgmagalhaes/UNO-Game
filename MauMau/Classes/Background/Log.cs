using System;
using System.Collections.Generic;
using MauMau.Classes.Background.Cartas;

namespace MauMau.Classes.Background
{
    public static class Log
    {
        static bool sentidoHorario = true;
        static string horaAtual = DateTime.Now.TimeOfDay.ToString().Substring(0, 8) + " - ";
        static List<String> listaEventos = new List<String>();

        public static List<String> ListaEventos { get => listaEventos; set => listaEventos = value; }

        /// <summary>
        /// Chamado sempre que uma carta for jogada, armazenando na lista o nome do Jogador e a(s) carta(s) jogada(s) por ele.
        /// </summary>
        static public void AddEventoJogarCarta(Player player, Carta card)
        {
            string textoEfeito;
            string textoPadrao = "O Jogador " + player.Infos.Name + " jogou a carta ";

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

                case "Curinga":
                    Curinga coringa = (Curinga)card;

                    textoEfeito = coringa.Efeito.ToString();

                    // Se for um tipo "curinga comprar 4", será resolvida no try. Se não, será no catch.
                    try
                    {
                        int aux = int.Parse(textoEfeito.Substring(textoEfeito.Length - 1));
                        textoEfeito = "Curinga Comprar " + aux.ToString();
                    }
                    catch
                    {
                        textoEfeito = "Curinga";
                    }

                    listaEventos.Add(horaAtual + textoPadrao + textoEfeito + ".");
                    break;
            }
        }

        /// <summary>
        /// Evento chamado sempre que o sentido do jogo for invertido (anti-horário -> horário ou vice-versa).
        /// </summary>
        static public void AddEventoInverterJogo()
        {
            if (sentidoHorario)
            {
                listaEventos.Add(horaAtual + "O sentido do jogo foi alterado para Anti-Horário.");
                sentidoHorario = false;
            }
            else
            {
                listaEventos.Add(horaAtual + "O sentido do jogo foi alterado para Horário.");
                sentidoHorario = true;
            }
        }

        /// <summary>
        /// Evento chamado assim que um Jogador chegar ao estado "UNO"
        /// </summary>
        /// <param name="player"></param>
        static public void AddEventoSayUno(Player player)
        {
            listaEventos.Add(horaAtual + "O Jogador " + player.Infos.Name + " disse UNO!!!");
        }

        /// <summary>
        /// Evetno chamado sempre que um Jogador comprar certa quantidade de cartas.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="quantCartas"></param>
        static public void AddEventoComprarCartas(Player player, int quantCartas)
        {
            listaEventos.Add(horaAtual + "O Jogador " + player.Infos.Name + " comprou " + quantCartas + " cartas.");
        }

        static public void AddEventoBloquearJogador(Player playerBloqueado)
        {
            listaEventos.Add(horaAtual + "O Jogador " + player.Infos.Name + " perdeu a vez.");
        }

        /// <summary>
        /// Evento chamado ao final de cada turno (4 jogadas).
        /// </summary>
        static public void AddEventoPassagemTurno()
        {
            listaEventos.Add(horaAtual + "- - - FIM DE TURNO - - -");
        }

        /// <summary>
        /// Permite ao usuário da classe adicionar um evento personalizado, para alguma situação não listada.
        /// </summary>
        /// <param name="descricaoEvento"></param>
        static public void AddEventoPersonalizado(string descricaoEvento)
        {
            listaEventos.Add(horaAtual + descricaoEvento);
        }
    }
}
