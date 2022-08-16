namespace tabuleiro
{
    class Peca
    {
        public Posicao Pos { get; set; }
        public Cor CorDaPeca { get; protected set; }
        public int QteDeMovimentos { get; protected set; }
        public Tabuleiro Tab { get; protected set; }

        public Peca(Cor corDaPeca, Tabuleiro tab)
        {
            CorDaPeca = corDaPeca;
            Tab = tab;
            Pos = null;
            QteDeMovimentos = 0;
        }
    }
}
