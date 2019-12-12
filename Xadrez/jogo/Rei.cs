using System;
using System.Collections.Generic;
using System.Text;
using Xadrez.tabuleiro;

namespace Xadrez.jogo
{
    class Rei:Peca
    {
        public Rei(Cor cor,Tabuleiro tab) : base(cor,tab) { }
        public override string ToString()
        {
            return "R";
        }
    }
}
