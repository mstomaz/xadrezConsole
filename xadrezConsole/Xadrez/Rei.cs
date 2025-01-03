using Tabuleiro;
namespace Xadrez;

public class Rei(Tabuleiro.Tabuleiro? tabuleiro, Cor cor, PartidaDeXadrez partida) : Peca(tabuleiro, cor)
{
    private readonly PartidaDeXadrez _partida = partida;

    public override string ToString()
    {
        return "R";
    }

    protected override bool PodeMover(Posicao pos)
    {
        Peca p = Tabuleiro!.RetornarPeca(pos);
        return p == null || p!.Cor != Cor;
    }

    private bool TesteTorreParaRoque(Posicao pos)
    {
        Peca p = Tabuleiro!.RetornarPeca(pos);
        return p != null && p is Torre && p.Cor == Cor && QtdMovimentos == 0;
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

        //jogada especial roque
        if (QtdMovimentos == 0 && !_partida.Xeque)
        {
            //roque peq
            Posicao posTor1 = new Posicao(Posicao.Linha, Posicao.Coluna + 3);
            if (TesteTorreParaRoque(posTor1))
            {
                Posicao pos1 = new Posicao(Posicao!.Linha, Posicao.Coluna + 1);
                Posicao pos2 = new Posicao(Posicao!.Linha, Posicao.Coluna + 2);
                if (Tabuleiro.RetornarPeca(pos1) == null && Tabuleiro.RetornarPeca(pos2) == null)
                    posicoesPossiveis[Posicao.Linha, Posicao.Coluna + 2] = true;
            }

            //roque grd
            Posicao posTor2 = new Posicao(Posicao.Linha, Posicao.Coluna - 4);
            if (TesteTorreParaRoque(posTor2))
            {
                Posicao pos1 = new Posicao(Posicao!.Linha, Posicao.Coluna - 1);
                Posicao pos2 = new Posicao(Posicao!.Linha, Posicao.Coluna - 2);
                Posicao pos3 = new Posicao(Posicao!.Linha, Posicao.Coluna - 3);
                if (Tabuleiro.RetornarPeca(pos1) == null && Tabuleiro.RetornarPeca(pos2) == null && Tabuleiro.RetornarPeca(pos2) == null)
                    posicoesPossiveis[Posicao.Linha, Posicao.Coluna - 2] = true;
            }
        }

        return posicoesPossiveis;
    }
}
