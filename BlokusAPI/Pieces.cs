using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlokusAPI.Pieces
{
    public class Pieces
    {
        // One Pieces
        // O
        public static int[,] onePiece = new int[,] {
            {1}
        };
        public static int[] onePieceDimensions = new int[] { 1, 1 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] onePieceSymmetries = new int[] { 1, 0, 0, 0, 0, 0, 0, 0 };

        // Dominos (https://en.wikipedia.org/wiki/Domino_(mathematics))
        // L
        public static int[,] domino = new int[,] {
            { 1 }, { 1 }
        };
        public static int[] dominoDimensions = new int[] { 2, 1 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] dominoSymmetries = new int[] { 1, 1, 0, 0, 0, 0, 0, 0 };

        // Trominos (https://en.wikipedia.org/wiki/Tromino)
        // I
        public static int[,] trominoI = new int[,] {
            { 1 },
            { 1 },
            { 1 }
        };
        public static int[] trominoIDimensions = new int[] { 3, 1 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] trominoISymmetries = new int[] { 1, 1, 0, 0, 0, 0, 0, 0 };

        // L
        public static int[,] trominoL = new int[,] {
            { 1, 1 },
            { 1, 0 }
        };
        public static int[] trominoLDimenions = new int[] { 2, 2 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] trominoLSymmetries = new int[] { 1, 1, 1, 1, 0, 0, 0, 0 };

        // Tetromino (https://en.wikipedia.org/wiki/Tetromino)
        // I
        public static int[,] tetrominoI = new int[,] {
            { 1 },
            { 1 },
            { 1 },
            { 1 }
        };
        public static int[] tetrominoIDimensions = new int[] { 4, 1 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] tetrominoISymmetries = new int[] { 1, 1, 0, 0, 0, 0, 0, 0 };

        // O
        public static int[,] tetrominoO = new int[,] {
            { 1, 1 },
            { 1, 1 }
        };
        public static int[] tetrominoODimensions = new int[] { 2, 2 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] tetrominoOSymmetries = new int[] { 1, 0, 0, 0, 0, 0, 0, 0 };

        // T
        public static int[,] tetrominoT = new int[,] {
            { 1, 0 },
            { 1, 1 },
            { 1, 0 }
        };
        public static int[] tetrominoTDimensions = new[] { 3, 2 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] tetrominoTSymmetries = new int[] { 1, 1, 1, 1, 0, 0, 0, 0 };

        // L
        public static int[,] tetrominoL = new int[,] {
            { 1, 1 },
            { 1, 0 },
            { 1, 0 }
        };
        public static int[] tetrominoLDimensions = new int[] { 3, 1 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] tetrominoLSymmetries = new int[] { 1, 1, 1, 1, 1, 1, 1, 1 };

        // S
        public static int[,] tetrominoS = new int[,] {
            { 1, 1, 0 },
            { 0, 1, 1 }
        };
        public static int[] tetrominoSDimensions = new[] { 2, 3 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] tetrominoSSymmetries = new int[] { 1, 1, 0, 0, 1, 1, 0, 0 };

        // Pentominos (https://en.wikipedia.org/wiki/Pentomino)
        // F
        public static int[,] pentominoF = new int[,] {
            { 1, 1, 0 },
            { 0, 1, 1 },
            { 0, 1, 0 },
        };
        public static int[] pentominoFDimensions = new[] { 3, 3 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] pentominoFSymmetries = new int[] { 1, 1, 1, 1, 1, 1, 1, 1 };

        // I
        public static int[,] pentominoI = new int[,] {
            { 1 },
            { 1 },
            { 1 },
            { 1 },
            { 1 }
        };
        public static int[] pentominoIDimensions = new[] { 5, 1 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] pentominoISymmetries = new int[] { 1, 1, 0, 0, 0, 0, 0, 0 };

        // L
        public static int[,] pentominoL = new int[,] {
            { 1, 1, 1, 1 },
            { 0, 0, 0, 1 },
        };
        public static int[] pentominoLDimensions = new[] { 2, 4 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] pentominoLSymmetries = new int[] { 1, 1, 1, 1, 1, 1, 1, 1 };

        // N
        public static int[,] pentominoN = new int[,] {
            { 1, 1, 0, 0 },
            { 0, 1, 1, 1 },
        };
        public static int[] pentominoNDimensions = new[] { 2, 4 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] pentominoNSymmetries = new int[] { 1, 1, 1, 1, 1, 1, 1, 1 };

        // P
        public static int[,] pentominoP = new int[,] {
            { 1, 1, 1 },
            { 0, 1, 1 },
        };
        public static int[] pentominoPDimensions = new[] { 2, 3 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] pentominoPSymmetries = new int[] { 1, 1, 1, 1, 1, 1, 1, 1 };

        // T
        public static int[,] pentominoT = new int[,] {
            { 1, 1, 1 },
            { 0, 1, 0 },
            { 0, 1, 0 },
        };
        public static int[] pentominoTDimensions = new[] { 3, 3 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] pentominoTSymmetries = new int[] { 1, 1, 1, 1, 0, 0, 0, 0 };

        // U
        public static int[,] pentominU = new int[,] {
            { 1, 1, 1 },
            { 1, 0, 1 },
        };
        public static int[] pentominoUDimensions = new[] { 2, 3 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] pentominoUSymmetries = new int[] { 1, 1, 1, 1, 0, 0, 0, 0 };

        // V
        public static int[,] pentominoV = new int[,] {
            { 1, 1, 1 },
            { 1, 0, 0 },
            { 1, 0, 0 },
        };
        public static int[] pentominoVDimensions = new[] { 3, 3 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] pentominoVSymmetries = new int[] { 1, 1, 1, 1, 0, 0, 0, 0 };

        // W
        public static int[,] pentominoW = new int[,] {
            { 1, 0, 0 },
            { 1, 1, 0 },
            { 0, 1, 1 },
        };
        public static int[] pentominoWDimensions = new[] { 3, 3 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] pentominoWSymmetries = new int[] { 1, 1, 1, 1, 0, 0, 0, 0 };

        // X
        public static int[,] pentominoX = new int[,] {
            { 0, 1, 0 },
            { 1, 1, 1 },
            { 0, 1, 0 },
        };
        public static int[] pentominoXDimensions = new[] { 3, 3 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] pentominoXSymmetries = new int[] { 1, 0, 0, 0, 0, 0, 0, 0 };

        // Y
        public static int[,] pentominoY = new int[,] {
            { 1, 1, 1, 1 },
            { 0, 0, 1, 0 },
        };
        public static int[] pentominoYDimensions = new[] { 2, 4 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] pentominoYSymmetries = new int[] { 1, 1, 1, 1, 1, 1, 1, 1 };

        // Z
        public static int[,] pentominoZ = new int[,] {
            { 1, 1, 0 },
            { 0, 1, 0 },
            { 0, 1, 1 },
        };
        public static int[] pentominoZDimensions = new[] { 3, 3 };

        // 0, 90, 180, 270, 0 + mirror, 90 + mirror, 180 + mirror, 270 + mirror
        public static int[] pentominoZSymmetries = new int[] { 1, 1, 0, 0, 1, 1, 0, 0 };

        //public int[,,] getSymmetriesOutput(int[,] shape, int[] symmetries)
        //{
        //    IEnumerable<int> rotationSymmetries = symmetries.Take(4);
        //    IEnumerable<int> mirrorAndRotationSymmetries = symmetries.Skip(4);
        //}

        public static int[,] rotatePiece(int[,] piece, int times)
        {
            for (int i = 0; i < times; i++)
            {
                piece = rotatePiece90(piece);
            }

            return piece;
        }

        public static int[,] rotatePiece90(int[,] piece)
        {
            return mirrorPieceHorizontally(transposePiece(piece));
        }

        public static int[,] transposePiece(int[,] piece)
        {
            int pieceWidth = piece.GetLength(0);
            int pieceHeight = piece.GetLength(1);

            int [,] transposedPiece = new int[pieceHeight, pieceWidth];
            for (int i = 0; i < pieceWidth; i++)
            {
                for (int j = 0; j < pieceHeight; j++)
                {
                    transposedPiece[j, i] = piece[i, j];
                }
            }

            return transposedPiece;
        }

        public static int[,] mirrorPieceHorizontally(int[,] piece)
        {
            int pieceWidth = piece.GetLength(0);
            int pieceHeight = piece.GetLength(1);

            int[,] mirroredPiece = new int[pieceWidth, pieceHeight];
            for (int i = 0; i < pieceWidth; i++)
            {
                for (int j = 0; j < pieceHeight; j++)
                {
                    mirroredPiece[i, j] = piece[i, pieceWidth - 1 - j];
                }
            }

            return mirroredPiece;
        }

        public static int[,] convertPieceToLocations(int xOrigin, int yOrigin, int[,] piece)
        {
            int locationCount = 0;
            for (int i = 0; i < piece.GetLength(0); i++)
            {
                for (int j = 0; j < piece.GetLength(1); j++)
                {
                    if (piece[i, j] == 1)
                    {
                        locationCount++;
                    }
                }
            }

            int locationIndex = 0;
            int[,] locations = new int[locationCount, 2];
            for (int i = 0; i < piece.GetLength(0); i++)
            {
                for (int j = 0; j < piece.GetLength(1); j++)
                {
                    if (piece[i, j] == 1)
                    {
                        locations[locationIndex, 0] = i + xOrigin;
                        locations[locationIndex, 1] = j + yOrigin;
                        locationIndex++;
                    }
                }
            }

            return locations;
        }
    }
}
