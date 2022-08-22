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

        public void DecrementarQteDeMovimentos()
        {
            QteDeMovimentos--;
        }

        public bool PodeMover(Posicao posicao)
        {
            Peca peca = Tab.AcessarPeca(posicao);
            return ((peca == null) || (peca.CorDaPeca != CorDaPeca));
        }

        public bool PodeMoverPara(Posicao posicao)
        {
            return MovimentosPossiveis()[posicao.Linha, posicao.Coluna];
        }

        public abstract bool[,] MovimentosPossiveis();

        public bool ExisteMovimentosPossiveis()
        {
            bool[,] matriz = MovimentosPossiveis();
            for(int i = 0; i< Tab.Linhas; i++)
            {
                for (int j = 0; j < Tab.Colunas; j++)
                {
                    if(matriz[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
