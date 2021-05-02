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
        }

        public void Update()
        {
            BuildMap();
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
                if (player.MyRow > 0)
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
                }
            }
        }

        public void RenderMap()
        {
            Console.Clear();
            Console.WriteLine("---FRAME " + currentFrame + "---");
            currentFrame++;
            for (int i = 0; i < maxRow; i++)
            {
                for (int j = 0; j < maxCol; j++)
                {
                    if(map[i, j] == map[player.MyRow, player.MyCol])
                    {
                        map[i, j].symbol = player.Symbol;
                    }
                    if (map[i, j] == map[monster.MyRow, monster.MyCol])
                    {
                        map[i, j].symbol = monster.Symbol;
                    }
                    Console.Write(map[i, j].symbol);
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
