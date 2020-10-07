using CSharpFunctionalExtensions;

namespace Gol.Application.Tests
{
    public class Game
    {
        public Game(int generation, int width, int height, CellType[,] cells)
        {
            Generation = generation;
            Width = width;
            Height = height;
            this.cells = cells;
        }

        public int Generation { get; }
        public int Width { get; }
        public int Height { get; }
        private readonly CellType[,] cells;

        public CellType GetCellType(int x, int y)
        {
            return cells[x, y];
        }

        public Result IsValid()
        {
            if (cells == null) return Result.Failure("No Cells found");
            if (Generation < 1) return Result.Failure("Generation above 1 expected. Found: " + Generation);
            foreach (var cellType in cells)
            {
                if (cellType == CellType.Unknown) return Result.Failure("Wrong CellType found: " + cellType);
            }
            return Result.Success();
        }

        public Game GenerateNextGeneration()
        {
            var newCells = new CellType[Width, Height];
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    var aliveNeighbours = CountAliveNeighbours(x, y);
                    newCells[x, y] = CalculateNewCell(cells[x, y], aliveNeighbours);
                }
            }
            return new Game(Generation + 1, Width, Height, newCells);
        }

        private int CountAliveNeighbours(int cellX, int cellY)
        {
            var aliveNeighbours = 0;
            for (int x = cellX - 1; x <= cellX + 1; x++)
            {
                for (int y = cellY - 1; y <= cellY + 1; y++)
                {
                    if (IsValidAndAliveNeigbour(x, y, cellX, cellY))
                    {
                        aliveNeighbours++;
                    }
                }
            }

            return aliveNeighbours;
        }

        private bool IsValidAndAliveNeigbour(int x, int y, int cellX, int cellY)
        {
            var isCurrentCell = cellX == x && cellY == y;
            return !isCurrentCell && IsValid(x, y) && IsAlive(x, y);
        }

        private bool IsValid(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        private bool IsAlive(int x, int y)
        {
            return cells[x, y] == CellType.Alive;
        }

        private CellType CalculateNewCell(CellType currentCellType, int aliveNeighbours)
        {
            switch (currentCellType)
            {
                case CellType.Alive:
                    switch (aliveNeighbours)
                    {
                        case 2:
                        case 3:
                            return CellType.Alive;

                        default:
                            return CellType.Dead;
                    }

                case CellType.Dead:
                    switch (aliveNeighbours)
                    {
                        case 3:
                            return CellType.Alive;

                        default:
                            return CellType.Dead;
                    }
                default:
                    return currentCellType;
            }
        }
    }
}