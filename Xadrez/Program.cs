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
                PosXadrez pos = new PosXadrez('c', 7);
                Console.WriteLine(pos);
                Console.WriteLine(pos.toPosition());
            }
            catch (TabuleiroException e)
            { Console.WriteLine(e.Message); }
        }
    }
}
