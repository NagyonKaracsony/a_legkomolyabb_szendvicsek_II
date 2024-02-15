using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace jani_a_varban
{
    internal class Map : Game1
    {
        public static Random random = new Random();
        public static List<List<int>> Generate(int width, int height)
        {
            List<List<int>> labyrinth = new List<List<int>>();

            for (int i = 0; i < height; i++)
            {
                List<int> row = new List<int>();
                for (int j = 0; j < width; j++) row.Add(1);
                labyrinth.Add(row);
            }

            for (int i = 1; i < height - 1; i++)
            {
                for (int j = 1; j < width - 1; j++) labyrinth[i][j] = 2;
            }

            int entranceRow = random.Next(2, (height - 2));
            labyrinth[entranceRow][0] = 0; // Entrance
            Game1.EnteranceCoordinates = new Vector2 { X = 0, Y = entranceRow * 72 };
            int exitRow = random.Next(2, height - 2);
            labyrinth[exitRow][width - 1] = 0; // Exit

            Stack<(int, int)> stack = new Stack<(int, int)>();
            (int, int) current = (entranceRow, 0);
            stack.Push(current);

            while (stack.Count > 0)
            {
                labyrinth[current.Item1][current.Item2] = 0; // Mark the cell as part of the path
                List<(int, int)> neighbors = GetUnvisitedNeighbors(current, width, height, labyrinth);
                if (neighbors.Count > 0)
                {
                    stack.Push(current);
                    (int, int) next = neighbors[random.Next(neighbors.Count)];
                    RemoveWall(current, next, labyrinth);
                    current = next;
                }
                else current = stack.Pop();
            }

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++) if (labyrinth[i][j] == 2)
                    {
                        if (!((i == 1) || (i == MapHeight) || (j == 1) || (j == MapWidth) || (i == (MapWidth - 1)) || (j == (MapWidth - 1))))
                        {
                            if (random.Next(20) == 1) labyrinth[i][j] = 0;
                        }
                    }
            }

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++) if (labyrinth[i][j] == 2) labyrinth[i][j] = 1;
            }

            for (int i = 1; i < 3; i++) labyrinth[exitRow][width - i] = 0;
            /*
             void corner checker()
             void hallway breaker()?
             */
            return labyrinth;
        }

        private static List<(int, int)> GetUnvisitedNeighbors((int, int) cell, int A, int B, List<List<int>> labyrinth)
        {
            int x = cell.Item1;
            int y = cell.Item2;
            List<(int, int)> neighbors = new List<(int, int)>();

            if (x - 2 > 0 && labyrinth[x - 2][y] == 2)
            {
                neighbors.Add((x - 2, y));
            }
            if (x + 2 < A && labyrinth[x + 2][y] == 2)
            {
                neighbors.Add((x + 2, y));
            }
            if (y - 2 > 0 && labyrinth[x][y - 2] == 2)
            {
                neighbors.Add((x, y - 2));
            }
            if (y + 2 < B && labyrinth[x][y + 2] == 2)
            {
                neighbors.Add((x, y + 2));
            }
            return neighbors;
        }
        private static void RemoveWall((int, int) current, (int, int) next, List<List<int>> labyrinth)
        {
            int x1 = current.Item1;
            int y1 = current.Item2;
            int x2 = next.Item1;
            int y2 = next.Item2;
            labyrinth[(x1 + x2) / 2][(y1 + y2) / 2] = 0; // Clear the wall between the cells
        }
        public static void Display()
        {
            List<List<int>> labyrinth = mapMatrix;

            int height = labyrinth.Count;
            int width = labyrinth[0].Count;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    // char cell = labyrinth[i][j] == 0 ? '0' : labyrinth[i][j] == 1 ? '1' : labyrinth[i][j] == 2 ? 'y' : 'x';                    
                    Debug.Write(labyrinth[i][j]);
                }
                Debug.WriteLine("");
            }
        }
        public static void DrawMap(SpriteBatch spriteBatch, List<List<int>> mapData)
        {
            int x = 0;
            int m = 0;
            for (int i = 0; i < MapHeight; i++)
            {
                for (int y = 0; y < MapWidth; y++)
                {
                    if (mapData[i][y] == 0) spriteBatch.Draw(floorTexture, new Vector2(x, m), Color.White);
                    else spriteBatch.Draw(wallTexture, new Vector2(x, m), Color.White);
                    x += 72;
                }
                x = 0;
                m += 72;
            }
        }
    }
}