using Tabuleiro;
namespace Xadrez;

public class Rei(Tabuleiro.Tabuleiro? tabuleiro, Cor cor) : Peca(tabuleiro, cor)
{
    public override string ToString()
    {
        return "R";
    }
}
