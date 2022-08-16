using System;
using tabuleiro;

namespace JogoXadrez_Terminal
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for(int i = 0; i < tab.Linhas; i++)
            {
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if(tab.AcessarPeca(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write($"{tab.AcessarPeca(i, j)} ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
