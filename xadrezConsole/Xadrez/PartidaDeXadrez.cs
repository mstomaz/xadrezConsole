using Tabuleiro;
using Tab = Tabuleiro;

namespace Xadrez;

public class PartidaDeXadrez
{
    private int _turnoGeral;
    private int _turnoJog1;
    private int _turnoJog2;
    private Cor _jogadorAtual;
    private HashSet<Peca> _pecas;
    private HashSet<Peca> _capturadas;
    private readonly Dictionary<string, string> _posIni;

    public PartidaDeXadrez()
    {
        _posIni = new Dictionary<string, string>
        {
            { "KiW", "e1" },
            { "QuW", "d1" },
            { "BiW1", "c1" },
            { "BiW2", "f1" },
            { "KnW1", "b1" },
            { "KnW2", "g1" },
            { "ToW1", "a1" },
            { "ToW2", "h1" },
            { "KiB", "e8" },
            { "QuB", "d8" },
            { "BiB1", "c8" },
            { "BiB2", "f8" },
            { "KnB1", "b8" },
            { "KnB2", "g8" },
            { "ToB1", "a8" },
            { "ToB2", "h8" }
        };
        Tab = new Tab.Tabuleiro(8, 8);
        _turnoGeral = 1;
        _turnoJog1 = 1;
        _turnoJog2 = 0;
        _jogadorAtual = Cor.Branca;
        Terminada = false;
        _pecas = [];
        _capturadas = [];
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
        
        if (pecaCapturada != null)
            _capturadas.Add(pecaCapturada);
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
            throw new TabuleiroException("Posicao de destino invalida!");
    }

    public HashSet<Peca> PecasCapturadas(Cor cor)
    {
        HashSet<Peca> aux = [];
        foreach (Peca p in _capturadas)
            if (p.Cor == cor)
                aux.Add(p);

        return aux;
    }

    public HashSet<Peca> PecasEmJogo(Cor cor)
    {
        HashSet<Peca> aux = [];
        foreach (Peca p in _pecas)
            if (p.Cor == cor)
                aux.Add(p);

        aux.ExceptWith(PecasCapturadas(cor));

        return aux;
    }

    public void ColocarNovaPeca(char coluna, short linha, Peca p)
    {
        Tab.ColocarPeca(p, new PosicaoXadrez(coluna, linha).ToPosicao());
        _pecas.Add(p);
    }

    private void ColocarPecas()
    {
        ColocarNovaPeca(_posIni["KiW"][0], short.Parse(_posIni["KiW"][1] + ""), new Rei(Tab, Cor.Branca));
        ColocarNovaPeca(_posIni["ToW1"][0], short.Parse(_posIni["ToW1"][1] + ""), new Torre(Tab, Cor.Branca));
        ColocarNovaPeca(_posIni["ToW2"][0], short.Parse(_posIni["ToW2"][1] + ""), new Torre(Tab, Cor.Branca));
        ColocarNovaPeca(_posIni["KiB"][0], short.Parse(_posIni["KiB"][1] + ""), new Rei(Tab, Cor.Preta));
        ColocarNovaPeca(_posIni["ToB1"][0], short.Parse(_posIni["ToB1"][1] + ""), new Torre(Tab, Cor.Preta));
        ColocarNovaPeca(_posIni["ToB2"][0], short.Parse(_posIni["ToB2"][1] + ""), new Torre(Tab, Cor.Preta));
    }

}
