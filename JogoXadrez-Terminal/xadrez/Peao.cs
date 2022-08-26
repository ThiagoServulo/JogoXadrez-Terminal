using tabuleiro;

namespace xadrez
{
    class Peao : Peca
    {
        private PartidaDeXadrez partida;
        public Peao(Cor corDaPeca, Tabuleiro tab, PartidaDeXadrez partidaDeXadrez) : base(corDaPeca, tab)
        {
            partida = partidaDeXadrez;
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

                // #jogadaespecial en passant
                if(PosicaoAtual.Linha == 3)
                {
                    Posicao esquerda = new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna - 1);
                    if(Tab.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && (Tab.AcessarPeca(esquerda) == partida.VulneravelEnPassant))
                    {
                        matriz[esquerda.Linha - 1, esquerda.Coluna] = true;
                    }

                    Posicao direita = new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna + 1);
                    if (Tab.PosicaoValida(direita) && ExisteInimigo(direita) && (Tab.AcessarPeca(direita) == partida.VulneravelEnPassant))
                    {
                        matriz[direita.Linha - 1, direita.Coluna] = true;
                    }
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

                // #jogadaespecial en passant
                if (PosicaoAtual.Linha == 4)
                {
                    Posicao esquerda = new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna - 1);
                    if (Tab.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && (Tab.AcessarPeca(esquerda) == partida.VulneravelEnPassant))
                    {
                        matriz[esquerda.Linha + 1, esquerda.Coluna] = true;
                    }

                    Posicao direita = new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna + 1);
                    if (Tab.PosicaoValida(direita) && ExisteInimigo(direita) && (Tab.AcessarPeca(direita) == partida.VulneravelEnPassant))
                    {
                        matriz[direita.Linha + 1, direita.Coluna] = true;
                    }
                }
            }

            return matriz;
        }
    }
}
