using Tabuleiro;
namespace Xadrez;

public class Rei(Tabuleiro.Tabuleiro? tabuleiro, Cor cor) : Peca(tabuleiro, cor)
{
    public override string ToString()
    {
        return "R";
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

        //Acima
        pos.DefinirValores(Posicao!.Linha - 1, Posicao.Coluna);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            posicoesPossiveis[pos.Linha, pos.Coluna] = true;

        //NE
        pos.DefinirValores(Posicao!.Linha - 1, Posicao.Coluna + 1);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            posicoesPossiveis[pos.Linha, pos.Coluna] = true;

        //Direita
        pos.DefinirValores(Posicao!.Linha, Posicao.Coluna + 1);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            posicoesPossiveis[pos.Linha, pos.Coluna] = true;

        //SE
        pos.DefinirValores(Posicao!.Linha + 1, Posicao.Coluna + 1);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            posicoesPossiveis[pos.Linha, pos.Coluna] = true;

        //Abaixo
        pos.DefinirValores(Posicao!.Linha + 1, Posicao.Coluna);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            posicoesPossiveis[pos.Linha, pos.Coluna] = true;

        //SW
        pos.DefinirValores(Posicao!.Linha + 1, Posicao.Coluna - 1);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            posicoesPossiveis[pos.Linha, pos.Coluna] = true;

        //Esquerda
        pos.DefinirValores(Posicao!.Linha, Posicao.Coluna - 1);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            posicoesPossiveis[pos.Linha, pos.Coluna] = true;

        //NW
        pos.DefinirValores(Posicao!.Linha - 1, Posicao.Coluna - 1);
        if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            posicoesPossiveis[pos.Linha, pos.Coluna] = true;



        return posicoesPossiveis;
    }
}
