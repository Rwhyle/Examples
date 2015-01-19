using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Backgammon_Project
{
    class Program
    {
        private static int white = 0;
        private static int black = 0;
        private static int player = 3;
        private static int AIType1 = 0; //0=random, 1=score, 2=aggressive, 3=defensive, 4=combo
        private static int AIType2 = 0; //0=random, 1=score, 2=aggressive, 3=defensive, 4=combo
        private static int Games = 1; //Number of games played
        private static int GamesOn = 0; //Start Game
        private static int Speedrun = 0;//whether to have contoll between games 0=on 1=off

        public void menu()
        {
            //sets console details
            Console.Clear();
            Console.Title = "Backgammon";
            Console.SetWindowSize(120, 50);
            Console.BufferHeight = 50;
            Console.BufferWidth = 120;

            string SpeedrunIO;
            if (Speedrun == 0)
                SpeedrunIO = "OFF";
            else
                SpeedrunIO = "ON";

            Console.WriteLine();
            Console.WriteLine("Welcome To Backgammon 1.0");
            Console.WriteLine("Final Year Project: Intelligent Backgammon Opponent");
            Console.WriteLine("By Richard Whyle, Student ID:05110994");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Main Menu");
            Console.WriteLine();
            Console.WriteLine("[1] For New Game");
            Console.WriteLine("[2] For Opponent Types");
            Console.WriteLine("[3] For Speed Run: {0}", SpeedrunIO);
            Console.WriteLine("[4] For Game amount: {0}", Games);
            Console.WriteLine("[5] Dice Analysis");
            //Console.WriteLine("[6] Player Controll: {0}, 0 = White, 1 = Black, 3 = Not Playing",player);
            Console.WriteLine();

            int read = int.Parse(Console.ReadLine());
            if (read == 1)
            {
                white = 0;
                black = 0;
                GamesOn = 1;
                Main();
            }
            if (read == 2)
            {
                while (read != 3)
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("Select Oppont Type");
                    Console.WriteLine("0=random, 1=score, 2=aggressive, 3=defensive, 4=combo");
                    Console.WriteLine("[1] Opponent 1 White: {0}", AIType1);
                    Console.WriteLine("[2] Opponent 2 Black: {0}", AIType2);
                    Console.WriteLine("[3] Return to Menu");
                    Console.WriteLine();
                    read = int.Parse(Console.ReadLine());
                    if (read == 1)
                    {
                        if (AIType1 == 4)
                            AIType1 = 0;
                        else
                            AIType1++;
                    }

                    if (read == 2)
                    {
                        if (AIType2 == 4)
                            AIType2 = 0;
                        else
                            AIType2++;
                    }
                }

                if (read == 3)
                    menu();
            }
            if (read == 3)
            {
                if (Speedrun == 1)
                {
                    Speedrun = 0;
                    SpeedrunIO = "OFF";
                }
                else
                {
                    Speedrun++;
                    SpeedrunIO = "ON";
                }
                menu();
            }
            if (read == 4)
            {
                Games = 0;
                Console.WriteLine("Enter Number of games you want played:");
                Games = int.Parse(Console.ReadLine());
                menu();
            }
            if (read == 5)
            {
                Dice dice = new Dice();
                dice.analysis(1000000);
                Console.WriteLine("Press Any Key to Return to Menu");
                Console.ReadKey();
                menu();
            }
            /* if (read == 6)
            {
                if (player == 3)
                {
                    player = 0;
                }
                else
                {
                    player++;
                }
                menu();
            }
            */
        }
        static void Main()
        {
            Program program = new Program();
            if(GamesOn == 0)
            { 
                program.menu();//(AIType1, AIType2, Games, Speedrun);
            }



            if (GamesOn == 1)
            {
                for (int i = 0; i < Games; i++)
                {
                    Board board = new Board();
                    Dice dice = new Dice();
                    AI ai = new AI();
                    Player playing = new Player();
                    board.NewBoard();


                    while (board.getPip(0) < 375 && board.getPip(1) > 0)
                    {
                        //Player 1 White
                        /*
                        if (player == 0)
                            playing.PlayerGo(dice, board, player, 1);
                        else
                        {
                        */
                            ai.AIMoves(dice, board, 0, AIType1, Speedrun);


                            
                            //tells us the Computer dice roll
                            Console.WriteLine();
                            Console.WriteLine("Player 1 throws a {0},{1}", dice.dice1, dice.dice2);

                            if (Speedrun == 0)
                            {
                                Console.WriteLine("Press any key for next move");
                                Console.ReadKey();
                                Console.WriteLine("");
                            }
                        //}

                        //Player 2 Black
                        /*if (player == 1)
                            playing.PlayerGo(dice, board, player, 0);
                        else
                        {
                        */
                            if (board.getPip(0) < 375)
                            {
                                ai.AIMoves(dice, board, 1, AIType2, Speedrun);

                                Console.WriteLine("");
                                Console.WriteLine("Player 2 throws a {0},{1}", dice.dice1, dice.dice2);

                                if (Speedrun == 0)
                                {
                                    Console.WriteLine("Press any key for next move");
                                    Console.ReadKey();
                                    Console.WriteLine("");
                                }
                            }
                    }
                    if (board.getPip(0) == 375)
                    {
                        Console.WriteLine("White Player Wins, press any key to finish");
                        white++;
                        if (Speedrun == 0)
                        {
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Black Player Wins, press any key to finish");
                        black++;
                        if (Speedrun == 0)
                        {
                            Console.ReadKey();
                        }
                    }
                    
                }
                Console.WriteLine("Games Won: White Player {0}, Black Player {1}", white, black);
                Console.WriteLine("Press any key to end game", white, black);
                Console.ReadKey();
                GamesOn = 0;
                program.menu();//(AIType1, AIType2, Games, Speedrun);
            }
        }
    }
}
