using BlokusAPI.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using static BlokusAPI.Services.Board;
using static BlokusAPI.Services.Pieces;

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

        [Test]
        public void Get_ReturnsBlue()
        {
            // Arrange
            var Board = new Board();
            Board.BoardValues[0, 0] = Board.Colors.Blue;

            // Act
            var actual = Board.Get();

            // Assert
            Assert.That(actual, Is.EqualTo((int)Board.Colors.Blue));
        }

        [Test]
        [TestCase(0, 0, Board.Colors.Empty, 5, 5, Board.Colors.Blue, 20, true)] // Check with empty board.
        [TestCase(0, 0, Board.Colors.Empty, -1, 0, Board.Colors.Blue, 20, false)] // Off of the Negative X side of the board.
        [TestCase(0, 0, Board.Colors.Empty, 0, -1, Board.Colors.Blue, 20, false)] // Off of the Negative Y side of the board.
        [TestCase(0, 0, Board.Colors.Empty, 20, 0, Board.Colors.Blue, 20, false)] // Off of the Positive X side of the board.
        [TestCase(0, 0, Board.Colors.Empty, 0, 20, Board.Colors.Blue, 20, false)] // Off of the Positive Y side of the board.
        [TestCase(0, 0, Board.Colors.Empty, 0, 0, Board.Colors.Blue, 20, true)] // Negative X and Y corner with empty board.
        [TestCase(0, 0, Board.Colors.Empty, 19, 19, Board.Colors.Blue, 20, true)] // Positive X and Y corner with empty board.
        [TestCase(0, 0, Board.Colors.Empty, 19, 0, Board.Colors.Blue, 20, true)] // Positive X and Negative Y corner with empty board.
        [TestCase(0, 0, Board.Colors.Empty, 0, 19, Board.Colors.Blue, 20, true)] // Negative X and Positive Y corner with empty board.
        [TestCase(5, 5, Board.Colors.Blue, 5, 5, Board.Colors.Blue, 20, false)] // Already populated location.
        [TestCase(4, 5, Board.Colors.Blue, 5, 5, Board.Colors.Blue, 20, false)] // Negative X side populated with same color.
        [TestCase(4, 5, Board.Colors.Red, 5, 5, Board.Colors.Blue, 20, true)] // Negative X side populated with different color.
        [TestCase(6, 5, Board.Colors.Blue, 5, 5, Board.Colors.Blue, 20, false)] // Positive X side populated with same color.
        [TestCase(6, 5, Board.Colors.Red, 5, 5, Board.Colors.Blue, 20, true)] // Positive X side populated with different color.
        [TestCase(5, 4, Board.Colors.Blue, 5, 5, Board.Colors.Blue, 20, false)] // Negative Y side populated with same color.
        [TestCase(5, 4, Board.Colors.Red, 5, 5, Board.Colors.Blue, 20, true)] // Negative Y side populated with different color.
        [TestCase(5, 6, Board.Colors.Blue, 5, 5, Board.Colors.Blue, 20, false)] // Positive Y side populated with same color.
        [TestCase(5, 6, Board.Colors.Red, 5, 5, Board.Colors.Blue, 20, true)] // Positive Y side populated with different color.
        public void ValidSquarePlacement(int pieceX, int pieceY, Board.Colors pieceColor, int locationX, int locationY, Board.Colors locationColor, int boardSize, bool expected)
        {
            // Arrange
            var Board = new Board(boardSize);
            Board.BoardValues[pieceX, pieceY] = pieceColor;

            // Act
            var actual = Board.ValidSquarePlacement(locationX, locationY, locationColor);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ValidSquarePlacements_ReturnsTrue()
        {
            // Arrange
            var Board = new Board(3);

            Colors e = Colors.Empty;
            Colors r = Colors.Red;
            Colors g = Colors.Green;
            Colors b = Colors.Blue;
            Colors y = Colors.Yellow;

            Board.BoardValues = new Colors[,]
            {
                { b, b, e },
                { e, e, y },
                { r, e, g},
            };

            // Valid location.
            int location1X = 0;
            int location1Y = 2;

            // Valid location.
            int location2X = 1;
            int location2Y = 1;
            Board.Colors selectedColor = Board.Colors.Red;

            var locations = new List<int[]>
            {
                new int[] { location1X, location1Y },
                new int[] { location2X, location2Y },
            };
     
            // Act
            var actual = Board.ValidSquarePlacements(locations, selectedColor);

            // Assert
            Assert.That(actual, Is.True);
        }

        [Test]
        public void ValidSquarePlacements_ReturnsFalse_WithPopulatedLocation()
        {
            // Arrange
            var Board = new Board(3);

            Colors e = Colors.Empty;
            Colors r = Colors.Red;
            Colors g = Colors.Green;
            Colors b = Colors.Blue;
            Colors y = Colors.Yellow;

            Board.BoardValues = new Colors[,]
            {
                { b, b, e },
                { e, e, y },
                { r, e, g},
            };

            // Unpopulated location. (valid)
            int location1X = 1;
            int location1Y = 1;

            // Populated location. (invalid)
            int location2X = 2;
            int location2Y = 2;
            Board.Colors selectedColor = Board.Colors.Red;

            var locations = new List<int[]>
            {
                new int[] { location1X, location1Y },
                new int[] { location2X, location2Y },
            };

            // Act
            var actual = Board.ValidSquarePlacements(locations, selectedColor);

            // Assert
            Assert.That(actual, Is.False);
        }

        [Test]
        public void ValidSquarePlacements_ReturnsFalse_WithSameColorEdge()
        {
            // Arrange
            var Board = new Board(3);

            Colors e = Colors.Empty;
            Colors r = Colors.Red;
            Colors g = Colors.Green;
            Colors b = Colors.Blue;
            Colors y = Colors.Yellow;

            Board.BoardValues = new Colors[,]
            {
                { b, b, e },
                { e, e, y },
                { r, e, g},
            };

            // Unpopulated location. (valid)
            int location1X = 0;
            int location1Y = 2;

            // Unpopulated location with same color edge. (invalid)
            int location2X = 1;
            int location2Y = 2;
            Board.Colors selectedColor = Board.Colors.Red;

            var locations = new List<int[]>
            {
                new int[] { location1X, location1Y },
                new int[] { location2X, location2Y },
            };

            // Act
            var actual = Board.ValidSquarePlacements(locations, selectedColor);

            // Assert
            Assert.That(actual, Is.False);
        }

        [Test]
        [TestCase(20, 5, 5, true)]
        [TestCase(20, 0, 0, true)] // Negative X and Y corner.
        [TestCase(20, 19, 19, true)] // Positive X and Y corner.
        [TestCase(20, 0, 19, true)] // Negative X and Positive Y corner.
        [TestCase(20, 19, 0, true)] // Positive X and Negative Y corner.
        [TestCase(20, -1, 0, false)] // Past Negative X bound.
        [TestCase(20, 20, 0, false)] // Past Positive X bound.
        [TestCase(20, 0, -1, false)] // Past Negative Y bound.
        [TestCase(20, 0, 20, false)] // Past Positive Y bound.
        [TestCase(20, -1, -1, false)] // Past Negative X and Y bound.
        [TestCase(20, 20, 20, false)] // Past Positive X and Y bound.
        [TestCase(20, -1, 20, false)] // Past Negative X and Positive Y bound.
        [TestCase(20, 20, -1, false)] // Past Positive X and Negative Y bound.
        [TestCase(1, 0, 0, true)] // Small board.
        [TestCase(1, 1, 0, false)] // Small board out of Positive X bound.
        public void SquareIsOnBoard(int boardSize, int locationX, int locationY, bool expected)
        {
            // Arrange
            var Board = new Board(boardSize);

            // Act
            var actual = Board.SquareIsOnBoard(locationX, locationY);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(Board.Colors.Empty)]
        [TestCase(Board.Colors.Blue)]
        [TestCase(Board.Colors.Red)]
        [TestCase(Board.Colors.Yellow)]
        [TestCase(Board.Colors.Green)]
        public void GetLocationColor(Board.Colors pieceColor)
        {
            // Arrange
            var Board = new Board(1);

            int x = 0;
            int y = 0;

            Board.BoardValues[x, y] = pieceColor;

            // Act
            var actual = Board.GetLocationColor(x, y);

            // Assert
            Assert.That(actual, Is.EqualTo(pieceColor));
        }

        [Test]
        [TestCase(Board.Colors.Empty, true)]
        [TestCase(Board.Colors.Blue, false)]
        [TestCase(Board.Colors.Red, false)]
        [TestCase(Board.Colors.Yellow, false)]
        [TestCase(Board.Colors.Green, false)]
        public void GetIsLocationEmpty(Board.Colors pieceColor, bool expected)
        {
            // Arrange
            var Board = new Board(1);

            int x = 0;
            int y = 0;

            Board.BoardValues[x, y] = pieceColor;

            // Act
            var actual = Board.GetIsLocationEmpty(x, y);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(Board.Colors.Empty, Board.Colors.Empty, true)]
        [TestCase(Board.Colors.Blue, Board.Colors.Empty, false)]
        [TestCase(Board.Colors.Red, Board.Colors.Yellow, false)]
        [TestCase(Board.Colors.Yellow, Board.Colors.Green, false)]
        [TestCase(Board.Colors.Green, Board.Colors.Green, true)]
        public void GetLocationHasColor(Board.Colors pieceColor, Board.Colors colorToCheck, bool expected)
        {
            // Arrange
            var Board = new Board(1);

            int x = 0;
            int y = 0;

            Board.BoardValues[x, y] = pieceColor;

            // Act
            var actual = Board.GetLocationHasColor(x, y, colorToCheck);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(20, 80)]
        [TestCase(2, 8)]
        public void GetPerimeterLength(int boardSize, int expected)
        {
            // Arrange
            var Board = new Board(boardSize);

            // Act
            var actual = Board.GetPerimeterLength();

            Assert.That(actual, Is.EqualTo(expected));
            
        }

        [Test]
        [TestCase(20, 76)]
        [TestCase(2, 4)]
        public void GetSquarePerimeterCount(int boardSize, int expected)
        {
            // Arrange
            var Board = new Board(boardSize);

            // Act
            var actual = Board.GetSquarePerimeterCount();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(Board.Colors.Empty, Board.Colors.Empty, false)] // None populated.
        [TestCase(Board.Colors.Empty, Board.Colors.Blue, true)] // One populated.
        [TestCase(Board.Colors.Red, Board.Colors.Blue, true)] // All populated.
        public void IsOneValuePopuloated(Board.Colors color1, Board.Colors color2, bool expected)
        {
            // Arrange
            var Board = new Board();
            var colors = new List<Board.Colors> { color1, color2 };

            // Act
            var actual = Board.IsOneValuePopuloated(colors);

            Assert.That(actual, Is.EqualTo(expected));

        }

        [Test]
        [TestCase(Board.Colors.Empty, Board.Colors.Empty, true)] // None populated.
        [TestCase(Board.Colors.Empty, Board.Colors.Blue, false)] // One populated.
        [TestCase(Board.Colors.Red, Board.Colors.Blue, false)] // All populated.
        public void AreAllValuesEmpty(Board.Colors color1, Board.Colors color2, bool expected)
        {
            // Arrange
            var Board = new Board();
            var colors = new List<Board.Colors> { color1, color2 };

            // Act
            var actual = Board.AreAllValuesEmpty(colors);

            Assert.That(actual, Is.EqualTo(expected));
        }

        private static readonly object[] _sourceGetEdgeLocations =
        {
            // Middle of the board.
            new object[] {20, 5, 5, new List<int[]> { new int[] { 6, 5 }, new int[] { 4, 5 }, new int[] { 5, 6 }, new int[] { 5, 4 } } },
            // Negative X side of the board.
            new object[] {20, 0, 1, new List<int[]> { new int[] { 1, 1 }, new int[] { 0, 2 }, new int[] { 0, 0 } } },
            // Positive X side of the board.
            new object[] {20, 19, 1, new List<int[]> { new int[] { 18, 1 }, new int[] { 19, 2 }, new int[] { 19, 0 } } },
            // Negative Y side of the board.
            new object[] {20, 1, 0, new List<int[]> { new int[] { 2, 0 }, new int[] { 0, 0 }, new int[] { 1, 1 } } },
            // Positive Y sode of the board.
            new object[] {20, 1, 19, new List<int[]> { new int[] { 2, 19 }, new int[] { 0, 19 }, new int[] { 1, 18 } } },
            // Negative X and Y corner of the board.
            new object[] {20, 0, 0, new List<int[]> { new int[] { 1, 0 }, new int[] { 0, 1 }} },
            // Positive X and Y corner of the board.
            new object[] {20, 19, 19, new List<int[]> { new int[] { 18, 19 }, new int[] { 19, 18 }} },
            // Positive X and Negative Y corner of the board.
            new object[] {20, 19, 0, new List<int[]> { new int[] { 18, 0 }, new int[] { 19, 1 }} },
            // Negative X and Positive Y corner of the board.
            new object[] {20, 0, 19, new List<int[]> { new int[] { 1, 19 }, new int[] { 0, 18 }} }
        };

        [Test]
        [TestCaseSource("_sourceGetEdgeLocations")]
        public void GetEdgeLocations(
            int boardSize,
            int locationX,
            int locationY,
            List<int[]> expected
        ) {
            // Arrange
            var Board = new Board(boardSize);

            // Act
            var actual = Board.GetEdgeLocations(locationX, locationY);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        private static readonly object[] _sourceGetCornerLocations =
        {
            // Middle of the board.
            new object[] {20, 5, 5, new List<int[]> { new int[] { 6, 6 }, new int[] { 4, 4 }, new int[] { 4, 6 }, new int[] { 6, 4 } } },
            // Negative X side of the board.
            new object[] {20, 0, 1, new List<int[]> { new int[] { 1, 2 }, new int[] { 1, 0 } } },
            // Positive X side of the board.
            new object[] {20, 19, 1, new List<int[]> { new int[] { 18, 0 }, new int[] { 18, 2 } } },
            // Negative Y side of the board.
            new object[] {20, 1, 0, new List<int[]> { new int[] { 2, 1 }, new int[] { 0, 1 } } },
            // Positive Y sode of the board.
            new object[] {20, 1, 19, new List<int[]> { new int[] { 0, 18 }, new int[] { 2, 18 } } },
            // Negative X and Y corner of the board.
            new object[] {20, 0, 0, new List<int[]> { new int[] { 1, 1 } } },
            // Positive X and Y corner of the board.
            new object[] {20, 19, 19, new List<int[]> { new int[] { 18, 18 } } },
            // Positive X and Negative Y corner of the board.
            new object[] {20, 19, 0, new List<int[]> { new int[] { 18, 1 } } },
            // Negative X and Positive Y corner of the board.
            new object[] {20, 0, 19, new List<int[]> { new int[] { 1, 18 } } }
        };

        [Test]
        [TestCaseSource("_sourceGetCornerLocations")]
        public void GetCornerLocations(
            int boardSize,
            int locationX,
            int locationY,
            List<int[]> expected
        )
        {
            // Arrange
            var Board = new Board(boardSize);

            // Act
            var actual = Board.GetCornerLocations(locationX, locationY);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void UpdateEmptySquare()
        {
            // Arrange
            var Board = new Board(20);
            int x = 5;
            int y = 6;
            var color = Board.Colors.Blue;

            // Act
            Board.UpdateEmptySquare(x, y, color);

            // Assert
            Assert.That(Board.BoardValues[x, y], Is.EqualTo(color));
        }

        [Test]
        public void UpdateSquare_WithEmptyLocation()
        {
            // Arrange
            var Board = new Board(20);
            var color = Board.Colors.Yellow;
            var locationX = 5;
            var locationY = 19;

            // Act
            var actual = Board.UpdateSquare(locationX, locationY, color);

            // Assert
            Assert.That(actual, Is.True);
            Assert.That(Board.BoardValues[locationX, locationY], Is.EqualTo(color));
        }

        [Test]
        public void UpdateSquare_WithPopulatedLocation()
        {
            // Arrange
            var Board = new Board(20);
            var color = Board.Colors.Yellow;
            var locationX = 5;
            var locationY = 19;

            var pieceColor = Board.Colors.Red;
            var pieceX = locationX;
            var pieceY = locationY;

            Board.BoardValues[pieceX, pieceY] = pieceColor;

            // Act
            var actual = Board.UpdateSquare(locationX, locationY, color);

            // Assert
            Assert.That(actual, Is.False);
            Assert.That(Board.BoardValues[locationX, locationY], Is.EqualTo(pieceColor));
        }

        [Test]
        public void GetValuesByLocations()
        {
            // Arrange
            var Board = new Board();
            int yellowPieceX = 5;
            int yellowPieceY = 4;

            int bluePieceX = 4;
            int bluePieceY = 12;

            int redPieceX = 10;
            int redPieceY = 1;

            int greenPieceX = 10;
            int greenPieceY = 12;

            int emptyX = 3;
            int emptyY = 1;

            Board.BoardValues[yellowPieceX, yellowPieceY] = Board.Colors.Yellow;
            Board.BoardValues[bluePieceX, bluePieceY] = Board.Colors.Blue;
            Board.BoardValues[redPieceX, redPieceY] = Board.Colors.Red;
            Board.BoardValues[greenPieceX, greenPieceY] = Board.Colors.Green;

            var locations = new List<int[]> {
                new int[] { yellowPieceX, yellowPieceY },
                new int[] { bluePieceX, bluePieceY },
                new int[] { redPieceX, redPieceY },
                new int[] { greenPieceX, greenPieceY },
                new int[] { emptyX, emptyY }
            };

            // Act
            var actual = Board.GetValuesByLocations(locations);

            // Assert
            Assert.That(actual, Is.EqualTo(new List<Board.Colors> {
                Board.Colors.Yellow,
                Board.Colors.Blue,
                Board.Colors.Red,
                Board.Colors.Green,
                Board.Colors.Empty
            }));
        }

        [Test]
        public void GetEdgeValues_InCenterOfBoard()
        {
            // Arrange
            var Board = new Board(20);

            var edgeLocationX = 4;
            var edgeLocationY = 5;

            var positiveXPieceX = edgeLocationX + 1;
            var positiveXPieceY = edgeLocationY;
            var positiveXPieceColor = Board.Colors.Green;
            Board.BoardValues[positiveXPieceX, positiveXPieceY] = positiveXPieceColor;

            var negativeXPieceX = edgeLocationX - 1;
            var negativeXPieceY = edgeLocationY;
            var negativeXPieceColor = Board.Colors.Red;
            Board.BoardValues[negativeXPieceX, negativeXPieceY] = negativeXPieceColor;

            var positiveYPieceX = edgeLocationX;
            var positiveYPieceY = edgeLocationY + 1;
            var positiveYPieceColor = Board.Colors.Yellow;
            Board.BoardValues[positiveYPieceX, positiveYPieceY] = positiveYPieceColor;

            // Don't set the negativeYPiece, because it is empty.
            //var negativeYPieceX = edgeLocationX;
            //var negativeYPieceY = edgeLocationY - 1;
            //var negativeYPieceColor = Board.Colors.Empty;
            //Board.BoardValues[negativeYPieceX, negativeYPieceX] = negativeYPieceColor;

            // Act
            var actual = Board.GetEdgeValues(edgeLocationX, edgeLocationY);

            // Assert
            Assert.That(actual, Is.EqualTo(new List<Board.Colors> { Board.Colors.Green, Board.Colors.Red, Board.Colors.Yellow, Board.Colors.Empty }));
        }

        [Test]
        public void GetEdgeValues_OnEdgeOfBoard()
        {
            // Arrange
            var Board = new Board(20);

            var edgeLocationX = 0;
            var edgeLocationY = 5;

            var positiveXPieceX = edgeLocationX + 1;
            var positiveXPieceY = edgeLocationY;
            var positiveXPieceColor = Board.Colors.Green;
            Board.BoardValues[positiveXPieceX, positiveXPieceY] = positiveXPieceColor;

            // Don't set the negativeXPiece, because it is off of the board.
            //var negativeXPieceX = edgeLocationX - 1;
            //var negativeXPieceY = edgeLocationY;
            //var negativeXPieceColor = Board.Colors.Red;
            //Board.BoardValues[negativeXPieceX, negativeXPieceY] = negativeXPieceColor;

            var positiveYPieceX = edgeLocationX;
            var positiveYPieceY = edgeLocationY + 1;
            var positiveYPieceColor = Board.Colors.Yellow;
            Board.BoardValues[positiveYPieceX, positiveYPieceY] = positiveYPieceColor;

            // Don't set the negativeYPiece, because it is empty.
            //var negativeYPieceX = edgeLocationX;
            //var negativeYPieceY = edgeLocationY - 1;
            //var negativeYPieceColor = Board.Colors.Empty;
            //Board.BoardValues[negativeYPieceX, negativeYPieceX] = negativeYPieceColor;

            // Act
            var actual = Board.GetEdgeValues(edgeLocationX, edgeLocationY);

            // Assert
            Assert.That(actual, Is.EqualTo(new List<Board.Colors> { Board.Colors.Green, Board.Colors.Yellow, Board.Colors.Empty }));
        }

        [Test]
        public void GetEdgeValues_OnCornerOfBoard()
        {
            // Arrange
            var Board = new Board(20);

            var edgeLocationX = 0;
            var edgeLocationY = 0;

            var positiveXPieceX = edgeLocationX + 1;
            var positiveXPieceY = edgeLocationY;
            var positiveXPieceColor = Board.Colors.Green;
            Board.BoardValues[positiveXPieceX, positiveXPieceY] = positiveXPieceColor;

            // Don't set the negativeXPiece, because it is off of the board.
            //var negativeXPieceX = edgeLocationX - 1;
            //var negativeXPieceY = edgeLocationY;
            //var negativeXPieceColor = Board.Colors.Red;
            //Board.BoardValues[negativeXPieceX, negativeXPieceY] = negativeXPieceColor;

            var positiveYPieceX = edgeLocationX;
            var positiveYPieceY = edgeLocationY + 1;
            var positiveYPieceColor = Board.Colors.Yellow;
            Board.BoardValues[positiveYPieceX, positiveYPieceY] = positiveYPieceColor;

            // Don't set the negativeYPiece, because it is empty.
            //var negativeYPieceX = edgeLocationX;
            //var negativeYPieceY = edgeLocationY - 1;
            //var negativeYPieceColor = Board.Colors.Empty;
            //Board.BoardValues[negativeYPieceX, negativeYPieceX] = negativeYPieceColor;

            // Act
            var actual = Board.GetEdgeValues(edgeLocationX, edgeLocationY);

            // Assert
            Assert.That(actual, Is.EqualTo(new List<Board.Colors> { Board.Colors.Green, Board.Colors.Yellow }));
        }

        [Test]
        public void GetCornerValues_InCenterOfBoard()
        {
            // Arrange
            var Board = new Board(20);

            var edgeLocationX = 4;
            var edgeLocationY = 5;

            var positiveXAndYPieceX = edgeLocationX + 1;
            var positiveXAndYPieceY = edgeLocationY + 1;
            var positiveXAndYPieceColor = Board.Colors.Green;
            Board.BoardValues[positiveXAndYPieceX, positiveXAndYPieceY] = positiveXAndYPieceColor;

            var negativeXAndYPieceX = edgeLocationX - 1;
            var negativeXAndYPieceY = edgeLocationY - 1;
            var negativeXAndYPieceColor = Board.Colors.Red;
            Board.BoardValues[negativeXAndYPieceX, negativeXAndYPieceY] = negativeXAndYPieceColor;

            var negativeXPositiveYPieceX = edgeLocationX - 1;
            var negativeXPositiveYPieceY = edgeLocationY + 1;
            var negativeXPositiveYPieceColor = Board.Colors.Yellow;
            Board.BoardValues[negativeXPositiveYPieceX, negativeXPositiveYPieceY] = negativeXPositiveYPieceColor;

            // Don't set the negativeXPositiveYPiece, because it is empty.
            //var negativeXPositiveYPieceX = edgeLocationX;
            //var negativeXPositiveYPieceY = edgeLocationY - 1;
            //var negativeXPositiveYPieceColor = Board.Colors.Empty;
            //Board.BoardValues[negativeXPositiveYPieceX, negativeXPositiveYPieceX] = negativeXPositiveYPieceColor;

            // Act
            var actual = Board.GetCornerValues(edgeLocationX, edgeLocationY);

            // Assert
            Assert.That(actual, Is.EqualTo(new List<Board.Colors> { Board.Colors.Green, Board.Colors.Red, Board.Colors.Yellow, Board.Colors.Empty }));
        }

        [Test]
        public void GetCornerValues_OnEdgeOfBoard()
        {
            // Arrange
            var Board = new Board(20);

            var edgeLocationX = 4;
            var edgeLocationY = 0;

            var positiveXAndYPieceX = edgeLocationX + 1;
            var positiveXAndYPieceY = edgeLocationY + 1;
            var positiveXAndYPieceColor = Board.Colors.Green;
            Board.BoardValues[positiveXAndYPieceX, positiveXAndYPieceY] = positiveXAndYPieceColor;

            // Don't set the negativeXPositiveYPiece, because it is off of the board.
            //var negativeXAndYPieceX = edgeLocationX - 1;
            //var negativeXAndYPieceY = edgeLocationY - 1;
            //var negativeXAndYPieceColor = Board.Colors.Red;
            //Board.BoardValues[negativeXAndYPieceX, negativeXAndYPieceY] = negativeXAndYPieceColor;

            var negativeXPositiveYPieceX = edgeLocationX - 1;
            var negativeXPositiveYPieceY = edgeLocationY + 1;
            var negativeXPositiveYPieceColor = Board.Colors.Yellow;
            Board.BoardValues[negativeXPositiveYPieceX, negativeXPositiveYPieceY] = negativeXPositiveYPieceColor;

            // Don't set the negativeXPositiveYPiece, because it is empty.
            //var negativeXPositiveYPieceX = edgeLocationX + 1;
            //var negativeXPositiveYPieceY = edgeLocationY - 1;
            //var negativeXPositiveYPieceColor = Board.Colors.Empty;
            //Board.BoardValues[negativeXPositiveYPieceX, negativeXPositiveYPieceX] = negativeXPositiveYPieceColor;

            // Act
            var actual = Board.GetCornerValues(edgeLocationX, edgeLocationY);

            // Assert
            Assert.That(actual, Is.EqualTo(new List<Board.Colors> { Board.Colors.Green, Board.Colors.Yellow }));
        }

        [Test]
        public void GetCornerValues_OnCornerOfBoard()
        {
            // Arrange
            var Board = new Board(20);

            var edgeLocationX = 0;
            var edgeLocationY = 0;

            var positiveXAndYPieceX = edgeLocationX + 1;
            var positiveXAndYPieceY = edgeLocationY + 1;
            var positiveXAndYPieceColor = Board.Colors.Blue;
            Board.BoardValues[positiveXAndYPieceX, positiveXAndYPieceY] = positiveXAndYPieceColor;

            // Don't set the negativeXPositiveYPiece, because it is off of the board.
            //var negativeXAndYPieceX = edgeLocationX - 1;
            //var negativeXAndYPieceY = edgeLocationY - 1;
            //var negativeXAndYPieceColor = Board.Colors.Red;
            //Board.BoardValues[negativeXAndYPieceX, negativeXAndYPieceY] = negativeXAndYPieceColor;

            // Don't set the negativeXPositiveYPiece, because it is off of the board.
            //var negativeXPositiveYPieceX = edgeLocationX - 1;
            //var negativeXPositiveYPieceY = edgeLocationY + 1;
            //var negativeXPositiveYPieceColor = Board.Colors.Yellow;
            //Board.BoardValues[negativeXPositiveYPieceX, negativeXPositiveYPieceY] = negativeXPositiveYPieceColor;

            // Don't set the negativeXPositiveYPiece, because it is empty.
            //var negativeXPositiveYPieceX = edgeLocationX + 1;
            //var negativeXPositiveYPieceY = edgeLocationY - 1;
            //var negativeXPositiveYPieceColor = Board.Colors.Empty;
            //Board.BoardValues[negativeXPositiveYPieceX, negativeXPositiveYPieceX] = negativeXPositiveYPieceColor;

            // Act
            var actual = Board.GetCornerValues(edgeLocationX, edgeLocationY);

            // Assert
            Assert.That(actual, Is.EqualTo(new List<Board.Colors> { Board.Colors.Blue }));
        }

        [Test]
        public void GetLocationsWithValue()
        {
            // Arrange
            var Board = new Board(20);

            var selectedColor = Board.Colors.Red;

            var location1X = 0;
            var location1Y = 0;
            Board.BoardValues[location1X, location1Y] = selectedColor;

            var location2X = 15;
            var location2Y = 2;
            Board.BoardValues[location2X, location2Y] = selectedColor;

            var location3X = 19;
            var location3Y = 19;
            Board.BoardValues[location3X, location3Y] = selectedColor;

            var location4X = 0;
            var location4Y = 3;
            Board.BoardValues[location4X, location4Y] = Board.Colors.Yellow;


            // Act
            var actual = Board.GetLocationsWithValue(selectedColor);

            // Assert
            Assert.That(actual, Is.EqualTo(new List<int[]> {
                new int[] { location1X, location1Y },
                new int[] { location2X, location2Y },
                new int[] { location3X, location3Y } 
            }));
        }

        [Test]
        public void GetLocationsWithoutValue()
        {
            // Arrange
            var Board = new Board(20);

            var selectedColor = Board.Colors.Empty;

            var location1X = 0;
            var location1Y = 0;
            Board.BoardValues[location1X, location1Y] = Board.Colors.Red;

            var location2X = 15;
            var location2Y = 2;
            Board.BoardValues[location2X, location2Y] = Board.Colors.Green;

            var location3X = 19;
            var location3Y = 19;
            Board.BoardValues[location3X, location3Y] = Board.Colors.Blue;

            var location4X = 0;
            var location4Y = 3;
            Board.BoardValues[location4X, location4Y] = Board.Colors.Yellow;


            // Act
            var actual = Board.GetLocationsWithoutValue(selectedColor);

            // Assert
            Assert.That(actual, Is.EqualTo(new List<int[]> {
                new int[] { location1X, location1Y },
                new int[] { location4X, location4Y },
                new int[] { location2X, location2Y },
                new int[] { location3X, location3Y }
            }));
        }

        [Test]
        public void GetUnpopulatedLocations()
        {
            // Arrange
            int boardSize = 20;
            var Board = new Board(boardSize);

            var selectedColor = Board.Colors.Red;

            // Fill the board with the selected color.
            for (int x = 0; x < boardSize; x++)
            {
                for (int y = 0; y < boardSize; y++)
                {
                    Board.BoardValues[x, y] = selectedColor;
                }
            }

            // The list must me ordered in the same way that the GetUnpopulatedLocations scans the board values.
            List<int[]> unpopulatedLocations = new List<int[]>
            {
                new int[] { 0, 0 },
                new int[] { 2, 4 },
                new int[] { 15, 15 },
                new int[] { boardSize - 1, boardSize - 1 },
            };


            // Set the color to 'empty' for each location in 'unpopulated locations.
            for (int i = 0; i < unpopulatedLocations.Count ; i++)
            {
                int[] location = unpopulatedLocations[i];

                int x = location[0];
                int y = location[1];

                Board.BoardValues[x, y] = Board.Colors.Empty;
            }


            // Act
            var actual = Board.GetUnpopulatedLocations();

            // Assert
            Assert.That(actual, Is.EqualTo(unpopulatedLocations));
        }

        [Test]
        public void GetPopulatedLocations()
        {
            // Arrange
            int boardSize = 20;
            var Board = new Board(boardSize);

            var selectedColor = Board.Colors.Red;


            // The list must me ordered in the same way that the GetPopulatedLocations scans the board values.
            List<int[]> populatedLocations = new List<int[]>
            {
                new int[] { 0, 0 },
                new int[] { 2, 4 },
                new int[] { 15, 15 },
                new int[] { boardSize - 1, boardSize - 1 },
            };


            // Set the color to 'empty' for each location in 'unpopulated locations.
            for (int i = 0; i < populatedLocations.Count; i++)
            {
                int[] location = populatedLocations[i];

                int x = location[0];
                int y = location[1];

                Board.BoardValues[x, y] = selectedColor;
            }


            // Act
            var actual = Board.GetPopulatedLocations();

            // Assert
            Assert.That(actual, Is.EqualTo(populatedLocations));
        }

        [Test]
        [TestCase(1, 1, 1, 1, 2, 2)]
        [TestCase(1, 1, -1, -1, 0, 0)]
        [TestCase(1, 4, 2, 6, 3, 10)]
        [TestCase(1, 4, -1, 6, 0, 10)]
        public void GetLocationFromOrigin(int originX, int originY, int locationX, int locationY, int expectedX, int expectedY)
        {
            // Arrange
            var Board = new Board(20);

            // Act
            var actual = Board.GetLocationFromOrigin(originX, originY, locationX, locationY);

            int actualX = actual[0];
            int actualY = actual[1];

            // Assert
            Assert.That(actualX, Is.EqualTo(expectedX));
            Assert.That(actualY, Is.EqualTo(expectedY));
        }

        [Test]
        [TestCase(1, 1, 1, 1, true)]
        [TestCase(1, 1, -1, -1, true)]
        [TestCase(1, 4, 2, 6, true)]
        [TestCase(1, 4, -1, 6, true)]
        [TestCase(-1, -1, -1, -1, false)] // All negatives returns false.
        [TestCase(-1, 2, -1, 2, false)] // Off the negative x side of the board.
        [TestCase(1, 2, 19, 2, false)] // Off the poxitive x side of the board.
        [TestCase(1, -5, 2, 4, false)] // Off the negative y side of the board.
        [TestCase(1, 15, 2, 10, false)] // Off the positive y side of the board.
        public void IsValidLocationFromOrigin(int originX, int originY, int locationX, int locationY, bool expected)
        {
            // Arrange
            var Board = new Board(20);

            // Act
            var actual = Board.IsValidLocationFromOrigin(originX, originY, locationX, locationY);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void AreAllValuesColor_ReturnsTrue()
        {
            // Arrange
            var Board = new Board(20);

            var values = new List<Board.Colors> { Colors.Yellow, Colors.Yellow, Colors.Yellow };

            // Act
            var actual = Board.AreAllValuesColor(values, Colors.Yellow);

            // Assert
            Assert.That(actual, Is.True);
        }

        [Test]
        public void AreAllValuesColor_ReturnsFalse()
        {
            // Arrange
            var Board = new Board(20);

            var values = new List<Board.Colors> { Colors.Yellow, Colors.Red, Colors.Green, Colors.Blue };

            // Act
            var actual = Board.AreAllValuesColor(values, Colors.Blue);

            // Assert
            Assert.That(actual, Is.False);
        }

        [Test]
        public void IsColorInValues_ReturnsTrue()
        {
            // Arrange
            var Board = new Board(20);

            var values = new List<Board.Colors> { Colors.Yellow, Colors.Red, Colors.Green, Colors.Empty, Colors.Blue, Colors.Red };

            // Act
            var actual = Board.IsColorInValues(values, Colors.Blue);

            // Assert
            Assert.That(actual, Is.True);
        }

        [Test]
        public void IsColorInValues_ReturnsFalse()
        {
            // Arrange
            var Board = new Board(20);

            var values = new List<Board.Colors> { Colors.Empty, Colors.Red, Colors.Green, Colors.Yellow };

            // Act
            var actual = Board.IsColorInValues(values, Colors.Blue);

            // Assert
            Assert.That(actual, Is.False);
        }

        [Test]
        public void IsPlayableNode_ReturnsFalse_WhenNoCornersArePopulatedWithColor()
        {
            // Arrange
            var Board = new Board(3);

            Colors e = Colors.Empty;
            Colors r = Colors.Red;
            Colors g = Colors.Green;
            Colors b = Colors.Blue;
            Colors y = Colors.Yellow;

            Board.BoardValues = new Colors[,]
            {
                { e, e, e },
                { e, e, e },
                { e, e, e },
            };

            int nodeX = 1;
            int nodeY = 1;
            Colors nodeColor = Colors.Blue;

            // Act
            var actual = Board.IsPlayableNode(nodeX, nodeY, nodeColor);

            // Assert
            Assert.That(actual, Is.False);
        }

        [Test]
        public void IsPlayableNode_ReturnsTrue_WhenCornerIsPopulatedWithColor()
        {
            // Arrange
            var Board = new Board(3);

            Colors e = Colors.Empty;
            Colors r = Colors.Red;
            Colors g = Colors.Green;
            Colors b = Colors.Blue;
            Colors y = Colors.Yellow;

            Board.BoardValues = new Colors[,]
            {
                { b, e, e },
                { e, e, e },
                { e, e, e },
            };

            int nodeX = 1;
            int nodeY = 1;
            Colors nodeColor = Colors.Blue;

            // Act
            var actual = Board.IsPlayableNode(nodeX, nodeY, nodeColor);

            // Assert
            Assert.That(actual, Is.True);
        }

        [Test]
        public void IsPlayableNode_ReturnsFalse_WhenLocationIsPopulated()
        {
            // Arrange
            var Board = new Board(5);

            Colors e = Colors.Empty;
            Colors r = Colors.Red;
            Colors g = Colors.Green;
            Colors b = Colors.Blue;
            Colors y = Colors.Yellow;

            Board.BoardValues = new Colors[,]
            {
                { b, e, e },
                { e, r, e },
                { e, e, e },
            };

            int nodeX = 1;
            int nodeY = 1;
            Colors nodeColor = Colors.Blue;

            // Act
            var actual = Board.IsPlayableNode(nodeX, nodeY, nodeColor);

            // Assert
            Assert.That(actual, Is.False);
        }

        [Test]
        public void IsPlayableNode_ReturnsFalse_WhenEdgeIsPopulatedWithColor()
        {
            // Arrange
            var Board = new Board(5);

            Colors e = Colors.Empty;
            Colors r = Colors.Red;
            Colors g = Colors.Green;
            Colors b = Colors.Blue;
            Colors y = Colors.Yellow;

            Board.BoardValues = new Colors[,]
            {
                { b, b, e },
                { e, e, e },
                { e, e, e },
            };

            int nodeX = 1;
            int nodeY = 1;
            Colors nodeColor = Colors.Blue;

            // Act
            var actual = Board.IsPlayableNode(nodeX, nodeY, nodeColor);

            // Assert
            Assert.That(actual, Is.False);
        }

        [Test]
        public void IsPlayableNode_ReturnsTrue_WhenPieceIsInCorner()
        {
            // Arrange
            var Board = new Board(5);

            Colors e = Colors.Empty;
            Colors r = Colors.Red;
            Colors g = Colors.Green;
            Colors b = Colors.Blue;
            Colors y = Colors.Yellow;

            Board.BoardValues = new Colors[,]
            {
                { e, e, e },
                { e, b, e },
                { e, e, e },
            };

            int nodeX = 0;
            int nodeY = 0;
            Colors nodeColor = Colors.Blue;

            // Act
            var actual = Board.IsPlayableNode(nodeX, nodeY, nodeColor);

            // Assert
            Assert.That(actual, Is.True);
        }

        [Test]
        public void UpdateEmptySquares()
        {
            // Arrange
            var Board = new Board(20);

            Board.Colors selectedColor = Board.Colors.Blue;

            int location1X = 5;
            int location1Y = 1;

            int location2X = 10;
            int location2Y = 3;

            var list = new List<int[]> {
                new int[] { location1X, location1Y },
                new int[] { location2X, location2Y }
            };

            // Act
            Board.UpdateEmptySquares(list, selectedColor);

            // Assert
            Assert.That(Board.BoardValues[location1X, location1Y], Is.EqualTo(selectedColor));
            Assert.That(Board.BoardValues[location2X, location2Y], Is.EqualTo(selectedColor));
            Assert.That(Board.BoardValues[location2X, location2Y], Is.EqualTo(selectedColor));
        }

        [Test]
        public void UpdateSquares_ReturnsFalse_WhenSquaresAreInvalidPlacements()
        {
            // Arrange
            var Board = new Board(3);

            Colors e = Colors.Empty;
            Colors r = Colors.Red;
            Colors g = Colors.Green;
            Colors b = Colors.Blue;
            Colors y = Colors.Yellow;

            Board.BoardValues = new Colors[,]
            {
                { b, b, e },
                { e, e, y },
                { r, e, g},
            };

            // Unpopulated location. (valid)
            int location1X = 1;
            int location1Y = 1;

            // Populated location. (invalid)
            int location2X = 2;
            int location2Y = 2;
            Board.Colors selectedColor = Board.Colors.Red;

            var locations = new List<int[]>
            {
                new int[] { location1X, location1Y },
                new int[] { location2X, location2Y },
            };

            // Act
            var actual = Board.UpdateSquares(locations, selectedColor);

            // Assert
            Assert.That(actual, Is.False);

            // Assert the squares weren't updated.
            Assert.That(Board.BoardValues[location1X, location1Y], Is.EqualTo(Board.Colors.Empty));
            Assert.That(Board.BoardValues[location2X, location2Y], Is.EqualTo(Board.Colors.Green));
        }

        [Test]
        public void UpdateSquares_ReturnsTrue_WhenSquaresAreValidPlacements()
        {
            // Arrange
            var Board = new Board(3);

            Colors e = Colors.Empty;
            Colors r = Colors.Red;
            Colors g = Colors.Green;
            Colors b = Colors.Blue;
            Colors y = Colors.Yellow;

            Board.BoardValues = new Colors[,]
            {
                { b, b, e },
                { e, e, y },
                { r, e, g},
            };

            // Unpopulated location. (valid)
            int location1X = 1;
            int location1Y = 1;

            // Unpopulated location. (valid)
            int location2X = 0;
            int location2Y = 2;
            Board.Colors selectedColor = Board.Colors.Red;

            var locations = new List<int[]>
            {
                new int[] { location1X, location1Y },
                new int[] { location2X, location2Y },
            };

            // Act
            var actual = Board.UpdateSquares(locations, selectedColor);

            // Assert
            Assert.That(actual, Is.True);

            // Assert the squares were updated.
            Assert.That(Board.BoardValues[location1X, location1Y], Is.EqualTo(selectedColor));
            Assert.That(Board.BoardValues[location2X, location2Y], Is.EqualTo(selectedColor));
        }

        [Test]
        public void GetPlayableNodes_WithNoPlayableSquares()
        {
            // Arrange
            var Board = new Board(3);

            Colors e = Colors.Empty;
            Colors r = Colors.Red;
            Colors g = Colors.Green;
            Colors b = Colors.Blue;
            Colors y = Colors.Yellow;

            Board.BoardValues = new Colors[,]
            {
                { e, e, e },
                { e, e, e },
                { e, e, e }
            };

            // Act 
            var redPlayableNodes = Board.GetPlayableNodes(Board.Colors.Red);

            // Assert
            Assert.That(redPlayableNodes, Is.EqualTo(new List<int[]>{}));
        }

        [Test]
        public void GetPlayableNodes_WithFourPlayableSquares()
        {
            // Arrange
            var Board = new Board(3);

            Colors e = Colors.Empty;
            Colors r = Colors.Red;
            Colors g = Colors.Green;
            Colors b = Colors.Blue;
            Colors y = Colors.Yellow;

            Board.BoardValues = new Colors[,]
            {
                { e, e, e },
                { e, r, e },
                { e, e, e }
            };

            // Act
            var redPlayableNodes = Board.GetPlayableNodes(Board.Colors.Red);

            // Assert
            Assert.That(redPlayableNodes, Is.EqualTo(new List<int[]> { 
                new int[] { 0, 0 },
                new int[] { 0, 2 },
                new int[] { 2, 0 },
                new int[] { 2, 2 },
            }));
        }

        [Test]
        public void GetPlayableNodes_ReturnsNoNodes_WithPopulatedLocations()
        {
            // Arrange
            var Board = new Board(3);

            Colors e = Colors.Empty;
            Colors r = Colors.Red;
            Colors g = Colors.Green;
            Colors b = Colors.Blue;
            Colors y = Colors.Yellow;

            Board.BoardValues = new Colors[,]
            {
                { y, e, b },
                { e, r, e },
                { r, e, g }
            };

            // Act
            var redPlayableNodes = Board.GetPlayableNodes(Board.Colors.Red);

            // Assert
            Assert.That(redPlayableNodes, Is.EqualTo(new List<int[]> {}));
        }

        [Test]
        public void GetPlayableNodes_ReturnsNoNodes_WithEdgesOfSameColor()
        {
            // Arrange
            var Board = new Board(3);

            Colors e = Colors.Empty;
            Colors r = Colors.Red;
            Colors g = Colors.Green;
            Colors b = Colors.Blue;
            Colors y = Colors.Yellow;

            Board.BoardValues = new Colors[,]
            {
                { e, r, e },
                { r, r, r },
                { e, r, e }
            };

            // Act
            var redPlayableNodes = Board.GetPlayableNodes(Board.Colors.Red);

            // Assert
            Assert.That(redPlayableNodes, Is.EqualTo(new List<int[]> { }));
        }

        [Test]
        public void GetPlayableNodes_ReturnsNodes_WithSurroundedLocation()
        {
            // Arrange
            var Board = new Board(3);

            Colors e = Colors.Empty;
            Colors r = Colors.Red;
            Colors g = Colors.Green;
            Colors b = Colors.Blue;
            Colors y = Colors.Yellow;

            Board.BoardValues = new Colors[,]
            {
                { y, g, b },
                { b, e, g },
                { r, b, b }
            };

            // Act
            var redPlayableNodes = Board.GetPlayableNodes(Board.Colors.Red);

            // Assert
            Assert.That(redPlayableNodes, Is.EqualTo(new List<int[]> { 
                new int[] { 1, 1 }
            }));
        }

        [Test]
        public void GetPlayableCorners_WithNoPlayableSquares()
        {
            // Arrange
            var Board = new Board(3);

            Colors e = Colors.Empty;
            Colors r = Colors.Red;
            Colors g = Colors.Green;
            Colors b = Colors.Blue;
            Colors y = Colors.Yellow;

            Board.BoardValues = new Colors[,]
            {
                { e, e, e },
                { e, e, e },
                { e, e, e }
            };

            // Act 
            var redPlayableNodes = Board.GetPlayableCorners(Board.Colors.Red);

            // Assert
            Assert.That(redPlayableNodes, Is.EqualTo(new List<int[]> { }));
        }

        [Test]
        public void GetPlayableCorners_WithFourPlayableSquares()
        {
            // Arrange
            var Board = new Board(3);

            Colors e = Colors.Empty;
            Colors r = Colors.Red;
            Colors g = Colors.Green;
            Colors b = Colors.Blue;
            Colors y = Colors.Yellow;

            Board.BoardValues = new Colors[,]
            {
                { e, e, e },
                { e, r, e },
                { e, e, e }
            };

            // Act
            var redPlayableNodes = Board.GetPlayableCorners(Board.Colors.Red);

            // Assert
            Assert.That(redPlayableNodes, Is.EqualTo(new List<int[]> {
                new int[] { 2, 2 },
                new int[] { 0, 0 },
                new int[] { 0, 2 },
                new int[] { 2, 0 },
            }));
        }

        [Test]
        public void GetPlayableCorners_ReturnsNoNodes_WithPopulatedLocations()
        {
            // Arrange
            var Board = new Board(3);

            Colors e = Colors.Empty;
            Colors r = Colors.Red;
            Colors g = Colors.Green;
            Colors b = Colors.Blue;
            Colors y = Colors.Yellow;

            Board.BoardValues = new Colors[,]
            {
                { y, e, b },
                { e, r, e },
                { r, e, g }
            };

            // Act
            var redPlayableNodes = Board.GetPlayableCorners(Board.Colors.Red);

            // Assert
            Assert.That(redPlayableNodes, Is.EqualTo(new List<int[]> { }));
        }

        [Test]
        public void GetPlayableCorners_ReturnsNoNodes_WithEdgesOfSameColor()
        {
            // Arrange
            var Board = new Board(3);

            Colors e = Colors.Empty;
            Colors r = Colors.Red;
            Colors g = Colors.Green;
            Colors b = Colors.Blue;
            Colors y = Colors.Yellow;

            Board.BoardValues = new Colors[,]
            {
                { e, r, e },
                { r, r, r },
                { e, r, e }
            };

            // Act
            var redPlayableNodes = Board.GetPlayableCorners(Board.Colors.Red);

            // Assert
            Assert.That(redPlayableNodes, Is.EqualTo(new List<int[]> { }));
        }

        [Test]
        public void GetPlayableCorners_ReturnsNodes_WithSurroundedLocation()
        {
            // Arrange
            var Board = new Board(3);

            Colors e = Colors.Empty;
            Colors r = Colors.Red;
            Colors g = Colors.Green;
            Colors b = Colors.Blue;
            Colors y = Colors.Yellow;

            Board.BoardValues = new Colors[,]
            {
                { y, g, b },
                { b, e, g },
                { r, b, b }
            };

            // Act
            var redPlayableNodes = Board.GetPlayableCorners(Board.Colors.Red);

            // Assert
            Assert.That(redPlayableNodes, Is.EqualTo(new List<int[]> {
                new int[] { 1, 1 }
            }));
        }

        [Test]
        public void IsPiecePlayable_ReturnsTrue_WithEmptyBoard()
        {
            // Arrange
            var Board = new Board(6);

            Colors e = Colors.Empty;
            Colors r = Colors.Red;
            Colors g = Colors.Green;
            Colors b = Colors.Blue;
            Colors y = Colors.Yellow;

            Board.BoardValues = new Colors[,]
            {
                { e, e, e, e, e, e },
                { e, e, e, e, e, e },
                { e, e, e, e, e, e },
                { e, e, e, e, e, e },
                { e, e, e, e, e, e },
                { e, e, e, e, e, e },
            };

            int xOrigin = 1;
            int yOrigin = 2;
            Board.Colors color = Board.Colors.Red;
            int[,] piece = Pieces.PentominoL;

            // Act
            var actual = Board.IsPiecePlayable(xOrigin, yOrigin, piece, color);

            // Assert
            Assert.That(actual, Is.True);
        }

        [Test]
        public void IsPiecePlayable_ReturnsFalse_WhenPieceOffBoard()
        {
            // Arrange
            var Board = new Board(6);

            Colors e = Colors.Empty;
            Colors r = Colors.Red;
            Colors g = Colors.Green;
            Colors b = Colors.Blue;
            Colors y = Colors.Yellow;

            Board.BoardValues = new Colors[,]
            {
                { e, e, e, e, e, e },
                { e, e, e, e, e, e },
                { e, e, e, e, e, e },
                { e, e, e, e, e, e },
                { e, e, e, e, e, e },
                { e, e, e, e, e, e },
            };

            int xOrigin = 5;
            int yOrigin = 5;
            Board.Colors color = Board.Colors.Red;
            int[,] piece = Pieces.PentominoL;

            // Act
            var actual = Board.IsPiecePlayable(xOrigin, yOrigin, piece, color);

            // Assert
            Assert.That(actual, Is.False);
        }

        [Test]
        public void IsPiecePlayable_ReturnsFalse_BoardIsFullyPopulated()
        {
            // Arrange
            var Board = new Board(6);

            Colors e = Colors.Empty;
            Colors r = Colors.Red;
            Colors g = Colors.Green;
            Colors b = Colors.Blue;
            Colors y = Colors.Yellow;

            Board.BoardValues = new Colors[,]
            {
                { r, b, g, y, r, r },
                { b, y, y, y, y, y },
                { g, y, g, b, b, y },
                { r, r, r, g, r, b },
                { b, y, y, g, b, b },
                { b, y, r, g, g, b },
            };

            int xOrigin = 1;
            int yOrigin = 1;
            Board.Colors color = Board.Colors.Red;
            int[,] piece = Pieces.PentominoL;

            // Act
            var actual = Board.IsPiecePlayable(xOrigin, yOrigin, piece, color);

            // Assert
            Assert.That(actual, Is.False);
        }

        [Test]
        public void IsPiecePlayable_ReturnsTrue_WhenPieceIsSurroundedWithDifferentColor()
        {
            // Arrange
            var Board = new Board(6);

            Colors e = Colors.Empty;
            Colors r
                = Colors.Red;
            Colors g = Colors.Green;
            Colors b = Colors.Blue;
            Colors y = Colors.Yellow;

            Board.BoardValues = new Colors[,]
            {
                { b, b, b, b, b, b },
                { b, e, e, e, e, b },
                { b, b, b, b, e, b },
                { e, e, e, b, b, b },
                { e, e, e, e, e, e },
                { e, e, e, e, e, e },
            };

            int xOrigin = 1;
            int yOrigin = 1;
            Board.Colors color = Board.Colors.Red;
            int[,] piece = Pieces.PentominoL;

            // Act
            var actual = Board.IsPiecePlayable(xOrigin, yOrigin, piece, color);

            // Assert
            Assert.That(actual, Is.True);
        }

        [Test]
        public void IsPiecePlayable_ReturnsFalse_WhenPieceIsNextToSameColor()
        {
            // Arrange
            var Board = new Board(6);

            Colors e = Colors.Empty;
            Colors r = Colors.Red;
            Colors g = Colors.Green;
            Colors b = Colors.Blue;
            Colors y = Colors.Yellow;

            Board.BoardValues = new Colors[,]
            {
                { e, e, e, e, e, e },
                { e, e, e, e, e, e },
                { e, e, e, e, e, e },
                { e, e, e, e, r, e },
                { e, e, e, e, e, e },
                { e, e, e, e, e, e },
            };

            int xOrigin = 1;
            int yOrigin = 1;
            Board.Colors color = Board.Colors.Red;
            int[,] piece = Pieces.PentominoL;

            // Act
            var actual = Board.IsPiecePlayable(xOrigin, yOrigin, piece, color);

            // Assert
            Assert.That(actual, Is.False);
        }

        [Test]
        public void IsPiecePlayable_ReturnsTrue_WithPetominoF()
        {
            // Arrange
            var Board = new Board(6);

            Colors e = Colors.Empty;
            Colors r = Colors.Red;
            Colors g = Colors.Green;
            Colors b = Colors.Blue;
            Colors y = Colors.Yellow;

            Board.BoardValues = new Colors[,]
            {
                { e, e, e, e, e, e },
                { e, e, e, e, e, e },
                { e, e, e, e, e, e },
                { e, e, e, e, e, e },
                { e, e, e, e, e, e },
                { e, e, e, e, e, e },
            };

            int xOrigin = 1;
            int yOrigin = 1;
            Board.Colors color = Board.Colors.Red;
            int[,] piece = Pieces.PentominoF;

            // Act
            var actual = Board.IsPiecePlayable(xOrigin, yOrigin, piece, color);

            // Assert
            Assert.That(actual, Is.True);
        }

        [Test]
        public void IsPiecePlayable_ReturnsTrue_WithPieceOnCornerOfBoard()
        {
            // Arrange
            var Board = new Board(6);

            Colors e = Colors.Empty;
            Colors r = Colors.Red;
            Colors g = Colors.Green;
            Colors b = Colors.Blue;
            Colors y = Colors.Yellow;

            Board.BoardValues = new Colors[,]
            {
                { e, e, e, e, e, e },
                { e, e, e, e, e, e },
                { e, e, e, e, e, e },
                { e, e, e, e, e, e },
                { e, e, e, e, e, e },
                { e, e, e, e, e, e },
            };

            int xOrigin = 0;
            int yOrigin = 0;
            Board.Colors color = Board.Colors.Red;
            int[,] piece = Pieces.PentominoF;

            // Act
            var actual = Board.IsPiecePlayable(xOrigin, yOrigin, piece, color);

            // Assert
            Assert.That(actual, Is.True);
        }

        [Test]
        public void ConvertPieceToLocations_WithPentominoF()
        {
            // Arrange
            var Board = new Board(6);

            int xOrigin = 1;
            int yOrigin = 1;
            int[,] piece = Pieces.PentominoF;

            // Act
            var actual = Board.ConvertPieceToLocations(xOrigin, yOrigin, piece);

            // Assert
            Assert.That(actual, Is.EqualTo(new List<int[]> {
                new int[] { 1, 1 },
                new int[] { 1, 2 },
                new int[] { 2, 2 },
                new int[] { 2, 3 },
                new int[] { 3, 2 },
            }));
        }

        [Test]
        public void ConvertPieceToLocations_WithPentominoL()
        {
            // Arrange
            var Board = new Board(6);

            int xOrigin = 2;
            int yOrigin = 1;
            int[,] piece = Pieces.PentominoL;

            // Act
            var actual = Board.ConvertPieceToLocations(xOrigin, yOrigin, piece);

            // Assert
            Assert.That(actual, Is.EqualTo(new List<int[]> {
                new int[] { 2, 1 },
                new int[] { 2, 2 },
                new int[] { 2, 3 },
                new int[] { 2, 4 },
                new int[] { 3, 4 },
            }));
        }

        [Test]
        public void ConvertPieceToLocations_WithPentominoLAndPieceOffOfBoard()
        {
            // Arrange
            var Board = new Board(6);

            int xOrigin = -3;
            int yOrigin = 1;
            int[,] piece = Pieces.PentominoL;

            // Act
            var actual = Board.ConvertPieceToLocations(xOrigin, yOrigin, piece);

            // Assert
            Assert.That(actual, Is.EqualTo(new List<int[]> {
                new int[] { -3, 1 },
                new int[] { -3, 2 },
                new int[] { -3, 3 },
                new int[] { -3, 4 },
                new int[] { -2, 4 },
            }));
        }

        [Test]
        public void PlacePiece()
        {
            // Arrange
            var Board = new Board(6);

            int xOrigin = 2;
            int yOrigin = 1;
            int[,] piece = Pieces.PentominoL;

            // Act
            var actual = Board.PlacePiece(xOrigin, yOrigin, piece, Board.Colors.Blue);

            // Assert
            Assert.That(Board.BoardValues[2, 1], Is.EqualTo(Board.Colors.Blue));
            Assert.That(Board.BoardValues[2, 2], Is.EqualTo(Board.Colors.Blue));
            Assert.That(Board.BoardValues[2, 3], Is.EqualTo(Board.Colors.Blue));
            Assert.That(Board.BoardValues[2, 4], Is.EqualTo(Board.Colors.Blue));
            Assert.That(Board.BoardValues[3, 4], Is.EqualTo(Board.Colors.Blue));
        }

        [Test]
        public void GetPlayablePieceOffsets_WithPentominoL()
        {
            // Arrange
            var Board = new Board(6);

            int[,] piece = Pieces.PentominoL;

            // Act
            var actual = Board.GetPlayablePieceOffsets(piece);

            // Assert
            Assert.That(actual, Is.EqualTo(new List<int[]> {
                new int[] { -1, -1 },
                new int[] { 1, -1 },
                new int[] { -1, 4 },
                new int[] { 2, 4 },
                new int[] { 2, 2 }
            }));
        }

        [Test]
        public void GetPlayablePieceOffsets_WithPentominI()
        {
            // Arrange
            var Board = new Board(6);

            int[,] piece = Pieces.PentominoI;

            // Act
            var actual = Board.GetPlayablePieceOffsets(piece);

            // Assert
            Assert.That(actual, Is.EqualTo(new List<int[]> {
                new int[] { -1, -1 },
                new int[] { -1, 1 },
                new int[] { 5, 1 },
                new int[] { 5, -1 },
            }));
        }

        [Test]
        public void Moves()
        {
            // Arrange
            var Board = new Board(20);

            //int[,] piece = Pieces.PentominoI;
            Board.BoardValues[0, 0] = Board.Colors.Red;
            Board.BoardValues[0, 19] = Board.Colors.Green;
            Board.BoardValues[19, 0] = Board.Colors.Blue;
            Board.BoardValues[19, 19] = Board.Colors.Yellow;

            // Act

            while (true)
            {
                var moves = Board.Moves(Board.Colors.Red);
                if (moves.Count == 0) break;
                var lowerBound = 0;
                var upperBound = moves.Count;
                var rngNum = RandomNumberGenerator.GetInt32(lowerBound, upperBound);
                Move move = moves[rngNum];
                Board.PlacePiece(move.X, move.Y, move.PieceVariant, Board.Colors.Red);

                moves = Board.Moves(Board.Colors.Green);
                if (moves.Count == 0) break;
                upperBound = moves.Count;
                rngNum = RandomNumberGenerator.GetInt32(lowerBound, upperBound);
                move = moves[rngNum];
                Board.PlacePiece(move.X, move.Y, move.PieceVariant, Board.Colors.Green);

                moves = Board.Moves(Board.Colors.Blue);
                if (moves.Count == 0) break;
                upperBound = moves.Count;
                rngNum = RandomNumberGenerator.GetInt32(lowerBound, upperBound);
                move = moves[rngNum];
                Board.PlacePiece(move.X, move.Y, move.PieceVariant, Board.Colors.Blue);

                moves = Board.Moves(Board.Colors.Yellow);
                if (moves.Count == 0) break;
                upperBound = moves.Count;
                rngNum = RandomNumberGenerator.GetInt32(lowerBound, upperBound);
                move = moves[rngNum];
                Board.PlacePiece(move.X, move.Y, move.PieceVariant, Board.Colors.Yellow);
            }

            var test = Board.GetBoardString();

            var test2 = test;
        }
    }
}