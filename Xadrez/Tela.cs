using System;
using System.Collections.Generic;
using System.Text;
using Xadrez.tabuleiro;
using Xadrez.jogo;

namespace Xadrez
{
    class Tela
    {
        public static void imprimirPartida(Partida partida)
        {
            Tela.imprimirTabu(partida.tab);
            Console.WriteLine();
            imprimePecasCap(partida);
            Console.WriteLine("Turno:{0}", partida.turno);
            Console.WriteLine("Aguardando movimento das peças:{0}", partida.jogadorAtual);
        }

        private static void imprimePecasCap(Partida partida)
        {
            Console.Write("Peças Capturadas:"+ "\n" +"Brancas:");
            imprimirConjunto(partida.pecasCapturadas(Cor.Branca));
            Console.Write("Pretas:");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            imprimirConjunto(partida.pecasCapturadas(Cor.Preta));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void imprimirConjunto(HashSet<Peca> Conjunto)
        {
            Console.Write("[");
            foreach (Peca item in Conjunto)
            {
                Console.Write(item+" ");
            }
            Console.WriteLine("]");
        }

        public static void imprimirTabu(Tabuleiro t)
        {
            for (int i = 0; i < t.linhas; i++)
            {
                Console.Write(t.linhas - i + " ");
                for (int j = 0; j < t.colunas; j++)
                {

                    imprimePeca(t.peca(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h ");
        }
        public static void imprimirTabu(Tabuleiro t,bool[,]possiveispos)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlter = ConsoleColor.DarkGray;
            for (int i = 0; i < t.linhas; i++)
            {
                Console.Write(t.linhas - i + " ");
                for (int j = 0; j < t.colunas; j++)
                {
                    if(possiveispos[i,j])
                    { Console.BackgroundColor = fundoAlter; }
                    else
                    { Console.BackgroundColor = fundoOriginal; }
                    imprimePeca(t.peca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h ");
            Console.BackgroundColor = fundoOriginal;
        }
        public static void imprimePeca(Peca pec)
        {
            if (pec == null)
            { Console.Write("- "); }
            else
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
                Console.Write(" ");
            }
        }
        public static PosXadrez lerPosXadrez()
        {
            string txt = Console.ReadLine();
            char coluna = txt[0];
            int linha = int.Parse(txt[1] + "");
            return new PosXadrez(coluna, linha);
        }
    }
}
