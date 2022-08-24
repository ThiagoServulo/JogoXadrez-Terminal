using tabuleiro;

namespace xadrez
{
    class Bispo : Peca
    {
        public Bispo(Cor corDaPeca, Tabuleiro tab) : base(corDaPeca, tab)
        {
        }

        public override string ToString()
        {
            return "B";
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[Tab.Linhas, Tab.Colunas];

            Posicao posicao = new Posicao(0, 0);

            // direção noroeste (diagonal superior esquerda)
            posicao.DefinirPosicao(PosicaoAtual.Linha + 1, PosicaoAtual.Coluna - 1);
            while (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if ((Tab.AcessarPeca(posicao) != null) && Tab.AcessarPeca(posicao).CorDaPeca != CorDaPeca)
                {
                    break;
                }
                posicao.DefinirPosicao(posicao.Linha + 1, posicao.Coluna - 1);
            }

            // direção nordeste (diagonal superior direita)
            posicao.DefinirPosicao(PosicaoAtual.Linha + 1, PosicaoAtual.Coluna + 1);
            while (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if ((Tab.AcessarPeca(posicao) != null) && Tab.AcessarPeca(posicao).CorDaPeca != CorDaPeca)
                {
                    break;
                }
                posicao.DefinirPosicao(posicao.Linha + 1, posicao.Coluna + 1);
            }

            // direção suldoeste (diagonal inferior esquerda)
            posicao.DefinirPosicao(PosicaoAtual.Linha - 1, PosicaoAtual.Coluna + 1);
            while (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if ((Tab.AcessarPeca(posicao) != null) && Tab.AcessarPeca(posicao).CorDaPeca != CorDaPeca)
                {
                    break;
                }
                posicao.DefinirPosicao(posicao.Linha - 1, posicao.Coluna + 1);
            }

            // direção suldeste (diagonal inferior direita)
            posicao.DefinirPosicao(PosicaoAtual.Linha - 1, PosicaoAtual.Coluna - 1);
            while (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if ((Tab.AcessarPeca(posicao) != null) && Tab.AcessarPeca(posicao).CorDaPeca != CorDaPeca)
                {
                    break;
                }
                posicao.DefinirPosicao(posicao.Linha - 1, posicao.Coluna - 1);
            }

            return matriz;
        }
    }
}
