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
}
