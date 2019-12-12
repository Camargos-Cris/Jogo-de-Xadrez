using System;
using System.Collections.Generic;
using System.Text;

namespace Xadrez.tabuleiro
{
    class Tabuleiro
    {
        public int linhas { get; set; }
        public int colunas { get; set; }
        private Peca[,] pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            this.linhas = linhas;
            this.colunas = colunas;
            pecas = new Peca[linhas, colunas];
        }
        public Peca peca(int i,int j)
        {
            return pecas[i, j];
        }
        public void insertPeca(Peca p,Posicao pos)
        {
            pecas[pos.linha, pos.coluna] = p;
            p.posicao = pos;
        }
    }
}
