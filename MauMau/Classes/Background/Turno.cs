using MauMau.Classes.Background.Enum;
using MauMau.Classes.Background.Estruturas;

namespace MauMau.Classes.Background
{
    public class Turno
    {
        int contTurno = 0;
        private Lista<Player> jogadores = new Lista<Player>();
        private Player current;
        private SentidoJogada sentido; // 1 sentido horario - -1 sentido anti-horario

        public Lista<Player> Jogadores { get { return this.jogadores; } }
        public Turno(Lista<Player> jogadores)
        {
            this.jogadores = jogadores;
            current = jogadores[2];
            sentido = SentidoJogada.AntiHorario; // independentemente vai começar sentido horario só troca com o andar do jogo
        }
        /// <summary>
        /// Pega o jogador da vez
        /// </summary>
        /// <returns></returns>
        public Player GetCurrentPlayer()
        {
            return this.current;
        }
        /// <summary>
        /// Define o jogador atual
        /// </summary>
        /// <param name="player"></param>
        public void SetCurrentPlayer(Player player)
        {
            this.current = player;
        }
        /// <summary>
        /// Define o a ordem de turno dos jogadores
        /// </summary>
        /// <param name="val"></param>
        public void SetSentido(SentidoJogada val)
        {
            this.sentido = val;
        }
        /// <summary>
        /// Retorna o sentido do turno dos jogadores
        /// </summary>
        /// <returns></returns>
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

            contTurno++;
        }

        private void VerificarPassagemTurno()
        {
            if (contTurno == 4)
            {
                Log.AddEventoPassagemTurno();

                contTurno = 0;
            }
        }

        public Player GetPlayerByPosition(PlayerPosition pos)
        {
            foreach (Player pl in this.jogadores)
            {
                if (pl.Position == pos) return pl;
            }
            return null;
        }
        /// <summary>
        /// Retorna quem irá jogar no próximo turno
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
                    return jogadores[jogadores.Count - 1];
                }
                else
                {
                    return jogadores[jogadores.GetIndexOf(current) - 1];
                }
            }
        }
    }
}
