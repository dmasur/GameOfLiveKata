using Gol.Application.Models;
using Gol.Application.Services;
using System;
using System.IO;

namespace Gol.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
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
                    game = game.GenerateNextGeneration();
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
            var lines = LinePrinter.GetLines(game);
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }
    }
}