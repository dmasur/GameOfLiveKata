using Gol.Application.Tests;
using System.Text;

namespace Gol.Application.Services
{
    public class LinePrinter
    {
        public string GetLines(Game game)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Generation {game.Generation}:");
            stringBuilder.AppendLine($"{game.Height} {game.Width}");
            for (int y = 0; y < game.Height; y++)
            {
                for (int x = 0; x < game.Width; x++)
                {
                    var cellType = game.GetCellType(x, y);
                    var cellChar = GetCellChar(cellType);
                    stringBuilder.Append(cellChar);
                }
                stringBuilder.AppendLine();
            }
            return stringBuilder.ToString();
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
