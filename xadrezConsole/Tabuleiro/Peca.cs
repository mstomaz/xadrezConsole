namespace Tabuleiro;

public abstract class Peca(Tabuleiro? tabuleiro, Cor cor)
{
    public Posicao? Posicao { get; set; } = null;
    public Cor Cor { get; protected set; } = cor;
    public int QtdMovimentos { get; protected set; } = 0;
    public Tabuleiro? Tabuleiro { get; protected set; } = tabuleiro;

    public void IncrementarQtdMovimentos()
    {
        QtdMovimentos++;
    }

    protected abstract bool PodeMover(Posicao pos);

    public bool ExisteMovimentoPossivel()
    {
        bool[,] movsPossiveis = MovimentosPossiveis();
        for (int i = 0; i < Tabuleiro!.Linhas; i++)
            for (int j = 0; j < Tabuleiro.Colunas; j++)
                if (movsPossiveis[i, j])
                    return true;

        return false;
    }

    public bool PodeMoverPara(Posicao pos)
    {
        return MovimentosPossiveis()[pos.Linha, pos.Coluna];
    }

    public abstract bool[,] MovimentosPossiveis();
}
