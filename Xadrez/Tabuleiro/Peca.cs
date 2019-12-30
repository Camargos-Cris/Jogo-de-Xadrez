using System;
using System.Collections.Generic;
using System.Text;

namespace Xadrez.tabuleiro
{
    abstract class Peca
    {
        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; }
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
        public void decrementarQtMovi()
        {
            quantmovi--;
        }
        public bool existeMovimentoPossivel()
        {
            bool[,] mat = possiveisMove();
            for (int i = 0; i < tab.linhas; i++)
            {
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }

                }
            }
            return false;
        }
        public bool possivelMove(Posicao pos)
        {
            return possiveisMove()[pos.linha, pos.coluna];
        }
        public abstract bool[,] possiveisMove();
    }
}
