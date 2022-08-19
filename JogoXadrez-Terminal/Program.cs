using System;
using tabuleiro;
using xadrez;

namespace JogoXadrez_Terminal
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaDeXadrez partida = new PartidaDeXadrez();
                Tela.ImprimirTabuleiro(partida.Tab);
                while(!partida.Terminada)
                {
                    try
                    {
                        Tela.ImprimirPartida(partida);

                        Console.Write("Origem: ");
                        Posicao origem = Tela.LerPosicao().ToPosicao();
                        partida.ValidarPosicaoDeOrigem(origem);

                        bool[,] posicoesPossiveis = partida.Tab.AcessarPeca(origem).MovimentosPossiveis();

                        Console.Clear();
                        Tela.ImprimirTabuleiro(partida.Tab, posicoesPossiveis);

                        Console.Write("\nDestino: ");
                        Posicao destino = Tela.LerPosicao().ToPosicao();
                        partida.ValidarPosicaoDeDestino(origem, destino);

                        partida.RealizaJogada(origem, destino);
                    }
                    catch (TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
            }
            catch(TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
