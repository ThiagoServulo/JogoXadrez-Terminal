using tabuleiro;

namespace xadrez
{
    class Rei : Peca
    {
        private PartidaDeXadrez partida;
        public Rei(Cor corDaPeca, Tabuleiro tab, PartidaDeXadrez partidaRei) : base(corDaPeca, tab)
        {
            partida = partidaRei;
        }

        public override string ToString()
        {
            return "R";
        }

        private bool TesteTorreParaRoque(Posicao posicao)
        {
            Peca peca = Tab.AcessarPeca(posicao);
            return ((peca != null) && (peca is Torre) && (peca.CorDaPeca == CorDaPeca) && (peca.QteDeMovimentos == 0));
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[Tab.Linhas, Tab.Colunas];

            Posicao posicao = new Posicao(0, 0);

            // direção norte (acima)
            posicao.DefinirPosicao(PosicaoAtual.Linha - 1, PosicaoAtual.Coluna);
            if(Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // direção nordeste (diagonal superior direita)
            posicao.DefinirPosicao(PosicaoAtual.Linha - 1, PosicaoAtual.Coluna + 1);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // direção leste (direita)
            posicao.DefinirPosicao(PosicaoAtual.Linha, PosicaoAtual.Coluna + 1);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // direção suldeste (diagonal inferior direita)
            posicao.DefinirPosicao(PosicaoAtual.Linha - 1, PosicaoAtual.Coluna - 1);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // direção sul (abaixo)
            posicao.DefinirPosicao(PosicaoAtual.Linha + 1, PosicaoAtual.Coluna);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // direção suldoeste (diagonal inferior esquerda)
            posicao.DefinirPosicao(PosicaoAtual.Linha + 1, PosicaoAtual.Coluna - 1);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // direção oeste (esquerda)
            posicao.DefinirPosicao(PosicaoAtual.Linha, PosicaoAtual.Coluna - 1);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // direção noroeste (diagonal superior esquerda)
            posicao.DefinirPosicao(PosicaoAtual.Linha + 1, PosicaoAtual.Coluna + 1);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            
            // #jogadaespecial roque
            if((QteDeMovimentos == 0) && (!partida.Xeque))
            {
                // #jogadaespecial roque pequeno
                Posicao posTorre1 = new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna + 3);
                if(TesteTorreParaRoque(posTorre1))
                {
                    Posicao posicao1 = new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna + 1);
                    Posicao posicao2 = new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna + 2);
                    if((Tab.AcessarPeca(posicao1) == null) && ((Tab.AcessarPeca(posicao2) == null)))
                    {
                        matriz[PosicaoAtual.Linha, PosicaoAtual.Coluna + 2] = true;
                    }
                }

                // #jogadaespecial roque grande
                Posicao posTorre2 = new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna - 4);
                if (TesteTorreParaRoque(posTorre2))
                {
                    Posicao posicao1 = new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna - 1);
                    Posicao posicao2 = new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna - 2);
                    Posicao posicao3 = new Posicao(PosicaoAtual.Linha, PosicaoAtual.Coluna - 3);
                    if ((Tab.AcessarPeca(posicao1) == null) && (Tab.AcessarPeca(posicao2) == null) && (Tab.AcessarPeca(posicao3) == null))
                    {
                        matriz[PosicaoAtual.Linha, PosicaoAtual.Coluna - 2] = true;
                    }
                }
            }
            
            return matriz;
        }
    }
}
