using System;
using System.Collections.Generic;
using System.Text;
using Xadrez.tabuleiro;

namespace Xadrez.jogo
{
    class Rei : Peca
    {
        private Partida partida;
        public Rei(Cor cor, Tabuleiro tab,Partida part) : base(cor, tab) { this.partida = part; }
        public override string ToString()
        {
            return "R";
        }
        private bool movimentoLivre(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p == null || p.cor != this.cor;
        }
        private bool testeTorreRoque(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p != null && p is Torre && p.cor == cor && p.quantmovi == 0;
        }
        public override bool[,] possiveisMove()
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas];
            Posicao pos = new Posicao(0, 0);
            pos.defineValor(posicao.linha - 1, posicao.coluna);
            verificarVizinho(mat, pos);
            pos.defineValor(posicao.linha - 1, posicao.coluna + 1);
            verificarVizinho(mat, pos);
            pos.defineValor(posicao.linha, posicao.coluna + 1);
            verificarVizinho(mat, pos);
            pos.defineValor(posicao.linha + 1, posicao.coluna + 1);
            verificarVizinho(mat, pos);
            pos.defineValor(posicao.linha + 1, posicao.coluna);
            verificarVizinho(mat, pos);
            pos.defineValor(posicao.linha + 1, posicao.coluna - 1);
            verificarVizinho(mat, pos);
            pos.defineValor(posicao.linha, posicao.coluna - 1);
            verificarVizinho(mat, pos);
            pos.defineValor(posicao.linha - 1, posicao.coluna - 1);
            verificarVizinho(mat, pos);
            //// #jogadaespecial roque
            if (quantmovi == 0 && !partida.xeque)
            {
                // #jogadaespecial roque pequeno
                Posicao posT1 = new Posicao(posicao.linha, posicao.coluna + 3);
                if (testeTorreRoque(posT1))
                {
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna + 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna + 2);
                    if (tab.peca(p1) == null && tab.peca(p2) == null)
                    {
                        mat[posicao.linha, posicao.coluna + 2] = true;
                    }
                }
                // #jogadaespecial roque grande
                Posicao posT2 = new Posicao(posicao.linha, posicao.coluna - 4);
                if (testeTorreRoque(posT2))
                {
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna - 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna - 2);
                    Posicao p3 = new Posicao(posicao.linha, posicao.coluna - 3);
                    if (tab.peca(p1) == null && tab.peca(p2) == null && tab.peca(p3) == null)
                    {
                        mat[posicao.linha, posicao.coluna - 2] = true;
                    }
                }
            }

            return mat;
        }
        private void verificarVizinho(bool[,] mat, Posicao pos)
        {
            if (tab.posicaoValida(pos) && movimentoLivre(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
        }
    }
}
