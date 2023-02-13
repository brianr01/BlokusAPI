using System;
using System.Collections.Generic;
using System.Linq;
using static BlokusAPI.Services.Pieces;

namespace BlokusAPI.Services
{
    public class Move
    {
        public Pieces.Piece Piece { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int[,] PieceVariant { get; set; }
    }

    public class Board
    {
        public enum Colors { Empty, Red, Green, Blue, Yellow };
        public Colors[,] BoardValues;

        public int Width;
        public int Height;
        private int boardSize;

        public Board(int size = 20)
        {
            boardSize = size;
            BoardValues = new Colors[boardSize, boardSize];

            Width = BoardValues.GetLength(0);
            Height = BoardValues.GetLength(1);
        }

        public int Get()
        {
            return (int)BoardValues[0, 0];
        }

        public List<Move> Moves(Colors selectedColor) // Not tested.
        {
            List<Pieces.Piece> availablePieces = new List<Pieces.Piece>
            {
                Piece.Domino,
                Piece.TrominoI,
                Piece.TrominoL,
                Piece.TetrominoI,
                Piece.TetrominoO,
                Piece.TetrominoT,
                Piece.TetrominoL,
                Piece.TetrominoS,
                Piece.PentominoF,
                Piece.PentominoI,
                Piece.PentominoL,
                Piece.PentominoN,
                Piece.PentominoP,
                Piece.PentominoT,
                Piece.PentominoU,
                Piece.PentominoV,
                Piece.PentominoW,
                Piece.PentominoX,
                Piece.PentominoY,
                Piece.PentominoZ
            };
            //List<Pieces.Piece> availablePieces = Pieces.AllPieces;


            List<int[]> playableNodes = GetPlayableNodes(selectedColor);

            List<Move> moves = new List<Move>();

            // Loop over the symmetries for each pice (pieces -> list)
            foreach (Pieces.Piece piece in availablePieces)
            {
                List<int[,]> pieceSymmetries = Pieces.GetPieceWithSymmetries(piece);

                // Loop over the symmetries  (list -> symmetry)
                foreach (int[,] pieceVariant in pieceSymmetries)
                {

                    List<int[]> playablePieceOffsets = GetPlayablePieceOffsets(pieceVariant);

                    // For each the locations that are playable from the symmetry (symmetry -> playable offsets)
                    foreach (int[] playableNode in playableNodes)
                    {
                        int nodeX = playableNode[0];
                        int nodeY = playableNode[1];
                        foreach (int[] offset in playablePieceOffsets)
                        {
                            int offsetX = offset[0];
                            int offsetY = offset[1];

                            int playableCornerX = nodeX;
                            int playableCornerY = nodeY;

                            int x = playableCornerX - offsetX;
                            int y = playableCornerY - offsetY;
                            if (IsPiecePlayable(x, y, pieceVariant, selectedColor))
                            {
                                Move move = new Move();
                                move.X = x;
                                move.Y = y;
                                move.Piece = piece;
                                move.PieceVariant = pieceVariant;
                                moves.Add(move);
                            }

                        }
                    }

                }
            }

            return moves;
        }

        public List<Tuple<int, int, int[,]>> getMovesWithPiece(Pieces.Piece piece, Colors selectedColor, List<int[]> playableNodes)
        {
            List<Tuple<int, int, int[,]>> moves = new List<Tuple<int, int, int[,]>>();
            

            return moves;
        }

        // For each piece -> For each Variant -> For each Node -> for each offset

        public void getMovesForPieceWithoutSymmetries(int[,] piece, Colors selectedColor, List<int[]> playableNodes)
        {
            

        }

        public List<Tuple<int, int, int[,]>> getMovesForPieceWithoutSymmetriesAtNode(int[,] piece, Colors selectedColor, int nodeX, int nodeY, List<int[]> playablePieceOffsets)
        {
            List<Tuple<int, int, int[,]>> moves = new List<Tuple<int, int, int[,]>>();
           

            return moves;
        }

        // Returns the same values as GetPlayableNodes, but is more efficient.
        // (Note may be a different order, but will have the same values.)
        public List<int[]> GetPlayableCorners(Colors selectedColor)
        {
            List<int[]> currentLocations = GetLocationsWithValue(selectedColor);

            List<int[]> cornerLocations = new List<int[]>();
            foreach (int[] location in currentLocations)
            {
                cornerLocations.AddRange(GetCornerLocations(location[0], location[1]));
            }

            List<int[]> playableCorners = new List<int[]>();
            foreach (int[] cornerLocation in cornerLocations)
            {
                if (IsPlayableNode(cornerLocation[0], cornerLocation[1], selectedColor))
                {
                    playableCorners.Add(cornerLocation);
               }
            }

            return playableCorners;
        }

