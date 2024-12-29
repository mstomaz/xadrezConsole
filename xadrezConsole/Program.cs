namespace xadrezConsole;
using Tabuleiro;
using Xadrez;

public class Program
{
    static void Main(string[] args)
    {
        Tabuleiro tab = new Tabuleiro(8, 8);

        tab.ColocarPeca(new Rei(tab, Cor.Preta), new Posicao(7, 2));
        tab.ColocarPeca(new Rei(tab, Cor.Branca), new Posicao(4, 4));
        tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 6));
        tab.ColocarPeca(new Torre(tab, Cor.Branca), new Posicao(5, 1));

        Tela.ImprimirTela(tab);
    }
}
