using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MauMau.Classes.Exceptions;
using MauMau.Classes.Background.Cartas;
using MauMau.Classes.Background.Cartas.Composicao;
using MauMau.Classes.Background.Estruturas;
using System.IO;
namespace MauMau.Classes.Background
{
    class Bot : Player
    {
        /// <summary>
        /// Lista de todas as cartas do baralho
        /// </summary>
        public Bot(Profile prf) : base(prf)
        {
            this.SetHand(new List<Carta>());
            SetProfile(prf);
        }
        public Bot(List<Carta> hand, Profile infos) : base(hand, infos)
        {
            this.SetHand(new List<Carta>());
            SetProfile(infos);
        }
        /// <summary>
        /// Simula jogadas de um player
        /// </summary>
        /// <param name="cardMonte">
        /// Recebe a carta do topo do coletor e o Monte</param> 
        public void FazJogada(Monte Mnt, Coletor Clt)
        {
            /// compra carta
            this.AddCardToHand(Mnt.GetCardOnTop());
            // faz animação de compra carta

            Carta ctMenor = null;
            // pega carta do top do coletor como referencia
            Carta cdTop = Clt.GetCardOnTop();
            int valorMenor = 0;

            //prioridade Normal(menor numero) > especial
            foreach (Carta cardMao in GetHand())
            {
                if (cardMao.Equals(cdTop))
                {
                    if (cardMao is Normal)
                    {
                        Normal auxCardMenor = (Normal)cardMao;
                        // seleciona a menor baseado no numero
                        if (valorMenor > auxCardMenor.Numero)
                        {
                            valorMenor = auxCardMenor.Numero;
                            ctMenor = cardMao;
                        }
                    }
                    // se não tiver uma carta menor escolhida irá pegar uma especial caso tenha
                    if (ctMenor == null && cardMao is Especial)
                    {
                        ctMenor = cardMao;
                    }
                }
            } 
            if (ctMenor != null)  
            {
                Carta ctAux = ctMenor; // auxiliar para a carta que será jogada
               //tinha carta para jogar , joga carta para o monte e remove da mão
               // faz animação de jogar carta da mão para monte
                this.GetHand().Remove(this.PlayCard(ctMenor));
                Clt.GetPlayedCard(ctAux);
            }
            if (TimeToUNO())
            {
                //pressiona botão uno
            }
        }
        public Carta JogarCard(Monte Mnt, Coletor Clt)
        {
            /// compra carta
            this.AddCardToHand(Mnt.GetCardOnTop());
            // faz animação de compra carta

            Carta ctMenor = null;
            // pega carta do top do coletor como referencia
            Carta cdTop = Clt.GetCardOnTop();
            int valorMenor = 0;

            //prioridade Normal(menor numero) > especial
            foreach (Carta cardMao in GetHand())
            {
                if (cardMao.Equals(cdTop))
                {
                    if (cardMao is Normal)
                    {
                        Normal auxCardMenor = (Normal)cardMao;
                        // seleciona a menor baseado no numero
                        if (valorMenor > auxCardMenor.Numero)
                        {
                            valorMenor = auxCardMenor.Numero;
                            ctMenor = cardMao;
                        }
                    }
                    // se não tiver uma carta menor escolhida irá pegar uma especial caso tenha
                    if (ctMenor == null && cardMao is Especial)
                    {
                        ctMenor = cardMao;
                    }
                }
            }
            //retorna o card menor que localizou ou uma especial
            return ctMenor;
        }
    }
}
