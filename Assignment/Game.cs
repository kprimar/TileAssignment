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
        Tile[,] tileMap = new Tile[maxRow, maxCol];
        List<Tile> monsterPath = new List<Tile>();
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
            int destinationRow = player.MyRow;
            int destinationCol = player.MyCol;
            int nextRow = monster.MyRow;
            int nextCol = monster.MyCol;

            if(destinationRow < monster.MyRow)
            {
                while (monster.MyRow != destinationRow)
                {
                    nextRow--;
                    Console.WriteLine(destinationRow);
                    Console.WriteLine(nextRow);
                    monsterPath.Add(tileMap[nextRow, monster.MyCol]);
                }
            }
            else if (destinationRow > monster.MyRow)
            {
                while (monster.MyRow != destinationRow)
                {
                    nextRow++;
                    monsterPath.Add(tileMap[nextRow, monster.MyCol]);
                }
            }

            if (destinationCol < monster.MyCol)
            {
                while (monster.MyCol != destinationCol)
                {
                    nextCol--;
                    monsterPath.Add(tileMap[nextRow, nextCol]);
                }
            }
            else if (destinationCol > monster.MyCol)
            {
                while (monster.MyCol != destinationCol)
                {
                    nextCol++;
                    monsterPath.Add(tileMap[nextRow, nextCol]);
                }
            }



            Console.WriteLine(monsterPath.Count);

            //if (monster.MyRow < player.MyRow)
            //{
            //    monster.MyRow++;
            //    if(tileMap[monster.MyRow,monster.MyCol].isWall == true)
            //    {
            //        monster.MyRow--;
            //    }
            //}
            //else if (monster.MyRow > player.MyRow)
            //{
            //    monster.MyRow--;
            //    if (tileMap[monster.MyRow, monster.MyCol].isWall == true)
            //    {
            //        monster.MyRow++;
            //    }
            //}
            //else if (monster.MyCol < player.MyCol)
            //{
            //    monster.MyCol++;
            //    if (tileMap[monster.MyRow, monster.MyCol].isWall == true)
            //    {
            //        monster.MyCol--;
            //    }
            //}
            //else if (monster.MyCol > player.MyCol)
            //{
            //    monster.MyCol--;
            //    if (tileMap[monster.MyRow, monster.MyCol].isWall == true)
            //    {
            //        monster.MyCol++;
            //    }
            //}
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
                    if(tileMap[player.MyRow - 1, player.MyCol].isWall == false)
                    {
                        player.MyRow--;
                    }
                }
            }
            else if (input.Key == ConsoleKey.S || input.Key == ConsoleKey.DownArrow)
            {
                if(player.MyRow < maxRow - 1)
                {
                    if(tileMap[player.MyRow + 1, player.MyCol].isWall == false)
                    {
                        player.MyRow++;
                    }                    
                }
            }
            else if (input.Key == ConsoleKey.A || input.Key == ConsoleKey.LeftArrow)
            {
                if (player.MyCol > 0)
                {
                    if (tileMap[player.MyRow, player.MyCol - 1].isWall == false)
                    {
                        player.MyCol--;
                    }
                }
            }
            else if (input.Key == ConsoleKey.D || input.Key == ConsoleKey.RightArrow)
            {
                if (player.MyCol < maxCol + 1)
                {
                    if (tileMap[player.MyRow, player.MyCol + 1].isWall == false)
                    {
                        player.MyCol++;
                    }
                }
            }
        }

        private void BuildMap()
        {

            char[][] map = new char[][]
            {
                new char[] {'*' ,'*' ,'*' ,'*' ,'*' ,'*' ,'*' ,'*' ,'*' ,'*' },
                new char[] {'*' ,'-' ,'-' ,'-' ,'-' ,'-' ,'-' ,'-' ,'-' ,'*' },
                new char[] {'*' ,'-' ,'*' ,'*' ,'*' ,'-' ,'-' ,'-' ,'-' ,'*' },
                new char[] {'*' ,'-' ,'-' ,'*' ,'-' ,'-' ,'-' ,'-' ,'-' ,'*' },
                new char[] {'*' ,'-' ,'-' ,'*' ,'-' ,'-' ,'-' ,'-' ,'-' ,'*' },
                new char[] {'*' ,'-' ,'-' ,'-' ,'-' ,'-' ,'-' ,'-' ,'-' ,'*' },
                new char[] {'*' ,'-' ,'-' ,'-' ,'-' ,'-' ,'*' ,'-' ,'-' ,'*' },
                new char[] {'*' ,'-' ,'-' ,'-' ,'-' ,'-' ,'*' ,'-' ,'-' ,'*' },
                new char[] {'*' ,'-' ,'-' ,'-' ,'-' ,'-' ,'*' ,'-' ,'-' ,'*' },
                new char[] {'*' ,'*' ,'*' ,'*' ,'*' ,'*' ,'*' ,'*' ,'*' ,'*' },
            };

            for (int i = 0; i < maxRow; i++)
            {
                for (int j = 0; j < maxCol; j++)
                {
                    Tile thisTile = new Tile();
                    thisTile.symbol = map[i][j];
                    if (thisTile.symbol == '*')
                    {
                        thisTile.isWall = true;
                    }
                    else
                    {
                        thisTile.isWall = false;
                    }
                    tileMap[i, j] = thisTile;
                }
            }
        }

        public void RenderMap()
        {
            //Console.Clear();
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
                        symbolToPrint = tileMap[i, j].symbol;
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
