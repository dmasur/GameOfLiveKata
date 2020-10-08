using Gol.Application.Enums;
using Gol.Application.Services;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Gol.Application.Tests
{
    public class ParserTests
    {
        [Fact]
        public async Task Input1()
        {
            var fileContent = await File.ReadAllLinesAsync("Files/Input1.txt");
            var parser = new Parser(fileContent);
            var gameResult = parser.ParseGame();
            var game = gameResult.Value;

            Assert.Equal(1, game.Generation);
            Assert.Equal(8, game.Width);
            Assert.Equal(4, game.Height);
            Assert.Equal(CellType.Dead, game.GetCellType(0, 0));
            Assert.Equal(CellType.Alive, game.GetCellType(4, 1));
        }

        [Fact]
        public async Task Generation100()
        {
            var fileContent = await File.ReadAllLinesAsync("Files/Generation100.txt");
            var parser = new Parser(fileContent);
            var gameResult = parser.ParseGame();
            var game = gameResult.Value;

            Assert.Equal(100, game.Generation);
        }

        [Fact]
        public async Task EmptyInput()
        {
            var fileContent = await File.ReadAllLinesAsync("Files/EmptyInput.txt");
            var parser = new Parser(fileContent);
            var gameResult = parser.ParseGame();

            Assert.Equal("No Generationline found", gameResult.Error);
        }

        [Fact]
        public async Task InvalidGeneration()
        {
            var fileContent = await File.ReadAllLinesAsync("Files/InvalidGeneration.txt");
            var parser = new Parser(fileContent);
            var gameResult = parser.ParseGame();

            Assert.Equal("Generation not found", gameResult.Error);
        }

        [Fact]
        public async Task InvalidGeneration2()
        {
            var fileContent = await File.ReadAllLinesAsync("Files/InvalidGeneration 2.txt");
            var parser = new Parser(fileContent);
            var gameResult = parser.ParseGame();

            Assert.Equal("Generation must be 1 or higher", gameResult.Error);
        }

        [Fact]
        public async Task WrongWidthTooBig()
        {
            var fileContent = await File.ReadAllLinesAsync("Files/WrongWidthTooBig.txt");
            var parser = new Parser(fileContent);
            var gameResult = parser.ParseGame();

            Assert.Equal("Wrong width in line 0: 8 found, but 9 expected", gameResult.Error);
        }

        [Fact]
        public async Task WrongHeightTooBig()
        {
            var fileContent = await File.ReadAllLinesAsync("Files/WrongHeightTooBig.txt");
            var parser = new Parser(fileContent);
            var gameResult = parser.ParseGame();

            Assert.Equal("Wrong height: 4 found, but 5 expected", gameResult.Error);
        }

        [Fact]
        public async Task InvalidChar()
        {
            var fileContent = await File.ReadAllLinesAsync("Files/InvalidChar.txt");
            var parser = new Parser(fileContent);
            var gameResult = parser.ParseGame();

            Assert.Equal("Invalid CellType found in Cell (0,0): a", gameResult.Error);
        }
    }
}