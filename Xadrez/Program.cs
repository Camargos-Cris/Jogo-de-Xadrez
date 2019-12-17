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
                    Console.Clear();
                    Tela.imprimirTabu(partida.tab);
                    Console.WriteLine();
                    Console.Write("Digite a posição de origem: ");
                    Posicao origem = Tela.lerPosXadrez().toPosition();
                    bool[,] possiveispos = partida.tab.peca(origem).possiveisMove();
                    Console.Clear();
                    Tela.imprimirTabu(partida.tab,possiveispos);
                    Console.WriteLine();
                    Console.Write("Digite a posição de destino: ");
                    Posicao destino = Tela.lerPosXadrez().toPosition();
                    partida.executarMove(origem, destino);
                }
                Tela.imprimirTabu(partida.tab);
            }
            catch (TabuleiroException e)
            { Console.WriteLine(e.Message); }
            catch (FormatException e)
            { Console.WriteLine(e.Message); }
        }
    }
}
