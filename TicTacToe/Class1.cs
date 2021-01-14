using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    class tClass
    {
        //checks for empty grid-spaces
        public Boolean isTurns(string[,] g)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (g[i, j] == "   ")
                        return true;
            return false;
        }

        //takes the player input
        public void takeInput(string[,] g)
        {
            try
            {
                try
                {
                    Console.WriteLine("Enter the row index");
                    string m = Console.ReadLine();
                    int x = Convert.ToInt32(m);
                    Console.WriteLine("Enter the column index");
                    string n = Console.ReadLine();
                    int y = Convert.ToInt32(n);
                    if (g[x, y] == "   ")
                        g[x, y] = " X ";
                    else
                    {
                        Console.WriteLine("Invalid input, retry!");
                        takeInput(g);
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Index out of bounds. Try again!");
                    takeInput(g);
                }
            }

            catch (FormatException)
            {
                Console.WriteLine("Invalid index, Try again!");
                takeInput(g);
            }
        }

        //prints the grid
        public void printGrid(string[,] g)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (j == 2)
                        Console.WriteLine(g[i, j]);
                    else
                        Console.Write(g[i, j] + "|");
                }
                if (i == 2)
                    Console.Write("\n");
                else
                    Console.WriteLine("---+---+---");
            }
        }

        //returns true if there is a winner
        public Boolean isWinner(string[,] g)
        {
            //checking the rows for winner
            for (int i = 0; i < 3; i++)
            {
                if (g[i, 0] != "   " && g[i, 0] == g[i, 1] && g[i, 1] == g[i, 2])
                {

                    return true;
                }
            }

            //checking columns for winner
            for (int i = 0; i < 3; i++)
            {
                if (g[0, i] != "   " && g[0, i] == g[1, i] && g[1, i] == g[2, i])
                {
                    return true;
                }
            }

            //first diagonal
            if (g[0, 0] != "   " && g[0, 0] == g[1, 1] && g[1, 1] == g[2, 2])
            {
                return true;
            }
            //second diagonal
            if (g[0, 2] != "   " && g[0, 2] == g[1, 1] && g[1, 1] == g[2, 0])
            {
                return true;
            }

            return false;
        }

        //gives the value of the grid configuration depending on the winner (minimizing or maximizing)
        public int gridValue(string[,] g, int depth)
        {

            //returns score when there is a winner by matching row
            for (int i = 0; i < 3; i++)
            {
                if (g[i, 0] == g[i, 1] && g[i, 1] == g[i, 2])
                {
                    if (g[i, 0] == " O ")
                        return 10 - depth;
                    else if (g[i, 0] == " X ")
                        return -10 + depth;
                }
            }

            //returns score when there is a winner by matching column
            for (int i = 0; i < 3; i++)
            {
                if (g[0, i] == g[1, i] &&
                    g[1, i] == g[2, i])
                {
                    if (g[0, i] == " O ")
                        return 10 - depth;

                    else if (g[0, i] == " X ")
                        return -10 + depth;
                }
            }

            //returns score when there is a winner by matching first diagonal
            if (g[0, 0] == g[1, 1] && g[1, 1] == g[2, 2])
            {
                if (g[0, 0] == " O ")
                    return 10 - depth;
                else if (g[0, 0] == " X ")
                    return -10 + depth;
            }

            //returns score when there is a winner by matching second diagonal
            if (g[0, 2] == g[1, 1] && g[1, 1] == g[2, 0])
            {
                if (g[0, 2] == " O ")
                    return 10 - depth;
                else if (g[0, 2] == " X ")
                    return -10 + depth;
            }

            return 0;
        }

        //minimax method with alpha beta pruning
        public int minimax(string[,] g, int depth, int alpha, int beta, Boolean isMax)
        {
            int currVal = gridValue(g, depth);


            if (currVal != 0)
            {
                return currVal;
            }

            if (!isTurns(g))
                return 0;

            //maximizing player makes move for getting the most positive score
            if (isMax)
            {
                int best = -1000;


                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {

                        if (g[i, j] == "   ")
                        {
                            //setting grid-space
                            g[i, j] = " O ";

                            best = Math.Max(best, minimax(g, depth + 1, alpha, beta, !isMax));
                            alpha = Math.Max(alpha, best);
                            //undoing move
                            g[i, j] = "   ";

                            if (beta <= alpha)
                                break;
                        }
                    }
                }
                return best;
            }



            //minimizing player makes move for getting the most negative score

            else
            {
                int best = 1000;


                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {

                        if (g[i, j] == "   ")
                        {

                            g[i, j] = " X ";

                            best = Math.Min(best, minimax(g, depth + 1, alpha, beta, !isMax));
                            beta = Math.Min(best, beta);

                            g[i, j] = "   ";

                            if (beta <= alpha)
                                break;
                        }
                    }
                }
                return best;
            }
        }


        //move of the computer opponent (maximizing player)
        public void aiOpponent(string[,] g)
        {
            int best = -1000, m = -1, n = -1;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {

                    if (g[i, j] == "   ")
                    {

                        g[i, j] = " O ";

                        int Currmove = minimax(g, 0, -1000, 1000, false);

                        g[i, j] = "   ";

                        if (Currmove > best)
                        {
                            m = i;
                            n = j;
                            best = Currmove;
                        }
                    }
                }
            }
            g[m, n] = " O ";
        }
    }
}
