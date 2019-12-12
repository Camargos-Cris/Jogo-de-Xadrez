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
                Console.Write(t.linhas - i + " ");
                for (int j = 0; j < t.colunas; j++)
                {

                    if (t.peca(i, j) == null)
                        Console.Write("- ");
                    else
                    {
                        imprimePeca(t.peca(i, j));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h ");
        }
        public static void imprimePeca(Peca pec)
        {
            if (pec.cor == Cor.Branca)
            { Console.Write(pec); }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(pec);
                Console.ForegroundColor = aux;

            }
        }
    }
}
