using Tabuleiro;

namespace Xadrez;

public class Peao(Tabuleiro.Tabuleiro tabuleiro, Cor cor, PartidaDeXadrez partida) : Peca(tabuleiro, cor)
{
    private PartidaDeXadrez _partida = partida;

    public override string ToString()
    {
        return "P";
    }

    protected override bool PodeMover(Posicao pos)
    {
        Peca p = Tabuleiro!.RetornarPeca(pos);
        return p == null;
    }

    private bool ExisteAdversarioNaDiagonal(Posicao pos)
    {
        Peca p = Tabuleiro!.RetornarPeca(pos);
        return p != null && p.Cor != Cor;
    }

    public override bool[,] MovimentosPossiveis()
    {
        bool[,] posicoesPossiveis = new bool[Tabuleiro!.Linhas, Tabuleiro.Colunas];
        Posicao pos = new Posicao(0, 0);

        if (Cor == Cor.Branca)
        {
            pos.DefinirValores(Posicao!.Linha - 2, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos) && QtdMovimentos == 0)
                posicoesPossiveis[pos.Linha, pos.Coluna] = true;

            pos.DefinirValores(Posicao!.Linha - 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
                posicoesPossiveis[pos.Linha, pos.Coluna] = true;

            //NE quando há peça inimiga preta
            pos.DefinirValores(Posicao!.Linha - 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(pos) && ExisteAdversarioNaDiagonal(pos))
                posicoesPossiveis[pos.Linha, pos.Coluna] = true;

            //NW quando há peça inimiga preta
            pos.DefinirValores(Posicao!.Linha - 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(pos) && ExisteAdversarioNaDiagonal(pos))
                posicoesPossiveis[pos.Linha, pos.Coluna] = true;

            //en passant
            if (Posicao.Linha == 3)
            {
                Posicao esquerda = new(Posicao!.Linha, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(esquerda) && ExisteAdversarioNaDiagonal(esquerda) && Tabuleiro.RetornarPeca(esquerda) == _partida.VulneravelEnPassant)
                    posicoesPossiveis[esquerda.Linha - 1, esquerda.Coluna] = true;

                Posicao direita = new(Posicao!.Linha, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(direita) && ExisteAdversarioNaDiagonal(direita) && Tabuleiro.RetornarPeca(direita) == _partida.VulneravelEnPassant)
                    posicoesPossiveis[direita.Linha - 1, direita.Coluna] = true;
            }
        }
        else
        {
            pos.DefinirValores(Posicao!.Linha + 2, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos) && QtdMovimentos == 0)
                posicoesPossiveis[pos.Linha, pos.Coluna] = true;

            pos.DefinirValores(Posicao!.Linha + 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
                posicoesPossiveis[pos.Linha, pos.Coluna] = true;

            //NE quando há peça inimiga branca
            pos.DefinirValores(Posicao!.Linha + 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(pos) && ExisteAdversarioNaDiagonal(pos))
                posicoesPossiveis[pos.Linha, pos.Coluna] = true;

            //NW quando há peça inimiga branca
            pos.DefinirValores(Posicao!.Linha + 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(pos) && ExisteAdversarioNaDiagonal(pos))
                posicoesPossiveis[pos.Linha, pos.Coluna] = true;

            //en passant
            if (Posicao.Linha == 4)
            {
                Posicao esquerda = new(Posicao!.Linha, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(esquerda) && ExisteAdversarioNaDiagonal(esquerda) && Tabuleiro.RetornarPeca(esquerda) == _partida.VulneravelEnPassant)
                    posicoesPossiveis[esquerda.Linha + 1, esquerda.Coluna] = true;

                Posicao direita = new(Posicao!.Linha, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(direita) && ExisteAdversarioNaDiagonal(direita) && Tabuleiro.RetornarPeca(direita) == _partida.VulneravelEnPassant)
                    posicoesPossiveis[direita.Linha + 1, direita.Coluna] = true;
            }
        }

        return posicoesPossiveis;
    }
}
