using System;
using System.Collections.Generic;
using System.Text;

namespace Xadrez.tabuleiro
{
    abstract class Peca
    {
        public Posicao posicao { get; set; }
        public Cor cor { get;protected  set; }
        public int quantmovi { get; protected set; }
        public Tabuleiro tab { get; protected set; }

        public Peca(Cor cor, Tabuleiro tabu)
        {
            this.posicao = null;
            this.cor = cor;
            this.tab = tabu;
            quantmovi = 0;
        }
        public void incrementarQtMovi()
        { quantmovi++; }
        public abstract bool[,] possiveisMove();
    }
}
