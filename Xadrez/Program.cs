using System;
using Xadrez.tabuleiro;
namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tab = new Tabuleiro(8, 8);
            Tela.imprimirTabu(tab);
        }
    }
}
