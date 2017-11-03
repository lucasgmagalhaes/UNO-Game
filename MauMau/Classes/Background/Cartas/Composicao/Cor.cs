using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauMau.Classes.Background.Cartas.Composicao
{
   public enum Cor
    {
        Azul = 1,
        Amarelo = 2,
        Vermelho = 3,
        Verde = 4
    };

    public static class PaletaCores
    {
        public static Cor GetFromString(string name)
        {
            switch (name.ToUpper())
            {
                case "AZUL": return Cor.Azul;
                case "AMARELO": return Cor.Amarelo;
                case "VERMELHO": return Cor.Vermelho;
                case "VERDE": return Cor.Verde;
                default: return 0;
            }
        } 
    }
}
