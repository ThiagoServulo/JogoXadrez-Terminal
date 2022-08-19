using System.Collections.Generic;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> pecasCapturadas;

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            Terminada = false;
            pecas = new HashSet<Peca>();
            pecasCapturadas = new HashSet<Peca>();
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
            if(pecaCapturada != null)
            {
                pecasCapturadas.Add(pecaCapturada);
            }
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

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('c', 1, new Torre(Cor.Preto, Tab));
            ColocarNovaPeca('c', 2, new Rei(Cor.Branco, Tab));
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca peca in pecasCapturadas)
            {
                if(peca.CorDaPeca == cor)
                {
                    aux.Add(peca);
                }
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca peca in pecas)
            {
                if (peca.CorDaPeca == cor)
                {
                    aux.Add(peca);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }
    }
}
