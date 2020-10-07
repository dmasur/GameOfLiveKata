using Gol.Application.Tests;
using System.Collections.Generic;

namespace Gol.Application.Services
{
    public class LinePrinter
    {
        public List<string> GetLines(Game game)
        {
            var lines = new List<string>()
            {
                $"Generation {game.Generation}:",
                $"{game.Height} {game.Width}"
            };
            for (int y = 0; y < game.Height; y++)
            {
                var line = "";
                for (int x = 0; x < game.Width; x++)
                {
                    var cellType = game.GetCellType(x, y);
                    var cellChar = GetCellChar(cellType);
                    line += cellChar;
                }
                lines.Add(line);
            }
            return lines;
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
