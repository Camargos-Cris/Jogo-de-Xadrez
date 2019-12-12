using System;
using System.Collections.Generic;
using System.Text;
using Xadrez.tabuleiro;

namespace Xadrez.jogo
{
    class PosXadrez
    {
        public char coluna { get; set; }
        public int linha { get; set; }

        public PosXadrez(char coluna, int linha)
        {
            this.coluna = coluna;
            this.linha = linha;
        }
        public override string ToString()
        {
            return ""+coluna+linha; 
        }
        public Posicao toPosition()
        {
            return new Posicao(8 - linha, coluna - 'a');
        }
    }
}
