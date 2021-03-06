﻿using CSharpFunctionalExtensions;
using Gol.Application.Enums;

namespace Gol.Application.Models
{
    public sealed class Game
    {
        private readonly CellType[,] cells;

        private Game(int generation, int width, int height, CellType[,] cells)
        {
            Generation = generation;
            Width = width;
            Height = height;
            this.cells = cells;
        }

        public int Generation { get; }
        public int Height { get; }
        public int Width { get; }

        public static Result<Game> CreateGame(int generation, int width, int height, CellType[,] cells)
        {
            var game = new Game(generation, width, height, cells);
            return ValidateGame(game);
        }

        public Game GenerateNextGeneration()
        {
            var newCells = new CellType[Width, Height];
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    newCells[x, y] = CalculateNewCell(cells[x, y], CountAliveNeighbours(x, y));
                }
            }
            return new Game(Generation + 1, Width, Height, newCells);
        }

        public CellType GetCellType(int x, int y) => cells[x, y];

        private static CellType CalculateNewCell(CellType currentCellType, int aliveNeighbours)
        {
            return aliveNeighbours switch
            {
                2 => currentCellType == CellType.Alive ? CellType.Alive : CellType.Dead,
                3 => CellType.Alive,
                _ => CellType.Dead,
            };
        }

        private static Result<Game> ValidateGame(Game game)
        {
            if (game.cells == null) return Result.Failure<Game>("No Cells found");
            if (game.Generation < 1) return Result.Failure<Game>("Generation above 1 expected. Found: " + game.Generation);
            foreach (var cellType in game.cells)
            {
                if (cellType == CellType.Unknown) return Result.Failure<Game>("Wrong CellType found: " + cellType);
            }
            return Result.Success(game);
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

        private bool IsAlive(int x, int y)
        {
            return cells[x, y] == CellType.Alive;
        }

        private bool IsValid(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        private bool IsValidAndAliveNeigbour(int x, int y, int cellX, int cellY)
        {
            var isCurrentCell = cellX == x && cellY == y;
            return !isCurrentCell && IsValid(x, y) && IsAlive(x, y);
        }
    }
}