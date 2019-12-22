using System;
using System.Collections.Generic;
using System.Text;
using Xadrez.tabuleiro;
namespace Xadrez.jogo
{
    class Partida
    {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminou { get; private set; }

        public Partida()
        {
            this.tab = new Tabuleiro(8, 8);
            this.turno = 1;
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
        public void realizaJogada(Posicao origem, Posicao destino)
        {
            executarMove(origem, destino);
            turno++;
            alterPlayer();
        }
        public void validarOrigem(Posicao origem)
        {
            if (tab.peca(origem) == null)
            {
                throw new TabuleiroException("Não existe peça na posição selecionada");
            }
            if (jogadorAtual != tab.peca(origem).cor)
            {
                throw new TabuleiroException("Esta peça não pertence a cor do jogador da rodada");
            }
            if (!tab.peca(origem).existeMovimentoPossivel())
            {
                throw new TabuleiroException("Não existe movimentos possiveis para a peça selecionada");
            }
        }
        public void validarDestino(Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).podeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino deve ser igual a uma das posições marcadas!");
            }
        }
        private void alterPlayer()
        {
            if (this.jogadorAtual == Cor.Branca)
            {
                this.jogadorAtual = Cor.Preta;
            }
            else
            { this.jogadorAtual = Cor.Branca; }
        }
        private void colocarPeca()
        {
            tab.insertPeca(new Torre(Cor.Branca, tab), new PosXadrez('a', 1).toPosition());
            tab.insertPeca(new Torre(Cor.Branca, tab), new PosXadrez('h', 1).toPosition());
            tab.insertPeca(new Torre(Cor.Preta, tab), new PosXadrez('a', 8).toPosition());
            tab.insertPeca(new Torre(Cor.Preta, tab), new PosXadrez('h', 8).toPosition());
            tab.insertPeca(new Rei(Cor.Preta, tab), new PosXadrez('d', 8).toPosition());
        }

    }
}
