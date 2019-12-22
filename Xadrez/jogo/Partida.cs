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
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;

        public Partida()
        {
            this.tab = new Tabuleiro(8, 8);
            this.turno = 1;
            this.jogadorAtual = Cor.Branca;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPeca();
            terminou = false;

        }
        public void executarMove(Posicao origem, Posicao destino)
        {
            Peca p = tab.removePeca(origem);
            p.incrementarQtMovi();
            Peca pecaCap = tab.removePeca(destino);
            tab.insertPeca(p, destino);
            if (pecaCap != null)
            {
                capturadas.Add(pecaCap);
            }
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
        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }
        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }
        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.insertPeca(peca, new PosXadrez(coluna, linha).toPosition());
            pecas.Add(peca);
        }
        private void colocarPeca()
        {
            colocarNovaPeca('a', 1, new Torre(Cor.Branca, tab));
            colocarNovaPeca('h', 1, new Torre(Cor.Branca, tab));
            colocarNovaPeca('a', 8, new Torre(Cor.Preta, tab));
            colocarNovaPeca('h', 8, new Torre(Cor.Preta, tab));
            colocarNovaPeca('d', 8, new Rei(Cor.Preta, tab));
            colocarNovaPeca('d', 1, new Rei(Cor.Branca, tab));
        }

    }
}
