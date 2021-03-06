﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VanEuclid.Content;
using System.Drawing;
using System.Web.Helpers;

namespace VanEuclid.Controllers
{
    public class PortfolioController : Controller
    {
        public ActionResult Index()
        {
            return View ();
        }


        public ActionResult SeamCarve()
        {
            return View();
        }

        public ActionResult SeamCarver()
        {
            byte[] byteImage = WebImage.GetImageFromRequest("postedFile").GetBytes();

            Bitmap originalImage; //taking the byte[] and turn into a Image
            using (var ms = new MemoryStream(byteImage))
            {
                originalImage = new Bitmap(ms);
            }

            int numberOfSeams = int.Parse(Request["numberOfSeams"]);

            bool seamVisible = true;
            if (Request["seamVisible"] == null)
            {
                seamVisible = false;
            }

            SeamCarver seamCarve = new SeamCarver(originalImage, seamVisible, numberOfSeams);
            Image filteredImage = seamCarve.fil;

            byte[] imageInBytes; //taking the Image and turn into byte[]
            using (var stream = new MemoryStream())
            {
                filteredImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                imageInBytes = stream.ToArray();
            }

            return View(imageInBytes);
        }

        public ActionResult Sudoku()
        {
            return View();
        }

        public ActionResult SudokuSolver()
        {
            int[] inputBoard = new int[81];
            String cell = "cell";
            for (int i = 0; i < 81; i++)
            {
                cell += i; //string to index cell names
                if (Request[cell] == "") //gather input cells from view
                {
                    inputBoard[i] = 0;
                }
                else
                {
                    inputBoard[i] = int.Parse(Request[cell]); //uses cell to call specific cell from view
                }
                cell = "cell"; //reset string indexer
            }

            SudokuSolver sudoku = new SudokuSolver(inputBoard);
            int success = sudoku.Sudokusolve(0, 0); //solve sudoku board from start

            if (success == 1) //solution to sudoku board found
            {
                //Console.WriteLine("Sudoku Solved");
                //Console.WriteLine("There was " + back + " backtracks done.");

                int index = 0;
                for (int x = 0; x < 9; x++) //puts 2d array back into 1d array
                    for (int y = 0; y < 9; y++)
                    {
                        inputBoard[index] = sudoku.b[x, y];
                        index++;
                    }

                ViewData["success"] = success;
                ViewData["solution"] = inputBoard;
                ViewData["backs"] = sudoku.back;
            }
            else //board not solved
            {
                //Console.WriteLine("Sudoku not solved");

                ViewData["success"] = success;
            }

            return View();
        }
    }
}
