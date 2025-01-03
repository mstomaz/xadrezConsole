using Tabuleiro;

namespace Xadrez;

public class Rainha(Tabuleiro.Tabuleiro tabuleiro, Cor cor) : Peca(tabuleiro, cor)
{
    public override string ToString()
    {
        return "Q";
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

        //NW
        pos.DefinirValores(Posicao!.Linha - 1, Posicao.Coluna - 1);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            posicoesPossiveis[pos.Linha, pos.Coluna] = true;
            if (Tabuleiro.RetornarPeca(pos) != null && Tabuleiro.RetornarPeca(pos).Cor != Cor)
                break;

            pos.Linha--;
            pos.Coluna--;
        }

        //NE
        pos.DefinirValores(Posicao!.Linha - 1, Posicao.Coluna + 1);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            posicoesPossiveis[pos.Linha, pos.Coluna] = true;
            if (Tabuleiro.RetornarPeca(pos) != null && Tabuleiro.RetornarPeca(pos).Cor != Cor)
                break;

            pos.Linha--;
            pos.Coluna++;
        }

        //SW
        pos.DefinirValores(Posicao!.Linha + 1, Posicao.Coluna - 1);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            posicoesPossiveis[pos.Linha, pos.Coluna] = true;
            if (Tabuleiro.RetornarPeca(pos) != null && Tabuleiro.RetornarPeca(pos).Cor != Cor)
                break;

            pos.Linha++;
            pos.Coluna--;
        }

        //SE
        pos.DefinirValores(Posicao!.Linha + 1, Posicao.Coluna + 1);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            posicoesPossiveis[pos.Linha, pos.Coluna] = true;
            if (Tabuleiro.RetornarPeca(pos) != null && Tabuleiro.RetornarPeca(pos).Cor != Cor)
                break;

            pos.Linha++;
            pos.Coluna++;
        }

        //Acima
        pos.DefinirValores(Posicao!.Linha - 1, Posicao.Coluna);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            posicoesPossiveis[pos.Linha, pos.Coluna] = true;
            if (Tabuleiro.RetornarPeca(pos) != null && Tabuleiro.RetornarPeca(pos).Cor != Cor)
                break;

            pos.Linha--;
        }

        //Direita
        pos.DefinirValores(Posicao!.Linha, Posicao.Coluna + 1);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            posicoesPossiveis[pos.Linha, pos.Coluna] = true;
            if (Tabuleiro.RetornarPeca(pos) != null && Tabuleiro.RetornarPeca(pos).Cor != Cor)
                break;


            pos.Coluna++;
        }

        //Abaixo
        pos.DefinirValores(Posicao!.Linha + 1, Posicao.Coluna);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            posicoesPossiveis[pos.Linha, pos.Coluna] = true;
            if (Tabuleiro.RetornarPeca(pos) != null && Tabuleiro.RetornarPeca(pos).Cor != Cor)
                break;

            pos.Linha++;
        }

        //Esquerda
        pos.DefinirValores(Posicao!.Linha, Posicao.Coluna - 1);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            posicoesPossiveis[pos.Linha, pos.Coluna] = true;
            if (Tabuleiro.RetornarPeca(pos) != null && Tabuleiro.RetornarPeca(pos).Cor != Cor)
                break;

            pos.Coluna--;
        }

        return posicoesPossiveis;
    }
}
