namespace MauMau.Classes.Background.Cartas.Composicao
{
    /// <summary>
    /// Auxilia na busca de um efeito
    /// </summary>
    public static class PaletaEfeito
    {
        /// <summary>
        /// Procura um efeito pelo seu nome
        /// </summary>
        /// <param name="efeito_nome"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Procura um efeito pelo seu número identificador
        /// </summary>
        /// <param name="cor_numero"></param>
        /// <returns></returns>
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
