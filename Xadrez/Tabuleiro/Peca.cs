﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Xadrez.tabuleiro
{
    class Peca
    {
        public Posicao posicao { get; set; }
        public Cor cor { get;protected  set; }
        public int quantmovi { get; protected set; }
        public Tabuleiro tabu { get; protected set; }

        public Peca(Posicao posicao, Cor cor, Tabuleiro tabu)
        {
            this.posicao = posicao;
            this.cor = cor;
            this.tabu = tabu;
            quantmovi = 0;
        }
    }
}