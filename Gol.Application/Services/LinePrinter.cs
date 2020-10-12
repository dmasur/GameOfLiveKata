using Gol.Application.Enums;
using Gol.Application.Models;
using System.Collections.Generic;
using System.Text;

namespace Gol.Application.Services
{
    public class LinePrinter
    {
        public static List<string> GetLines(Game game)
        {
            var lines = new List<string>
            {
                $"Generation {game.Generation}:",
                $"{game.Height} {game.Width}"
            };
            for (int y = 0; y < game.Height; y++)
            {
                var builder = new StringBuilder();
                for (int x = 0; x < game.Width; x++)
                {
                    var cellType = game.GetCellType(x, y);
                    var cellChar = GetCellChar(cellType);
                    builder.Append(cellChar);
                }
                lines.Add(builder.ToString());
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