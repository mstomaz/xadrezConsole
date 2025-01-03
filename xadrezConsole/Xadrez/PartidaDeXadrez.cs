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
        Xeque = false;
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
    public bool Xeque { get; private set; }

    private Peca ExecutaMovimento(Posicao origem, Posicao destino)
    {
        Peca p = Tab.RetornarPeca(origem);
        if (EstaEmXeque(_jogadorAtual) && p is not Rei)
        {
            throw new TabuleiroException("Voce esta em xeque. Mova seu rei!");
        }

        p = Tab.RetirarPeca(origem)!;
        p.IncrementarQtdMovimentos();
        Peca? pecaCapturada = Tab.RetirarPeca(destino)!;
        Tab.ColocarPeca(p, destino);
        
        if (pecaCapturada != null)
            _capturadas.Add(pecaCapturada);

        return pecaCapturada;
    }

    private void DesfazMovimento(Posicao origem, Posicao destino, Peca capturada)
    {
        Peca p = Tab.RetirarPeca(destino)!;
        p.DecrementarQtdMovimentos();
        
        if (capturada != null)
        {
            Tab.ColocarPeca(capturada, destino);
            _capturadas.Remove(capturada);
        }
        Tab.ColocarPeca(p, origem);
    }

    public void ValidarMovimento(Posicao origem, bool[,] movsPossiveis)
    {
        Peca rei = Tab.RetirarPeca(origem)!;

        for (int i = 0; i < Tab.Linhas; i++)
        {
            for (int j = 0; j < Tab.Colunas; j++)
            {
                if (!movsPossiveis[i, j])
                    continue;

                if (Tab.ExistePeca(new Posicao(i, j)))
                    continue;

                if (MovEntraraEmXeque(i, j, rei))
                {
                    movsPossiveis[i, j] = false;
                }
                Tab.RetirarPeca(new Posicao(i, j));
            }
        }
        Tab.ColocarPeca(rei, origem);
    }

    private bool MovEntraraEmXeque(int linha, int coluna, Peca p)
    {
        Posicao destino = new(linha, coluna);
        Tab.ColocarPeca(p, destino);

        return EstaEmXeque(_jogadorAtual);
    }

    public void RealizaJogada(Posicao origem, Posicao destino)
    {
        Peca capturada = ExecutaMovimento(origem, destino);

        if (EstaEmXeque(_jogadorAtual))
        {
            DesfazMovimento(origem, destino, capturada);
            throw new TabuleiroException("Voce nao pode se colocar em xeque!");
        }

        if (EstaEmXeque(Adversario(_jogadorAtual)))
            Xeque = true;
        else
            Xeque = false;

        if (TesteXequemate(Adversario(_jogadorAtual)))
            Terminada = true;
        else
        {
            _turnoGeral++;
            MudaJogador();
        }
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
        if (!Tab.RetornarPeca(origem).MovimentoPossivel(destino))
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

    private Cor Adversario(Cor cor)
    {
        return cor switch
        {
            Cor.Branca => Cor.Preta,
            _ => Cor.Branca
        };
    }

    private Peca? RetornarRei(Cor cor)
    {
        foreach (Peca p in PecasEmJogo(cor))
            if (p is Rei)
                return p;

        return null;
    }

    public bool EstaEmXeque(Cor cor)
    {
        Peca rei = RetornarRei(cor)!;
        
        if (rei == null)
        {
            throw new TabuleiroException($"Nao ha nenhum rei da cor {cor} no tabuleiro!");
        }

        foreach (Peca p in PecasEmJogo(Adversario(cor)))
        {
            bool[,] movsPossiveis = p.MovimentosPossiveis();
            if (movsPossiveis[rei.Posicao!.Linha, rei.Posicao.Coluna])
                return true;
        }

        return false;
    }

    public bool TesteXequemate(Cor cor)
    {
        if (!EstaEmXeque(cor))
            return false;

        foreach (Peca p in PecasEmJogo(cor))
        {
            bool[,] movsPossiveis = p.MovimentosPossiveis();
            for (int i = 0; i < Tab.Linhas; i++)
                for (int j = 0; j < Tab.Colunas; j++)
                    if (movsPossiveis[i, j])
                    {
                        Posicao origem = p.Posicao!;
                        Posicao destino = new Posicao(i, j);
                        Peca capturada = ExecutaMovimento(origem, destino);
                        bool testeXeque = EstaEmXeque(cor);
                        DesfazMovimento(origem, destino, capturada);
                        if (!testeXeque) return false;
                    }
        }

        return true;
    }

    private void ColocarNovaPeca(char coluna, short linha, Peca p)
    {
        Tab.ColocarPeca(p, new PosicaoXadrez(coluna, linha).ToPosicao());
        _pecas.Add(p);
    }

    private void ColocarNovaPeca(int linha, int coluna, Peca p)
    {
        Tab.ColocarPeca(p, new Posicao(linha, coluna));
        _pecas.Add(p);
    }

    private void ColocarPecas()
    {
        ColocarNovaPeca(_posIni["KiW"][0], short.Parse(_posIni["KiW"][1] + ""), new Rei(Tab, Cor.Branca));
        ColocarNovaPeca(_posIni["ToW1"][0], short.Parse(_posIni["ToW1"][1] + ""), new Torre(Tab, Cor.Branca));
        ColocarNovaPeca(_posIni["ToW2"][0], short.Parse(_posIni["ToW2"][1] + ""), new Torre(Tab, Cor.Branca));
        ColocarNovaPeca(_posIni["BiW1"][0], short.Parse(_posIni["BiW1"][1] + ""), new Bispo(Tab, Cor.Branca));
        ColocarNovaPeca(_posIni["BiW2"][0], short.Parse(_posIni["BiW2"][1] + ""), new Bispo(Tab, Cor.Branca));
        ColocarNovaPeca(_posIni["KnW1"][0], short.Parse(_posIni["KnW1"][1] + ""), new Cavalo(Tab, Cor.Branca));
        ColocarNovaPeca(_posIni["KnW2"][0], short.Parse(_posIni["KnW2"][1] + ""), new Cavalo(Tab, Cor.Branca));
        ColocarNovaPeca(_posIni["QuW"][0], short.Parse(_posIni["QuW"][1] + ""), new Rainha(Tab, Cor.Branca));
        for (int i = 0; i < Tab.Linhas; i++)
            for (int j = 0; j < Tab.Colunas; j++)
            {
                if (i == 6)
                    ColocarNovaPeca(i, j, new Peao(Tab, Cor.Branca));
                else
                    break;
            }

        ColocarNovaPeca(_posIni["KiB"][0], short.Parse(_posIni["KiB"][1] + ""), new Rei(Tab, Cor.Preta));
        ColocarNovaPeca(_posIni["ToB1"][0], short.Parse(_posIni["ToB1"][1] + ""), new Torre(Tab, Cor.Preta));
        ColocarNovaPeca(_posIni["ToB2"][0], short.Parse(_posIni["ToB2"][1] + ""), new Torre(Tab, Cor.Preta));
        ColocarNovaPeca(_posIni["BiB1"][0], short.Parse(_posIni["BiB1"][1] + ""), new Bispo(Tab, Cor.Preta));
        ColocarNovaPeca(_posIni["BiB2"][0], short.Parse(_posIni["BiB2"][1] + ""), new Bispo(Tab, Cor.Preta));
        ColocarNovaPeca(_posIni["KnB1"][0], short.Parse(_posIni["KnB1"][1] + ""), new Cavalo(Tab, Cor.Preta));
        ColocarNovaPeca(_posIni["KnB2"][0], short.Parse(_posIni["KnB2"][1] + ""), new Cavalo(Tab, Cor.Preta));
        ColocarNovaPeca(_posIni["QuB"][0], short.Parse(_posIni["QuB"][1] + ""), new Rainha(Tab, Cor.Preta));
        for (int i = 0; i < Tab.Linhas; i++)
            for (int j = 0; j < Tab.Colunas; j++)
            {
                if (i == 1)
                    ColocarNovaPeca(i, j, new Peao(Tab, Cor.Preta));
                else
                    break;
            }
    }

}
