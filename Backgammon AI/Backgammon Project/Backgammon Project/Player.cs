using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backgammon_Project
{
    class Player
    {
        private int[] diceRolls = new int[3];

        private void DiceRolled(Dice dice, Board board)
        {
            //Waits for player to throw the dice
            Console.WriteLine("Press any key to throw dice");
            Console.ReadKey();

            //Calls for dice and prints out which dice are thrown
            diceRolls = dice.diceThrow();
            Console.WriteLine();
            Console.WriteLine("Player throws a {0},{1}", diceRolls[0], diceRolls[1]);

            //temp posis incase a double comes up
            if (diceRolls[2] == 1)
                Console.WriteLine("Oh you got a double");
        }

        private void MovePiece(Dice dice, Board board, int playerColour, int opp)
        {
            int piece=27;
            Console.WriteLine("Enter the piece placement you want to move then press enter");
            piece =int.Parse(Console.ReadLine()); //needs error detection

            //This will check if the player selected a number on the board and has a piece there
            if (piece >= 1 && piece <= 24)
            {
                int[,] PlayerLayout = new int[27,2];
                PlayerLayout = board.getLayout();

                if (PlayerLayout[piece, playerColour] > 0)
                {
                    //goes to select new place TEMP TESTER ONE needs checking areas
                    int newPlace = 0;
                    Console.WriteLine("Enter where you want the piece to move to then press enter");
                    newPlace = int.Parse(Console.ReadLine()); //needs error detection

                    if (PlayerLayout[newPlace, opp] < 2)
                    {
                        //if there 1 opponent piece in space remove piece and place it on bar)
                        if (PlayerLayout[newPlace, opp] == 1)
                        {
                            PlayerLayout[newPlace, opp]--;
                            PlayerLayout[26, opp]++;
                        }
                        PlayerLayout[piece, playerColour]--;
                        PlayerLayout[newPlace, playerColour]++;

                        board.SendLayout(PlayerLayout);
                        board.RefreshBoard();
                    }
                    else
                    {
                        Console.WriteLine("Place blocked by opponent. Please try again.");
                        MovePiece(dice, board, playerColour, opp);
                    }
                }
                else
                {
                    Console.WriteLine("You dont have a piece there. Please try again.");
                    MovePiece(dice, board, playerColour, opp);
                }
            }
            else
            {
                Console.WriteLine("Invalid Selection. Please Try again");
                MovePiece(dice, board, playerColour, opp);
            }
        }

//**********************************************************************************************

        //Players Main method
        public void PlayerGo(Dice dice, Board board,int Colour, int opp)
        {
            Console.WriteLine("Players Turn");
            DiceRolled(dice, board);
            MovePiece(dice, board, Colour, opp);
        }
    }
}
