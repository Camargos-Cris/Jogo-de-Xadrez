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
        public bool xeque { get; private set; }
        public Partida()
        {
            this.tab = new Tabuleiro(8, 8);
            this.turno = 1;
            this.jogadorAtual = Cor.Branca;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPeca();
            terminou = false;
            xeque = false;
        }
        public Peca executarMove(Posicao origem, Posicao destino)
        {
            Peca p = tab.removePeca(origem);
            p.incrementarQtMovi();
            Peca pecaCap = tab.removePeca(destino);
            tab.insertPeca(p, destino);
            if (pecaCap != null)
            {
                capturadas.Add(pecaCap);
            }
            return pecaCap;
        }
        public void realizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecacap = executarMove(origem, destino);
            if (estaEmXeque(jogadorAtual))
            {
                desfazMove(origem, destino, pecacap);
                throw new TabuleiroException("Você não pode se colocar ou permanecer em xeque");
            }
            if (estaEmXeque(adversaria(jogadorAtual)))
            {
                xeque = true;
            }
            else { xeque = false; }
            if (testeXequemate(adversaria(jogadorAtual)))
            {
                terminou = true;
            }
            else
            {
                turno++;
                alterPlayer();
            }
        }
        public void desfazMove(Posicao origem, Posicao destino, Peca cap)
        {
            Peca p = tab.removePeca(destino);
            p.decrementarQtMovi();
            if (cap != null)
            {
                tab.insertPeca(cap, destino);
                capturadas.Remove(cap);
            }
            tab.insertPeca(p, origem);
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
        private Cor adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            { return Cor.Branca; }
        }
        private Peca rei(Cor cor)
        {
            foreach (Peca item in pecasEmJogo(cor))
            {
                if (item is Rei)
                {
                    return item;
                }
            }
            return null;
        }
        public bool estaEmXeque(Cor cor)
        {
            Peca R = rei(cor);
            if (R == null)
            {
                throw new TabuleiroException("Não existe rei da cor " + cor + " no tabuleiro");
            }
            foreach (Peca item in pecasEmJogo(adversaria(cor)))
            {
                bool[,] mat = item.possiveisMove();
                if (mat[R.posicao.linha, R.posicao.coluna])
                {
                    return true;
                }
            }
            return false;
        }
        public bool testeXequemate(Cor cor)
        {
            if (!estaEmXeque(cor))
            {
                return false;
            }
            foreach (Peca item in pecasEmJogo(cor))
            {
                bool[,] mat = item.possiveisMove();
                for (int i = 0; i < tab.linhas; i++)
                    for (int j = 0; j < tab.colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = item.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecacap = executarMove(origem,destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMove(origem, destino,pecacap);
                            if(!testeXeque)
                            { return false; }
                        }
                    }
            }
            return true;
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
