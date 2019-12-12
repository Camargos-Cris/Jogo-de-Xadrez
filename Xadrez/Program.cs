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
                Tabuleiro t = new Tabuleiro(8, 8);
                t.insertPeca(new Torre(Cor.Branca, t), new Posicao(0, 0));
                t.insertPeca(new Torre(Cor.Branca, t), new Posicao(0, 7));
                t.insertPeca(new Torre(Cor.Preta, t), new Posicao(7, 7));
                t.insertPeca(new Torre(Cor.Preta, t), new Posicao(7, 0));
                Tela.imprimirTabu(t);
            }
            catch (TabuleiroException e)
            { Console.WriteLine(e.Message); }
        }
    }
}
