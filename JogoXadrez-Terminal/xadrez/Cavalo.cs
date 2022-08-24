using tabuleiro;

namespace xadrez
{
    class Cavalo : Peca
    {
        public Cavalo(Cor corDaPeca, Tabuleiro tab) : base(corDaPeca, tab)
        {
        }

        public override string ToString()
        {
            return "C";
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[Tab.Linhas, Tab.Colunas];

            Posicao posicao = new Posicao(0, 0);

            posicao.DefinirPosicao(PosicaoAtual.Linha - 1, PosicaoAtual.Coluna - 2);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.DefinirPosicao(PosicaoAtual.Linha - 2, PosicaoAtual.Coluna - 1);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.DefinirPosicao(PosicaoAtual.Linha - 2, PosicaoAtual.Coluna + 1);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.DefinirPosicao(PosicaoAtual.Linha - 1, PosicaoAtual.Coluna + 2);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.DefinirPosicao(PosicaoAtual.Linha + 1, PosicaoAtual.Coluna + 2);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.DefinirPosicao(PosicaoAtual.Linha + 2, PosicaoAtual.Coluna + 1);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.DefinirPosicao(PosicaoAtual.Linha + 2, PosicaoAtual.Coluna - 1);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.DefinirPosicao(PosicaoAtual.Linha + 1, PosicaoAtual.Coluna - 2);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            return matriz;
        }
    }
}
