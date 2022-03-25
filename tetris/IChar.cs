using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    public class IChar : Block
    {
        private readonly BlockMove[][] tiles = new BlockMove[][]
        {
            new BlockMove[] { new(1,0), new(1,1), new(1,2), new(1,3) }, //0 deg
            new BlockMove[] { new(0,2), new(1,2), new(2,2), new(3,2) }, //90 deg
            new BlockMove[] { new(2,0), new(2,1), new(2,2), new(2,3) }, //180 deg
            new BlockMove[] { new(0,1), new(1,1), new(2,1), new(3,1) }  //270 deg
        };

        public override int Id => 1;
        protected override BlockMove StartOffset => new BlockMove(-1, 3); //middle of top row pos
        protected override BlockMove[][] Tiles => tiles; //return tiles arr on top 
    }
}