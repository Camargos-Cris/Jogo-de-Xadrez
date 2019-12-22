using System;
using Xadrez.tabuleiro;
using Xadrez.jogo;
namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                Partida partida = new Partida();

                while (!partida.terminou)
                {
                    try
                    {
                        Console.Clear();
                        Tela.imprimirTabu(partida.tab);
                        Console.WriteLine();
                        Console.WriteLine("Turno:{0}", partida.turno);
                        Console.WriteLine("Aguardando movimento das peças:{0}", partida.jogadorAtual);
                        Console.Write("Digite a posição de origem: ");

                        Posicao origem = Tela.lerPosXadrez().toPosition();
                        partida.validarOrigem(origem);
                        bool[,] possiveispos = partida.tab.peca(origem).possiveisMove();
                        Console.Clear();
                        Tela.imprimirTabu(partida.tab, possiveispos);
                        Console.WriteLine();
                        Console.Write("Digite a posição de destino: ");
                        Posicao destino = Tela.lerPosXadrez().toPosition();
                        partida.validarDestino(origem, destino);
                        partida.realizaJogada(origem, destino);
                    }
                    catch (TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }

            }
            catch (TabuleiroException e)
            { Console.WriteLine(e.Message); }
            catch (FormatException e)
            { Console.WriteLine(e.Message); }
        }
    }
}
