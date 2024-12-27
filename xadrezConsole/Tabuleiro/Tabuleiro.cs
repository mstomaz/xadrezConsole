namespace Tabuleiro;

public class Tabuleiro(int linhas, int colunas)
{
    public int Linhas { get; set; } = linhas;
    public int Colunas { get; set; } = colunas;
    public Peca[,]? Pecas { get; set; } = new Peca[linhas, colunas];
}
