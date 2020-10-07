using Gol.Application.Services;
using Gol.Application.Tests;
using System;
using System.IO;

namespace Gol.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No Input found");
                return;
            }
            var lines = File.ReadAllLines(args[0]);
            var parser = new Parser(lines);
            var gameResult = parser.ParseGame();
            if (gameResult.IsFailure)
            {
                Console.WriteLine("Error on Parsing: " + gameResult.Error);
                return;
            }
            var game = gameResult.Value;
            var linePrinter = new LinePrinter();
            while (true)
            {
                PrintGame(game, linePrinter);
                var inputChar = Console.ReadKey();
                if (inputChar.KeyChar == ' ')
                {
                    game.Tick();
                    Console.Clear();
                }
                else
                {
                    return;
                }
            }
        }

        private static void PrintGame(Game game, LinePrinter linePrinter)
        {
            var lines = linePrinter.GetLines(game);
            Console.WriteLine(lines);
        }
    }
}
