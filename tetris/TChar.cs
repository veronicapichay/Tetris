using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    public class TChar : Block
    {
        public override int Id => 6;

        protected override BlockMove StartOffset => new(0, 3);

        protected override BlockMove[][] Tiles => new BlockMove[][] {
            new BlockMove[] {new(0,1), new(1,0), new(1,1), new(1,2)},
            new BlockMove[] {new(0,1), new(1,1), new(1,2), new(2,1)},
            new BlockMove[] {new(1,0), new(1,1), new(1,2), new(2,1)},
            new BlockMove[] {new(0,1), new(1,0), new(1,1), new(2,1)}
        };
    }
}