using System;
using System.Collections.Generic;
using System.Text;
using Xadrez.tabuleiro;
namespace Xadrez.jogo
{
    class Cavalo:Peca
    {
        public Cavalo(Cor cor, Tabuleiro tab) : base(cor, tab) { }
        public override string ToString()
        {
            return "C";
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
            pos.defineValor(posicao.linha - 1, posicao.coluna-2);
            verificarVizinho(mat, pos);
            pos.defineValor(posicao.linha - 2, posicao.coluna -1);
            verificarVizinho(mat, pos);
            pos.defineValor(posicao.linha-2, posicao.coluna + 1);
            verificarVizinho(mat, pos);
            pos.defineValor(posicao.linha -1, posicao.coluna + 2);
            verificarVizinho(mat, pos);
            pos.defineValor(posicao.linha -1, posicao.coluna+2);
            verificarVizinho(mat, pos);
            pos.defineValor(posicao.linha + 1, posicao.coluna +2);
            verificarVizinho(mat, pos);
            pos.defineValor(posicao.linha +2, posicao.coluna + 1);
            verificarVizinho(mat, pos);
            pos.defineValor(posicao.linha + 2, posicao.coluna - 1);
            verificarVizinho(mat, pos);
            pos.defineValor(posicao.linha + 1, posicao.coluna - 2);
            verificarVizinho(mat, pos);
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
