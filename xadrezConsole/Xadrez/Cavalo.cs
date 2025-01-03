using Tabuleiro;

namespace Xadrez;

public class Cavalo(Tabuleiro.Tabuleiro tabuleiro, Cor cor) : Peca(tabuleiro, cor)
{
    public override string ToString()
    {
        return "C";
    }

    protected override bool PodeMover(Posicao pos)
    {
        Peca p = Tabuleiro!.RetornarPeca(pos);
        return p == null || p!.Cor != Cor;
    }

    public override bool[,] MovimentosPossiveis()
    {
        bool[,] posicoesPossiveis = new bool[Tabuleiro!.Linhas, Tabuleiro.Colunas];
        Posicao pos = new Posicao(0, 0);

        //L para cima direita
        pos.DefinirValores(Posicao!.Linha - 2, Posicao.Coluna + 1);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            posicoesPossiveis[pos.Linha, pos.Coluna] = true;

        //L para cima esquerda
        pos.DefinirValores(Posicao!.Linha - 2, Posicao.Coluna - 1);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            posicoesPossiveis[pos.Linha, pos.Coluna] = true;

        //L para baixo direita
        pos.DefinirValores(Posicao!.Linha + 2, Posicao.Coluna + 1);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            posicoesPossiveis[pos.Linha, pos.Coluna] = true;

        //L para baixo esquerda
        pos.DefinirValores(Posicao!.Linha + 2, Posicao.Coluna - 1);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            posicoesPossiveis[pos.Linha, pos.Coluna] = true;

        pos.DefinirValores(Posicao!.Linha + 1, Posicao.Coluna - 2);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            posicoesPossiveis[pos.Linha, pos.Coluna] = true;

        pos.DefinirValores(Posicao!.Linha - 1, Posicao.Coluna - 2);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            posicoesPossiveis[pos.Linha, pos.Coluna] = true;

        pos.DefinirValores(Posicao!.Linha - 1, Posicao.Coluna + 2);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            posicoesPossiveis[pos.Linha, pos.Coluna] = true;

        pos.DefinirValores(Posicao!.Linha + 1, Posicao.Coluna + 2);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            posicoesPossiveis[pos.Linha, pos.Coluna] = true;

        return posicoesPossiveis;
    }
}
