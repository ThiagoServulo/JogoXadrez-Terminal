using System;
using System.Collections.Generic;
using tabuleiro;
using xadrez;

namespace JogoXadrez_Terminal
{
    class Tela
    {
        public static void ImprimirPartida(PartidaDeXadrez partida)
        {
            Console.Clear();
            ImprimirTabuleiro(partida.Tab);
            Console.WriteLine();
            ImprimirPecasCapturadas(partida);
            Console.WriteLine();
            Console.WriteLine($"\nTurno: {partida.Turno}");
            Console.WriteLine($"Aguardando jogada: {partida.JogadorAtual}");
            Console.WriteLine();
            if(partida.Xeque)
            {
                Console.WriteLine("Você está em xeque!");
            }
            Console.WriteLine();
        }

        public static void ImprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            Console.WriteLine("Peças capturadas:");
            Console.Write("Brancas: ");
            ImprimirConjunto(partida.PecasCapturadas(Cor.Branco));
            Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            ImprimirConjunto(partida.PecasCapturadas(Cor.Preto));
            Console.ForegroundColor = aux;
        }

        public static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("{");
            foreach(Peca peca in conjunto)
            {
                Console.Write(peca + " ");
            }
            Console.Write("} ");
        }

        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for(int i = 0; i < tab.Linhas; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    ImprimirPeca(tab.AcessarPeca(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void ImprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if(posicoesPossiveis[i, j])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    ImprimirPeca(tab.AcessarPeca(i, j));
                }
                Console.BackgroundColor = fundoOriginal;
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void ImprimirPeca(Peca peca)
        {
            if(peca == null)
            {
                Console.Write("-");
            }
            else
            {
                if(peca.CorDaPeca == Cor.Branco)
                {
                    Console.Write($"{peca}");
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"{peca}");
                    Console.ForegroundColor = aux;
                }
            }
            Console.Write(" ");
        }

        public static PosicaoXadrez LerPosicao()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }
    }
}
