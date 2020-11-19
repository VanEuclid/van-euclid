using System;
using System.Drawing;

namespace VanEuclid.Content
{
    public class SeamCarver
    {
        private int[,] cost;
        private int[,] totalcost;
        private int col;
        private int row;

        private Bitmap im;
        public Bitmap fil;

        public SeamCarver(Bitmap inputImage, bool seamVisible, int numberOfSeams)
        {
            im = inputImage;

            for(int times = 0; times < numberOfSeams; times ++)
            {
                ImageCost();
                ImageTotalCost();

                //HERE SEAM CARVING BEGINS

                int rows = FindMinTotalCost();
                int[] targetSeam = HorizontalSeam(rows);

                fil = new Bitmap(im.Width, im.Height - 1);

                if(seamVisible)
                {
                    CreateSeamCarvedImageWithTears(targetSeam);
                }
                else
                {
                    CreateSeamCarvedImage(targetSeam);
                }

                im = fil; //set im to the new fil image for compounding seam carve effect
            }
        }

        private int Grad(Color a, Color b)
        {
            return Math.Abs(a.R - b.R) + Math.Abs(a.G - b.G) + Math.Abs(a.B - b.B);
        }

        private void ImageCost()
        {
            col = im.Width;
            row = im.Height;

            cost = new int[col, row];

            for (int x = 0; x < col; x++)
            {
                cost[x, 0] = Grad(im.GetPixel(x, 0), im.GetPixel(x, 1));
                cost[x, row - 1] = Grad(im.GetPixel(x, row - 1), im.GetPixel(x, row - 2));
            }

            for (int y = 0; y < row; y++)
            {
                cost[0, y] = Grad(im.GetPixel(0, y), im.GetPixel(1, y));
                cost[col - 1, y] = Grad(im.GetPixel(col - 2, y), im.GetPixel(col - 1, y));
            }

            for (int y = 1; y < row - 1; y++)
            {
                for (int x = 1; x < col - 1; x++)
                {
                    cost[x, y] = Grad(im.GetPixel(x, y - 1), im.GetPixel(x, y + 1)) + Grad(im.GetPixel(x - 1, y), im.GetPixel(x + 1, y));  //calculate ENERGY OF EACH PIXEL IN IMAGE
                }
            }
        }

        private void ImageTotalCost()
        {
            totalcost = new int[col, row];

            //first row to start
            for (int y = 0; y < row; y++)
            {
                totalcost[0, y] = cost[0, y];
            }

            //rest of cost
            for (int x = 1; x < col; x++)
            {
                totalcost[x, 0] = cost[x, 0] + Math.Min(totalcost[x - 1, 0], totalcost[x - 1, 1]);  //special case for first row that looks at two elements

                totalcost[x, row - 1] = cost[x, row - 1] + Math.Min(totalcost[x - 1, row - 1], totalcost[x - 1, row - 2]); //special case for last row that looks at two elements

                for (int y = 1; y < row - 1; y++)
                {
                    totalcost[x, y] = cost[x, y] + Math.Min(totalcost[x - 1, y], Math.Min(totalcost[x - 1, y - 1], totalcost[x - 1, y + 1])); //middle pixels look at three elements
                }
            }
        }

        public int FindMinTotalCost()
        {
            int min = totalcost[col - 1, 0];
            int minI = 0;
            for (int y = 1; y < row; y++)
            {
                if (min > totalcost[col - 1, y])
                {
                    min = totalcost[col - 1, y];
                    minI = y;
                }
            }

            return minI;
        }

        private int[] HorizontalSeam(int rows)
        {
            int[] seam = new int[col]; //Cceate seam of size column

            for (int x = col - 1; x >= 0; x--)
            { //start from last column all the way to the left
                int temp = rows;
                if (rows < row - 1 && totalcost[x, rows + 1] < totalcost[x, rows])
                {
                    temp = rows + 1;
                }
                if (rows > 0 && totalcost[x, rows - 1] < totalcost[x, temp])
                {
                    temp = rows - 1;
                }
                rows = temp; //sets rows to temp to use minimum adjacent rows in the next iteration
                seam[x] = rows;
            }

            return seam;
        }

        public void CreateSeamCarvedImageWithTears(int[] targetSeam)
        {
            for (int x = 0; x < col; x++)
            {
                for (int y = 0; y < targetSeam[x]; y++)
                {
                    fil.SetPixel(x, y, im.GetPixel(x, y));
                }
                for (int k = targetSeam[x] + 1; k < row - 1; k++) // + 1 leaves a gap
                {
                    fil.SetPixel(x, k, im.GetPixel(x, k + 1)); // + 1 here gets the pixel above k
                }
            }
        }

        public void CreateSeamCarvedImage(int[] targetSeam)
        {
            for (int x = 0; x < col; x++)
            {
                for (int y = 0; y < targetSeam[x]; y++)
                {
                    fil.SetPixel(x, y, im.GetPixel(x, y));
                }
                for (int k = targetSeam[x]; k < row - 1; k++) 
                {
                    fil.SetPixel(x, k, im.GetPixel(x, k + 1)); // + 1 here gets the pixel above k
                }
            }
        }
    }
}