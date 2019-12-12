using System;
using System.Collections.Generic;
using System.Text;
using Xadrez.tabuleiro;

namespace Xadrez.jogo
{
    class Bispo:Peca
    {
        public Bispo(Cor cor, Tabuleiro tab) : base(cor, tab) { }
        public override string ToString()
        {
            return "B";
        }
    }
}
