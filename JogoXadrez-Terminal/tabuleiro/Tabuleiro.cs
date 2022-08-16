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
            pecas[pos.Linha, pos.Coluna] = peca;
            peca.Pos = pos;
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
