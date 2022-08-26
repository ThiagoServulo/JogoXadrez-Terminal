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
        public Peca VulneravelEnPassant { get; private set; }
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
            VulneravelEnPassant = null;
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
            if(!Tab.AcessarPeca(origem).MovimentoPossivel(destino))
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

            // #jogadaespecial roque pequeno
            if((peca is Rei) && (destino.Coluna == origem.Coluna + 2))
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca torre = Tab.RetirarPeca(origemTorre);
                torre.IncrementarQteDeMovimentos();
                Tab.ColocarPeca(torre, destinoTorre);
            }

            // #jogadaespecial roque grande
            if ((peca is Rei) && (destino.Coluna == origem.Coluna - 2))
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna -  1);
                Peca torre = Tab.RetirarPeca(origemTorre);
                torre.IncrementarQteDeMovimentos();
                Tab.ColocarPeca(torre, destinoTorre);
            }

            // #jogadaespecial en passant
            if (peca is Peao)
            {
                if((origem.Coluna != destino.Coluna) && pecaCapturada == null)
                {
                    Posicao posicaoPeao;
                    if(peca.CorDaPeca == Cor.Branco)
                    {
                        posicaoPeao = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posicaoPeao = new Posicao(destino.Linha - 1, destino.Coluna);
                    }
                    pecaCapturada = Tab.RetirarPeca(posicaoPeao);
                    pecasCapturadas.Add(pecaCapturada);
                }
            }

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
            
            // #jogadaespecial roque pequeno
            if ((peca is Rei) && (destino.Coluna == origem.Coluna + 2))
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca torre = Tab.RetirarPeca(destinoTorre);
                torre.DecrementarQteDeMovimentos();
                Tab.ColocarPeca(torre, origemTorre);
            }

            // #jogadaespecial roque grande
            if ((peca is Rei) && (destino.Coluna == origem.Coluna - 2))
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca torre = Tab.RetirarPeca(destinoTorre);
                torre.DecrementarQteDeMovimentos();
                Tab.ColocarPeca(torre, origemTorre);
            }

            // #jogadaespecial en passant
            if (peca is Peao)
            {
                if ((origem.Coluna != destino.Coluna) && pecaCapturada == VulneravelEnPassant)
                {
                    Peca peao = Tab.RetirarPeca(destino);
                    Posicao posicaoPeao;
                    if(peao.CorDaPeca == Cor.Branco)
                    {
                        posicaoPeao = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        posicaoPeao = new Posicao(4, destino.Coluna);
                    }

                    peao.DecrementarQteDeMovimentos();
                    Tab.ColocarPeca(peao, posicaoPeao);
                }
            }
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if(EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque");
            }

            // #jogadaespecial promocao
            Peca peca = Tab.AcessarPeca(destino);
            if(peca is Peao)
            {
                if(((peca.CorDaPeca == Cor.Branco) && (destino.Linha == 0)) || ((peca.CorDaPeca == Cor.Preto) && (destino.Linha == 7)))
                {
                    peca = Tab.RetirarPeca(destino);
                    pecas.Remove(peca);
                    Peca rainha = new Rainha(peca.CorDaPeca, Tab);
                    Tab.ColocarPeca(rainha, destino);
                    pecas.Add(rainha);
                }
            }

            Xeque = EstaEmXeque(Adversaria(JogadorAtual));

            if(XequeMate(Adversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            }

            // #jogadaespecial en passant
            if (peca is Peao && ((destino.Linha == origem.Linha + 2) || (destino.Linha == origem.Linha - 2)))
            {
                VulneravelEnPassant = peca;
            }
            else
            {
                VulneravelEnPassant = null;
            }
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
            ColocarNovaPeca('a', 7, new Peao(Cor.Preto, Tab, this));
            ColocarNovaPeca('b', 7, new Peao(Cor.Preto, Tab, this));
            ColocarNovaPeca('c', 7, new Peao(Cor.Preto, Tab, this));
            ColocarNovaPeca('d', 7, new Peao(Cor.Preto, Tab, this));
            ColocarNovaPeca('e', 7, new Peao(Cor.Preto, Tab, this));
            ColocarNovaPeca('f', 7, new Peao(Cor.Preto, Tab, this));
            ColocarNovaPeca('g', 7, new Peao(Cor.Preto, Tab, this));
            ColocarNovaPeca('h', 7, new Peao(Cor.Preto, Tab, this));
            
            ColocarNovaPeca('a', 8, new Torre(Cor.Preto, Tab));
            ColocarNovaPeca('b', 8, new Cavalo(Cor.Preto, Tab));
            ColocarNovaPeca('c', 8, new Bispo(Cor.Preto, Tab));
            ColocarNovaPeca('d', 8, new Rainha(Cor.Preto, Tab));
            ColocarNovaPeca('e', 8, new Rei(Cor.Preto, Tab, this));
            ColocarNovaPeca('f', 8, new Bispo(Cor.Preto, Tab));
            ColocarNovaPeca('g', 8, new Cavalo(Cor.Preto, Tab));
            ColocarNovaPeca('h', 8, new Torre(Cor.Preto, Tab));
            
            ColocarNovaPeca('a', 2, new Peao(Cor.Branco, Tab, this));
            ColocarNovaPeca('b', 2, new Peao(Cor.Branco, Tab, this));
            ColocarNovaPeca('c', 2, new Peao(Cor.Branco, Tab, this));
            ColocarNovaPeca('d', 2, new Peao(Cor.Branco, Tab, this));
            ColocarNovaPeca('e', 2, new Peao(Cor.Branco, Tab, this));
            ColocarNovaPeca('f', 2, new Peao(Cor.Branco, Tab, this));
            ColocarNovaPeca('g', 2, new Peao(Cor.Branco, Tab, this));
            ColocarNovaPeca('h', 2, new Peao(Cor.Branco, Tab, this));
            
            ColocarNovaPeca('a', 1, new Torre(Cor.Branco, Tab));
            ColocarNovaPeca('b', 1, new Cavalo(Cor.Branco, Tab));
            ColocarNovaPeca('c', 1, new Bispo(Cor.Branco, Tab));
            ColocarNovaPeca('d', 1, new Rainha(Cor.Branco, Tab));
            ColocarNovaPeca('e', 1, new Rei(Cor.Branco, Tab, this));
            ColocarNovaPeca('f', 1, new Bispo(Cor.Branco, Tab));
            ColocarNovaPeca('g', 1, new Cavalo(Cor.Branco, Tab));
            ColocarNovaPeca('h', 1, new Torre(Cor.Branco, Tab));
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
