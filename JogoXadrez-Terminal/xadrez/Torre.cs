using tabuleiro;

namespace xadrez
{
    class Torre : Peca
    {
        public Torre(Cor corDaPeca, Tabuleiro tab) : base(corDaPeca, tab)
        {
        }

        public override string ToString()
        {
            return "T";
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[Tab.Linhas, Tab.Colunas];

            Posicao posicao = new Posicao(0, 0);

            // direção norte (acima)
            posicao.DefinirPosicao(PosicaoAtual.Linha - 1, PosicaoAtual.Coluna);
            while (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if((Tab.AcessarPeca(posicao) != null) && Tab.AcessarPeca(posicao).CorDaPeca != CorDaPeca)
                {
                    break;
                }
                posicao.Linha -= 1;
            }

            // direção sul (abaixo)
            posicao.DefinirPosicao(PosicaoAtual.Linha + 1, PosicaoAtual.Coluna);
            while (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if ((Tab.AcessarPeca(posicao) != null) && Tab.AcessarPeca(posicao).CorDaPeca != CorDaPeca)
                {
                    break;
                }
                posicao.Linha += 1;
            }

            // direção leste (direita)
            posicao.DefinirPosicao(PosicaoAtual.Linha, PosicaoAtual.Coluna + 1);
            while (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if ((Tab.AcessarPeca(posicao) != null) && Tab.AcessarPeca(posicao).CorDaPeca != CorDaPeca)
                {
                    break;
                }
                posicao.Coluna += 1;
            }

            // direção oeste (esquerda)
            posicao.DefinirPosicao(PosicaoAtual.Linha, PosicaoAtual.Coluna - 1);
            while (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if ((Tab.AcessarPeca(posicao) != null) && Tab.AcessarPeca(posicao).CorDaPeca != CorDaPeca)
                {
                    break;
                }
                posicao.Coluna -= 1;
            }

            return matriz;
        }
    }
}
