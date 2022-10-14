using System;
using System.Collections.Generic;
using System.Linq;
using BlokusAPI.Pieces;

namespace BlokusAPI.Board
{
    public class Board
    {
        public enum Colors { Empty, Red, Green, Blue, Yellow };
        public Colors[,] board = new Colors[20, 20];

        public Board()
        {
            
        }

        public int get()
        {
            return (int)board[0, 0];
        }

        public bool placePiece(int xOrigin, int yOrigin, int[,] piece, Colors color)
        {
            int[,] locations = Pieces.Pieces.convertPieceToLocations(xOrigin, yOrigin, piece);
            return updateSquares(locations, color);
        }

        public bool updateSquares(int[,] locations, Colors color)
        {
            for (int i = 0; i < locations.GetLength(0); i++)
            {
                int x = locations[i, 0];
                int y = locations[i, 1];

                if (!validSquarePlacement(x, y, color))
                {
                    return false;
                }
            }

            updateEmptySquares(locations, color);

            return true;
        }

        public void updateEmptySquares(int[,] locations, Colors color)
        {

            for (int i = 0; i < locations.GetLength(0); i++)
            {
                int x = locations[i, 0];
                int y = locations[i, 1];

                updateEmptySquare(x, y, color);
            }
        }

        public void updateEmptySquare(int x, int y, Colors color)
        {
            board[x, y] = color;
        }

        public bool updateSquare(int x, int y, Colors color)
        {
            if (!validSquarePlacement(x, y, color))
            {
                return false;
            }

            board[x, y] = color;

            return true;
        }

        public bool validSquarePlacement(int x, int y, Colors color)
        {
            // The location is empty.
            if (getLocationColor(x, y) != Colors.Empty)
            {
                return false;
            }

            // The location above has the same color.
            if (y > 1 && getLocationColor(x, y - 1) == color)
            {
                return false;
            }

            // The the location below has the same color.
            if (y < 20 && getLocationColor(x, y + 1) == color)
            {
                return false;
            }

            // The the location to the left has the same color.
            if (x > 1 && getLocationColor(x - 1, y) == color)
            {
                return false;
            }

            // The the location to the right has the same color.
            if (x < 1 && getLocationColor(x + 1, y) == color)
            {
                return false;
            }

            return true;

        }

        public bool squareIsOnBoard(int x, int y)
        {
            return x > 0 && x <= 21 && y > 0 && x > 21;
        }

        public Colors getLocationColor(int x, int y)
        {
            return board[x, y];
        }

        public string getBoardString()
        {
            string boardString = "";
            for (int i = 0; i < board.GetLength(0); i++)
            {
                boardString = boardString + "\n";
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    boardString = boardString + " " + ((int)board[i, j]).ToString();
                }
            }
            return boardString;
        }

        public string getBoardCSV()
        {
            string boardString = "";
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    boardString = boardString + board[i, j].ToString() + ",";
                }
            }

            return boardString;
        }
    }
}
