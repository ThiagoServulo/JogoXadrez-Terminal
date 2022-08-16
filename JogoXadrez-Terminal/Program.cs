using System;
using tabuleiro;
using xadrez;

namespace JogoXadrez_Terminal
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tab = new Tabuleiro(8, 8);
            tab.ColocarPeca(new Torre(Cor.Preto, tab), new Posicao(0, 0));
            tab.ColocarPeca(new Rei(Cor.Branco, tab), new Posicao(5, 5));
            Tela.ImprimirTabuleiro(tab);
        }
    }
}
