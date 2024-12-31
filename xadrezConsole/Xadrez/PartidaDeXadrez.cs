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
        _turnoJog2 = 0;
        _jogadorAtual = Cor.Branca;
        Terminada = false;
        ColocarPecas();
    }

    public Tab.Tabuleiro Tab { get; private set; }

    public int TurnoGeral
    {
        get => _turnoGeral;
    }

    public int TurnoJog1
    {
        get => _turnoJog1;
    }

    public int TurnoJog2
    {
        get => _turnoJog2;
    }

    public Cor JogadorAtual
    {
        get => _jogadorAtual;
    }

    public bool Terminada { get; private set; }

    private void ExecutaMovimento(Posicao origem, Posicao destino)
    {
        Peca p = Tab.RetirarPeca(origem)!;
        p.IncrementarQtdMovimentos();
        Peca pecaCapturada = Tab.RetirarPeca(destino)!;
        Tab.ColocarPeca(p, destino);
    }

    public void RealizaJogada(Posicao origem, Posicao destino)
    {
        ExecutaMovimento(origem, destino);
        _turnoGeral++;
        MudaJogador();
    }

    private void MudaJogador()
    {
        if (_jogadorAtual == Cor.Branca)
        {
            _turnoJog2++;
            _jogadorAtual = Cor.Preta;
        }
        else
        {
            _turnoJog1++;
            _jogadorAtual = Cor.Branca;
        }
    }

    public void ValidarPosicaoDeOrigem(Posicao pos)
    {
        if (Tab.RetornarPeca(pos) == null)
            throw new TabuleiroException("Nao ha nenhuma peca na posicao escolhida!");
        if (_jogadorAtual != Tab.RetornarPeca(pos).Cor)
            throw new TabuleiroException("A peca escolhida nao e sua!");
        if (!Tab.RetornarPeca(pos).ExisteMovimentoPossivel())
            throw new TabuleiroException("Nao ha nenhum movimento possivel para a peça!");
    }

    public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
    {
        if (Tab.RetornarPeca(destino) != null && Tab.RetornarPeca(origem).Cor == Tab.RetornarPeca(destino).Cor)
            throw new TabuleiroException("Nao e possivel tomar uma peca sua!");
        if (!Tab.RetornarPeca(origem).PodeMoverPara(destino))
            throw new TabuleiroException("Esta peca nao pode se mover para o destino escolhido!");
    }

    private void ColocarPecas()
    {
        Tab!.ColocarPeca(new Rei(Tab, Cor.Preta), new PosicaoXadrez('e', 8).ToPosicao());
        Tab.ColocarPeca(new Rei(Tab, Cor.Branca), new PosicaoXadrez('d', 1).ToPosicao());
        Tab.ColocarPeca(new Torre(Tab, Cor.Preta), new PosicaoXadrez('a', 8).ToPosicao());
        Tab.ColocarPeca(new Torre(Tab, Cor.Branca), new PosicaoXadrez('e', 2).ToPosicao());
    }

}