        public List<int[]> GetPlayablePieceOffsets(int[,] piece)
        {
            // Create a small board. All the pieces will fit inside the inner 5x5 area allowing for the corner caluclations to all appear.
            Board miniBoard = new Board(7);
            Colors color = Colors.Blue;

            // Place the piece on it.
            miniBoard.PlacePiece(1, 1, piece, color);

            // Get the playable corners (the offsets).
            List<int[]> playableCorners = miniBoard.GetPlayableCorners(color);

            // Change the origin of the piece to zero.
            List<int[]> playablePieceOffsets = new List<int[]>();
            foreach (int[] cornerLocation in playableCorners)
            {
                playablePieceOffsets.Add(new int[] { cornerLocation[0] - 1, cornerLocation[1] - 1 });
            }

            return playablePieceOffsets;
        }

        public bool PlacePiece(int xOrigin, int yOrigin, int[,] piece, Colors color)
        {
            List<int[]> locations = ConvertPieceToLocations(xOrigin, yOrigin, piece);
            return UpdateSquares(locations, color);
        }

        public bool IsPiecePlayable(int xOrigin, int yOrigin, int[,] piece, Colors color)
        {
            List<int[]> locations = ConvertPieceToLocations(xOrigin, yOrigin, piece);

            return ValidSquarePlacements(locations, color);
        }

        public List<int[]> ConvertPieceToLocations(int xOrigin, int yOrigin, int[,] piece)
        {
            List<int[]> locations = new List<int[]>();
            for (int i = 0; i < piece.GetLength(0); i++)
            {
                for (int j = 0; j < piece.GetLength(1); j++)
                {
                    if (piece[i, j] == 1)
                    {
                        locations.Add(new int[] { xOrigin + i, yOrigin + j });
                    }
                }
            }

            return locations;
        }

        public bool ValidSquarePlacements(List<int[]> locations, Colors color)
        {
            foreach (int[] location in locations)
            {

                if (!ValidSquarePlacement(location[0], location[1], color))
                {
                    return false;
                }
            }

            return true;
        }

        public bool UpdateSquares(List<int[]> locations, Colors color)
        {
            if (!ValidSquarePlacements(locations, color))
            {
                return false;
            }

            UpdateEmptySquares(locations, color);


            return true;
        }

        public void UpdateEmptySquares(List<int[]> locations, Colors color)
        {

            foreach (int[] location in locations)
            {
                int x = location[0];
                int y = location[1];

                UpdateEmptySquare(x, y, color);
            }
        }

        public void UpdateEmptySquare(int x, int y, Colors color)
        {
            BoardValues[x, y] = color;
        }

        public bool UpdateSquare(int x, int y, Colors color)
        {
            // If the square is not on the board.
            if (!SquareIsOnBoard(x, y))
            {
                // The placement was not valid.
                return false;
            }

            // If the location is already populated.
            if (!GetIsLocationEmpty(x, y))
            {
                // The placment was not valid.
                return false;
            }

            UpdateEmptySquare(x, y, color);

            // The placement was valid.
            return true;
        }

        public bool ValidSquarePlacement(int x, int y, Colors color)
        {
            // The square is off of the board.
            if (!SquareIsOnBoard(x, y))
            {
                return false;
            }

            // The location is already populated.
            if (GetLocationColor(x, y) != Colors.Empty)
            {
                return false;
            }

            // If there is a neighboring square that is the same color.
            if (IsColorInValues(GetValuesByLocations(GetEdgeLocations(x, y)), color))
            {
                return false;
            }

            // The squre placement is valid.
            return true;

        }

        public Colors GetLocationColor(int x, int y)
        {
            return BoardValues[x, y];
        }

        public bool GetIsLocationEmpty(int x, int y)
        {
            return GetLocationHasColor(x, y, Colors.Empty);
        }

        public bool GetLocationHasColor(int x, int y, Colors color)
        {
            return GetLocationColor(x, y) == color;
        }

        public List<int[]> GetPlayableNodes(Colors color)
        {
            List<int[]> playableNodes = new List<int[]>();
            for (int i = 0; i < BoardValues.GetLength(0); i++)
            {
                for (int j = 0; j < BoardValues.GetLength(1); j++)
                {
                    if (IsPlayableNode(i, j, color))
                    {
                        playableNodes.Add(new int[] { i, j });
                    }
                }
            }

            return playableNodes;
        }

        public bool IsPlayableNode(int x, int y, Colors color)
        {
            // If the location is already taken
            if (GetLocationColor(x, y) != Colors.Empty)
            {
                return false;
            }

            // If there is already the same color in a neighboring square.
            if (IsColorInValues(GetEdgeValues(x, y), color))
            {
                return false;
            }

            // If there is not another piece of the same color to play off of.
            if (!IsColorInValues(GetCornerValues(x, y), color))
            {
                return false;
            }

            // The node is playable.
            return true;
        }

        public string GetBoardString() // Not tested.
        {
            string boardString = "";
            for (int i = 0; i < BoardValues.GetLength(0); i++)
            {
                boardString = boardString + "\n";
                for (int j = 0; j < BoardValues.GetLength(1); j++)
                {
                    boardString = boardString + " " + ((int)BoardValues[i, j]).ToString();
                }
            }
            return boardString;
        }

