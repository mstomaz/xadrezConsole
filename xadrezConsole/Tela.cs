using Tabuleiro;
using Tab = Tabuleiro;

namespace xadrezConsole;

public class Tela
{
    public static void ImprimirTela(Tab.Tabuleiro tab)
    {
        for (int i = 0; i < tab.Linhas;  i++)
        {
            for (int j = 0; j < tab.Linhas; j++)
            {
                Peca? pecaAtual = tab.RetornarPeca(i, j);

                if (pecaAtual != null)
                    Console.Write("{0} ", pecaAtual);
                else
                    Console.Write("- ");
            }
            Console.WriteLine();
        }
    }
}
