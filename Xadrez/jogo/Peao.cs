using System;
using System.Collections.Generic;
using System.Text;
using Xadrez.tabuleiro;

namespace Xadrez.jogo
{
    class Peao : Peca
    {
        public Peao(Cor cor, Tabuleiro tab) : base(cor, tab) { }
        public override string ToString()
        {
            return "P";
        }
        private bool existeInimigo(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p != null && p.cor != this.cor;
        }
        private bool livre(Posicao pos)
        {
            return tab.peca(pos) == null;
        }

        public override bool[,] possiveisMove()
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas];
            Posicao pos = new Posicao(0, 0);
            if (cor == Cor.Branca)
            {
                pos.defineValor(posicao.linha - 1, posicao.coluna);
                if (tab.posicaoValida(pos) && livre(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.defineValor(posicao.linha - 2, posicao.coluna);
                if (tab.posicaoValida(pos) && livre(pos) && quantmovi==0)
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.defineValor(posicao.linha - 1, posicao.coluna - 1);
                capturarInimigo(mat, pos);
                pos.defineValor(posicao.linha - 1, posicao.coluna + 1);
                capturarInimigo(mat, pos);
            }
            else
            {
                pos.defineValor(posicao.linha + 1, posicao.coluna);
                if (tab.posicaoValida(pos) && livre(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.defineValor(posicao.linha + 2, posicao.coluna);
                if (tab.posicaoValida(pos) && livre(pos) && quantmovi == 0)
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.defineValor(posicao.linha + 1, posicao.coluna - 1);
                capturarInimigo(mat, pos);
                pos.defineValor(posicao.linha + 1, posicao.coluna + 1);
                capturarInimigo(mat, pos);
            }
            return mat;
        }
        private void capturarInimigo(bool[,] mat, Posicao pos)
        {
            if (tab.posicaoValida(pos) && existeInimigo(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
        }
    }
}
