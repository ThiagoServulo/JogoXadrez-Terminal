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
        public bool Xeque { get; private set; }
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
            Xeque = false;
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

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = Tab.RetirarPeca(origem);
            peca.IncrementarQteDeMovimentos();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            if(pecaCapturada != null)
            {
                pecasCapturadas.Add(pecaCapturada);
            }
            Tab.ColocarPeca(peca, destino);

            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca peca = Tab.RetirarPeca(destino);
            peca.DecrementarQteDeMovimentos();
            if(pecaCapturada != null)
            {
                Tab.ColocarPeca(pecaCapturada, destino);
                pecasCapturadas.Remove(pecaCapturada);
            }
            Tab.ColocarPeca(peca, origem);
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if(EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque");
            }

            Xeque = EstaEmXeque(Adversaria(JogadorAtual));

            Terminada = XequeMate(Adversaria(JogadorAtual));

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
            ColocarNovaPeca('b', 3, new Torre(Cor.Preto, Tab));
            ColocarNovaPeca('h', 7, new Torre(Cor.Preto, Tab));
            ColocarNovaPeca('c', 8, new Torre(Cor.Branco, Tab));
            ColocarNovaPeca('a', 8, new Rei(Cor.Branco, Tab));
            ColocarNovaPeca('f', 1, new Rei(Cor.Preto, Tab));
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

        private Cor Adversaria(Cor cor)
        {
            return (cor == Cor.Branco) ? Cor.Preto : Cor.Branco;
        }

        private Peca PecaRei(Cor cor)
        {
            foreach(Peca peca in PecasEmJogo(cor))
            {
                if(peca is Rei)
                {
                    return peca;
                }
            }
            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca rei = PecaRei(cor);

            if(rei == null)
            {
                throw new TabuleiroException($"Não tem rei da cor: {cor} no tabuleiro");
            }

            foreach (Peca peca in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] matriz = peca.MovimentosPossiveis();
                if(matriz[rei.PosicaoAtual.Linha, rei.PosicaoAtual.Coluna])
                {
                    return true;
                }
            }

            return false;
        }

        public bool XequeMate(Cor cor)
        {
            if(!EstaEmXeque(cor))
            {
                return false;
            }

            foreach(Peca peca in PecasEmJogo(cor))
            {
                bool[,] matriz = peca.MovimentosPossiveis();
                for(int i = 0; i < Tab.Linhas; i++)
                {
                    for (int j = 0; j < Tab.Colunas; j++)
                    {
                        if(matriz[i, j])
                        {
                            Posicao origem = peca.PosicaoAtual;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool xeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if(!xeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }
    }
}
