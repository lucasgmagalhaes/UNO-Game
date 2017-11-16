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

        public Lista<Player> Jogadores { get { return this.jogadores; } }
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
        public void SetCurrentPlayer(Player player)
        {
            this.current = player;
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
        /// <summary>
        /// Retorna o jogador no próximo turno
        /// </summary>
        /// <returns></returns>
        public Player GetNextPlayerInOrder()
        {
            if (sentido == SentidoJogada.Horario)
            {
                if (jogadores.GetIndexOf(current) + 1 == jogadores.Count)
                {
                    return jogadores[0];
                }
                else
                {
                    return jogadores[jogadores.GetIndexOf(current) + 1];
                }
            }
            else
            {
                if (jogadores.GetIndexOf(current) - 1 < 0)
                {
                    return  jogadores[jogadores.Count - 1];
                }
                else
                {
                    return jogadores[jogadores.GetIndexOf(current) - 1];
                }
            }
        }
    }
}
