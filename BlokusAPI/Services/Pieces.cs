using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlokusAPI.Services
{
    public class Pieces
    {
        // One Pieces
        // O
        public static int[,] OnePiece = new int[,] {
            {1}
        };
        public static int[] OnePieceDimensions = new int[] { 1, 1 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] OnePieceSymmetries = new int[] { 1, 0, 0, 0, 0, 0, 0, 0 };

        // Dominos (https://en.wikipedia.org/wiki/Domino_(mathematics))
        // L
        public static int[,] Domino = new int[,] {
            { 1 }, { 1 }
        };
        public static int[] DominoDimensions = new int[] { 2, 1 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] DominoSymmetries = new int[] { 1, 1, 0, 0, 0, 0, 0, 0 };

        // Trominos (https://en.wikipedia.org/wiki/Tromino)
        // I
        public static int[,] TrominoI = new int[,] {
            { 1 },
            { 1 },
            { 1 }
        };
        public static int[] TrominoIDimensions = new int[] { 3, 1 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] TrominoISymmetries = new int[] { 1, 1, 0, 0, 0, 0, 0, 0 };

        // L
        public static int[,] TrominoL = new int[,] {
            { 1, 1 },
            { 1, 0 }
        };
        public static int[] TrominoLDimenions = new int[] { 2, 2 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] TrominoLSymmetries = new int[] { 1, 1, 1, 1, 0, 0, 0, 0 };

        // Tetromino (https://en.wikipedia.org/wiki/Tetromino)
        // I
        public static int[,] TetrominoI = new int[,] {
            { 1 },
            { 1 },
            { 1 },
            { 1 }
        };
        public static int[] TetrominoIDimensions = new int[] { 4, 1 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] TetrominoISymmetries = new int[] { 1, 1, 0, 0, 0, 0, 0, 0 };

        // O
        public static int[,] TetrominoO = new int[,] {
            { 1, 1 },
            { 1, 1 }
        };
        public static int[] TetrominoODimensions = new int[] { 2, 2 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] TetrominoOSymmetries = new int[] { 1, 0, 0, 0, 0, 0, 0, 0 };

        // T
        public static int[,] TetrominoT = new int[,] {
            { 1, 0 },
            { 1, 1 },
            { 1, 0 }
        };
        public static int[] TetrominoTDimensions = new[] { 3, 2 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] TetrominoTSymmetries = new int[] { 1, 1, 1, 1, 0, 0, 0, 0 };

        // L
        public static int[,] TetrominoL = new int[,] {
            { 1, 1 },
            { 1, 0 },
            { 1, 0 }
        };
        public static int[] TetrominoLDimensions = new int[] { 3, 1 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] TetrominoLSymmetries = new int[] { 1, 1, 1, 1, 1, 1, 1, 1 };

        // S
        public static int[,] TetrominoS = new int[,] {
            { 1, 1, 0 },
            { 0, 1, 1 }
        };
        public static int[] TetrominoSDimensions = new[] { 2, 3 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] TetrominoSSymmetries = new int[] { 1, 1, 0, 0, 1, 1, 0, 0 };

        // Pentominos (https://en.wikipedia.org/wiki/Pentomino)
        // F
        public static int[,] PentominoF = new int[,] {
            { 1, 1, 0 },
            { 0, 1, 1 },
            { 0, 1, 0 },
        };
        public static int[] PentominoFDimensions = new[] { 3, 3 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] PentominoFSymmetries = new int[] { 1, 1, 1, 1, 1, 1, 1, 1 };

        // I
        public static int[,] PentominoI = new int[,] {
            { 1 },
            { 1 },
            { 1 },
            { 1 },
            { 1 }
        };
        public static int[] PentominoIDimensions = new[] { 5, 1 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] PentominoISymmetries = new int[] { 1, 1, 0, 0, 0, 0, 0, 0 };

        // L
        public static int[,] PentominoL = new int[,] {
            { 1, 1, 1, 1 },
            { 0, 0, 0, 1 },
        };
        public static int[] PentominoLDimensions = new[] { 2, 4 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] PentominoLSymmetries = new int[] { 1, 1, 1, 1, 1, 1, 1, 1 };

        // N
        public static int[,] PentominoN = new int[,] {
            { 1, 1, 0, 0 },
            { 0, 1, 1, 1 },
        };
        public static int[] PentominoNDimensions = new[] { 2, 4 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] PentominoNSymmetries = new int[] { 1, 1, 1, 1, 1, 1, 1, 1 };

        // P
        public static int[,] PentominoP = new int[,] {
            { 1, 1, 1 },
            { 0, 1, 1 },
        };
        public static int[] PentominoPDimensions = new[] { 2, 3 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] PentominoPSymmetries = new int[] { 1, 1, 1, 1, 1, 1, 1, 1 };

        // T
        public static int[,] PentominoT = new int[,] {
            { 1, 1, 1 },
            { 0, 1, 0 },
            { 0, 1, 0 },
        };
        public static int[] PentominoTDimensions = new[] { 3, 3 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] PentominoTSymmetries = new int[] { 1, 1, 1, 1, 0, 0, 0, 0 };

        // U
        public static int[,] PentominoU = new int[,] {
            { 1, 1, 1 },
            { 1, 0, 1 },
        };
        public static int[] PentominoUDimensions = new[] { 2, 3 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] PentominoUSymmetries = new int[] { 1, 1, 1, 1, 0, 0, 0, 0 };

        // V
        public static int[,] PentominoV = new int[,] {
            { 1, 1, 1 },
            { 1, 0, 0 },
            { 1, 0, 0 },
        };
        public static int[] PentominoVDimensions = new[] { 3, 3 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] PentominoVSymmetries = new int[] { 1, 1, 1, 1, 0, 0, 0, 0 };

        // W
        public static int[,] PentominoW = new int[,] {
            { 1, 0, 0 },
            { 1, 1, 0 },
            { 0, 1, 1 },
        };
        public static int[] PentominoWDimensions = new[] { 3, 3 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] PentominoWSymmetries = new int[] { 1, 1, 1, 1, 0, 0, 0, 0 };

        // X
        public static int[,] PentominoX = new int[,] {
            { 0, 1, 0 },
            { 1, 1, 1 },
            { 0, 1, 0 },
        };
        public static int[] PentominoXDimensions = new[] { 3, 3 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] PentominoXSymmetries = new int[] { 1, 0, 0, 0, 0, 0, 0, 0 };

        // Y
        public static int[,] PentominoY = new int[,] {
            { 1, 1, 1, 1 },
            { 0, 0, 1, 0 },
        };
        public static int[] PentominoYDimensions = new[] { 2, 4 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] PentominoYSymmetries = new int[] { 1, 1, 1, 1, 1, 1, 1, 1 };

        // Z
        public static int[,] PentominoZ = new int[,] {
            { 1, 1, 0 },
            { 0, 1, 0 },
            { 0, 1, 1 },
        };
        public static int[] pentominoZDimensions = new[] { 3, 3 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mir *ror, 270 + mirror
        public static int[] pentominoZSymmetries = new int[] { 1, 1, 0, 0, 1, 1, 0, 0 };

        public enum Piece
        {
            OnePiece,
            Domino,
            TrominoI,
            TrominoL,
            TetrominoI,
            TetrominoO,
            TetrominoT,
            TetrominoL,
            TetrominoS,
            PentominoF,
            PentominoI,
            PentominoL,
            PentominoN,
            PentominoP,
            PentominoT,
            PentominoU,
            PentominoV,
            PentominoW,
            PentominoX,
            PentominoY,
            PentominoZ
        };

        public static Dictionary<Piece, int[,]> AllPieces = new Dictionary<Piece, int[,]>
        {
            { Piece.OnePiece, OnePiece },
            { Piece.Domino, Domino },
            { Piece.TrominoI, TrominoI },
            { Piece.TrominoL, TrominoL },
            { Piece.TetrominoI, TetrominoI },
            { Piece.TetrominoO, TetrominoO },
            { Piece.TetrominoT, TetrominoT },
            { Piece.TetrominoL, TetrominoL },
            { Piece.TetrominoS, TetrominoS },
            { Piece.PentominoF, PentominoF },
            { Piece.PentominoI, PentominoI },
            { Piece.PentominoL, PentominoL },
            { Piece.PentominoN, PentominoN },
            { Piece.PentominoP, PentominoP },
            { Piece.PentominoT, PentominoT },
            { Piece.PentominoU, PentominoU },
            { Piece.PentominoV, PentominoV },
            { Piece.PentominoW, PentominoW },
            { Piece.PentominoX, PentominoX },
            { Piece.PentominoY, PentominoY },
            { Piece.PentominoZ, PentominoZ }
        };

        public static Dictionary<Piece, int[]> AllPieceSymmetries = new Dictionary<Piece, int[]>
        {
            { Piece.OnePiece, OnePieceSymmetries },
            { Piece.Domino, DominoSymmetries },
            { Piece.TrominoI, TrominoISymmetries },
            { Piece.TrominoL, TrominoLSymmetries },
            { Piece.TetrominoI, TetrominoISymmetries },
            { Piece.TetrominoO, TetrominoOSymmetries },
            { Piece.TetrominoT, TetrominoTSymmetries },
            { Piece.TetrominoL, TetrominoLSymmetries },
            { Piece.TetrominoS, TetrominoSSymmetries },
            { Piece.PentominoF, PentominoFSymmetries },
            { Piece.PentominoI, PentominoISymmetries },
            { Piece.PentominoL, PentominoLSymmetries },
            { Piece.PentominoN, PentominoNSymmetries },
            { Piece.PentominoP, PentominoPSymmetries },
            { Piece.PentominoT, PentominoTSymmetries },
            { Piece.PentominoU, PentominoUSymmetries },
            { Piece.PentominoV, PentominoVSymmetries },
            { Piece.PentominoW, PentominoWSymmetries },
            { Piece.PentominoX, PentominoXSymmetries },
            { Piece.PentominoY, PentominoYSymmetries },
            { Piece.PentominoZ, pentominoZSymmetries }
        };

        public static List<int[,]> GetAllPiecesWithSymmetries()
        {
            List<int[,]> allPiecesWithSymmetries = new List<int[,]>();

            foreach (KeyValuePair<Piece, int[,]> piece in AllPieces)
            {
                allPiecesWithSymmetries.AddRange(GetSymmetriesOutput(piece.Value, AllPieceSymmetries[piece.Key]));
            }

            return allPiecesWithSymmetries;
        }

        public static Dictionary<Piece, List<int[,]>> GetPiecesWithSymmetries(List<Piece> pieces)
        {
            Dictionary<Piece, List<int[,]>> piecesWithSymmetries = new Dictionary<Piece, List<int[,]>>();

            foreach (Piece piece in pieces)
            {
                piecesWithSymmetries.Add(piece, GetSymmetriesOutput(AllPieces[piece], AllPieceSymmetries[piece]));
            }

            return piecesWithSymmetries;
        }

        public static List<int[,]> GetSymmetriesOutput(int[,] shape, int[] symmetries)
        {
            List<int[,]> symmetriesResult = new List<int[,]>();

            List<int> symmetriesList = symmetries.ToList();
            List<int> rotationSymmetries = symmetriesList.GetRange(0, 4);
            List<int> mirrorAndRotationSymmetries = symmetriesList.GetRange(4, 4);

            int i = 0;
            foreach (int rotationSymmetry in rotationSymmetries)
            {
                if (rotationSymmetry == 1)
                {
                    symmetriesResult.Add(RotatePiece(shape, i));
                }
                i++;
            }

            i = 0;
            foreach (int mirrorAndRotationSymmetry in mirrorAndRotationSymmetries)
            {
                if (mirrorAndRotationSymmetry == 1)
                {
                    symmetriesResult.Add(RotatePiece(MirrorPieceHorizontally(shape), i));
                }
                i++;
            }

            return symmetriesResult;
        }

        public static int[,] RotatePiece(int[,] piece, int times)
        {
            for (int i = 0; i < times; i++)
            {
                piece = RotatePiece90(piece);
            }

            return piece;
        }

        public static int[,] RotatePiece90(int[,] piece)
        {
            return MirrorPieceHorizontally(TransposePiece(piece));
        }

        public static int[,] TransposePiece(int[,] piece)
        {
            int pieceWidth = piece.GetLength(0);
            int pieceHeight = piece.GetLength(1);

            int[,] transposedPiece = new int[pieceHeight, pieceWidth];
            for (int i = 0; i < pieceWidth; i++)
            {
                for (int j = 0; j < pieceHeight; j++)
                {
                    transposedPiece[j, i] = piece[i, j];
                }
            }

            return transposedPiece;
        }

        public static int[,] MirrorPieceHorizontally(int[,] piece)
        {
            int pieceWidth = piece.GetLength(0);
            int pieceHeight = piece.GetLength(1);

            int[,] mirroredPiece = new int[pieceWidth, pieceHeight];
            for (int i = 0; i < pieceWidth; i++)
            {
                for (int j = 0; j < pieceHeight; j++)
                {
                    mirroredPiece[i, j] = piece[i, pieceHeight - 1 - j];
                }
            }

            return mirroredPiece;
        }

        public static List<int[]> ConvertPieceToLocations(int[,] piece)
        {
            List<int[]> locations = new List<int[]>();
            for (int i = 0; i < piece.GetLength(0); i++)
            {
                for (int j = 0; j < piece.GetLength(1); j++)
                {
                    if (piece[i, j] == 1)
                    {
                        locations.Add(new int[] { i, j });
                    }
                }
            }

            return locations;
        }

        public static bool IsInsideGrid(int x, int y, int width, int height)
        {
            return x < 0 || x > width || y < 0 || y < height;
        }

        //public static List<int[]> GetPlayableLocationsFromPiece(int[,] piece)
        //{
        //    int empty = 0;
        //    int populated = 1;

        //    List<int[]> playableNodes = new List<int[]>();
        //    // Go over the piece locations from -1, -1 to n + 1, n + 1.
        //    int pieceWidth = piece.GetLength(0);
        //    int pieceHeight = piece.GetLength(1);
        //    for (int x = -1; x < pieceWidth + 1; x++)
        //    {
        //        for (int y = -1; y < pieceHeight + 1; y++)
        //        {
        //            bool validAbove = isInsideGrid(x, y + 1, pieceWidth, pieceHeight);
        //            bool populatedAbove = false;
        //            if (validAbove && piece[x, y - 1] == populated)
        //            {
        //                populatedAbove = true;
        //            }

        //            bool validBelow = isInsideGrid(x, y - 1, pieceWidth, pieceHeight);
        //            bool populatedBelow = false;
        //            if (validAbove && piece[x, y] == populated)
        //            {
        //                populatedAbove = true;
        //            }

        //            // If the location is already taken
        //            if (piece[x, y] != empty)
        //            {
        //                continue;
        //            }

        //            // If there is already the same color in a neighboring square.
        //            if (x < 19 && piece[x + 1, y] == populated)
        //            {
        //                continue;
        //            }
        //            if (x > 0 && piece[x - 1, y] == populated)
        //            {
        //                continue;
        //            }
        //            if (y < 19 && piece[x, y + 1] == populated)
        //            {
        //                continue;
        //            }
        //            if (y > 0 && piece[x, y - 1] == populated)
        //            {
        //                continue;
        //            }

        //            // If there is another piece to play off of.
        //            if (x < 19 && y < 19 && piece[x + 1, y + 1] == populated)
        //            {
        //                continue;
        //            }

        //            if (x > 0 && y < 19 && piece[x - 1, y + 1] == populated)
        //            {
        //                continue;
        //            }

        //            if (x < 19 && y > 0 && piece[x + 1, y - 1] == populated)
        //            {
        //                continue;
        //            }

        //            if (x > 0 && y > 0 && piece[x - 1, y - 1] == populated)
        //            {
        //                continue;
        //            }

        //            // There was not a piece to play off of.
        //            return false;

        //        }
        //    }
        //}
    }
}
