using System.Diagnostics.Contracts;
using Tabuleiro;

namespace Xadrez;

public class PosicaoXadrez
{
    public PosicaoXadrez(char coluna, short linha)
    {
        Coluna = coluna <= 90 ? coluna : (char)(coluna - 32);
        Linha = linha;
    }

    public Posicao ToPosicao()
    {
        return new Posicao(8 - Linha, Coluna - 'A');
    }

    public char Coluna { get; set; }
    public short Linha { get; set; }

    public override string ToString()
    {
        return "" + Coluna + Linha;
    }
}
