using System;

namespace RPG_Assignment
{
    class Program
    {

        static void Main(string[] args)
        {
            Game game = new Game();
            game.StartGame();
            while (game.gameActive)
            {
                game.Update();
            }
        }
    }
}
