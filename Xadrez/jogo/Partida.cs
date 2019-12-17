using System;
using System.Collections.Generic;
using System.Text;
using Xadrez.tabuleiro;
namespace Xadrez.jogo
{
    class Partida
    {
        public Tabuleiro tab { get; private set; }
        private int turno;
        private Cor jogadorAtual;
        public bool terminou { get; private set; }

        public Partida()
        {
            this.tab = new Tabuleiro(8, 8);
            this.turno = 0;
            this.jogadorAtual = Cor.Branca;
            colocarPeca();
            terminou = false;
        }
        public void executarMove(Posicao origem, Posicao destino)
        {
            Peca p = tab.removePeca(origem);
            p.incrementarQtMovi();
            tab.removePeca(destino);
            Peca pecaCap = tab.removePeca(destino);
            tab.insertPeca(p, destino);
        }
        private void colocarPeca()
        {
            tab.insertPeca(new Torre(Cor.Branca, tab), new PosXadrez('a', 1).toPosition());
            tab.insertPeca(new Torre(Cor.Branca, tab), new PosXadrez('h', 1).toPosition());
            tab.insertPeca(new Torre(Cor.Preta, tab), new PosXadrez('a', 8).toPosition());
            tab.insertPeca(new Torre(Cor.Preta, tab), new PosXadrez('h', 8).toPosition());
            tab.insertPeca(new Rei(Cor.Preta, tab), new PosXadrez('d',8 ).toPosition());
        }

    }
}
