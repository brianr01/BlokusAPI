using System;
using System.Collections.Generic;
using System.Linq;

namespace BlokusAPI.Services
{
    public class Board
    {
        public enum Colors { Empty, Red, Green, Blue, Yellow };
        public Colors[,] BoardValues;

        public int Width;
        public int Height;

        public Board(int size = 20)
        {
            BoardValues = new Colors[20, 20];

            Width = BoardValues.GetLength(0);
            Height = BoardValues.GetLength(1);
        }

        public int Get()
        {
            return (int)BoardValues[0, 0];
        }

        public void Moves()
        {
            Colors selectedColor = Colors.Red;
            List<Pieces.Piece> availablePieces = new List<Pieces.Piece> { Pieces.Piece.PentominoF, Pieces.Piece.PentominoL };


            List<int[]> playableCorners = GetPlayableCorners(selectedColor);

            Dictionary<Pieces.Piece, List<int[,]>> piecesWithSymmetries = Pieces.GetPiecesWithSymmetries(availablePieces);

            List<Tuple<Pieces.Piece, int, int, int[,]>> options = new List<Tuple<Pieces.Piece, int, int, int[,]>>();

            // For each possible symmetry list (pieces -> list)
            foreach (KeyValuePair<Pieces.Piece, List<int[,]>> pieceSymmetries in piecesWithSymmetries)
            {
                // For each possible symmetry (list -> symmetry)
                foreach (int[,] piece in pieceSymmetries.Value)
                {
                    List<int[]> playablePieceOffsets = GetPlayablePieceOffsets(piece);

                    // For each the locations that are playable from the symmetry (symmetry -> playable offsets)
                    foreach (int[] offset in playablePieceOffsets)
                    {
                        // For each playable location from the symmetry (playable offsets -> corner)
                        foreach (int[] playableCorner in playableCorners)
                        {
                            int x = playableCorner[0] - offset[0];
                            int y = playableCorner[1] - offset[1];
                            if (isPiecePlayable(x, y, piece, selectedColor))
                            {
                                options.Add(new Tuple<Pieces.Piece, int, int, int[,]>(pieceSymmetries.Key, x, y, piece));
                            }

                        }
                    }
                }
            }

            // return options
        }

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
            Board miniBoard = new Board(7);
            miniBoard.PlacePiece(1, 1, piece, Colors.Blue);

            List<int[]> playableCorners = miniBoard.GetPlayableCorners(Colors.Blue);

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

        public bool isPiecePlayable(int x, int y, int[,] piece, Colors color)
        {
            List<int[]> locations = ConvertPieceToLocations(x, y, piece);

            return AreSquaresPlayable(locations, color);
        }

        public List<int[]> ConvertPieceToLocations(int x, int y, int[,] piece)
        {
            List<int[]> locations = new List<int[]>();
            for (int i = 0; i < piece.GetLength(0); i++)
            {
                for (int j = 0; j < piece.GetLength(1); j++)
                {
                    if (piece[i, j] == 1)
                    {
                        locations.Add(new int[] { x + i, y + j });
                    }
                }
            }

            return locations;
        }

        public bool AreSquaresPlayable(int[,] locations, Colors color)
        {
            for (int i = 0; i < locations.GetLength(0); i++)
            {
                int x = locations[i, 0];
                int y = locations[i, 1];

                if (!ValidSquarePlacement(x, y, color))
                {
                    return false;
                }
            }

            return true;
        }

        public bool AreSquaresPlayable(List<int[]> locations, Colors color)
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

        public bool UpdateSquares(int[,] locations, Colors color)
        {
            if (!AreSquaresPlayable(locations, color))
            {
                return false;
            }

            UpdateEmptySquares(locations, color);

            return true;
        }

        public bool UpdateSquares(List<int[]> locations, Colors color)
        {
            for (int i = 0; i < locations.Count(); i++)
            {
                int[] location = locations[i];

                if (!ValidSquarePlacement(location[0], location[1], color))
                {
                    return false;
                }
            }

            UpdateEmptySquares(locations, color);

            return true;
        }

        public void UpdateEmptySquares(int[,] locations, Colors color)
        {

            for (int i = 0; i < locations.GetLength(0); i++)
            {
                int x = locations[i, 0];
                int y = locations[i, 1];

                UpdateEmptySquare(x, y, color);
            }
        }

        public void UpdateEmptySquares(List<int[]> locations, Colors color)
        {

            for (int i = 0; i < locations.Count(); i++)
            {
                int[] location = locations[i];

                UpdateEmptySquare(location[0], location[1], color);
            }
        }

        public void UpdateEmptySquaresFromList(List<int[]> locations, Colors color)
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
            if (!ValidSquarePlacement(x, y, color))
            {
                return false;
            }

            BoardValues[x, y] = color;

            return true;
        }

