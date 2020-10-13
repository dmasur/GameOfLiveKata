using Gol.Application.Enums;
using Gol.Application.Models;
using Gol.Application.Services;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Gol.Application.Tests
{
    public class GameTests
    {
        [Fact]
        public void InvalidCells()
        {
            var gameResult = Game.CreateGame(1, 0, 0, null);
            Assert.Equal("No Cells found", gameResult.Error);
        }

        [Fact]
        public void InvalidGeneration()
        {
            var gameResult = Game.CreateGame(-1, 0, 0, new CellType[0, 0]);
            Assert.Equal("Generation above 1 expected. Found: -1", gameResult.Error);
        }

        [Fact]
        public void IsValidEmptySuccess()
        {
            var game = Game.CreateGame(1, 0, 0, new CellType[,] { }).Value;

            Assert.Equal(1, game.Generation);
            Assert.Equal(0, game.Width);
            Assert.Equal(0, game.Height);
        }

        [Fact]
        public void IsValidFailWithInvalidCellType()
        {
            var gameResult = Game.CreateGame(1, 0, 0, new CellType[,] { { CellType.Unknown } });

            Assert.True(gameResult.IsFailure);
            Assert.Equal("Wrong CellType found: Unknown", gameResult.Error);
        }

        [Fact]
        public async Task IsValidWithInput1Async()
        {
            var fileContent = await File.ReadAllLinesAsync("Files/Input1.txt");
            var parser = new Parser(fileContent);
            var gameResult = parser.ParseGame();
            var game = gameResult.Value;

            Assert.Equal(1, game.Generation);
            Assert.Equal(8, game.Width);
            Assert.Equal(4, game.Height);
        }

        [Fact]
        public async Task NextGenerationAsync()
        {
            var fileContent = await File.ReadAllLinesAsync("Files/Input1.txt");
            var parser = new Parser(fileContent);
            var gameResult = parser.ParseGame();
            var game = gameResult.Value;

            game = game.GenerateNextGeneration();
            Assert.Equal(2, game.Generation);
            Assert.Equal(CellType.Alive, game.GetCellType(3, 1));
            Assert.Equal(CellType.Alive, game.GetCellType(3, 2));
            Assert.Equal(CellType.Alive, game.GetCellType(4, 1));
            Assert.Equal(CellType.Alive, game.GetCellType(4, 2));
        }

        [Fact]
        public async Task NextGenerationGliderAsync()
        {
            var fileContent = await File.ReadAllLinesAsync("Files/Glider.txt");
            var parser = new Parser(fileContent);
            var gameResult = parser.ParseGame();
            var game = gameResult.Value;

            game = game.GenerateNextGeneration();
            Assert.Equal(2, game.Generation);
            Assert.Equal(CellType.Dead, game.GetCellType(0, 0));
            Assert.Equal(CellType.Dead, game.GetCellType(1, 0));
            Assert.Equal(CellType.Dead, game.GetCellType(2, 0));
            Assert.Equal(CellType.Dead, game.GetCellType(3, 0));
            Assert.Equal(CellType.Dead, game.GetCellType(4, 0));
            Assert.Equal(CellType.Dead, game.GetCellType(5, 0));
            Assert.Equal(CellType.Dead, game.GetCellType(6, 0));
            Assert.Equal(CellType.Dead, game.GetCellType(7, 0));

            Assert.Equal(CellType.Alive, game.GetCellType(0, 1));
            Assert.Equal(CellType.Dead, game.GetCellType(1, 1));
            Assert.Equal(CellType.Alive, game.GetCellType(2, 1));
            Assert.Equal(CellType.Dead, game.GetCellType(3, 1));
            Assert.Equal(CellType.Dead, game.GetCellType(4, 1));
            Assert.Equal(CellType.Dead, game.GetCellType(5, 1));
            Assert.Equal(CellType.Dead, game.GetCellType(6, 1));
            Assert.Equal(CellType.Dead, game.GetCellType(7, 1));

            Assert.Equal(CellType.Dead, game.GetCellType(0, 2));
            Assert.Equal(CellType.Alive, game.GetCellType(1, 2));
            Assert.Equal(CellType.Alive, game.GetCellType(2, 2));
            Assert.Equal(CellType.Dead, game.GetCellType(3, 2));
            Assert.Equal(CellType.Dead, game.GetCellType(4, 2));
            Assert.Equal(CellType.Dead, game.GetCellType(5, 2));
            Assert.Equal(CellType.Dead, game.GetCellType(6, 2));
            Assert.Equal(CellType.Dead, game.GetCellType(7, 2));

            Assert.Equal(CellType.Dead, game.GetCellType(0, 3));
            Assert.Equal(CellType.Alive, game.GetCellType(1, 3));
            Assert.Equal(CellType.Dead, game.GetCellType(2, 3));
            Assert.Equal(CellType.Dead, game.GetCellType(3, 3));
            Assert.Equal(CellType.Dead, game.GetCellType(4, 3));
            Assert.Equal(CellType.Dead, game.GetCellType(5, 3));
            Assert.Equal(CellType.Dead, game.GetCellType(6, 3));
            Assert.Equal(CellType.Dead, game.GetCellType(7, 3));
        }
    }
}