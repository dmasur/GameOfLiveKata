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
            while (true)
            {
                PrintGame(game);
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

        private static void PrintGame(Game game)
        {
            Console.WriteLine($"Generation {game.Generation}:");
            Console.WriteLine($"{game.Height} {game.Width}");
            for (int y = 0; y < game.Height; y++)
            {
                for (int x = 0; x < game.Width; x++)
                {
                    var cellType = game.GetCellType(x, y);
                    var cellChar = GetCellChar(cellType);
                    Console.Write(cellChar);
                }
                Console.WriteLine();
            }
        }

        private static char GetCellChar(CellType cellType)
        {
            switch (cellType)
            {
                case CellType.Alive:
                    {
                        return '*';
                    }
                case CellType.Dead:
                    {
                        return '.';
                    }
                default:
                    {
                        return '?';
                    }
            }
        }
    }
}
