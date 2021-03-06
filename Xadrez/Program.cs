﻿using System;
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
                        Tela.imprimirPartida(partida);
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
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine("Valores de posição inválido");
                        Console.ReadLine();
                    }
                    catch(FormatException)
                    {
                        Console.WriteLine("Formato de dado inválido");
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Tela.imprimirPartida(partida);


            }
            catch (TabuleiroException e)
            { Console.WriteLine(e.Message); }
            catch (FormatException e)
            { Console.WriteLine(e.Message); }
        }
    }
}
