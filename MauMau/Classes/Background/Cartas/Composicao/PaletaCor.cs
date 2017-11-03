namespace MauMau.Classes.Background.Cartas.Composicao
{
    public static class PaletaCor
    {
        public static Cor GetCor(string cor_nome)
        {
            switch (cor_nome.ToUpper())
            {
                case "AZUL": return Cor.Azul;
                case "AMARELO": return Cor.Amarelo;
                case "VERMELHO": return Cor.Vermelho;
                case "VERDE": return Cor.Verde;
                default: return 0;
            }
        }
        public static Cor GetCor(int cor_numero)
        {
            switch (cor_numero)
            {
                case 1: return Cor.Azul;
                case 2: return Cor.Amarelo;
                case 3: return Cor.Vermelho;
                case 4: return Cor.Verde;
                default: return 0;
            }
        }
    }
}
