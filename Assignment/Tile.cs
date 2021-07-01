using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Assignment
{
    class Tile
    {
        public char Symbol;

        public int MyRow = 0;
        public int MyCol = 0;

        public Tile(char symbol)
        {
            Symbol = symbol;
        }

        public bool IsEmpty()
        {
            return (Symbol == '-');
        }

    }
}
