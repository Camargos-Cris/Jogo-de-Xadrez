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
                Tabuleiro tab = new Tabuleiro(8, 8);
                tab.insertPeca(new Torre(Cor.Preta, tab), new Posicao(0, 0));
                tab.insertPeca(new Torre(Cor.Preta, tab), new Posicao(0, 7));
                tab.insertPeca(new Torre(Cor.Branca, tab), new Posicao(7, 0));
                tab.insertPeca(new Torre(Cor.Branca, tab), new Posicao(7, 7));
                tab.insertPeca(new Rei(Cor.Branca, tab), new Posicao(7, 8));
                tab.insertPeca(new Rei(Cor.Preta, tab), new Posicao(0, 4));
                Tela.imprimirTabu(tab);
            }
            catch(TabuleiroException e)
            { Console.WriteLine(e.Message); }
        }
    }
}
