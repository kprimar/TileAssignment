using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Assignment
{
    class Tile
    {
        public char symbol;
        public bool isWall = false;

        public Tile()
        {
            symbol = '*';
        }

        public char GetSymbol()
        {
            if (isWall)
            {
                return symbol = '*';
            }
            else
            {
                return symbol = '-';
            }

        }

    }
}
