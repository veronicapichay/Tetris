using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    public class BlockMove
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public BlockMove(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}