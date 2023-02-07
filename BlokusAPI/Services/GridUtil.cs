using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlokusAPI.Services
{
    public class GridUtil
    {
        public int[,] Grid;
        public int Width;
        public int Height;

        public GridUtil(int[,] pGrid)
        {
            Grid = pGrid;
            Width = Grid.GetLength(0);
            Height = Grid.GetLength(1);
        }

        public bool IsValidLocationFromOrigin(int xOrigin, int yOrigin, int xOffset, int yOffset)
        {
            int[] location = GetLocationFromOrigin(xOrigin, yOrigin, xOffset, yOffset);
            return IsLocationOnGrid(location[0], location[1]);
        }

        public int[] GetLocationFromOrigin(int xOrigin, int yOrigin, int xOffset, int yOffset)
        {
            return new int[] { xOrigin + xOffset, yOrigin + yOffset };
        }

        // Method to get a list of populated locations
        public List<int[]> GetPopulatedLocations()
        {
            List<int[]> populatedLocations = GetLocationsWithValue(1);

            return populatedLocations;
        }

        public List<int[]> GetUnpopulatedLocations()
        {
            List<int[]> populatedLocations = GetLocationsWithValue(0);

            return populatedLocations;
        }

        public List<int[]> GetLocationsWithValue(int value)
        {
            List<int[]> locations = new List<int[]>();
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (Grid[x, y] == value)
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

        public List<int> GetEdgeValues(int x, int y)
        {
            return GetValuesByLocations(GetCornerLocations(x, y));
        }

        public List<int> GetCornerValues(int x, int y)
        {
            return GetValuesByLocations(GetEdgeLocations(x, y));
        }

        public List<int> GetValuesByLocations(List<int[]> locations)
        {
            List<int> values = new List<int>();
            for (int i = 0; i < locations.Count(); i++)
            {
                values.Add(Grid[locations[i][0], locations[i][1]]);
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
