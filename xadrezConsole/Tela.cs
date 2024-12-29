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
                Peca? pecaAtual = tab.RetornarPeca(i, j);

                if (pecaAtual != null)
                    ImprimirPeca(pecaAtual);
                else
                    Console.Write("- ");
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

    public static void ImprimirPeca(Peca peca)
    {
        if (peca.Cor == Cor.Branca)
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
