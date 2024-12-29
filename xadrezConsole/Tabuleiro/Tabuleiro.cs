namespace Tabuleiro;

public class Tabuleiro(int linhas, int colunas)
{
    public int Linhas { get; set; } = linhas;
    public int Colunas { get; set; } = colunas;
    public Peca[,]? Pecas { get; set; } = new Peca[linhas, colunas];

    public Peca? RetornarPeca(int linha, int coluna)
    {
        return Pecas![linha, coluna];
    }

    public Peca RetornarPeca(Posicao pos)
    {
        return Pecas![pos.Linha, pos.Coluna];
    }

    public bool ExistePeca(Posicao pos)
    {
        ValidarPosicao(pos);
        return RetornarPeca(pos) != null;
    }

    public void ColocarPeca(Peca p, Posicao pos)
    {
        if (ExistePeca(pos))
            throw new TabuleiroException("Ja existe uma peca nessa posicao!");

        Pecas![pos.Linha, pos.Coluna] = p;
        p.Posicao = pos;
    }

    public Peca? RetirarPeca(Posicao? pos)
    {
        if (!ExistePeca(pos!))
            return null;

        Peca aux = RetornarPeca(pos!);
        aux.Posicao = null;
        Pecas![pos!.Linha, pos.Coluna] = null;
        return aux;
    }

    private bool PosicaoValida(Posicao pos)
    {
        return pos.Linha >= 0 && pos.Linha < Linhas && pos.Coluna >= 0 && pos.Coluna < Colunas;
    }

    public void ValidarPosicao(Posicao pos)
    {
        if (!PosicaoValida(pos))
            throw new TabuleiroException("Posicao invalida!");
    }
}
