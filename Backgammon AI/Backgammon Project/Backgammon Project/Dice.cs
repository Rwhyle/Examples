using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backgammon_Project
{
    class Dice
    {
        private Random rand;
        public int dice1, dice2;
        //Initialises new Dice
        private void init()
        {
            rand = new Random();
        }

        //Rolls the dice to the next random number
        private void DiceRoll()
        {
            dice1 = rand.Next(1, 7);
            dice2 = rand.Next(1, 7);
        }

        //**********************************************************************************************

        //Creats a statistical analysis to see which dice rolls show up the most
        public void analysis(int count)
        {
            init();
            int[] diceRolls = new int[6];
            int[] doubleDiceRolls = new int[12];
            int[] sameNumberDice = new int[6];

            //Does dice rolls and adds them into the arrays
            for (int i = 0; i < count; i++)
            {
                DiceRoll();
                diceRolls[(dice1 - 1)]++;
                diceRolls[(dice2 - 1)]++;
                doubleDiceRolls[(dice1 + dice2 - 1)]++;

                if (dice1 == dice2)
                    sameNumberDice[(dice1 - 1)]++;
            }

            for (int i = 0; i < diceRolls.Length; i++)
            {
                Console.WriteLine("Dice number {0} rolled {1} times.", (i + 1), diceRolls[i]);
            }
            Console.WriteLine("");

            for (int i = 0; i < doubleDiceRolls.Length; i++)
            {
                Console.WriteLine("Dice number added up total {0} rolled {1} times.", (i + 1), doubleDiceRolls[i]);
            }
            Console.WriteLine("");

            for (int i = 0; i < sameNumberDice.Length; i++)
            {
                Console.WriteLine("Dice number {0} are the same on both dice {1} times.", (i + 1), sameNumberDice[i]);
            }
        }

        //**********************************************************************************************

        //Throw the dice
        public int[] diceThrow()
        {
            /*
             * dice roll
             * checks if dice are the same if so bool double will be true
             * returns numbers and bool back to player/ai
             * */

            //Initialises the dice and rolls  

            int[] diceRolls = new int[3];
            int diceDouble = 0;
            if (rand == null)
                init();

            DiceRoll();

            //check if the dice are the same aka a double
            if (dice1 == dice2)
                diceDouble = 1;

            //enters and returns the dice rolls into a array
            diceRolls[0] = dice1;
            diceRolls[1] = dice2;
            diceRolls[2] = diceDouble;
            return diceRolls;
        }

    }
}
