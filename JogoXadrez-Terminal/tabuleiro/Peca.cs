namespace tabuleiro
{
    class Peca
    {
        public Posicao Pos { get; set; }
        public Cor CorDaPeca { get; protected set; }
        public int QteDeMovimentos { get; protected set; }
        public Tabuleiro Tab { get; protected set; }

        public Peca(Posicao pos, Cor corDaPeca, Tabuleiro tab)
        {
            Pos = pos;
            CorDaPeca = corDaPeca;
            Tab = tab;
            QteDeMovimentos = 0;
        }
    }
}
