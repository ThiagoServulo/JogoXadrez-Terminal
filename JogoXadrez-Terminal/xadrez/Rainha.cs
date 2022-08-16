using tabuleiro;

namespace xadrez
{
    class Rainha : Peca
    {
        public Rainha(Cor corDaPeca, Tabuleiro tab) : base(corDaPeca, tab)
        {
        }

        public override string ToString()
        {
            return "D";
        }
    }
}
