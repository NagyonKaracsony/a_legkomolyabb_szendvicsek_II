using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace jani_a_varban
{
    internal class Map : Game1
    {
        public static List<List<int>> Generate(int width, int height, Vector2 enterance, bool initialGeneration)
        {
            List<List<int>> labyrinth = new List<List<int>>();

            for (int i = 0; i < height; i++)
            {
                List<int> row = new List<int>();
                for (int j = 0; j < width; j++) row.Add(1);
                labyrinth.Add(row);
            }

            for (int i = 1; i < height - 1; i++) for (int j = 1; j < width - 1; j++) labyrinth[i][j] = 2;
            // chunk logic:
            // EnteranceCoordinates = new Vector2 { X = (int)enterance.X, Y = (int)enterance.Y };
            // ExitCoordinates = new Vector2 { X = random.Next(2, height - 2), Y = width - 1 };
            // otherwise:
            EnteranceCoordinates = new Vector2 { X = random.Next(2, height - 2), Y = 0 };
            ExitCoordinates = new Vector2 { X = random.Next(2, height - 2), Y = width - 1 };

            Stack<(int, int)> stack = new Stack<(int, int)>();
            (int, int) current = ((int)EnteranceCoordinates.X, 0);
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

            // kár hogy nem tudtam felhasználni ezt:
            /*
            int rows = labyrinth.Count;
            int columns = labyrinth[0].Count;
            for (int sorIndex = 0; sorIndex < labyrinth.Count; sorIndex++)
            {
                for (int oszlopIndex = 0; oszlopIndex < labyrinth[0].Count; oszlopIndex++)
                {
                    if (oszlopIndex + 1 < columns && oszlopIndex >= 1 && sorIndex + 1 < rows && sorIndex >= 1)
                    {
                        //Végig kell nézni a bal oldalt          
                        int[] balElemek = new int[3];
                        balElemek[0] = labyrinth[(sorIndex - 1)][(oszlopIndex - 1)];
                        balElemek[1] = labyrinth[(sorIndex)][(oszlopIndex - 1)];
                        balElemek[2] = labyrinth[(sorIndex + 1)][(oszlopIndex - 1)];

                        int[] kozepsoElemek = new int[3];
                        kozepsoElemek[0] = labyrinth[(sorIndex - 1)][(oszlopIndex)];
                        kozepsoElemek[1] = labyrinth[(sorIndex)][(oszlopIndex)]; //RÁ NINCSEN SZÜKSÉGEM
                        kozepsoElemek[2] = labyrinth[(sorIndex + 1)][(oszlopIndex)];

                        int[] jobbElemek = new int[3];
                        jobbElemek[0] = labyrinth[(sorIndex - 1)][(oszlopIndex + 1)];
                        jobbElemek[1] = labyrinth[(sorIndex)][(oszlopIndex + 1)];
                        jobbElemek[2] = labyrinth[(sorIndex + 1)][(oszlopIndex + 1)];

                        //Maximum 8 szomszéd lehet
                        //Minimum 3

                        int[] szomszedok = new int[8];
                        for (int i = 0; i < 8; i++)
                        {

                            //If elágazás vagy valami vagy switch vagy valami más
                            //Arról szólna, hogy mit kell ellenőrizni

                            //Bal oldal
                            if (i < 3)
                            {
                                int oszlopModosito = -1;
                                if (i % 3 == 0)
                                {
                                    szomszedok[i] = labyrinth[sorIndex - 1][oszlopIndex + oszlopModosito];
                                }
                                else if (i % 3 == 1)
                                {
                                    szomszedok[i] = labyrinth[sorIndex][oszlopIndex + oszlopModosito];
                                }
                                else if (i % 3 == 2)
                                {
                                    szomszedok[i] = labyrinth[sorIndex + 1][oszlopIndex + oszlopModosito];
                                }
                            }
                            //Jobb oldal
                            if (i >= 3 && i <= 5)
                            {
                                int oszlopModosito = 1;
                                if (i % 3 == 0)
                                {
                                    szomszedok[i] = labyrinth[sorIndex - 1][oszlopIndex + oszlopModosito];
                                }
                                else if (i % 3 == 1)
                                {
                                    szomszedok[i] = labyrinth[sorIndex][oszlopIndex + oszlopModosito];
                                }
                                else if (i % 3 == 2)
                                {
                                    szomszedok[i] = labyrinth[sorIndex + 1][oszlopIndex + oszlopModosito];
                                }
                            }
                            //Középső oldal
                            if (i >= 6 && i <= 8)
                            {
                                if (i % 3 == 0)
                                {
                                    szomszedok[i] = labyrinth[sorIndex - 1][oszlopIndex]; //Közép felső
                                }
                                else if (i % 3 == 1)
                                {
                                    szomszedok[i] = labyrinth[sorIndex + 1][oszlopIndex]; //Közép alsó
                                }
                            }
                        }
                    }
                    else
                    {
                        // corner check
                    }
                }
            }
            */

            for (int i = 0; i < height; i++) for (int j = 0; j < width; j++) if (labyrinth[i][j] == 2) labyrinth[i][j] = 1;

            for (int i = 1; i < height - 1; i++)
            {
                for (int j = 1; j < width - 1; j++)
                {
                    if (labyrinth[i][j] == 1)
                    {
                        int neighbourWallCount = 0;
                        int diagonalNeighbourWallCount = 0;

                        if (labyrinth[i + 1][j] == 1) neighbourWallCount++;
                        if (labyrinth[i - 1][j] == 1) neighbourWallCount++;
                        if (labyrinth[i][j + 1] == 1) neighbourWallCount++;
                        if (labyrinth[i][j - 1] == 1) neighbourWallCount++;

                        if (labyrinth[i + 1][j + 1] == 1) diagonalNeighbourWallCount++;
                        if (labyrinth[i + 1][j - 1] == 1) diagonalNeighbourWallCount++;
                        if (labyrinth[i - 1][j - 1] == 1) diagonalNeighbourWallCount++;
                        if (labyrinth[i - 1][j + 1] == 1) diagonalNeighbourWallCount++;
                        if (neighbourWallCount + diagonalNeighbourWallCount < 2) if (random.Next(5) == 1) labyrinth[i][j] = 0;
                        // else if (diagonalNeighbourWallCount == 2) if (random.Next(5) == 1) labyrinth[i][j] = 0;
                    }
                }
            }
            for (int i = 1; i < 4; i++) labyrinth[(int)ExitCoordinates.X][width - i] = 0;
            /* teszteléshez
            for (int i = 0; i < height; i++)  for (int j = 0; j < width; j++) labyrinth[i][j] = 0;
            */

            // nincs idő megírni az átlépkedés logikáját... //TODO: fix
            // undorító de működik:
            if (initialGeneration) Chunks.Add(labyrinth);
            else Chunks[0] = labyrinth;

            StartPosition = EnteranceCoordinates;
            // EnteranceCoordinates = ExitCoordinates; (ha lennének chunkok)

            return labyrinth;
        }

        private static List<(int, int)> GetUnvisitedNeighbors((int, int) cell, int A, int B, List<List<int>> labyrinth)
        {
            int x = cell.Item1;
            int y = cell.Item2;
            List<(int, int)> neighbors = new List<(int, int)>();

            if (x - 2 > 0 && labyrinth[x - 2][y] == 2) neighbors.Add((x - 2, y));
            if (x + 2 < A && labyrinth[x + 2][y] == 2) neighbors.Add((x + 2, y));
            if (y - 2 > 0 && labyrinth[x][y - 2] == 2) neighbors.Add((x, y - 2));
            if (y + 2 < B && labyrinth[x][y + 2] == 2) neighbors.Add((x, y + 2));
            return neighbors;
        }
        private static void RemoveWall((int, int) current, (int, int) next, List<List<int>> labyrinth)
        {
            labyrinth[(current.Item1 + next.Item1) / 2][(current.Item2 + next.Item2) / 2] = 0; // Clear the wall between the cells
        }
        public static void Display()
        {
            // char cell = labyrinth[i][j] == 0 ? '0' : labyrinth[i][j] == 1 ? '1' : labyrinth[i][j] == 2 ? 'y' : 'x';                    
            for (int chunkID = 0; chunkID < Chunks.Count; chunkID++)
            {
                Debug.WriteLine($"{chunkID}---------------------------");
                for (int i = 0; i < Chunks[chunkID].Count; i++)
                {
                    for (int j = 0; j < Chunks[chunkID].Count; j++) Debug.Write(Chunks[chunkID][i][j]);
                    Debug.WriteLine("");
                }
            }
        }   
        public static void DrawMap(SpriteBatch spriteBatch)
        {
            for (int chunkID = 0; chunkID < Chunks.Count; chunkID++)
            {
                int offset = (chunkID * MapWidth * 72);
                int m = 0;
                for (int i = 0; i < MapHeight; i++)
                {
                    for (int y = 0; y < MapWidth; y++)
                    {
                        if (Chunks[chunkID][i][y] == 0) spriteBatch.Draw(floorTexture, new Rectangle(offset, m, 75, 75), new Rectangle(0, 0, 95, 95), Color.White);
                        else if (Chunks[chunkID][i][y] == 1) spriteBatch.Draw(wallTexture, new Rectangle(offset, m, 75, 75), new Rectangle(0, 0, 94, 94), Color.White);
                        offset += 72;
                    }
                    offset = (chunkID * MapWidth * 72);
                    m += 72;
                }
            }
        }
        public static void LoadNextLevel(int difficulty)
        {
            mapMatrix = Generate(MapWidth, MapHeight, ExitCoordinates, false);
            enemyList = new List<Enemy>();

            for (int i = 0; i < difficulty; i++) enemyList.Add(new Enemy());
            enemyList[random.Next(1, enemyList.Count)].HasKey = true;
            Player.position = new Vector2(EnteranceCoordinates.Y * 72, EnteranceCoordinates.X * 72);

        }
        public static void GenerateNextChunk()
        {
            mapMatrix = Generate(MapWidth, MapHeight, ExitCoordinates, false);
            Camera.Position = new Vector2(Chunks.Count * 72 * MapWidth - (MapWidth * 72 / 2), MapHeight * 36);

            // next chunk logic
        }
        // neighbour value occurance counter
        public static int CountArrayOccurance(int[] inputArray, int target)
        { // imádom a C#-ot, nincs ennél jobb nyelv
            int occurance = 0;
            for (int i = 0; i < inputArray.Length; i++) if (inputArray[i] == target) occurance++;
            return occurance;
        }
    }
}