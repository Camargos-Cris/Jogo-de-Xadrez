using System;
using System.Collections.Generic;
using System.Text;
using Xadrez.tabuleiro;

namespace Xadrez.jogo
{
    class Peao : Peca
    {
        private Partida partida;
        public Peao(Cor cor, Tabuleiro tab,Partida part) : base(cor, tab) { this.partida = part; }
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
                if (tab.posicaoValida(pos) && livre(pos) && quantmovi == 0)
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.defineValor(posicao.linha - 1, posicao.coluna - 1);
                capturarInimigo(mat, pos);
                pos.defineValor(posicao.linha - 1, posicao.coluna + 1);
                capturarInimigo(mat, pos);
                //#jogada especial en passant
                if (posicao.linha == 3)
                {
                    verificarInimigoEnPassant(mat,1);
                }
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
                //#jogada especial en passant
                if (posicao.linha == 4)
                {
                    verificarInimigoEnPassant(mat,-1);
                }
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
        private void verificarInimigoEnPassant(bool[,]mat,int i)
        {
            Posicao esq = new Posicao(posicao.linha, posicao.coluna - 1);
            if (tab.posicaoValida(esq) && existeInimigo(esq) && tab.peca(esq) == partida.possivelEnPassant)
            {
                mat[esq.linha-i, esq.coluna] = true;
            }
            Posicao dir = new Posicao(posicao.linha, posicao.coluna + 1);
            if (tab.posicaoValida(dir) && existeInimigo(dir) && tab.peca(dir) == partida.possivelEnPassant)
            {
                mat[dir.linha-i, dir.coluna] = true;
            }
        }
    }
}
