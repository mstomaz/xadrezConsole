using Tabuleiro;

namespace Xadrez;

public class Peao(Tabuleiro.Tabuleiro tabuleiro, Cor cor) : Peca(tabuleiro, cor)
{
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
        }

        return posicoesPossiveis;
    }
}
