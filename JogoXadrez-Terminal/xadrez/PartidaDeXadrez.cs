using System;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            Terminada = false;
            ColocarPecas();
        }

        public void ValidarPosicaoDeOrigem(Posicao origem)
        {
            if(Tab.AcessarPeca(origem) == null)
            {
                throw new TabuleiroException("Não existe peça nessa posição!");
            }

            if(JogadorAtual != Tab.AcessarPeca(origem).CorDaPeca)
            {
                throw new TabuleiroException("A peça não pertence a esse jogador!");
            }

            if(!Tab.AcessarPeca(origem).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Essa peça não possui movimentos possíveis!");
            }
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if(!Tab.AcessarPeca(origem).PodeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = Tab.RetirarPeca(origem);
            peca.IncrementarQteDeMovimentos();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(peca, destino);
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);
            Turno++;
            MudaJogador();
        }

        private void MudaJogador()
        {
            JogadorAtual = (JogadorAtual == Cor.Branco) ? Cor.Preto : Cor.Branco;
        }

        private void ColocarPecas()
        {
            Tab.ColocarPeca(new Torre(Cor.Preto, Tab), new PosicaoXadrez('c', 1).ToPosicao());
            Tab.ColocarPeca(new Rei(Cor.Branco, Tab), new PosicaoXadrez('c', 2).ToPosicao());
        }
    }
}
