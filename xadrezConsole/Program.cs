﻿namespace xadrezConsole;
using Tabuleiro;
using Xadrez;

public class Program
{
    static void Main(string[] args)
    {
        PosicaoXadrez pos = new PosicaoXadrez('c', 7);

        Console.WriteLine(pos);
        Console.WriteLine(pos.ToPosicao());
    }
}
