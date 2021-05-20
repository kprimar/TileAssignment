using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Assignment
{
    class Game
    {
        int currentFrame = 0;
        public bool gameActive = false;
        public const int maxRow = 10;
        public const int maxCol = 10;
        Tile[,] map = new Tile[maxRow, maxCol];
        Player player;
        Monster monster;

        public void StartGame()
        {
            gameActive = true;
            player = new Player();
            monster = new Monster();
            BuildMap();
        }

        public void Update()
        {
            RenderMap();
            MovePlayer();
            MoveMonster();
            CheckMap();
        }

        private void MoveMonster()
        {
            if (monster.MyRow < player.MyRow)
            {
                monster.MyRow++;
            }
            else if (monster.MyRow > player.MyRow)
            {
                monster.MyRow--;
            }
            else if (monster.MyCol < player.MyCol)
            {
                monster.MyCol++;
            }
            else if (monster.MyCol > player.MyCol)
            {
                monster.MyCol--;
            }
        }

        private void GameOver()
        {
            gameActive = false;
            Console.Clear();
            Console.WriteLine("Game Over");
        }

        private void MovePlayer()
        {
            ConsoleKeyInfo input = Console.ReadKey();

            if (input.Key == ConsoleKey.W || input.Key == ConsoleKey.UpArrow)
            {
                if (player.MyRow > 0 && map[player.MyRow, player.MyCol].isWall != true)
                {
                    player.MyRow--;
                }
            }
            else if (input.Key == ConsoleKey.S || input.Key == ConsoleKey.DownArrow)
            {
                if(player.MyRow < maxRow - 1)
                {
                    player.MyRow++;
                }
            }
            else if (input.Key == ConsoleKey.A || input.Key == ConsoleKey.LeftArrow)
            {
                if (player.MyCol > 0)
                {
                    player.MyCol--;
                }
            }
            else if (input.Key == ConsoleKey.D || input.Key == ConsoleKey.RightArrow)
            {
                if(player.MyCol < maxCol - 1)
                {
                    player.MyCol++;
                }
            }
        }

        private void BuildMap()
        {
            for (int i = 0; i < maxRow; i++)
            {
                for (int j = 0; j < maxCol; j++)
                {
                    Tile thisTile = new Tile();
                    map[i, j] = thisTile;
                    if (i == 0 || j == 0 || i == maxRow - 1 || j == maxCol - 1)
                    {
                        thisTile.isWall = true;
                    }
                }
            }
            map[3, 2].isWall = true;
            map[3, 3].isWall = true;
            map[3, 6].isWall = true;
            map[3, 7].isWall = true;
            map[6, 5].isWall = true;
            map[6, 6].isWall = true;
            map[7, 3].isWall = true;
            map[7, 4].isWall = true;
        }

        public void RenderMap()
        {
            Console.Clear();
            Console.WriteLine("---FRAME " + currentFrame + "---");
            currentFrame++;
            char symbolToPrint;
            for (int i = 0; i < maxRow; i++)
            {
                for (int j = 0; j < maxCol; j++)
                {
                    if(i == player.MyRow && j == player.MyCol)
                    {
                        symbolToPrint = player.Symbol;
                    }
                    else if (i == monster.MyRow && j == monster.MyCol)
                    {
                        symbolToPrint = monster.Symbol;
                    }
                    else
                    {
                        symbolToPrint = map[i, j].GetSymbol();
                    }
                    Console.Write(symbolToPrint);
                }
                Console.WriteLine();
            }
        }
        private void CheckMap()
        {
            if (player.MyRow == monster.MyRow && player.MyCol == monster.MyCol)
            {
                GameOver();
            }
        }
    }

}
