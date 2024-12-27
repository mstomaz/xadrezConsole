using Tabuleiro;
namespace Xadrez;

public class Torre(Tabuleiro.Tabuleiro? tabuleiro, Cor cor) : Peca(tabuleiro, cor)
{
    public override string ToString()
    {
        return "T";
    }
}
