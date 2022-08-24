using tabuleiro;

namespace xadrez
{
    class Peao : Peca
    {
        public Peao(Cor corDaPeca, Tabuleiro tab) : base(corDaPeca, tab)
        {
        }

        public override string ToString()
        {
            return "P";
        }

        private bool ExisteInimigo(Posicao posicao)
        {
            Peca peca = Tab.AcessarPeca(posicao);
            return ((peca != null) && (peca.CorDaPeca != CorDaPeca));
        }

        private bool Livre(Posicao posicao)
        {
            return (Tab.AcessarPeca(posicao) == null);
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[Tab.Linhas, Tab.Colunas];

            Posicao posicao = new Posicao(0, 0);

            if(CorDaPeca == Cor.Branco)
            {
                posicao.DefinirPosicao(PosicaoAtual.Linha - 1, PosicaoAtual.Coluna);
                if(Tab.PosicaoValida(posicao) && Livre(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirPosicao(PosicaoAtual.Linha - 2, PosicaoAtual.Coluna);
                if (Tab.PosicaoValida(posicao) && Livre(posicao) && QteDeMovimentos == 0)
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirPosicao(PosicaoAtual.Linha - 1, PosicaoAtual.Coluna - 1);
                if (Tab.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirPosicao(PosicaoAtual.Linha - 1, PosicaoAtual.Coluna + 1);
                if (Tab.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }
            }
            else
            {
                posicao.DefinirPosicao(PosicaoAtual.Linha + 1, PosicaoAtual.Coluna);
                if (Tab.PosicaoValida(posicao) && Livre(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirPosicao(PosicaoAtual.Linha + 2, PosicaoAtual.Coluna);
                if (Tab.PosicaoValida(posicao) && Livre(posicao) && QteDeMovimentos == 0)
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirPosicao(PosicaoAtual.Linha + 1, PosicaoAtual.Coluna + 1);
                if (Tab.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirPosicao(PosicaoAtual.Linha + 1, PosicaoAtual.Coluna - 1);
                if (Tab.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }
            }

            return matriz;
        }
    }
}
