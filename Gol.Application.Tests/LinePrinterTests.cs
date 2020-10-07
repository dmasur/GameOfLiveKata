using Gol.Application.Services;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Gol.Application.Tests
{
    public class LinePrinterTests
    {
        [Fact]
        public async Task Input1Output()
        {
            var fileContent = await File.ReadAllLinesAsync("Files/Input1.txt");
            var parser = new Parser(fileContent);
            var gameResult = parser.ParseGame();
            var game = gameResult.Value;

            var linePrinter = new LinePrinter();
            var lines = linePrinter.GetLines(game);
            Assert.Equal(6, lines.Count);
            Assert.Equal("Generation 1:", lines[0]);
            Assert.Equal("4 8", lines[1]);
            Assert.Equal("........", lines[2]);
            Assert.Equal("....*...", lines[3]);
            Assert.Equal("...**...", lines[4]);
            Assert.Equal("........", lines[5]);
        }
    }
}