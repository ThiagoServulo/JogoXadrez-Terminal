namespace tabuleiro
{
    abstract class Peca
    {
        public Posicao PosicaoAtual { get; set; }
        public Cor CorDaPeca { get; protected set; }
        public int QteDeMovimentos { get; protected set; }
        public Tabuleiro Tab { get; protected set; }

        public Peca(Cor corDaPeca, Tabuleiro tab)
        {
            CorDaPeca = corDaPeca;
            Tab = tab;
            PosicaoAtual = null;
            QteDeMovimentos = 0;
        }

        public void IncrementarQteDeMovimentos()
        {
            QteDeMovimentos++;
        }

        public bool PodeMover(Posicao pos)
        {
            Peca peca = Tab.AcessarPeca(pos);
            return ((peca == null) || (peca.CorDaPeca != CorDaPeca));
        }

        public abstract bool[,] MovimentosPossiveis();
    }
}
