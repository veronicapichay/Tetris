using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    public class SquareChar: Block
    {
        private readonly BlockMove[][] tiles = new BlockMove[][]
        {
            new BlockMove[] { new (0,0), new (0,1), new (1,0), new (1,1) }
        };

        public override int Id => 4;
        protected override BlockMove StartOffset => new BlockMove (0, 4);
        protected override BlockMove [][] Tiles => tiles;
    }
}