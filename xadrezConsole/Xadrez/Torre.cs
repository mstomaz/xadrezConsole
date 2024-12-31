using Tabuleiro;
namespace Xadrez;

public class Torre(Tabuleiro.Tabuleiro? tabuleiro, Cor cor) : Peca(tabuleiro, cor)
{
    public override string ToString()
    {
        return "T";
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

        int linhaPossivel = Posicao!.Linha;
        int colunaPossivel = Posicao.Coluna;

        //Acima
        pos.DefinirValores(--linhaPossivel, Posicao.Coluna);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            posicoesPossiveis[pos.Linha, pos.Coluna] = true;
            if (Tabuleiro.RetornarPeca(pos) != null && Tabuleiro.RetornarPeca(pos).Cor != Cor)
                break;

            pos.Linha--;
        }

        //Direita
        pos.DefinirValores(Posicao!.Linha, ++colunaPossivel);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            posicoesPossiveis[pos.Linha, pos.Coluna] = true;
            if (Tabuleiro.RetornarPeca(pos) != null && Tabuleiro.RetornarPeca(pos).Cor != Cor)
                break;


            pos.Coluna++;
        }

        //Abaixo
        linhaPossivel = Posicao!.Linha;
        pos.DefinirValores(++linhaPossivel, Posicao.Coluna);
        while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
        {
            posicoesPossiveis[pos.Linha, pos.Coluna] = true;
            if (Tabuleiro.RetornarPeca(pos) != null && Tabuleiro.RetornarPeca(pos).Cor != Cor)
                break;

            pos.Linha++;
        }

        //Esquerda
        colunaPossivel = Posicao.Coluna;
        pos.DefinirValores(Posicao!.Linha, --colunaPossivel);
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
