using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Gol.Application.Tests
{
    public class GameTests
    {
        [Fact]
        public void IsValidEmptySuccess()
        {
            var game = new Game(1, 0, 0, new CellType[,] { });

            Assert.Equal(1, game.Generation);
            Assert.Equal(0, game.Width);
            Assert.Equal(0, game.Height);
            Assert.True(game.IsValid().IsSuccess);
        }

        [Fact]
        public async Task IsValidWithInput1()
        {
            var fileContent = await File.ReadAllLinesAsync("Files/Input1.txt");
            var parser = new Parser(fileContent);
            var gameResult = parser.ParseGame();
            var game = gameResult.Value;

            Assert.Equal(1, game.Generation);
            Assert.Equal(8, game.Width);
            Assert.Equal(4, game.Height);
            Assert.True(game.IsValid().IsSuccess);
        }

        [Fact]
        public async Task NextGeneration()
        {
            var fileContent = await File.ReadAllLinesAsync("Files/Input1.txt");
            var parser = new Parser(fileContent);
            var gameResult = parser.ParseGame();
            var game = gameResult.Value;

            game.Tick();
            Assert.Equal(2, game.Generation);
            Assert.True(game.IsValid().IsSuccess);
            Assert.Equal(CellType.Alive, game.GetCellType(3, 1));
            Assert.Equal(CellType.Alive, game.GetCellType(3, 2));
            Assert.Equal(CellType.Alive, game.GetCellType(4, 1));
            Assert.Equal(CellType.Alive, game.GetCellType(4, 2));
        }
    }
}
