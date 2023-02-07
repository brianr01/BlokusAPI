using NUnit.Framework;
using BlokusAPI;
using BlokusAPI.Services;

namespace UnitTests
{
    public class BoardTests
    {
        [Test]
        public void Get_ReturnsZero()
        {
            // Arrange
            var Board = new Board();

            // Act
            Board.Get();

            // Assert
            Assert.That(Board.Get(), Is.Zero);
        }
    }
}