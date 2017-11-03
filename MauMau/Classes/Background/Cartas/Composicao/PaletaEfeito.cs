namespace MauMau.Classes.Background.Cartas.Composicao
{
    public static class PaletaEfeito
    {
        public static Efeito GetCor(string efeito_nome)
        {
            switch (efeito_nome.ToUpper())
            {
                case "BLOQUEAR": return Efeito.Bloquear;
                case "INVERTER": return Efeito.Inverter;
                case "CORINGA": return Efeito.MudarCor;
                case "CORINGACOMPRA": return Efeito.MudarCorEComprar4;
                case "COMPRAR": return Efeito.Comprar2;
                default: return 0;
            }
        }
        public static Efeito GetCor(int cor_numero)
        {
            switch (cor_numero)
            {
                case 1: return Efeito.Bloquear;
                case 2: return Efeito.Inverter;
                case 3: return Efeito.MudarCor;
                case 4: return Efeito.MudarCorEComprar4;
                case 5: return Efeito.Comprar2;
                default: return 0;
            }
        }
    }
}
