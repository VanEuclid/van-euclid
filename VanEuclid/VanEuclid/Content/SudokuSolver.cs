using System;
using System.Collections.Generic;
using System.Linq;

namespace VanEuclid.Content
{
    public class SudokuSolver
    {
        //variables for SudokuSolver()
        public int back; //number of back tracks
        public int[,] b; //board
        private List<int> x; //for rapid row check
        private List<int> y; //for rapid col check

        public SudokuSolver(int[] inputBoard)
        {
            b = new int[9, 9];
            x = new List<int>();
            y = new List<int>();

            int index = 0;
            for (int i = 0; i < 9; i++) //orient board into a 2d array
                for (int j = 0; j < 9; j++)
                {
                    b[i, j] = inputBoard[index];
                    index++;
                }
        }

        /// <summary>
        /// Solves the board from row and column position
        /// </summary>
        /// <param name="row">Row to check from</param>
        /// <param name="col">Column to check from</param>
        /// <returns>1 for solved / 0 for backtrack</returns>
        public int Sudokusolve(int row, int col)
        {
            if (row == 8 && col == 9) //solution found therefore ending recursion / base case
                return 1;

            if (col == 9) //move to next row when reach end of columns
            {
                col = 0;
                row++;
            }

            int backHappened = 0; //means a backtrack is needed
            if (b[row, col] != 0) //skip numbers already found on board
                return Sudokusolve(row, col + 1);
            else
            {
                for (int num = 1; num < 10; num++) //check numbers 1 - 9 on the sudoku board
                {
                    if (!Squarecheck(num, row, col) && !Rowcheck(num, row) && !Colcheck(num, col)) //check sudoku board rules
                    {
                        b[row, col] = num; //inserting the number into the board because it is valid so far
                        backHappened = Sudokusolve(row, col + 1); //if it returns here with 0, that means a backtrack is needed. if it returns here with 1 then the board is solved

                        if (backHappened == 0) //means that recursion returned with a 0 and must erased
                        {
                            back++;
                            b[row, col] = 0;
                        }
                    }
                }
            }

            if (backHappened == 1) //solution found returning with answer
            {
                return 1;
            }
            else //means 1-9 was checked and must return with 0 to backtrack
            {
                return 0;
            }
        }

        /// <summary>
        /// Checks the 9x9 square for a duplicate
        /// </summary>
        /// <param name="num">Number to check for insert</param>
        /// <param name="row">Row to check on board</param>
        /// <param name="col">Column to check on board</param>
        /// <returns></returns>
        private Boolean Squarecheck(int num, int row, int col)
        {
            int startRow = row - row % 3; //orients square check
            int startCol = col - col % 3;

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (b[i + startRow, j + startCol] == num)
                        return true;

            return false;
        }

        /// <summary>
        /// Checks the row of the board
        /// </summary>
        /// <param name="num">Number to check for insert</param>
        /// <param name="row">Row to check on board</param>
        /// <returns>True if can insert / False if can't insert</returns>
        private Boolean Rowcheck(int num, int row)
        {
            x.Clear();
            x.Add(num);
            for (int i = 0; i < 9; i++)
                x.Add(b[row, i]);

            return Duplicates(x);
        }

        /// <summary>
        /// Checks the col of the board
        /// </summary>
        /// <param name="num">Number to check for insert</param>
        /// <param name="col">Column to check on board</param>
        /// <returns>True if can insert / False if can't insert</returns>
        private Boolean Colcheck(int num, int col)
        {
            y.Clear();
            y.Add(num);
            for (int i = 0; i < 9; i++)
                y.Add(b[i, col]);

            return Duplicates(y);
        }

        /// <summary>
        /// Checks for duplications iteratively
        /// </summary>
        /// <param name="d">List of numbers from num + board column or row</param>
        /// <returns>True if dup exist / False if dup doesn't exist</returns>
        private Boolean Duplicates(List<int> d)
        {
            d.Sort();
            for (int i = 0; i < d.Count - 1; i++)
            {
                if (d.ElementAt(i) == 0)
                    continue;
                else if (d.ElementAt(i) == d.ElementAt(i + 1)) //since the check list is sorted, we just need to check i and i + 1
                    return true;
            }
            return false;
        }
    }
}
