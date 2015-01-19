using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backgammon_Project
{
    class Board
    {
        /*Board layout in array. 24 places, 2 end areas and the bar where taken units are kepted. A point for the score and stored in each place is 2 numbers. 0 for the no of white units num 1 for black units
         * 0 & 25 are end pieces
         * 26 is bar
         * 1-24 are board places
         * 27 are the board scores
         * 0-1 are the colours
        */

        private int[,] BoardLayout;
        private int WhitePip = 0, BlackPip = 0;

        private void init()
        {
            //clears the board with a new array
            BoardLayout = new int[28, 2];

            //Starting layout of the board [posis,white/black]
            BoardLayout[1, 0] = 2;
            BoardLayout[6, 1] = 5;
            BoardLayout[8, 1] = 3;
            BoardLayout[12, 0] = 5;
            BoardLayout[13, 1] = 5;
            BoardLayout[17, 0] = 3;
            BoardLayout[19, 0] = 5;
            BoardLayout[24, 1] = 2;

            //adds board score
            pipCount();

            //sets console details
            Console.Title = "Backgammon";
            Console.SetWindowSize(120, 50);
            Console.BufferHeight = 50;
            Console.BufferWidth = 120;
        }

        private void unitPlaces()
        {
            //Draws in the unit locations on the board
            for (int i = 13; i <= 24; i++)
            {
                Console.Write("|  {0}|{1}   |", BoardLayout[i, 0], BoardLayout[i, 1]);
            }

            Console.WriteLine("");

            for (int i = 12; i >= 1; i--)
            {
                Console.Write("  W|v|B   ");
            }

            Console.WriteLine("");
            Console.WriteLine("");

            for (int i = 12; i >= 1; i--)
            {
                Console.Write("  W|^|B   ");
            }

            Console.WriteLine("");

            for (int i = 12; i >= 1; i--)
            {
                Console.Write("|  {0}|{1}   |", BoardLayout[i, 0], BoardLayout[i, 1]);
            }
        }

        private void printBoard()
        {
            //Clears Console to draw new board
            Console.Clear();

            //sets the board sizes so they are equal length
            for (int i = 13; i <= 24; i++)
            {
                Console.Write("|   {0}   |", i);
            }
            Console.WriteLine("");


            Console.WriteLine("");

            //adds in the unit values
            unitPlaces();

            Console.WriteLine("");

            //finishes off the board size
            for (int i = 12; i >= 1; i--)
            {
                if (i < 10)
                    Console.Write("|    {0}   |", i); //adds an extra space in when number under 10
                else
                    Console.Write("|   {0}   |", i);
            }

            //Adds the bar in
            Console.WriteLine("Pieces on Bar White = {0}, Black = {1}", BoardLayout[26, 0], BoardLayout[26, 1]);

            //Adds the pot in
            Console.WriteLine("Pieces in Pot White = {0}, Black = {1}", BoardLayout[25, 0], BoardLayout[0, 1]);

            //adds board score
            pipCount();

            //Adds in the pip score
            Console.WriteLine("Pip Score White = {0}, Black = {1}", WhitePip, BlackPip);

        }

        //sends Board Layout to other class files
        public int[,] getLayout()
        {
            return BoardLayout;
        }

        //Receive Board Layout from another file
        public void SendLayout(int[,] TempLayout)
        {
            BoardLayout = TempLayout;
        }

        //**********************************************************************************************

        //This will work out the total pip score for both players on the temp score
        private void pipCount()
        {

            int w = 0, b = 0;
            for (int i = 0; i <= 25; i++)
            {
                if (BoardLayout[i, 0] > 0)
                {
                    w = w + (i * BoardLayout[i, 0]);
                }
                if (BoardLayout[i, 1] > 0)
                {
                    b = b + (i * BoardLayout[i, 1]);
                }
            }
            if (BoardLayout[26, 0] > 0 && BoardLayout[26, 1] > 0)
            {
                w = w + (26 * BoardLayout[26, 0]);
                b = b + (26 * BoardLayout[26, 1]);
            }
            WhitePip = w;
            BlackPip = b;
            BoardLayout[27, 0] = WhitePip;
            BoardLayout[27, 1] = BlackPip;
        }

        //This will send the pip score outside
        public int getPip(int colour)
        {
            pipCount();
            if (colour==1)
                return BlackPip;
            else
                return WhitePip;
        }
        //**********************************************************************************************

        //Creates a New Board
        public void NewBoard()
        {
            //initialises the board and prints it 
            init();
            printBoard();

        }

        //Refreshes Board with new layout
        public void RefreshBoard()
        {
            printBoard();
        }
    }
}
