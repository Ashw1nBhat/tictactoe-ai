using System;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            //initializing the grid
            string[,] grid = new string[3, 3]
                {
                 { "   ", "   ", "   " },
                 { "   ", "   ", "   " },
                 { "   ", "   ", "   " }
                };

            tClass t = new tClass();

            int count = 0; //count variable for keeping track of the player turns

            //infinite for loop until the game ends
            for (; ; )
            {
                //printing grid
                t.printGrid(grid);

                //winner un-decided
                if (!t.isWinner(grid) && count != 9)
                {
                    //player turn
                    if (count % 2 == 0)
                    {
                        t.takeInput(grid);
                        count++;
                    }
                    //computer's turn
                    else if (count % 2 != 0)
                    {
                        t.aiOpponent(grid);
                        count++;
                    }
                }
                else if (!t.isWinner(grid) && count == 9)  //all grid-spaces filled
                {
                    Console.WriteLine("It is a draw!");
                    return;
                }
                //deciding winners
                else if (count % 2 == 0)
                {
                    Console.WriteLine("The winner is O!");
                    return;
                }
                else if (count % 2 != 0)
                {
                    Console.WriteLine("The winner is X!");
                    return;
                }


            }
        }
    }
}
