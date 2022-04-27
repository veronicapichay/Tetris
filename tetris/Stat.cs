using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    public class Stat
    {
        private Block currentBlock;

        public Block CurrentBlock
        {
            get => currentBlock;

            private set
            {
                currentBlock = value;
                currentBlock.Reset();
                //spawning of char
                for (int i = 0; i < 2; i++)
                {
                    currentBlock.Move(1, 0);

                    if (!BlockFits())
                    {
                        currentBlock.Move(-1, 0);
                    }
                }
            }
        }
        //properties 
        public Grid GameGrid { get; }
        public BlockQueue BlockQueue { get; }
        public bool GameOver { get; private set; }
        public int Score { get; private set; }
        public Block HeldBlock { get; private set; }
        public bool CanHold { get; private set; }

        //constructor
        public Stat()
        {
            GameGrid = new Grid(22, 10);
            BlockQueue = new BlockQueue();
            CurrentBlock = BlockQueue.GetAndUpdate();
            CanHold = true;
        }
        //checks if char is inside the grid 
        private bool BlockFits()
        {
            foreach (BlockMove p in CurrentBlock.TileBlockMoves())
            {
                if (!GameGrid.IsEmpty(p.Row, p.Column))
                {
                    return false;
                }
            }

            return true;
        }
        public void HoldBlock()
        {
            if (!CanHold)
            {
                return;
            }

            if (HeldBlock == null)
            {
                HeldBlock = CurrentBlock;
                CurrentBlock = BlockQueue.GetAndUpdate();
            }
            else
            {
                Block tmp = CurrentBlock;
                CurrentBlock = HeldBlock;
                HeldBlock = tmp;
            }

            CanHold = false;
        }
        //if char is not inside the grid then we rotate
        public void RotateBlockCW()
        {
            CurrentBlock.RotateClock();

            if (!BlockFits())
            {
                CurrentBlock.RotateCounter();
            }
        }

        public void RotateBlockCCW()
        {
            CurrentBlock.RotateCounter();

            if (!BlockFits())
            {
                CurrentBlock.RotateClock();
            }
        }

        public void MoveBlockLeft()
        {
            CurrentBlock.Move(0, -1);

            if (!BlockFits())
            {
                CurrentBlock.Move(0, 1);
            }
        }

        public void MoveBlockRight()
        {
            CurrentBlock.Move(0, 1);

            if (!BlockFits())
            {
                CurrentBlock.Move(0, -1);
            }
        }

        private bool IsGameOver()
        {
            return !(GameGrid.IsrEmpty(0) && GameGrid.IsrEmpty(1));
        }

        private void PlaceBlock()
        {
            foreach (BlockMove p in CurrentBlock.TileBlockMoves())
            {
                GameGrid[p.Row, p.Column] = CurrentBlock.Id;
            }

            Score += GameGrid.ClearfRows(); //score is incremented by # of cleared rows 

            if (IsGameOver())
            {
                GameOver = true;
            }
            else
            {
                CurrentBlock = BlockQueue.GetAndUpdate();
                CanHold = true;
            }
        }

        public void MoveBlockDown()
        {
            CurrentBlock.Move(1, 0);

            if (!BlockFits())
            {
                CurrentBlock.Move(-1, 0);
                PlaceBlock();
            }
        }

        private int TileDropDistance(BlockMove p)
        {
            int drop = 0;

            while (GameGrid.IsEmpty(p.Row + drop + 1, p.Column))
            {
                drop++;
            }

            return drop;
        } 
        //fast drop ca'c
        public int BlockDropDistance()
        {
            int drop = GameGrid.Rows;

            foreach (BlockMove p in CurrentBlock.TileBlockMoves())
            {
                drop = System.Math.Min(drop, TileDropDistance(p));
            }

            return drop;
        }

        public void DropBlock()
        {
            CurrentBlock.Move(BlockDropDistance(), 0);
            PlaceBlock();
        }
    }
}


