using tabuleiro;

namespace xadrez
{
    class Rei : Peca
    {
        public Rei(Cor corDaPeca, Tabuleiro tab) : base(corDaPeca, tab)
        {
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