        public bool ValidSquarePlacement(int x, int y, Colors color)
        {
            // The location is empty.
            if (GetLocationColor(x, y) != Colors.Empty)
            {
                return false;
            }

            // The location above has the same color.
            if (y > 1 && GetLocationColor(x, y - 1) == color)
            {
                return false;
            }

            // The the location below has the same color.
            if (y < 20 && GetLocationColor(x, y + 1) == color)
            {
                return false;
            }

            // The the location to the left has the same color.
            if (x > 1 && GetLocationColor(x - 1, y) == color)
            {
                return false;
            }

            // The the location to the right has the same color.
            if (x < 1 && GetLocationColor(x + 1, y) == color)
            {
                return false;
            }

            return true;

        }

        public bool SquareIsOnBoard(int x, int y)
        {
            return x > 0 && x <= 21 && y > 0 && x > 21;
        }

        public Colors GetLocationColor(int x, int y)
        {
            return BoardValues[x, y];
        }

        public List<int[]> getPlayableNodes(Colors color)
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
            if (BoardValues[x, y] != Colors.Empty)
            {
                return false;
            }

            // If there is already the same color in a neighboring square.
            if (!areAllValuesEmpty(GetEdgeValues(x, y)))
            {
                return false;
            }

            // If there is another piece to play off of.
            if (!IsOneValuePopuloated(GetCornerValues(x, y)))
            {
                return false;
            }

            // There was not a piece to play off of.
            return false;
        }

        public string GetBoardString()
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

        public string GetBoardCSV()
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
            int[] location = getLocationFromOrigin(xOrigin, yOrigin, xOffset, yOffset);
            return IsLocationOnGrid(location[0], location[1]);
        }

        public int[] getLocationFromOrigin(int xOrigin, int yOrigin, int xOffset, int yOffset)
        {
            return new int[] { xOrigin + xOffset, yOrigin + yOffset };
        }

        public bool areAllValuesEmpty(List<Colors> values)
        {
            for (int i = 0; i < values.Count(); i++)
            {
                if ((int)values[i] >= 1)
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsOneValuePopuloated(List<Colors> values)
        {
            for (int i = 0; i < values.Count(); i++)
            {
                if ((int)values[i] >= 1)
                {
                    return true;
                }
            }

            return false;
        }

        // Method to get a list of populated locations
        public List<int[]> GetPopulatedLocations()
        {
            List<int[]> locations = new List<int[]>();
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if ((int)BoardValues[x, y] >= 1)
                    {
                        locations.Add(new int[] { x, y });
                    }
                }
            }

            return locations;
        }

        public List<int[]> GetUnpopulatedLocations()
        {
            List<int[]> populatedLocations = GetLocationsWithValue(Colors.Empty);

            return populatedLocations;
        }

        public List<int[]> GetLocationsWithValue(Colors color)
        {
            List<int[]> locations = new List<int[]>();
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (BoardValues[x, y] == color)
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
            return GetValuesByLocations(GetCornerLocations(x, y));
        }

        public List<Colors> GetCornerValues(int x, int y)
        {
            return GetValuesByLocations(GetEdgeLocations(x, y));
        }

        public List<Colors> GetValuesByLocations(List<int[]> locations)
        {
            List<Colors> values = new List<Colors>();
            for (int i = 0; i < locations.Count(); i++)
            {
                values.Add(BoardValues[locations[i][0], locations[i][1]]);
            }

            return values;
        }

        public List<int[]> GetEdgeLocations(int x, int y)
        {
            List<int[]> locations = new List<int[]>();

            if (IsLocationOnGrid(x + 1, y))
            {
                locations.Add(new int[] { x + 1, y });
            }

            if (IsLocationOnGrid(x - 1, y))
            {
                locations.Add(new int[] { x - 1, y });
            }

            if (IsLocationOnGrid(x, y + 1))
            {
                locations.Add(new int[] { x, y + 1 });
            }

            if (IsLocationOnGrid(x, y - 1))
            {
                locations.Add(new int[] { x, y - 1 });
            }

            return locations;
        }

        public List<int[]> GetCornerLocations(int x, int y)
        {
            List<int[]> locations = new List<int[]>();

            if (IsLocationOnGrid(x + 1, y + 1))
            {
                locations.Add(new int[] { x + 1, y + 1 });
            }

            if (IsLocationOnGrid(x - 1, y - 1))
            {
                locations.Add(new int[] { x - 1, y - 1 });
            }

            if (IsLocationOnGrid(x - 1, y + 1))
            {
                locations.Add(new int[] { x - 1, y + 1 });
            }

            if (IsLocationOnGrid(x + 1, y - 1))
            {
                locations.Add(new int[] { x + 1, y - 1 });
            }

            return locations;
        }

        public bool IsLocationOnGrid(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }
    }
}
