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

        public int Generation { get; private set; }
        public int Width { get; }
        public int Height { get; }
        private CellType[,] cells;

        public CellType GetCellType(int x, int y)
        {
            return cells[x, y];
        }

        public Result IsValid()
        {
            if (Generation < 1) return Result.Failure("Generation above 1 expected. Found: " + Generation);
            foreach (var cellType in cells)
            {
                if (cellType == CellType.Unknown) return Result.Failure("Wrong CellType found: " + cellType);
            }
            return Result.Success();
        }

        public void Tick()
        {
            Generation++;
            var newCells = new CellType[Width, Height]; 
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    var aliveNeighbours = CountAliveNeighbours(x, y);
                    if (cells[x, y] == CellType.Alive)
                    {
                        if (aliveNeighbours < 2 || aliveNeighbours > 3)
                        {
                            newCells[x, y] = CellType.Dead;
                        }
                        else
                        {
                            newCells[x, y] = CellType.Alive;
                        }
                    }
                    if (cells[x, y] == CellType.Dead)
                    {
                        if (aliveNeighbours == 3)
                        {
                            newCells[x, y] = CellType.Alive;
                        }
                        else
                        {
                            newCells[x, y] = CellType.Dead;
                        }
                    }
                }
            }
            cells = newCells;
        }

        private int CountAliveNeighbours(int cellX, int cellY)
        {
            var aliveNeighbours = 0;
            for (int x = cellX - 1; x <= cellX + 1; x++)
            {
                if (x >= 0 && x < Width)
                {
                    for (int y = cellY - 1; y <= cellY + 1; y++)
                    {
                        if (y>=0 && y < Height)
                        {
                            if(cells[x,y] == CellType.Alive)
                            {
                                aliveNeighbours++;
                            }
                        }
                    }
                }
            }
            return aliveNeighbours;
        }
    }
}