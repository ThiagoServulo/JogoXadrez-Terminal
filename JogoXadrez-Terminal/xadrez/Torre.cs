using tabuleiro;

namespace xadrez
{
    class Torre : Peca
    {
        public Torre(Cor corDaPeca, Tabuleiro tab) : base(corDaPeca, tab)
        {
        }

        public override string ToString()
        {
            return "T";
        }
    }
}
