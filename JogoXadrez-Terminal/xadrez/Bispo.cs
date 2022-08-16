using tabuleiro;

namespace xadrez
{
    class Bispo : Peca
    {
        public Bispo(Cor corDaPeca, Tabuleiro tab) : base(corDaPeca, tab)
        {
        }

        public override string ToString()
        {
            return "B";
        }
    }
}
