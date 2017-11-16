using MauMau.Classes.Background.Enum;
using MauMau.Classes.Background.Estruturas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauMau.Classes.Background
{
    class Turno
    {
        private Lista<Player> jogadores = new Lista<Player>();
        private Player current;
        private SentidoJogada sentido; // 1 sentido horario - -1 sentido anti-horario

        public Turno(Lista<Player> jogadores)
        {
            this.jogadores = jogadores;
            current = jogadores[2];
            sentido = SentidoJogada.Horario; // independentemente vai começar sentido horario só troca com o andar do jogo
        }
        /// <summary>
        /// Pega o jogador da vez
        /// </summary>
        /// <returns></returns>
        public Player GetCurrentPlayer()
        {
            return this.current;
        }
        public void SetSentido(SentidoJogada val)
        {
            this.sentido = val;
        }
        public SentidoJogada GetSentido()
        {
            return this.sentido;
        }
        /// <summary>
        /// Bloqueia a jogada do proximo jogador
        /// </summary>
        public void PularProximo()
        {
            switch (this.sentido)
            {
                case SentidoJogada.Horario:
                    if (jogadores.GetIndexOf(current) + 2 >= jogadores.Count)
                    {
                        current = jogadores[0];
                    }
                    else
                    {
                        current = jogadores[jogadores.GetIndexOf(current) + 2];
                    }
                    break;
                case SentidoJogada.AntiHorario:
                    if (jogadores.GetIndexOf(current) - 2 < 0)
                    {
                        current = jogadores[jogadores.Count - 1];
                    }
                    else
                    {
                        current = jogadores[jogadores.GetIndexOf(current) - 1];
                    }
                    break;
            }
        }
        /// <summary>
        /// Finaliza o turno do jogador definindo o proximo na lista
        /// </summary>
        public void EndPLayerTurn()
        {
            if (sentido == SentidoJogada.Horario)
            {
                if (jogadores.GetIndexOf(current) + 1 == jogadores.Count)
                {
                    current = jogadores[0];
                }
                else
                {
                    current = jogadores[jogadores.GetIndexOf(current) + 1];
                }
            }
            else
            {
                if (jogadores.GetIndexOf(current) - 1 < 0)
                {
                    current = jogadores[jogadores.Count - 1];
                }
                else
                {
                    current = jogadores[jogadores.GetIndexOf(current) - 1];
                }
            }

        }
    }
}
