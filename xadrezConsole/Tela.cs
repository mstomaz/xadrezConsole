using Tabuleiro;
using Xadrez;
using Tab = Tabuleiro;

namespace xadrezConsole;

public class Tela
{
    public static void ImprimirPartida(PartidaDeXadrez partida)
    {
        ImprimirTela(partida.Tab);
        Console.WriteLine();
        ImprimirPecasCapturadas(partida);
        Console.WriteLine();
        Console.WriteLine("Turno: {0}", partida.TurnoGeral);
        if (!partida.Terminada)
        {
            int turnoJogAtual = partida.JogadorAtual == Cor.Branca ? partida.TurnoJog1 : partida.TurnoJog2;
            Console.WriteLine("Peças {0}s: turno {1}", partida.JogadorAtual, turnoJogAtual);
            Console.WriteLine("Aguardando jogada...");
            if (partida.Xeque)
                Console.WriteLine("XEQUE");
        }
        else
        {
            Console.WriteLine("XEQUEMATE!");
            Console.WriteLine("Vencedor: {0}", partida.JogadorAtual);
        }
    }

    private static void ImprimirPecasCapturadas(PartidaDeXadrez partida)
    {
        Console.WriteLine("Peças capturadas:");
        Console.Write("Brancas: ");
        ImprimirConjunto(partida.PecasCapturadas(Cor.Branca));
        Console.Write("Pretas: ");
        ConsoleColor aux = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Yellow;
        ImprimirConjunto(partida.PecasCapturadas(Cor.Preta));
        Console.ForegroundColor = aux;
    }

    private static void ImprimirConjunto(HashSet<Peca> pecas)
    {
        int contaPecas = 0;
        Console.Write('[');
        foreach (Peca peca in pecas)
        {
            if (contaPecas == 0)
                Console.Write(" {0}", peca);
            else
                Console.Write(", {0}", peca);
            contaPecas++;
        }
        Console.WriteLine(" ]");
    }

    public static void ImprimirTela(Tab.Tabuleiro tab)
    {
        for (int i = 0; i < tab.Linhas;  i++)
        {
            Console.Write("{0} ", tab.Linhas - i);
            for (int j = 0; j < tab.Colunas; j++)
            {
                ImprimirPeca(tab.RetornarPeca(i, j));
            }
            Console.WriteLine();
        }
        Console.WriteLine("  A B C D E F G H");
    }

    public static void ImprimirTela(Tab.Tabuleiro tab, bool[,] posicoesPossiveis, Peca pecaOrigem)
    {
        for (int i = 0; i < tab.Linhas; i++)
        {
            Console.Write("{0} ", tab.Linhas - i);
            for (int j = 0; j < tab.Colunas; j++)
            {
                ConsoleColor movPossivel = ConsoleColor.DarkGray;
                ConsoleColor pecaInimiga = ConsoleColor.Red;
                ConsoleColor fundoOriginal = Console.BackgroundColor;
                if (posicoesPossiveis[i, j])
                {
                    if (tab.RetornarPeca(i, j) != null && tab.RetornarPeca(i, j)!.Cor != pecaOrigem.Cor)
                        Console.BackgroundColor = pecaInimiga;
                    else Console.BackgroundColor = movPossivel;
                }

                ImprimirPeca(tab.RetornarPeca(i, j));
                Console.BackgroundColor = fundoOriginal;
            }
            Console.WriteLine();
        }
        Console.WriteLine("  A B C D E F G H");
    }

    public static PosicaoXadrez LerPosicaoXadrez()
    {
        string posicaoEscolhida = Console.ReadLine()!;
        char coluna = posicaoEscolhida[0];
        short linha = short.Parse(posicaoEscolhida[1] + "");
        return new PosicaoXadrez(coluna, linha);
    }

    public static void ImprimirPeca(Peca? peca)
    {
        if (peca == null)
            Console.Write("- ");
        else
        {
            if (peca?.Cor == Cor.Branca)
                Console.Write("{0} ", peca);
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("{0} ", peca);
                Console.ForegroundColor = aux;
            }
        }
    }
}
