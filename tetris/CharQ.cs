using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    //responsible for picking the char that is being recycled 
 
    public class BlockQueue
    {
        private readonly Block[] blocks = new Block[]
        {
            new IChar(),
            new JChar(),
            new LChar(),
            new SquareChar(),
            new SChar(),
            new TChar(),
            new ZChar()
        };

        private readonly Random random = new Random(); //ran char on queue

        public Block NextBlock { get; private set; }  //to preview next 
        public BlockQueue() //initializing ran char to be next 
        {
            NextBlock = RandomBlock();
        }

        private Block RandomBlock()
        {
            return blocks[random.Next(blocks.Length)];
        }

        public Block GetAndUpdate() //returns next ran char
        {
            Block block = NextBlock;

            do
            {
                NextBlock = RandomBlock();
            }
            while (block.Id == NextBlock.Id);

            return block;
        }
    }
}