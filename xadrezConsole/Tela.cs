using System.Drawing;
using Tabuleiro;
using Xadrez;
using Tab = Tabuleiro;

namespace xadrezConsole;

public class Tela
{
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

    public static void ImprimirTela(Tab.Tabuleiro tab, bool[, ] posicoesPossiveis, Peca pecaOrigem)
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
