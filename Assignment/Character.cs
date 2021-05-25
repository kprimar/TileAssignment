using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Assignment
{
    public class Character
    {
        public char Symbol;
        public int MyRow;
        public int MyCol;
    }

    public class Player : Character
    {
        public Player()
        {
            MyRow = 1;
            MyCol = 1;
            Symbol = 'P';
        }
    }

    public class Monster : Character
    {
        public Monster()
        {
            MyRow = Game.maxRow - 2;
            MyCol = Game.maxCol - 2;
            Symbol = 'M';
        }
    }
}
