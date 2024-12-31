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
                try
                {
                    Console.Clear();
                    Tela.ImprimirTela(partida.Tab);
                    Console.WriteLine();
                    Console.WriteLine("Turno: {0}", partida.TurnoGeral);
                    int turnoJogAtual = partida.JogadorAtual == Cor.Branca ? partida.TurnoJog1 : partida.TurnoJog2;
                    Console.WriteLine("Peças {0}s: turno {1}", partida.JogadorAtual, turnoJogAtual);
                    Console.WriteLine("Aguardando jogada...");

                    Console.Write("\nOrigem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
                    partida.ValidarPosicaoDeOrigem(origem);

                    bool[,] posicoesPossiveis = partida.Tab.RetornarPeca(origem).MovimentosPossiveis();

                    Console.Clear();
                    Tela.ImprimirTela(partida.Tab, posicoesPossiveis, partida.Tab.RetornarPeca(origem));

                    Console.WriteLine();
                    Console.Write("Destino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
                    partida.ValidarPosicaoDeDestino(origem, destino);

                    partida.RealizaJogada(origem, destino);
                }
                catch (TabuleiroException ex)
                {
                    Console.WriteLine(ex.Message);
                    ConsoleKey teclaEscolhida;
                    do
                    {
                        Console.WriteLine("Aperte 'ENTER' para continuar:");
                        teclaEscolhida = Console.ReadKey().Key;
                        if (teclaEscolhida != ConsoleKey.Enter)
                            Console.Clear();
                    } while (teclaEscolhida != ConsoleKey.Enter);
                }
            }
        }
        catch (TabuleiroException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
