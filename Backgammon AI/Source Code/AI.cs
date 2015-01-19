using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backgammon_Project
{
    class AI
    {
        private int[] diceRolls = new int[3];
        private int AIColour = 0;

        private void DiceRolled(Dice dice, Board board)
        {
            //Calls for dice and prints out which dice are thrown
            diceRolls = dice.diceThrow();
            //Console.WriteLine();
            //Console.WriteLine("Computer throws a {0},{1}", diceRolls[0], diceRolls[1]);
        }
//**********************************************************************************************   
        private bool BoardSizeCheck(int i)
        {
            if (i < 25 && i > 0)
            {
                return true;
            }
            else
                return false;
        }

        //This will check if the game is in it last stages
        private bool FinalMovesCheck(int[,] Tempboard)
        {
            //if black do this
            if (AIColour == 1)
            {
                for (int i = 24; i > 6; i--)
                {
                    if (Tempboard[i, AIColour] >= 1)
                    {
                        return false; //returns false if finds number
                    }
                }
                return true;//returns true if cant find number
            }
            else //white
            {
                for (int i = 1; i < 19; i++)
                {
                    if (Tempboard[i, AIColour] >= 1)
                    {
                        return false; //returns false if finds number
                    }
                }
                return true;//returns true if cant find number
            }
        }

        //scores pip count
        private void TempPipCount(int[,] TempBoard)
        {

            int w = 0, b = 0;
            for (int i = 0; i <= 25; i++)
            {
                if (TempBoard[i, 0] > 0)
                {
                    w = w + (i * TempBoard[i, 0]);
                }
                if (TempBoard[i, 1] > 0)
                {
                    b = b + (i * TempBoard[i, 1]);
                }
            }
            if (TempBoard[26, 0] > 0 || TempBoard[26, 1] > 0)
            {
                w = w + (26 * TempBoard[26, 0]);
                b = b + (26 * TempBoard[26, 1]);
            }
            TempBoard[27, 0] = w;
            TempBoard[27, 1] = b;
        }

 //**********************************************************************************************  


        //This does parts 2, 3 and 4 on the move list to cut down on repeating code
        private void TempBoards2(int[] TempDiceRolls, List<int[,]> TempBoardList, int[,] TempBoard1, int opp, int i, int dice, int dice2)
        {
            int[,] TempBoard2;
            int t = 0;

            //if player black do this
            if (AIColour == 1)
            {
                if (FinalMovesCheck(TempBoard1) == true)
                {
                    TempBoard2 = new int[28, 2];
                    Array.Copy(TempBoard1, TempBoard2, TempBoard1.Length);

                    FinalMove2(TempBoard2, TempBoardList, dice, dice2, 0, opp);
                }
                else
                {
                    for (int j = i; j >= 1; j--)
                    {
                        TempBoard2 = new int[28, 2];
                        Array.Copy(TempBoard1, TempBoard2, TempBoard1.Length);

                        t = j + TempDiceRolls[dice];

                        //will do code if there less than 2 opponents colours in the space

                        if (BoardSizeCheck(t) == true && TempBoard2[j, AIColour] >= 1 && TempBoard2[t, opp] < 2)
                        {
                            //if there 1 opponent piece in space remove piece and place it on bar)
                            if (TempBoard2[t, opp] == 1)
                            {
                                TempBoard2[t, opp]--;
                                TempBoard2[26, opp]++;
                            }
                            TempBoard2[j, AIColour]--;
                            TempBoard2[t, AIColour]++;


                            //If dice are a double move 2 more times
                            if (TempDiceRolls[2] == 1)
                            {
                                TempBoards3(TempDiceRolls, TempBoardList, TempBoard2, opp, j, dice);
                            }
                            else
                            {
                                TempPipCount(TempBoard2);
                                TempBoardList.Add(TempBoard2);
                            }
                        }
                    }
                }
            }
            else //white
            {
                if (FinalMovesCheck(TempBoard1) == true)
                {
                    TempBoard2 = new int[28, 2];
                    Array.Copy(TempBoard1, TempBoard2, TempBoard1.Length);

                    FinalMove2(TempBoard2, TempBoardList, dice, dice2, 25, opp);
                }
                else
                {
                    for (int j = i; j <= 24; j++)
                    {
                        TempBoard2 = new int[28, 2];
                        Array.Copy(TempBoard1, TempBoard2, TempBoard1.Length);

                        t = j + TempDiceRolls[dice];

                        //will do code if there less than 2 opponents colours in the space

                        if (BoardSizeCheck(t) == true && TempBoard2[j, AIColour] >= 1 && TempBoard2[t, opp] < 2)
                        {
                            //if there 1 opponent piece in space remove piece and place it on bar)
                            if (TempBoard2[t, opp] == 1)
                            {
                                TempBoard2[t, opp]--;
                                TempBoard2[26, opp]++;
                            }
                            TempBoard2[j, AIColour]--;
                            TempBoard2[t, AIColour]++;


                            //If dice are a double move 2 more times
                            if (TempDiceRolls[2] == 1)
                            {
                                TempBoards3(TempDiceRolls, TempBoardList, TempBoard2, opp, j, dice);
                            }

                            else
                            {
                                TempPipCount(TempBoard2);
                                TempBoardList.Add(TempBoard2);
                            }
                        }
                    }
                }
            }
        }

        private void TempBoards3(int[] TempDiceRolls, List<int[,]> TempBoardList, int[,] TempBoard2, int opp, int j, int dice)
        {
            int[,] TempBoard3;
            int t=0;
            if (AIColour == 1)
            {
                TempBoard3 = new int[28, 2];
                Array.Copy(TempBoard2, TempBoard3, TempBoard2.Length);
                if (FinalMovesCheck(TempBoard3) == true)
                {
                    FinalMove3(TempBoard3, TempBoardList, 0, 1, 0, opp);
                }
                else
                {
                    for (int k = j; k >= 1; k--)
                    {
                        TempBoard3 = new int[28, 2];
                        Array.Copy(TempBoard2, TempBoard3, TempBoard2.Length);

                        t = k + TempDiceRolls[dice];

                        //will do code if there less than 2 opponents colours in the space
                        if (BoardSizeCheck(t) == true && TempBoard3[k, AIColour] >= 1 && TempBoard3[t, opp] < 2)
                        {
                            //if there 1 opponent piece in space remove piece and place it on bar)
                            if (TempBoard3[t, opp] == 1)
                            {
                                TempBoard3[t, opp]--;
                                TempBoard3[26, opp]++;
                            }
                            TempBoard3[k, AIColour]--;
                            TempBoard3[t, AIColour]++;

                            TempBoards4(TempDiceRolls, TempBoardList, TempBoard3, opp, k, dice);
                        }
                    }
                }
            }
            else //white
            {
                TempBoard3 = new int[28, 2];
                Array.Copy(TempBoard2, TempBoard3, TempBoard2.Length);

                if (FinalMovesCheck(TempBoard3) == true)
                {
                    FinalMove3(TempBoard3, TempBoardList, 0, 1, 25, opp);
                }
                else
                {
                    for (int k = j; k <= 24; k++)
                    {
                        TempBoard3 = new int[28, 2];
                        Array.Copy(TempBoard2, TempBoard3, TempBoard2.Length);

                        t = k + TempDiceRolls[dice];

                        //will do code if there less than 2 opponents colours in the space
                        if (BoardSizeCheck(t) == true && TempBoard3[k, AIColour] >= 1 && TempBoard3[t, opp] < 2)
                        {
                            //if there 1 opponent piece in space remove piece and place it on bar)
                            if (TempBoard3[t, opp] == 1)
                            {
                                TempBoard3[t, opp]--;
                                TempBoard3[26, opp]++;
                            }
                            TempBoard3[k, AIColour]--;
                            TempBoard3[t, AIColour]++;

                            TempBoards4(TempDiceRolls, TempBoardList, TempBoard3, opp, k, dice);
                        }
                    }
                }
            }
        }
        private void TempBoards4(int[] TempDiceRolls, List<int[,]> TempBoardList, int[,] TempBoard3, int opp, int k, int dice)
        {
            int[,] TempBoard4;
            int t = 0;

            if (AIColour == 1)
            {
                TempBoard4 = new int[28, 2];
                Array.Copy(TempBoard3, TempBoard4, TempBoard3.Length);
                //moves next piece in list
                if (FinalMovesCheck(TempBoard4) == true)
                {
                    FinalMove4(TempBoard4, TempBoardList, 0, 1, 0, opp);
                }
                else
                {
                    for (int l = k; l >= 1; l--)
                    {
                        TempBoard4 = new int[28, 2];
                        Array.Copy(TempBoard3, TempBoard4, TempBoard3.Length);

                        t = l + TempDiceRolls[dice];

                        //will do code if there less than 2 opponents colours in the space
                        if (BoardSizeCheck(t) == true && TempBoard4[l, AIColour] >= 1 && TempBoard4[t, opp] < 2)
                        {
                            //if there 1 opponent piece in space remove piece and place it on bar)
                            if (TempBoard4[t, opp] == 1)
                            {
                                TempBoard4[t, opp]--;
                                TempBoard4[26, opp]++;
                            }
                            TempBoard4[l, AIColour]--;
                            TempBoard4[t, AIColour]++;

                            //add count
                            TempPipCount(TempBoard4);

                            //Adds TempBoard4 to list instead of TempBoard2
                            TempBoardList.Add(TempBoard4);
                        }
                    }
                }
            }
            else //white
            {
                TempBoard4 = new int[28, 2];
                Array.Copy(TempBoard3, TempBoard4, TempBoard3.Length);
                //moves next piece in list
                if (FinalMovesCheck(TempBoard4) == true)
                {
                    FinalMove4(TempBoard4, TempBoardList, 0, 1, 25, opp);
                }
                else
                {
                    for (int l = k; l <= 24; l++)
                    {
                        TempBoard4 = new int[28, 2];
                        Array.Copy(TempBoard3, TempBoard4, TempBoard3.Length);

                        t = l + TempDiceRolls[dice];

                        //will do code if there less than 2 opponents colours in the space
                        if (BoardSizeCheck(t) == true && TempBoard4[l, AIColour] >= 1 && TempBoard4[t, opp] < 2)
                        {
                            //if there 1 opponent piece in space remove piece and place it on bar)
                            if (TempBoard4[t, opp] == 1)
                            {
                                TempBoard4[t, opp]--;
                                TempBoard4[26, opp]++;
                            }
                            TempBoard4[l, AIColour]--;
                            TempBoard4[t, AIColour]++;

                            //add count 
                            TempPipCount(TempBoard4);

                            //Adds TempBoard4 to list instead of TempBoard2
                            TempBoardList.Add(TempBoard4);
                        }
                    }
                }
            }
        }
        private void FinalMove(int[,] DefaultBoard, List<int[,]> TempBoardList, int diceNum1, int diceNum2, int end,int opp)
        {
            if (AIColour == 1)
            {  
          
                int[,] TempBoard1 = new int[28, 2];
                Array.Copy(DefaultBoard, TempBoard1, DefaultBoard.Length);
                //If we find a piece with the correct dice rolls put it straight into the pot
                if (TempBoard1[diceRolls[diceNum1], AIColour] >= 1)
                {
                    TempBoard1[diceRolls[diceNum1], AIColour]--;
                    TempBoard1[end, AIColour]++;
                    //This places the piece on the finishing point when their only 1 piece on the board
                    if (TempBoard1[end, AIColour] >= 15)
                    {
                        TempPipCount(TempBoard1);
                        TempBoardList.Add(TempBoard1);
                    }
                    else
                        FinalMove2(TempBoard1, TempBoardList, diceNum2, diceNum1, end, opp);
                }
                else
                {
                    int count = 0;
                    int t = 0;
                    //if we dont find the next possible move we can do
                    for (int i = 6; i >= 1; i--)
                    {
                        TempBoard1 = new int[28, 2];
                        Array.Copy(DefaultBoard, TempBoard1, DefaultBoard.Length);

                        t = i - diceRolls[diceNum1];

                        if (TempBoard1[i, AIColour] >= 1)
                        {
                            if (t >= 1)
                            {
                                if (TempBoard1[t, opp] < 2)
                                {
                                    if (TempBoard1[t, opp] == 1)
                                    {
                                        TempBoard1[t, opp]--;
                                        TempBoard1[26, opp]++;
                                    }
                                    TempBoard1[i, AIColour]--;
                                    TempBoard1[t, AIColour]++;
                                    count++;
                                }
                            }
                            else if (count == 0)
                            {

                                TempBoard1[i, AIColour]--;
                                TempBoard1[end, AIColour]++;
                                count++;
                            }
                            //This places the piece on the finishing point when their only 1 piece on the board
                            if (TempBoard1[end, AIColour] >= 15)
                            {
                                TempPipCount(TempBoard1);
                                TempBoardList.Add(TempBoard1);
                            }
                            else
                                FinalMove2(TempBoard1, TempBoardList, diceNum2, diceNum1, end, opp);
                        }
                    }
                }
            }
            else //white
            {
                int[,] TempBoard1 = new int[28, 2];
                Array.Copy(DefaultBoard, TempBoard1, DefaultBoard.Length);
                //If we find a piece with the correct dice rolls put it straight into the pot
                if (TempBoard1[25 - diceRolls[diceNum1], AIColour] >= 1)
                {
                    TempBoard1[25 - diceRolls[diceNum1], AIColour]--;
                    TempBoard1[end, AIColour]++;

                    //This places the piece on the finishing point when their only 1 piece on the board
                    if (TempBoard1[end, AIColour] >= 15)
                    {
                        TempPipCount(TempBoard1);
                        TempBoardList.Add(TempBoard1);
                    }
                    else
                        FinalMove2(TempBoard1, TempBoardList, diceNum2, diceNum1, end, opp);
                }
                else
                {
                    int count = 0;
                    int t = 0;
                    //if we dont find the next possible move we can do
                    for (int i = 19; i <= 24; i++)
                    {
                        TempBoard1 = new int[28, 2];
                        Array.Copy(DefaultBoard, TempBoard1, DefaultBoard.Length);

                        t = i + diceRolls[diceNum1];

                        if (TempBoard1[i, AIColour] >= 1)
                        {
                            if (t <= 24)
                            {
                                if (TempBoard1[t, opp] < 2)
                                {
                                    if (TempBoard1[t, opp] == 1)
                                    {
                                        TempBoard1[t, opp]--;
                                        TempBoard1[26, opp]++;
                                    }
                                    TempBoard1[i, AIColour]--;
                                    TempBoard1[t, AIColour]++;
                                    count++;
                                }
                            }
                            else if (count == 0)
                            {

                                TempBoard1[i, AIColour]--;
                                TempBoard1[end, AIColour]++;
                                count++;
                            }
                            //This places the piece on the finishing point when their only 1 piece on the board
                            if (TempBoard1[end, AIColour] >= 15)
                            {
                                TempPipCount(TempBoard1);
                                TempBoardList.Add(TempBoard1);
                            }
                            else
                                FinalMove2(TempBoard1, TempBoardList, diceNum2, diceNum1, end, opp);
                        }
                    }
                }
            }

        }


        private void FinalMove2(int[,] TempBoard1, List<int[,]> TempBoardList, int diceNum1, int diceNum2, int end, int opp)
        {
            if (AIColour == 1)
            {
                int[,] TempBoard2 = new int[28, 2];
                Array.Copy(TempBoard1, TempBoard2, TempBoard1.Length);

                //If we find a piece with the correct dice rolls put it straight into the pot
                if (TempBoard2[diceRolls[diceNum1], AIColour] >= 1)
                {
                    TempBoard2[diceRolls[diceNum1], AIColour]--;
                    TempBoard2[end, AIColour]++;
                    if (diceRolls[2] == 1)
                    {
                        FinalMove3(TempBoard2, TempBoardList, diceNum1, diceNum2, end, opp);
                    }
                    else
                    {
                        TempPipCount(TempBoard2);
                        TempBoardList.Add(TempBoard2);
                    }
                }
                else
                {
                    int count = 0;
                    int t = 0;
                    //if we dont find the next possible move we can do
                    for (int i = 6; i >= 1; i--)
                    {
                        TempBoard2 = new int[28, 2];
                        Array.Copy(TempBoard1, TempBoard2, TempBoard1.Length);

                        t = i - diceRolls[diceNum1];

                        if (TempBoard2[i, AIColour] >= 1)
                        {
                            if (t >= 1)
                            {
                                if (TempBoard2[t, opp] < 2)
                                {
                                    if (TempBoard2[t, opp] == 1)
                                    {
                                        TempBoard2[t, opp]--;
                                        TempBoard2[26, opp]++;
                                    }
                                    TempBoard2[i, AIColour]--;
                                    TempBoard2[t, AIColour]++;
                                    count++;
                                }
                            }
                            else if (count == 0)
                            {

                                TempBoard2[i, AIColour]--;
                                TempBoard2[end, AIColour]++;
                                count++;
                            }
                            if (diceRolls[2] == 1)
                            {
                                FinalMove3(TempBoard2, TempBoardList, diceNum1, diceNum2, end, opp);
                            }
                            else
                            {
                                TempPipCount(TempBoard2);
                                TempBoardList.Add(TempBoard2);
                            }
                        }
                    }
                }
            }
            else //white
            {
                int[,] TempBoard2 = new int[28, 2];
                Array.Copy(TempBoard1, TempBoard2, TempBoard1.Length);
                //If we find a piece with the correct dice rolls put it straight into the pot
                if (TempBoard2[25-diceRolls[diceNum1], AIColour] >= 1)
                {
                    TempBoard2[25-diceRolls[diceNum1], AIColour]--;
                    TempBoard2[end, AIColour]++;
                    if (diceRolls[2] == 1)
                    {
                        FinalMove3(TempBoard2, TempBoardList, diceNum1, diceNum2, end, opp);
                    }
                    else
                    {
                        TempPipCount(TempBoard2);
                        TempBoardList.Add(TempBoard2);
                    }
                }
                else
                {
                    int count = 0;
                    int t = 0;
                    //if we dont find the next possible move we can do
                    for (int i = 19; i <= 24; i++)
                    {
                        TempBoard2 = new int[28, 2];
                        Array.Copy(TempBoard1, TempBoard2, TempBoard1.Length);

                        t = i + diceRolls[diceNum1];

                        if (TempBoard2[i, AIColour] >= 1)
                        {
                            if (t <= 24)
                            {
                                if (TempBoard2[t, opp] < 2)
                                {
                                    if (TempBoard2[t, opp] == 1)
                                    {
                                        TempBoard2[t, opp]--;
                                        TempBoard2[26, opp]++;
                                    }
                                    TempBoard2[i, AIColour]--;
                                    TempBoard2[t, AIColour]++;
                                    count++;
                                }
                            }
                            else if (count == 0)
                            {

                                TempBoard2[i, AIColour]--;
                                TempBoard2[end, AIColour]++;
                                count++;
                            }
                            if (diceRolls[2] == 1)
                            {
                                FinalMove3(TempBoard2, TempBoardList, diceNum1, diceNum2, end, opp);
                            }
                            else
                            {
                                TempPipCount(TempBoard2);
                                TempBoardList.Add(TempBoard2);
                            }
                        }
                    }
                }
            }
        }
        private void FinalMove3(int[,] TempBoard2, List<int[,]> TempBoardList, int diceNum1, int diceNum2, int end, int opp)
        {
            if (AIColour == 1)
            {
                int[,] TempBoard3 = new int[28, 2];
                Array.Copy(TempBoard2, TempBoard3, TempBoard2.Length);
                //If we find a piece with the correct dice rolls put it straight into the pot
                if (TempBoard3[diceRolls[diceNum1], AIColour] >= 1)
                {
                    TempBoard3[diceRolls[diceNum1], AIColour]--;
                    TempBoard3[end, AIColour]++;

                    FinalMove4(TempBoard3, TempBoardList, diceNum1, diceNum2, end, opp);
                }
                else
                {
                    int count = 0;
                    int t = 0;
                    //if we dont find the next possible move we can do
                    for (int i = 6; i >= 1; i--)
                    {
                        TempBoard3 = new int[28, 2];
                        Array.Copy(TempBoard2, TempBoard3, TempBoard2.Length);

                        t = i - diceRolls[diceNum1];

                        if (TempBoard3[i, AIColour] >= 1)
                        {
                            if (t >= 1)
                            {
                                if (TempBoard3[t, opp] < 2)
                                {
                                    if (TempBoard3[t, opp] == 1)
                                    {
                                        TempBoard3[t, opp]--;
                                        TempBoard3[26, opp]++;
                                    }
                                    TempBoard3[i, AIColour]--;
                                    TempBoard3[t, AIColour]++;
                                    count++;
                                }
                            }
                            else if (count == 0)
                            {

                                TempBoard3[i, AIColour]--;
                                TempBoard3[end, AIColour]++;
                                count++;
                            }
                            FinalMove4(TempBoard3, TempBoardList, diceNum1, diceNum2, end, opp);
                        }
                    }
                }
            }
            else //white
            {
                int[,] TempBoard3 = new int[28, 2];
                Array.Copy(TempBoard2, TempBoard3, TempBoard2.Length);
                //If we find a piece with the correct dice rolls put it straight into the pot
                if (TempBoard3[25-diceRolls[diceNum1], AIColour] >= 1)
                {
                    TempBoard3[25-diceRolls[diceNum1], AIColour]--;
                    TempBoard3[end, AIColour]++;

                    FinalMove4(TempBoard3, TempBoardList, diceNum1, diceNum2, end, opp);
                }
                else
                {
                    int count = 0;
                    int t = 0;
                    //if we dont find the next possible move we can do
                    for (int i = 19; i <= 24; i++)
                    {
                        TempBoard3 = new int[28, 2];
                        Array.Copy(TempBoard2, TempBoard3, TempBoard2.Length);

                        t = i + diceRolls[diceNum1];

                        if (TempBoard3[i, AIColour] >= 1)
                        {
                            if (t <= 24)
                            {
                                if (TempBoard3[t, opp] < 2)
                                {
                                    if (TempBoard3[t, opp] == 1)
                                    {
                                        TempBoard3[t, opp]--;
                                        TempBoard3[26, opp]++;
                                    }
                                    TempBoard3[i, AIColour]--;
                                    TempBoard3[t, AIColour]++;
                                    count++;
                                }
                            }
                            else if (count == 0)
                            {

                                TempBoard3[i, AIColour]--;
                                TempBoard3[end, AIColour]++;
                                count++;
                            }
                            FinalMove4(TempBoard3, TempBoardList, diceNum1, diceNum2, end, opp);
                        }
                    }
                }
            }
        }
        private void FinalMove4(int[,] TempBoard3, List<int[,]> TempBoardList, int diceNum1, int diceNum2, int end, int opp)
        {
            if (AIColour == 1)
            {
                int[,] TempBoard4 = new int[28, 2];
                Array.Copy(TempBoard3, TempBoard4, TempBoard3.Length);
                //If we find a piece with the correct dice rolls put it straight into the pot
                if (TempBoard4[diceRolls[diceNum1], AIColour] >= 1)
                {
                    TempBoard4[diceRolls[diceNum1], AIColour]--;
                    TempBoard4[end, AIColour]++;

                    TempPipCount(TempBoard4);
                    TempBoardList.Add(TempBoard4);
                }
                else
                {
                    int count = 0;
                    int t = 0;
                    //if we dont find the next possible move we can do
                    for (int i = 6; i >= 1; i--)
                    {
                        TempBoard4 = new int[28, 2];
                        Array.Copy(TempBoard3, TempBoard4, TempBoard3.Length);

                        t = i - diceRolls[diceNum1];

                        if (TempBoard4[i, AIColour] >= 1)
                        {
                            if (t >= 1)
                            {
                                if (TempBoard4[t, opp] < 2)
                                {
                                    if (TempBoard4[t, opp] == 1)
                                    {
                                        TempBoard4[t, opp]--;
                                        TempBoard4[26, opp]++;
                                    }
                                    TempBoard4[i, AIColour]--;
                                    TempBoard4[t, AIColour]++;
                                    count++;
                                }
                            }
                            else if (count == 0)
                            {

                                TempBoard4[i, AIColour]--;
                                TempBoard4[end, AIColour]++;
                                count++;
                            }
                            TempPipCount(TempBoard4);
                            TempBoardList.Add(TempBoard4);
                        }
                    }
                }
            }
            else //white
            {
                int[,] TempBoard4 = new int[28, 2];
                Array.Copy(TempBoard3, TempBoard4, TempBoard3.Length);
                //If we find a piece with the correct dice rolls put it straight into the pot
                if (TempBoard4[25-diceRolls[diceNum1], AIColour] >= 1)
                {
                    TempBoard4[25-diceRolls[diceNum1], AIColour]--;
                    TempBoard4[end, AIColour]++;

                    TempPipCount(TempBoard4);
                    TempBoardList.Add(TempBoard4);
                }
                else
                {
                    int count = 0;
                    int t = 0;
                    //if we dont find the next possible move we can do
                    for (int i = 19; i <= 24; i++)
                    {
                        TempBoard4 = new int[28, 2];
                        Array.Copy(TempBoard3, TempBoard4, TempBoard3.Length);

                        t = i + diceRolls[diceNum1];

                        if (TempBoard4[i, AIColour] >= 1)
                        {
                            if (t <= 24)
                            {
                                if (TempBoard4[t, opp] < 2)
                                {
                                    if (TempBoard4[t, opp] == 1)
                                    {
                                        TempBoard4[t, opp]--;
                                        TempBoard4[26, opp]++;
                                    }
                                    TempBoard4[i, AIColour]--;
                                    TempBoard4[t, AIColour]++;
                                    count++;
                                }
                            }
                            else if (count == 0)
                            {

                                TempBoard4[i, AIColour]--;
                                TempBoard4[end, AIColour]++;
                                count++;
                            }
                            TempPipCount(TempBoard4);
                            TempBoardList.Add(TempBoard4);
                        }
                    }
                }
            }
        }


        //This checks the bar and does the movelist beginning off the bar 
        private void BarCheck(int[,] DefaultBoard, List<int[,]> TempBoardList)
        {
            int[,] TempBoard1;
            int[,] TempBoard2;
            int[,] TempBoard3;
            int[,] TempBoard4;


            //black
            if (AIColour == 1)
            {
                //if Ai is playing black we'll be inverting the dice numbers so that they are removing from the board rather than adding on
                int[] TempDiceRolls = new int[3];
                TempDiceRolls[0] = -diceRolls[0];
                TempDiceRolls[1] = -diceRolls[1];
                TempDiceRolls[2] = diceRolls[2];

                int opp = 0;
                int i = 26;
                int t = 25 + TempDiceRolls[0];

                TempBoard1 = new int[28, 2];
                Array.Copy(DefaultBoard, TempBoard1, DefaultBoard.Length);

                //will do code if there less than 2 opponents colours in the space
                if (TempBoard1[t, opp] < 2)
                {
                    //if there 1 opponent piece in space remove piece and place it on bar)
                    if (TempBoard1[t, opp] == 1)
                    {
                        TempBoard1[t, opp]--;
                        TempBoard1[26, opp]++;
                    }
                    TempBoard1[i, AIColour]--;
                    TempBoard1[t, AIColour]++;

                    //checks to see if their any pieces stuck on bar
                    if (TempBoard1[26, AIColour] > 0)
                    {
                        t = 25 + TempDiceRolls[1];

                        TempBoard2 = new int[28, 2];
                        Array.Copy(TempBoard1, TempBoard2, TempBoard1.Length);

                        //will do code if there less than 2 opponents colours in the space
                        if (TempBoard2[t, opp] < 2)
                        {
                            //if there 1 opponent piece in space remove piece and place it on bar)
                            if (TempBoard2[t, opp] == 1)
                            {
                                TempBoard2[t, opp]--;
                                TempBoard2[26, opp]++;
                            }
                            TempBoard2[i, AIColour]--;
                            TempBoard2[t, AIColour]++;

                            //checks to see if their any pieces stuck on bar
                            if (TempBoard2[26, AIColour] > 0 && diceRolls[2] >= 1)
                            {

                                t = 25 + TempDiceRolls[0];

                                TempBoard3 = new int[28, 2];
                                Array.Copy(TempBoard2, TempBoard3, TempBoard2.Length);

                                //will do code if there less than 2 opponents colours in the space
                                if (TempBoard3[t, opp] < 2)
                                {
                                    //if there 1 opponent piece in space remove piece and place it on bar)
                                    if (TempBoard3[t, opp] == 1)
                                    {
                                        TempBoard3[t, opp]--;
                                        TempBoard3[26, opp]++;
                                    }
                                    TempBoard3[i, AIColour]--;
                                    TempBoard3[t, AIColour]++;

                                    //checks to see if their any pieces stuck on bar
                                    if (TempBoard3[26, AIColour] > 0)
                                    {

                                        t = 25 + TempDiceRolls[0];

                                        TempBoard4 = new int[28, 2];
                                        Array.Copy(TempBoard3, TempBoard4, TempBoard3.Length);

                                        //will do code if there less than 2 opponents colours in the space
                                        if (TempBoard4[t, opp] < 2)
                                        {
                                            //if there 1 opponent piece in space remove piece and place it on bar)
                                            if (TempBoard4[t, opp] == 1)
                                            {
                                                TempBoard4[t, opp]--;
                                                TempBoard4[26, opp]++;
                                            }
                                            TempBoard4[i, AIColour]--;
                                            TempBoard4[t, AIColour]++;

                                            TempPipCount(TempBoard4);
                                            TempBoardList.Add(TempBoard4);
                                        }

                                    }
                                    else
                                        TempBoards4(TempDiceRolls, TempBoardList, TempBoard3, opp, 24, 1);
                                }
                            }
                            else if (diceRolls[2] == 1)
                            {
                                TempBoards3(TempDiceRolls, TempBoardList, TempBoard2, opp, 24, 1);
                            }
                            else
                            {
                                TempPipCount(TempBoard2);
                                TempBoardList.Add(TempBoard2);
                            }

                        }
                    }
                    else
                        TempBoards2(TempDiceRolls, TempBoardList, TempBoard1, opp, 24, 1, 0);
                }

                //check for moves with dice roll 2 first

                TempBoard1 = new int[28, 2];
                Array.Copy(DefaultBoard, TempBoard1, DefaultBoard.Length);

                t = 25 + TempDiceRolls[1];

                //will do code if there less than 2 opponents colours in the space
                if (TempBoard1[t, opp] < 2)
                {
                    //if there 1 opponent piece in space remove piece and place it on bar)
                    if (TempBoard1[t, opp] == 1)
                    {
                        TempBoard1[t, opp]--;
                        TempBoard1[26, opp]++;
                    }
                    TempBoard1[i, AIColour]--;
                    TempBoard1[t, AIColour]++;

                    //checks to see if their any pieces stuck on bar
                    if (TempBoard1[26, AIColour] > 0)
                    {
                        t = 25 + TempDiceRolls[0];

                        TempBoard2 = new int[28, 2];
                        Array.Copy(TempBoard1, TempBoard2, TempBoard1.Length);

                        //will do code if there less than 2 opponents colours in the space
                        if (TempBoard2[t, opp] < 2)
                        {
                            //if there 1 opponent piece in space remove piece and place it on bar)
                            if (TempBoard2[t, opp] == 1)
                            {
                                TempBoard2[t, opp]--;
                                TempBoard2[26, opp]++;
                            }
                            TempBoard2[i, AIColour]--;
                            TempBoard2[t, AIColour]++;

                            //checks to see if their any pieces stuck on bar
                            if (TempBoard2[26, AIColour] > 0 && diceRolls[2] >= 1)
                            {

                                t = 25 + TempDiceRolls[0];

                                TempBoard3 = new int[28, 2];
                                Array.Copy(TempBoard2, TempBoard3, TempBoard2.Length);

                                //will do code if there less than 2 opponents colours in the space
                                if (TempBoard3[t, opp] < 2)
                                {
                                    //if there 1 opponent piece in space remove piece and place it on bar)
                                    if (TempBoard3[t, opp] == 1)
                                    {
                                        TempBoard3[t, opp]--;
                                        TempBoard3[26, opp]++;
                                    }
                                    TempBoard3[i, AIColour]--;
                                    TempBoard3[t, AIColour]++;

                                    //checks to see if their any pieces stuck on bar
                                    if (TempBoard3[26, AIColour] > 0)
                                    {

                                        t = 25 + TempDiceRolls[0];

                                        TempBoard4 = new int[28, 2];
                                        Array.Copy(TempBoard3, TempBoard4, TempBoard3.Length);

                                        //will do code if there less than 2 opponents colours in the space
                                        if (TempBoard4[t, opp] < 2)
                                        {
                                            //if there 1 opponent piece in space remove piece and place it on bar)
                                            if (TempBoard4[t, opp] == 1)
                                            {
                                                TempBoard4[t, opp]--;
                                                TempBoard4[26, opp]++;
                                            }
                                            TempBoard4[i, AIColour]--;
                                            TempBoard4[t, AIColour]++;

                                            TempPipCount(TempBoard4);
                                            TempBoardList.Add(TempBoard4);
                                        }

                                    }
                                    else
                                        TempBoards4(TempDiceRolls, TempBoardList, TempBoard3, opp, 24, 1);
                                }
                            }
                            else if (diceRolls[2] == 1)
                            {
                                TempBoards3(TempDiceRolls, TempBoardList, TempBoard2, opp, 24, 1);
                            }
                            else
                            {
                                TempPipCount(TempBoard2);
                                TempBoardList.Add(TempBoard2);
                            }

                        }
                    }
                    else
                        TempBoards2(TempDiceRolls, TempBoardList, TempBoard1, opp, 24, 0, 1);
                }
            }
     
            
            else //white
            {

                int opp = 1;
                int i = 26;
                int t = 0 + diceRolls[0];

                TempBoard1 = new int[28, 2];
                Array.Copy(DefaultBoard, TempBoard1, DefaultBoard.Length);

                //will do code if there less than 2 opponents colours in the space
                if (TempBoard1[t, opp] < 2)
                {
                    //if there 1 opponent piece in space remove piece and place it on bar)
                    if (TempBoard1[t, opp] == 1)
                    {
                        TempBoard1[t, opp]--;
                        TempBoard1[26, opp]++;
                    }
                    TempBoard1[i, AIColour]--;
                    TempBoard1[t, AIColour]++;

                    //checks to see if their any pieces stuck on bar
                    if (TempBoard1[26, AIColour] > 0)
                    {
                        t = 0 + diceRolls[1];

                        TempBoard2 = new int[28, 2];
                        Array.Copy(TempBoard1, TempBoard2, TempBoard1.Length);

                        //will do code if there less than 2 opponents colours in the space
                        if (TempBoard2[t, opp] < 2)
                        {
                            //if there 1 opponent piece in space remove piece and place it on bar)
                            if (TempBoard2[t, opp] == 1)
                            {
                                TempBoard2[t, opp]--;
                                TempBoard2[26, opp]++;
                            }
                            TempBoard2[i, AIColour]--;
                            TempBoard2[t, AIColour]++;

                            //checks to see if their any pieces stuck on bar
                            if (TempBoard2[26, AIColour] > 0 && diceRolls[2] >= 1)
                            {

                                t = 0 + diceRolls[0];

                                TempBoard3 = new int[28, 2];
                                Array.Copy(TempBoard2, TempBoard3, TempBoard2.Length);

                                //will do code if there less than 2 opponents colours in the space
                                if (TempBoard3[t, opp] < 2)
                                {
                                    //if there 1 opponent piece in space remove piece and place it on bar)
                                    if (TempBoard3[t, opp] == 1)
                                    {
                                        TempBoard3[t, opp]--;
                                        TempBoard3[26, opp]++;
                                    }
                                    TempBoard3[i, AIColour]--;
                                    TempBoard3[t, AIColour]++;

                                    //checks to see if their any pieces stuck on bar
                                    if (TempBoard3[26, AIColour] > 0)
                                    {

                                        t = 0 + diceRolls[0];

                                        TempBoard4 = new int[28, 2];
                                        Array.Copy(TempBoard3, TempBoard4, TempBoard3.Length);

                                        //will do code if there less than 2 opponents colours in the space
                                        if (TempBoard4[t, opp] < 2)
                                        {
                                            //if there 1 opponent piece in space remove piece and place it on bar)
                                            if (TempBoard4[t, opp] == 1)
                                            {
                                                TempBoard4[t, opp]--;
                                                TempBoard4[26, opp]++;
                                            }
                                            TempBoard4[i, AIColour]--;
                                            TempBoard4[t, AIColour]++;

                                            TempPipCount(TempBoard4);
                                            TempBoardList.Add(TempBoard4);
                                        }

                                    }
                                    else
                                        TempBoards4(diceRolls, TempBoardList, TempBoard3, opp, 1, 1);
                                }
                            }
                            else if (diceRolls[2] == 1)
                            {
                                TempBoards3(diceRolls, TempBoardList, TempBoard2, opp, 1, 1);
                            }
                            else
                            {
                                TempPipCount(TempBoard2);
                                TempBoardList.Add(TempBoard2);
                            }

                        }
                    }
                    else
                        TempBoards2(diceRolls, TempBoardList, TempBoard1, opp, 1, 1, 0);
                }

                //check for moves with dice roll 2 first

                TempBoard1 = new int[28, 2];
                Array.Copy(DefaultBoard, TempBoard1, DefaultBoard.Length);

                t = 0 + diceRolls[1];

                //will do code if there less than 2 opponents colours in the space
                if (TempBoard1[t, opp] < 2)
                {
                    //if there 1 opponent piece in space remove piece and place it on bar)
                    if (TempBoard1[t, opp] == 1)
                    {
                        TempBoard1[t, opp]--;
                        TempBoard1[26, opp]++;
                    }
                    TempBoard1[i, AIColour]--;
                    TempBoard1[t, AIColour]++;

                    //checks to see if their any pieces stuck on bar
                    if (TempBoard1[26, AIColour] > 0)
                    {
                        t = 0 + diceRolls[0];

                        TempBoard2 = new int[28, 2];
                        Array.Copy(TempBoard1, TempBoard2, TempBoard1.Length);

                        //will do code if there less than 2 opponents colours in the space
                        if (TempBoard2[t, opp] < 2)
                        {
                            //if there 1 opponent piece in space remove piece and place it on bar)
                            if (TempBoard2[t, opp] == 1)
                            {
                                TempBoard2[t, opp]--;
                                TempBoard2[26, opp]++;
                            }
                            TempBoard2[i, AIColour]--;
                            TempBoard2[t, AIColour]++;

                            //checks to see if their any pieces stuck on bar
                            if (TempBoard2[26, AIColour] > 0 && diceRolls[2] >= 1)
                            {

                                t = 0 + diceRolls[0];

                                TempBoard3 = new int[28, 2];
                                Array.Copy(TempBoard2, TempBoard3, TempBoard2.Length);

                                //will do code if there less than 2 opponents colours in the space
                                if (TempBoard3[t, opp] < 2)
                                {
                                    //if there 1 opponent piece in space remove piece and place it on bar)
                                    if (TempBoard3[t, opp] == 1)
                                    {
                                        TempBoard3[t, opp]--;
                                        TempBoard3[26, opp]++;
                                    }
                                    TempBoard3[i, AIColour]--;
                                    TempBoard3[t, AIColour]++;

                                    //checks to see if their any pieces stuck on bar
                                    if (TempBoard3[26, AIColour] > 0)
                                    {

                                        t = 0 + diceRolls[0];

                                        TempBoard4 = new int[28, 2];
                                        Array.Copy(TempBoard3, TempBoard4, TempBoard3.Length);

                                        //will do code if there less than 2 opponents colours in the space
                                        if (TempBoard4[t, opp] < 2)
                                        {
                                            //if there 1 opponent piece in space remove piece and place it on bar)
                                            if (TempBoard4[t, opp] == 1)
                                            {
                                                TempBoard4[t, opp]--;
                                                TempBoard4[26, opp]++;
                                            }
                                            TempBoard4[i, AIColour]--;
                                            TempBoard4[t, AIColour]++;

                                            TempPipCount(TempBoard4);
                                            TempBoardList.Add(TempBoard4);
                                        }

                                    }
                                    else
                                        TempBoards4(diceRolls, TempBoardList, TempBoard3, opp, 1, 0);
                                }
                            }
                            else if (diceRolls[2] == 1)
                            {
                                TempBoards3(diceRolls, TempBoardList, TempBoard2, opp, 1, 0);
                            }
                            else
                            {
                                TempPipCount(TempBoard2);
                                TempBoardList.Add(TempBoard2);
                            }

                        }
                    }
                    else
                        TempBoards2(diceRolls, TempBoardList, TempBoard1, opp, 1, 0, 1);
                }
            }
        }

        private void BlackMoveTree(int[,] DefaultBoard, List<int[,]> TempBoardList)
        {

            int[,] TempBoard1 = new int[28, 2];
            int opp = 0;
            int t = 0;


            Array.Copy(DefaultBoard, TempBoard1, DefaultBoard.Length);

            //if Ai is playing black we'll be inverting the dice numbers so that they are removing from the board rather than adding on
            int[] TempDiceRolls = new int[3];
            TempDiceRolls[0] = -diceRolls[0];
            TempDiceRolls[1] = -diceRolls[1];
            TempDiceRolls[2] = diceRolls[2];

            //Checks the bar first to see if there any pieces there that needs moving first
            if (TempBoard1[26, AIColour] > 0)
            {
                BarCheck(DefaultBoard, TempBoardList);
            }
            else
            {
                if (FinalMovesCheck(DefaultBoard) == true)
                {
                    TempBoard1 = new int[28, 2];
                    Array.Copy(DefaultBoard, TempBoard1, DefaultBoard.Length);

                    FinalMove(DefaultBoard, TempBoardList, 0, 1,0, opp);
                }
                else
                {
                    //This loop will be searching the board to see if we can find any pieces to move around the map
                    for (int i = 24; i >= 1; i--)
                    {


                        TempBoard1 = new int[28, 2];
                        Array.Copy(DefaultBoard, TempBoard1, DefaultBoard.Length);

                        t = i + TempDiceRolls[0];

                        //will do code if there less than 2 opponents colours in the space
                        if (BoardSizeCheck(t) == true && TempBoard1[i, AIColour] >= 1 && TempBoard1[t, opp] < 2)
                        {
                            //if there 1 opponent piece in space remove piece and place it on bar)
                            if (TempBoard1[t, opp] == 1)
                            {
                                TempBoard1[t, opp]--;
                                TempBoard1[26, opp]++;
                            }
                            TempBoard1[i, AIColour]--;
                            TempBoard1[t, AIColour]++;


                            TempBoards2(TempDiceRolls, TempBoardList, TempBoard1, opp, i, 1, 0);
                        }
                    }
                }

                //check for moves with dice roll 2 first

                //This loop will be searching the board to see if we can find any pieces to move around the map
                if (FinalMovesCheck(DefaultBoard) == true)
                {
                    TempBoard1 = new int[28, 2];
                    Array.Copy(DefaultBoard, TempBoard1, DefaultBoard.Length);

                    FinalMove(DefaultBoard, TempBoardList, 1, 0, 0, opp);
                }
                else
                {
                    for (int i = 24; i >= 1; i--)
                    {
                        TempBoard1 = new int[28, 2];
                        Array.Copy(DefaultBoard, TempBoard1, DefaultBoard.Length);

                        t = i + TempDiceRolls[1];

                        //will do code if there less than 2 opponents colours in the space
                        if (BoardSizeCheck(t) == true && TempBoard1[i, AIColour] >= 1 && TempBoard1[t, opp] < 2)
                        {
                            //if there 1 opponent piece in space remove piece and place it on bar)
                            if (TempBoard1[t, opp] == 1)
                            {
                                TempBoard1[t, opp]--;
                                TempBoard1[26, opp]++;
                            }
                            TempBoard1[i, AIColour]--;
                            TempBoard1[t, AIColour]++;

                            TempBoards2(TempDiceRolls, TempBoardList, TempBoard1, opp, i, 0, 1);
                        }
                    }
                }
            }
        }

        private void WhiteMoveTree(int[,] DefaultBoard, List<int[,]> TempBoardList)
        {
            int[,] TempBoard1 = new int[28, 2];

            int opp = 1;
            int t = 0;


            Array.Copy(DefaultBoard, TempBoard1, DefaultBoard.Length);

            //Checks the bar first to see if there any pieces there that needs moving first
            if (TempBoard1[26, AIColour] > 0)
            {
                BarCheck(DefaultBoard, TempBoardList);
            }
            else
            {
                if (FinalMovesCheck(DefaultBoard) == true)
                {
                    TempBoard1 = new int[28, 2];
                    Array.Copy(DefaultBoard, TempBoard1, DefaultBoard.Length);

                    FinalMove(DefaultBoard, TempBoardList, 0, 1, 25, opp);
                }
                else
                {
                    //This loop will be searching the board to see if we can find any pieces to move around the map
                    for (int i = 1; i <= 24; i++)
                    {

                        TempBoard1 = new int[28, 2];
                        Array.Copy(DefaultBoard, TempBoard1, DefaultBoard.Length);

                        t = i + diceRolls[0];

                        //will do code if there less than 2 opponents colours in the space
                        if (BoardSizeCheck(t) == true && TempBoard1[i, AIColour] >= 1 && TempBoard1[t, opp] < 2)
                        {
                            //if there 1 opponent piece in space remove piece and place it on bar)
                            if (TempBoard1[t, opp] == 1)
                            {
                                TempBoard1[t, opp]--;
                                TempBoard1[26, opp]++;
                            }
                            TempBoard1[i, AIColour]--;
                            TempBoard1[t, AIColour]++;

                            TempBoards2(diceRolls, TempBoardList, TempBoard1, opp, i, 1, 0);
                        }
                    }
                }

                //check for moves with dice roll 2 first

                //This loop will be searching the board to see if we can find any pieces to move around the map
                if (FinalMovesCheck(DefaultBoard) == true)
                {
                    TempBoard1 = new int[28, 2];
                    Array.Copy(DefaultBoard, TempBoard1, DefaultBoard.Length);

                    FinalMove(DefaultBoard, TempBoardList, 1, 0, 25, opp);
                }
                else
                {
                    for (int i = 1; i <= 24; i++)
                    {
                        TempBoard1 = new int[28, 2];
                        Array.Copy(DefaultBoard, TempBoard1, DefaultBoard.Length);
  
                        t = i + diceRolls[1];

                        //will do code if there less than 2 opponents colours in the space
                        if (BoardSizeCheck(t) == true && TempBoard1[i, AIColour] >= 1 && TempBoard1[t, opp] < 2)
                        {
                            //if there 1 opponent piece in space remove piece and place it on bar)
                            if (TempBoard1[t, opp] == 1)
                            {
                                TempBoard1[t, opp]--;
                                TempBoard1[26, opp]++;
                            }
                            TempBoard1[i, AIColour]--;
                            TempBoard1[t, AIColour]++;

                            TempBoards2(diceRolls, TempBoardList, TempBoard1, opp, i, 0, 1);
                        }
                    }
                }
            }
        }


        //**********************************************************************************************  
        //Random AI turn
        private void RandAITurn(Dice dice, Board board, int Speedrun)
        {
            //Calls for dicethrow
            DiceRolled(dice, board);

            //Finds all possable moves with dicerolls
            int[,] DefaultBoard = new int[28, 2];
            List<int[,]> TempBoardList = new List<int[,]>();
            DefaultBoard = board.getLayout();

            if (AIColour == 1)
            {
                BlackMoveTree(DefaultBoard, TempBoardList);
            }
            else
            {
                WhiteMoveTree(DefaultBoard, TempBoardList);
            };


            //If Bar is blocked off message that AI cant move and goes to next player
            if (TempBoardList.Count < 1)
            {
                Console.WriteLine("Player {0} Cant Move. Missed Turn.", AIColour + 1);
                if (Speedrun == 0)
                {
                    Console.WriteLine("Press any key for next move");
                    Console.ReadKey();
                    Console.WriteLine("");
                }
            }
            else
            {
                //picks a random move out of the possable moves
                Random rand = new Random(); ;
                int PossMove = TempBoardList.Count - 1;
                int Move = rand.Next(0, PossMove);
                board.SendLayout(TempBoardList[Move]);
                board.RefreshBoard();
            }

        }

        //Choose Move from best score
        private void ScoreAITurn(Dice dice, Board board, int Speedrun)
        {
            //Calls for dicethrow
            DiceRolled(dice, board);

            //Finds all possable moves with dicerolls
            int[,] DefaultBoard = new int[28, 2];
            List<int[,]> TempBoardList = new List<int[,]>();
            DefaultBoard = board.getLayout();

            if (AIColour == 1)
            {
                BlackMoveTree(DefaultBoard, TempBoardList);
            }
            else
            {
                WhiteMoveTree(DefaultBoard, TempBoardList);
            };


            //If Bar is blocked off message that AI cant move and goes to next player
            if (TempBoardList.Count < 1)
            {
                //tells us the Computer dice roll
                Console.WriteLine();
                Console.WriteLine("Player {0} throws a {1},{2}", AIColour + 1, dice.dice1, dice.dice2);
                Console.WriteLine("Player {0} Cant Move. Missed Turn.", AIColour + 1);
                if (Speedrun == 0)
                {
                    Console.WriteLine("Press any key for next move");
                    Console.ReadKey();
                    Console.WriteLine("");
                }
            }
            else
            {
                //This looks through the list picking the highest scoring move
                //Score is a minus number so if the player is behind it can still do a move
                int score = -400;
                int best = 0;
                for (int i = 0; i <= (TempBoardList.Count - 1); i++)
                {
                    int[,] TempBoard = TempBoardList[i];
                    if ((TempBoard[27, 0] - TempBoard[27, 1]) > score)
                    {
                        score = TempBoard[27, 0] - TempBoard[27, 1];
                        best = i;
                    }
                }

                //do best move
                board.SendLayout(TempBoardList[best]);
                board.RefreshBoard();
            }
        }

        //This AI is aggressive ai making it priority to attack
        private void AggAITurn(Dice dice, Board board, int Speedrun)
        {
            //Calls for dicethrow
            DiceRolled(dice, board);

            //Finds all possable moves with dicerolls
            int[,] DefaultBoard = new int[28, 2];
            List<int[,]> TempBoardList = new List<int[,]>();
            DefaultBoard = board.getLayout();

            if (AIColour == 1)
            {
                BlackMoveTree(DefaultBoard, TempBoardList);
            }
            else
            {
                WhiteMoveTree(DefaultBoard, TempBoardList);
            };


            //If Bar is blocked off message that AI cant move and goes to next player
            if (TempBoardList.Count < 1)
            {
                //tells us the Computer dice roll
                Console.WriteLine();
                Console.WriteLine("Player {0} throws a {1},{2}", AIColour + 1, dice.dice1, dice.dice2);
                Console.WriteLine("Player {0} Cant Move. Missed Turn.", AIColour + 1);
                if (Speedrun == 0)
                {
                    Console.WriteLine("Press any key for next move");
                    Console.ReadKey();
                    Console.WriteLine("");
                }
            }
            else
            {
                //This looks through the list picking the highest scoring move
                //Score is a minus number so if the player is behind it can still do a move
                int score = -400;
                int best = 0;
                int opp;
                int modifier = 2;

                //Assign the opponent colour 0 for white 1 for black
                if (AIColour == 0)
                    opp = 1;
                else
                    opp = 0;

                for (int i = 0; i <= (TempBoardList.Count - 1); i++)
                {
                    int[,] TempBoard = TempBoardList[i];
                    if ((TempBoard[27, 0] - TempBoard[27, 1] + (TempBoard[26, opp] * modifier)) > score)
                    {
                        score = TempBoard[27, 0] - TempBoard[27, 1] + (TempBoard[26, opp] * modifier);
                        best = i;
                    }
                }

                //do best move
                board.SendLayout(TempBoardList[best]);
                board.RefreshBoard();
            }
        }

        //This AI is defensive ai taking moves that have a less chance of being taken
        private void DefAITurn(Dice dice, Board board, int Speedrun)
        {
            //Calls for dicethrow
            DiceRolled(dice, board);

            //Finds all possable moves with dicerolls
            int[,] DefaultBoard = new int[28, 2];
            List<int[,]> TempBoardList = new List<int[,]>();
            DefaultBoard = board.getLayout();

            if (AIColour == 1)
            {
                BlackMoveTree(DefaultBoard, TempBoardList);
            }
            else
            {
                WhiteMoveTree(DefaultBoard, TempBoardList);
            };


            //If Bar is blocked off message that AI cant move and goes to next player
            if (TempBoardList.Count < 1)
            {
                //tells us the Computer dice roll
                Console.WriteLine();
                Console.WriteLine("Player {0} throws a {1},{2}", AIColour + 1, dice.dice1, dice.dice2);
                Console.WriteLine("Player {0} Cant Move. Missed Turn.", AIColour + 1);
                if (Speedrun == 0)
                {
                    Console.WriteLine("Press any key for next move");
                    Console.ReadKey();
                    Console.WriteLine("");
                }
            }
            else
            {
                //This looks through the list picking the highest scoring move
                //Score is a minus number so if the player is behind it can still do a move
                int score = -400;
                int best = 0;
                int modifier1 = 4;
                int modifier2 = 3;

                for (int i = 0; i <= (TempBoardList.Count - 1); i++)
                {
                    int defBonus = 0;
                    int[,] TempBoard = TempBoardList[i];

                    for (int j = 1; j <= 24; j++)
                    {
                        if (TempBoard[j, AIColour] >= 2)
                        {
                            defBonus = defBonus + modifier1;
                        }
                        if (TempBoard[j, AIColour] == 1)
                        {
                            defBonus = defBonus - modifier2;
                        }
                    }

                    if ((TempBoard[27, 0] - TempBoard[27, 1] + defBonus) > score)
                    {
                        score = TempBoard[27, 0] - TempBoard[27, 1] + defBonus;
                        best = i;
                    }
                }

                //do best move
                board.SendLayout(TempBoardList[best]);
                board.RefreshBoard();
            }
        }

        //This AI mixes both aggressive and defensive modifiers together
        private void ComboAITurn(Dice dice, Board board, int Speedrun)
        {
            //Calls for dicethrow
            DiceRolled(dice, board);

            //Finds all possable moves with dicerolls
            int[,] DefaultBoard = new int[28, 2];
            List<int[,]> TempBoardList = new List<int[,]>();
            DefaultBoard = board.getLayout();

            if (AIColour == 1)
            {
                BlackMoveTree(DefaultBoard, TempBoardList);
            }
            else
            {
                WhiteMoveTree(DefaultBoard, TempBoardList);
            };


            //If Bar is blocked off message that AI cant move and goes to next player
            if (TempBoardList.Count < 1)
            {
                //tells us the Computer dice roll
                Console.WriteLine();
                Console.WriteLine("Player {0} throws a {1},{2}", AIColour + 1, dice.dice1, dice.dice2);
                Console.WriteLine("Player {0} Cant Move. Missed Turn.", AIColour + 1);
                if (Speedrun == 0)
                {
                    Console.WriteLine("Press any key for next move");
                    Console.ReadKey();
                    Console.WriteLine("");
                }
            }
            else
            {
                //This looks through the list picking the highest scoring move
                //Score is a minus number so if the player is behind it can still do a move
                int score = -400;
                int best = 0;
                int opp = 0;
                int modifier = 2;
                int modifier1 = 4;
                int modifier2 = 3;


                if (AIColour == 0)
                    opp = 1;
                else
                    opp = 0;

                for (int i = 0; i <= (TempBoardList.Count - 1); i++)
                {
                    int defBonus = 0;
                    int[,] TempBoard = TempBoardList[i];

                    for (int j = 1; j <= 24; j++)
                    {
                        if (TempBoard[j, AIColour] >= 2)
                        {
                            defBonus = defBonus + modifier1;
                        }
                        if (TempBoard[j, AIColour] == 1)
                        {
                            defBonus = defBonus - modifier2;
                        }
                    }

                    if ((TempBoard[27, 0] - TempBoard[27, 1] + defBonus + (TempBoard[26, opp] * modifier)) > score)
                    {
                        score = TempBoard[27, 0] - TempBoard[27, 1] + defBonus + (TempBoard[26, opp] * modifier);
                        best = i;
                    }
                }

                //do best move
                board.SendLayout(TempBoardList[best]);
                board.RefreshBoard();
            }
        }

        //Calls the fuctions nessercery to make a random AI aka Random AI main method
        public void AIMoves(Dice dice, Board board, int Colour, int AIType, int Speedrun)
        {
            AIColour = Colour;

            if (AIType == 0)
                RandAITurn(dice, board, Speedrun);
            else if (AIType == 1)
                ScoreAITurn(dice, board, Speedrun);
            else if (AIType == 2)
                AggAITurn(dice, board, Speedrun);
            else if (AIType == 3)
                DefAITurn(dice, board, Speedrun);
            else if (AIType == 4)
                ComboAITurn(dice, board, Speedrun);
        }
    }
}

