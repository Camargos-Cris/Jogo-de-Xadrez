using System;
using System.Collections.Generic;
using System.Text;
using Xadrez.tabuleiro;

namespace Xadrez
{
    class Tela
    {
        public static void imprimirTabu(Tabuleiro t)
        {
            for (int i = 0; i < t.linhas; i++)
            {
                for (int j = 0; j < t.colunas; j++)
                {
                    if (t.peca(i, j) == null)
                        Console.Write(" - ");
                    else
                        Console.Write(t.peca(i, j) + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
