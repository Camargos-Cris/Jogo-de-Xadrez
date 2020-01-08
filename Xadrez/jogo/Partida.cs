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
        public Peca possivelEnPassant { get; private set; }
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
            possivelEnPassant = null;
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
            // #jogadaespecial roque pequeno
            if (p is Rei && destino.coluna == origem.coluna + 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tab.removePeca(origemT);
                T.incrementarQtMovi();
                tab.insertPeca(T, destinoT);
            }

            // #jogadaespecial roque grande
            if (p is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tab.removePeca(origemT);
                T.incrementarQtMovi();
                tab.insertPeca(T, destinoT);
            }
            //#jogada especial en passant
            if (p is Peao)
            {
                if (origem.coluna != destino.coluna && pecaCap == null)
                {
                    Posicao aux;
                    if (p.cor == Cor.Branca)
                    {
                        aux = new Posicao(destino.linha + 1, destino.coluna);
                    }
                    else
                    { aux = new Posicao(destino.linha - 1, destino.coluna); }
                    pecaCap = tab.removePeca(aux);
                    capturadas.Add(pecaCap);
                }
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
            Peca p = tab.peca(destino);
            //#jogada especial promoção
            if (p is Peao)
            {
                if ((p.cor==Cor.Branca && destino.linha==0)||(p.cor==Cor.Preta && destino.linha==7))
                {
                    p = tab.removePeca(destino);
                    pecas.Remove(p);
                    Peca dama = new Rainha(p.cor,tab);
                    tab.insertPeca(dama, destino);
                    pecas.Add(dama);
                }
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

            //jogada especial en passant
            if (p is Peao && (destino.linha == origem.linha - 2 || destino.linha == origem.linha + 2))
            {
                possivelEnPassant = p;
            }
            else
            { possivelEnPassant = null; }
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
            // #jogadaespecial roque pequeno
            if (p is Rei && destino.coluna == origem.coluna + 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tab.removePeca(destinoT);
                T.decrementarQtMovi();
                tab.insertPeca(T, origemT);
            }

            // #jogadaespecial roque grande
            if (p is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tab.removePeca(destinoT);
                T.decrementarQtMovi();
                tab.insertPeca(T, origemT);
            }
            //#jogada especial en passant
            if (p is Peao)
            {
                if (origem.coluna != destino.coluna && cap == possivelEnPassant)
                {
                    Peca peao = tab.removePeca(destino);
                    Posicao posP;
                    if (p.cor == Cor.Branca)
                    { posP = new Posicao(3, destino.coluna); }
                    else
                    {
                        posP = new Posicao(4, destino.coluna);
                    }
                    tab.insertPeca(peao,posP);
                }
            }
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
            if (!tab.peca(origem).possivelMove(destino))
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
                            Peca pecacap = executarMove(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMove(origem, destino, pecacap);
                            if (!testeXeque)
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
            colocarNovaPeca('a', 2, new Peao(Cor.Branca, tab, this));
            colocarNovaPeca('b', 2, new Peao(Cor.Branca, tab, this));
            colocarNovaPeca('c', 2, new Peao(Cor.Branca, tab, this));
            colocarNovaPeca('d', 2, new Peao(Cor.Branca, tab, this));
            colocarNovaPeca('e', 2, new Peao(Cor.Branca, tab, this));
            colocarNovaPeca('f', 2, new Peao(Cor.Branca, tab, this));
            colocarNovaPeca('g', 2, new Peao(Cor.Branca, tab, this));
            colocarNovaPeca('h', 2, new Peao(Cor.Branca, tab, this));

            colocarNovaPeca('a', 1, new Torre(Cor.Branca, tab));
            colocarNovaPeca('b', 1, new Cavalo(Cor.Branca, tab));
            colocarNovaPeca('c', 1, new Bispo(Cor.Branca, tab));
            colocarNovaPeca('d', 1, new Rainha(Cor.Branca, tab));
            colocarNovaPeca('e', 1, new Rei(Cor.Branca, tab, this));
            colocarNovaPeca('f', 1, new Bispo(Cor.Branca, tab));
            colocarNovaPeca('g', 1, new Cavalo(Cor.Branca, tab));
            colocarNovaPeca('h', 1, new Torre(Cor.Branca, tab));

            colocarNovaPeca('a', 7, new Peao(Cor.Preta, tab, this));
            colocarNovaPeca('b', 7, new Peao(Cor.Preta, tab, this));
            colocarNovaPeca('c', 7, new Peao(Cor.Preta, tab, this));
            colocarNovaPeca('d', 7, new Peao(Cor.Preta, tab, this));
            colocarNovaPeca('e', 7, new Peao(Cor.Preta, tab, this));
            colocarNovaPeca('f', 7, new Peao(Cor.Preta, tab, this));
            colocarNovaPeca('g', 7, new Peao(Cor.Preta, tab, this));
            colocarNovaPeca('h', 7, new Peao(Cor.Preta, tab, this));

            colocarNovaPeca('a', 8, new Torre(Cor.Preta, tab));
            colocarNovaPeca('b', 8, new Cavalo(Cor.Preta, tab));
            colocarNovaPeca('c', 8, new Bispo(Cor.Preta, tab));
            colocarNovaPeca('d', 8, new Rainha(Cor.Preta, tab));
            colocarNovaPeca('e', 8, new Rei(Cor.Preta, tab, this));
            colocarNovaPeca('f', 8, new Bispo(Cor.Preta, tab));
            colocarNovaPeca('g', 8, new Cavalo(Cor.Preta, tab));
            colocarNovaPeca('h', 8, new Torre(Cor.Preta, tab));

        }

    }
}
