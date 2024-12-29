using Tabuleiro;
using Tab = Tabuleiro;

namespace Xadrez;

public class PartidaDeXadrez
{
    private int _turnoGeral;
    private int _turnoJog1;
    private int _turnoJog2;
    private Cor _jogadorAtual;

    public PartidaDeXadrez()
    {
        Tab = new Tab.Tabuleiro(8, 8);
        _turnoGeral = 1;
        _turnoJog1 = 1;
        _turnoJog2 = 1;
        _jogadorAtual = Cor.Branca;
        Terminada = false;
        ColocarPecas();
    }

    public Tab.Tabuleiro? Tab { get; private set; }
    public bool Terminada { get; private set; }

    public void ExecutaMovimento(Posicao origem, Posicao destino)
    {
        Peca p = Tab!.RetirarPeca(origem)!;
        p.IncrementarQtdMovimentos();
        Peca pecaCapturada = Tab.RetirarPeca(destino)!;
        Tab.ColocarPeca(p, destino);
    }

    private void ColocarPecas()
    {
        Tab!.ColocarPeca(new Rei(Tab, Cor.Preta), new PosicaoXadrez('e', 8).ToPosicao());
        Tab.ColocarPeca(new Rei(Tab, Cor.Branca), new PosicaoXadrez('d', 1).ToPosicao());
        Tab.ColocarPeca(new Torre(Tab, Cor.Preta), new PosicaoXadrez('a', 8).ToPosicao());
        Tab.ColocarPeca(new Torre(Tab, Cor.Branca), new PosicaoXadrez('h', 1).ToPosicao());
    }

}
