using System;
using System.Collections.Generic;
using System.Text;

namespace Xadrez.tabuleiro
{
    class Posicao
    {
        public int linha{ get; set; }
        public int coluna { get; set; }

        public Posicao(int linha, int coluna)
        {
            this.linha = linha;
            this.coluna = coluna;
        }
        public void defineValor(int linha,int coluna)
        {
            this.linha = linha;
            this.coluna = coluna;
        }
        public override string ToString()
        {
            return "Linha: "+ linha
                +" Coluna: "+coluna; 
        }
    }
}
