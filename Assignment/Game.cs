using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Assignment
{
    public enum Directions
    {
        Up,
        Down,
        Right,
        Left,
    }
    class Game
    {
        int currentFrame = 0;
        public bool gameActive = false;

        private Tile[,] tileMap;
        public static int maxRow;
        public static int maxCol;
       
        Player player;
        Monster monster;
        List<Tile> monsterPath = new List<Tile>();
        

        public void StartGame()
        {
            gameActive = true;
            BuildMap();
            player = new Player();
            monster = new Monster();
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
            int targetRow = monster.MyRow;
            int targetCol = monster.MyCol;

            if(targetRow > player.MyRow)
            {
                targetRow--;
            }
            else if (targetRow < player.MyRow)
            {
                targetRow++;
            }
            else if (targetCol > player.MyCol)
            {
                targetCol--;
            }
            else if (targetCol < player.MyCol)
            {
                targetCol++;
            }

            monster.MyRow = targetRow;
            monster.MyCol = targetCol;
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
            int row;
            int col;
            Directions playerDirection = Directions.Right;

            if (input.Key == ConsoleKey.W || input.Key == ConsoleKey.UpArrow)
            {
                playerDirection = Directions.Up;
            }
            else if (input.Key == ConsoleKey.S || input.Key == ConsoleKey.DownArrow)
            {
                playerDirection = Directions.Down;
            }
            else if (input.Key == ConsoleKey.A || input.Key == ConsoleKey.LeftArrow)
            {
                playerDirection = Directions.Left;
            }
            else if (input.Key == ConsoleKey.D || input.Key == ConsoleKey.RightArrow)
            {
                playerDirection = Directions.Right;
            }

            GetRowAndColForDirection(playerDirection, out row, out col);

            int targetRow = player.MyRow + row;
            int targetCol = player.MyCol + col;

            if (IsPositionEmptyAndValid(targetRow, targetCol, tileMap))
            {
                player.MyRow = targetRow;
                player.MyCol = targetCol;
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

            maxRow = map.Length;
            maxCol = map[0].Length;
            tileMap = new Tile[maxRow, maxCol];

            for (int i = 0; i < maxRow; i++)
            {
                for (int j = 0; j < maxCol; j++)
                {
                    char symbol = map[i][j];                    //get symbol from jagged array
                    
                    Tile thisTile = new Tile(symbol);           //create new tile and pass the symbol
                    thisTile.MyRow = i; 
                    thisTile.MyCol = j;
                    tileMap[i, j] = thisTile;                   //add this tile to the multidimensional array
                }
            }
        }

        public static void GetRowAndColForDirection(Directions dir, out int row, out int col)
        {
            switch (dir)
            {
                case Directions.Up:
                    row = -1;
                    col = 0;
                    break;
                case Directions.Right:
                    row = 0;
                    col = 1;
                    break;
                case Directions.Down:
                    row = 1;
                    col = 0;
                    break;
                case Directions.Left:
                    row = 0;
                    col = -1;
                    break;

                default:
                    row = col = 0;
                    break;
            }

        }

        public static bool IsPositionEmptyAndValid(int row, int col, Tile[,] tileMap)
        {
            bool validRow = (row >= 0) && (row < maxRow);
            bool validCol = (col >= 0) && (col < maxCol);

            bool isEmpty = (validRow && validCol) && tileMap[row, col].IsEmpty();

            return validRow && validCol && isEmpty;
        }

        public void RenderMap()
        {
            Console.Clear();

            currentFrame++;
            Console.WriteLine("---FRAME " + currentFrame + "---");

            Tile monsterTile = tileMap[monster.MyRow, monster.MyCol];
            Tile playerTile = tileMap[player.MyRow, player.MyCol];

            List<Tile> path = Pathfinder.GetPath(monsterTile, playerTile, tileMap);


            for (int i = 0; i < maxRow; i++)
            {
                for (int j = 0; j < maxCol; j++)
                {
                    char symbolToPrint;
                    if (i == player.MyRow && j == player.MyCol)
                    {
                        symbolToPrint = player.Symbol;
                    }
                    else if (i == monster.MyRow && j == monster.MyCol)
                    {
                        symbolToPrint = monster.Symbol;
                    }
                    else
                    {
                        Tile currentTile = tileMap[i, j];
                        if(path.Contains(currentTile))
                        {
                            symbolToPrint = 'x';
                        }
                        else
                        {
                            symbolToPrint = currentTile.Symbol;
                        }
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
