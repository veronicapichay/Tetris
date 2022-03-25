using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    public abstract class Block
    {
        protected abstract BlockMove[][] Tiles { get; } //tile position on 4 states
        protected abstract BlockMove StartOffset { get; } //bounding grid location inside the grid
        public abstract int Id { get; }

        private int rotationState;
        private BlockMove offset;

        public Block()
        {
            offset = new BlockMove(StartOffset.Row, StartOffset.Column);

        }

        public IEnumerable<BlockMove> TileBlockMoves()
        {
            foreach (BlockMove p in Tiles [rotationState])
            {
                yield return new BlockMove(p.Row + offset.Row, p.Column + offset.Column); //adds row and column offset 
            }
        }

        //rotates character state 1 (90 deg) clockwise
        public void RotateClock() { rotationState = (rotationState + 1) % Tiles.Length; }

        //rotates counter clockwise
        public void RotateCounter()
        {

            if (rotationState == 0) { rotationState = Tiles.Length - 1; }
            else { rotationState--; }
        }

        //moves the character grid by given r and c
        public void Move(int rows, int columns)
        {
            offset.Row += rows;
            offset.Column += columns;
        }

        //resets positon 
        public void Reset()
        {
            rotationState = 0;
            offset.Row = StartOffset.Row;
            offset.Column = StartOffset.Column; 
        }
    }
}