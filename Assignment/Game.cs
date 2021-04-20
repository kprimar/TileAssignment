using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Assignment
{
    class Game
    {
        int currentFrame = 0;
        public bool gameActive = false;
        public const int maxRow = 4;
        public const int maxCol = 4;
        char[,] map = new char[4, 4];
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
                    map[i, j] = '*';
                }
            }
            map[player.MyRow, player.MyCol] = player.Symbol;
            map[monster.MyRow, monster.MyCol] = monster.Symbol;
            if (map[player.MyRow, player.MyCol] == map[monster.MyRow, monster.MyCol])
            {
                map[player.MyRow, player.MyCol] = player.Symbol;
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
                    Console.Write(map[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
