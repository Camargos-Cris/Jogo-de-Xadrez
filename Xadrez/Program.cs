using System;
using Xadrez.tabuleiro;
namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            Posicao p = new Posicao(3, 4);
            Console.WriteLine("Posição: "+ p);
            Tabuleiro tab = new Tabuleiro(8, 8);
        }
    }
}
