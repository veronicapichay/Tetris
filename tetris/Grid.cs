using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    public class Grid
    {

        private readonly int[,] grid; //2D array row and column

        //properties 
        public int Rows { get; }
        public int Columns { get; }

        public int this[int r, int c] //index ref for the array
        {
            get => grid[r, c];
            set => grid[r, c] = value;

        }
        public Grid(int rows, int columns)  //constructor accepts # r and c as parameters
        {
            Rows = rows;
            Columns = columns;
            grid = new int[rows, columns];  //initializing an array 

        }

        public bool IsInside(int r, int c) //checks if r or c is inside the grid
        {
            return r >= 0 && r < Rows && c >= 0 && c < Columns;
        }
        public bool IsEmpty(int r, int c) //checks if cell is empty
        {
            return IsInside(r, c) && grid[r, c] == 0;

        }
        public bool IsrFull(int r) //checks if r is full 
        {
            for (int c = 0; c < Columns; c++)
            {
                if (grid[r, c] == 0) { return false; }
            }
            return true;

        }
        public bool IsrEmpty(int r) //checks if r is empty
        {
            for (int c = 0; c < Columns; c++)
            {
                if (grid[r, c] != 0) { return false; }
            }
            return true;

        }

        private void ClearR(int r) //clears row if full 
        {
            for (int c = 0; c < Columns; c++) { grid[r, c] = 0; }

        }

        private void MoverDown(int r, int numRows) //moves row by # of cleared rows
        {
            for (int c = 0; c < Columns; c++)
            {
                grid[r + numRows, c] = grid[r, c];
                grid[r, c] = 0;

            }
        }
        public int ClearfRows() //removes buttom to top, increments counter if its clear 
        {
            int clear = 0;

            for (int r = Rows - 1; r >= 0; r--)
            {
                if (IsrFull(r))
                {
                    ClearR(r);
                    clear++;

                }
                else if (clear > 0) { MoverDown(r, clear); } //moves down by the number of cleared row              
            }

            return clear;
        }

    }
}










