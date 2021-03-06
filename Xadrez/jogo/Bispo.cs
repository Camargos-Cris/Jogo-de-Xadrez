﻿using System;
using System.Collections.Generic;
using System.Text;
using Xadrez.tabuleiro;

namespace Xadrez.jogo
{
    class Bispo : Peca
    {
        public Bispo(Cor cor, Tabuleiro tab) : base(cor, tab) { }
        public override string ToString()
        {
            return "B";
        }
        private bool movimentoLivre(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p == null || p.cor != this.cor;
        }
        public override bool[,] possiveisMove()
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas];
            Posicao pos = new Posicao(0, 0);
            pos.defineValor(posicao.linha - 1, posicao.coluna - 1);
            verificarVizinhoL(mat, pos, 1);
            pos.defineValor(posicao.linha + 1, posicao.coluna + 1);
            verificarVizinhoL(mat, pos, -1);
            pos.defineValor(posicao.linha - 1, posicao.coluna + 1);
            verificarVizinhoO(mat, pos, 1);
            pos.defineValor(posicao.linha + 1, posicao.coluna - 1);
            verificarVizinhoO(mat, pos, -1);
            return mat;
        }
        private void verificarVizinhoL(bool[,] mat, Posicao pos, int i)
        {
            while (tab.posicaoValida(pos) && movimentoLivre(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor)
                { break; }
                pos.defineValor(pos.linha - i, pos.coluna - i);
            }
        }
        private void verificarVizinhoO(bool[,] mat, Posicao pos, int i)
        {
            while (tab.posicaoValida(pos) && movimentoLivre(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor)
                { break; }
                pos.defineValor(pos.linha - i, pos.coluna + i);
            }
        }
    }
}
