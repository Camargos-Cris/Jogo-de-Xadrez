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
        public Peca peca(int i, int j)
        {
            return pecas[i, j];
        }
        public Peca peca(Posicao pos)
        {
            return pecas[pos.linha, pos.coluna];
        }
        public void insertPeca(Peca p, Posicao pos)
        {
            if (existPeca(pos))
            {
                throw new TabuleiroException("Já existe uma peça nesta posição");
            }
            else
            {
                pecas[pos.linha, pos.coluna] = p;
                p.posicao = pos;
            }
        }
        public Peca removePeca(Posicao pos)
        {
            if (peca(pos) == null)
            { return null; }
            else
            {
                Peca aux = peca(pos);
                aux.posicao = null;
                pecas[pos.linha, pos.coluna] = null;
                return aux;
            }
        }
        public bool posicaoValida(Posicao pos)
        {
            if (pos.linha < 0 || pos.linha >= linhas || pos.coluna < 0 || pos.coluna >= colunas)
            {
                return false;
            }
            return true;
        }
        public void validarPos(Posicao pos)
        {
            if (!posicaoValida(pos))
                throw new TabuleiroException("Posição Invalida");
        }
        public bool existPeca(Posicao pos)
        {
            validarPos(pos);
            return peca(pos) != null;
            ;
        }
    }
}