        public string GetBoardCSV() // Not tested.
        {
            string boardString = "";
            for (int i = 0; i < BoardValues.GetLength(0); i++)
            {
                for (int j = 0; j < BoardValues.GetLength(1); j++)
                {
                    boardString = boardString + BoardValues[i, j].ToString() + ",";
                }
            }

            return boardString;
        }

        public bool IsValidLocationFromOrigin(int xOrigin, int yOrigin, int xOffset, int yOffset)
        {
            int[] location = GetLocationFromOrigin(xOrigin, yOrigin, xOffset, yOffset);
            int locationX = location[0];
            int locationY = location[1];
            return SquareIsOnBoard(locationX, locationY);
        }

        public int[] GetLocationFromOrigin(int xOrigin, int yOrigin, int xOffset, int yOffset)
        {
            return new int[] { xOrigin + xOffset, yOrigin + yOffset };
        }

        public bool AreAllValuesEmpty(List<Colors> values)
        {
            // If one value is not populated then they are all populated.
            return !IsOneValuePopuloated(values);
        }

        public bool IsOneValuePopuloated(List<Colors> values)
        {
            return !AreAllValuesColor(values, Colors.Empty);
        }

        public bool IsColorInValues(List<Colors> values, Colors color)
        {
            for (int i = 0; i < values.Count(); i++)
            {
                if (values[i] == color)
                {
                    return true;
                }
            }

            return false;
        }

        public bool AreAllValuesColor(List<Colors> values, Colors color)
        {
            for (int i = 0; i < values.Count(); i++)
            {
                Colors value = values[i];
                if (value != color)
                {
                    return false;
                }
            }

            return true;
        }

        public List<int[]> GetPopulatedLocations()
        {
            return GetLocationsWithoutValue(Colors.Empty);
        }

        public List<int[]> GetUnpopulatedLocations()
        {
             return GetLocationsWithValue(Colors.Empty);
        }

        public List<int[]> GetLocationsWithoutValue(Colors color)
        {
            List<int[]> locations = new List<int[]>();
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (GetLocationColor(x, y) != color)
                    {
                        locations.Add(new int[] { x, y });
                    }
                }
            }

            return locations;
        }

        public List<int[]> GetLocationsWithValue(Colors color)
        {
            List<int[]> locations = new List<int[]>();
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (GetLocationColor(x, y) == color)
                    {
                        locations.Add(new int[] { x, y });
                    }
                }
            }

            return locations;
        }

        public int GetSquarePerimeterCount()
        {
            return GetPerimeterLength() - 4;
        }

        public int GetPerimeterLength()
        {
            return Width * 2 + Height * 2;
        }

        public List<Colors> GetEdgeValues(int x, int y)
        {
            return GetValuesByLocations(GetEdgeLocations(x, y));
        }

        public List<Colors> GetCornerValues(int x, int y)

        {
            return GetValuesByLocations(GetCornerLocations(x, y));
        }

        public List<Colors> GetValuesByLocations(List<int[]> locations)
        {
            List<Colors> values = new List<Colors>();
            for (int i = 0; i < locations.Count(); i++)
            {

                int x = locations[i][0];
                int y = locations[i][1];

                values.Add(GetLocationColor(x, y));
            }

            return values;
        }

        public List<int[]> GetEdgeLocations(int x, int y)
        {
            List<int[]> locations = new List<int[]>();

            if (SquareIsOnBoard(x + 1, y)) // Positive X.
            {
                locations.Add(new int[] { x + 1, y });
            }

            if (SquareIsOnBoard(x - 1, y)) // Negative X
            {
                locations.Add(new int[] { x - 1, y });
            }

            if (SquareIsOnBoard(x, y + 1)) // Positive Y
            {
                locations.Add(new int[] { x, y + 1 });
            }

            if (SquareIsOnBoard(x, y - 1)) // Negative Y
            {
                locations.Add(new int[] { x, y - 1 });
            }

            return locations;
        }

        public List<int[]> GetCornerLocations(int x, int y)
        {
            List<int[]> locations = new List<int[]>();

            if (SquareIsOnBoard(x + 1, y + 1)) // Positive X and Y.
            {
                locations.Add(new int[] { x + 1, y + 1 });
            }

            if (SquareIsOnBoard(x - 1, y - 1)) // Negative X and Y
            {
                locations.Add(new int[] { x - 1, y - 1 });
            }

            if (SquareIsOnBoard(x - 1, y + 1)) // Negative X and positive Y.
            {
                locations.Add(new int[] { x - 1, y + 1 });
            }

            if (SquareIsOnBoard(x + 1, y - 1)) // Positive X and negative Y.
            {
                locations.Add(new int[] { x + 1, y - 1 });
            }

            return locations;
        }

        public bool SquareIsOnBoard(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }
    }
}
