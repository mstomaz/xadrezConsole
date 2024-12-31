namespace xadrezConsole;
using Tabuleiro;
using Xadrez;

public class Program
{
    static void Main(string[] args)
    {
        try
        {
            PartidaDeXadrez partida = new PartidaDeXadrez();

            while (!partida.Terminada)
            {
                Console.Clear();
                Tela.ImprimirTela(partida.Tab);

                Console.Write("\nOrigem: ");
                Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();

                bool[, ] posicoesPossiveis = partida.Tab.RetornarPeca(origem).MovimentosPossiveis();

                Console.Clear();
                Tela.ImprimirTela(partida.Tab, posicoesPossiveis, partida.Tab.RetornarPeca(origem));

                Console.Write("Destino: ");
                Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();

                partida.ExecutaMovimento(origem, destino);
            }
        }
        catch (TabuleiroException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
