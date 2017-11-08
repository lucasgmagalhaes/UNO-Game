namespace MauMau.Classes.Background.Estruturas
{
    class Dado
    {
        private object info;
        private int index;

        public object Info { get { return this.info; } set { this.info = value; } }
        public int Index { get { return this.index; } set { this.index = value; } }

        public Dado(object info, int index)
        {
            this.info = info;
            this.index = index;
        }
    }
}
