namespace tabuleiro
{
    class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            pecas = new Peca[linhas, colunas];
        }

        public Peca AcessarPeca(int linha, int coluna)
        {
            return pecas[linha, coluna];
        }

        public Peca AcessarPeca(Posicao pos)
        {
            return pecas[pos.Linha, pos.Coluna];
        }

        public void ColocarPeca(Peca peca, Posicao pos)
        {
            if(ExistePeca(pos))
            {
                throw new TabuleiroException("Já existe uma peça nessa posição!");
            }
            pecas[pos.Linha, pos.Coluna] = peca;
            peca.PosicaoAtual = pos;
        }

        public Peca RetirarPeca(Posicao pos)
        {
            if(!ExistePeca(pos))
            {
                return null;
            }
            Peca aux = AcessarPeca(pos);
            aux.PosicaoAtual = null;
            pecas[pos.Linha, pos.Coluna] = null;
            return aux;
        }

        public bool ExistePeca(Posicao pos)
        {
            ValidarPosicao(pos);
            return AcessarPeca(pos) != null;
        }

        public bool PosicaoValida(Posicao pos)
        {
            if(pos.Linha < 0 || pos.Linha >= Linhas || pos.Coluna < 0 || pos.Coluna >= Colunas)
            {
                return false;
            }
            return true;
        }

        public void ValidarPosicao(Posicao pos)
        {
            if(!PosicaoValida(pos))
            {
                throw new TabuleiroException("Posição Inválida!");
            }
        }
    }
}
