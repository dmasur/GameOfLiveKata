using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace Gol.Application.Tests
{
    public class Parser
    {
        private readonly string[] fileContent;

        public Parser(string[] fileContent)
        {
            this.fileContent = fileContent;
        }

        public Result<Game> ParseGame()
        {
            var generationResult = GetGeneration();
            if (generationResult.IsFailure)
            {
                return generationResult.ConvertFailure<Game>();
            }
            var generation = generationResult.Value;

            var widthResult = GetWidth();
            if (widthResult.IsFailure)
            {
                return widthResult.ConvertFailure<Game>();
            }
            var width = widthResult.Value;

            var heightResult = GetHeigth();
            if (heightResult.IsFailure)
            {
                return heightResult.ConvertFailure<Game>();
            }
            var height = heightResult.Value;

            var cellsResult = GetCells(width, height);
            if (cellsResult.IsFailure)
            {
                return cellsResult.ConvertFailure<Game>();
            }
            var cells = cellsResult.Value;
            return Game.CreateGame(generation, width, height, cells);
        }

        private Result<CellType[,]> GetCells(int width, int height)
        {
            var cells = new CellType[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var cellChar = fileContent[y + 2][x];
                    var cellType = ParseCellType(cellChar);
                    if (cellType == CellType.Unknown)
                    {
                        return Result.Failure<CellType[,]>($"Invalid CellType found in Cell ({x},{y}): {cellChar}");
                    }
                    cells[x, y] = cellType;
                }
            }
            return Result.Success(cells);
        }

        private CellType ParseCellType(char cellChar)
        {
            switch (cellChar)
            {
                case '.':
                    return CellType.Dead;

                case '*':
                    return CellType.Alive;

                default:
                    return CellType.Unknown;
            }
        }

        private Result<int> GetWidth()
        {
            var generationMatch = Regex.Match(fileContent[1], @"(\d) (\d)");
            var widthString = generationMatch.Groups[2].Value;
            var width = int.Parse(widthString);

            for (int i = 2; i < fileContent.Length; i++)
            {
                var actualWidth = fileContent[i].Length;
                if (actualWidth != width)
                {
                    return Result.Failure<int>($"Wrong width in line {i - 2}: {actualWidth} found, but {width} expected");
                }
            }
            return Result.Success(width);
        }

        private Result<int> GetHeigth()
        {
            var generationMatch = Regex.Match(fileContent[1], @"(\d) (\d)");
            var heightString = generationMatch.Groups[1].Value;
            var height = int.Parse(heightString);

            var actualheight = fileContent.Length - 2;
            if (actualheight != height)
            {
                return Result.Failure<int>($"Wrong height: {actualheight} found, but {height} expected");
            }
            return Result.Success(height);
        }

        private Result<int> GetGeneration()
        {
            if (fileContent.Length == 0)
            {
                return Result.Failure<int>("No Generationline found");
            }
            var generationMatch = Regex.Match(fileContent[0], @"Generation (\d*):");
            if (!generationMatch.Success)
            {
                return Result.Failure<int>("Generation not found");
            }
            var generationString = generationMatch.Groups[1].Value;
            var generation = int.Parse(generationString);
            if (generation < 1)
            {
                return Result.Failure<int>("Generation must be 1 or higher");
            }
            return Result.Success(generation);
        }
    }
}