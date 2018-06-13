using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {

            //Variables
            int userScoreTotal = 0; //User's score against computer from total games played in a session
            int computerScoreTotal = 0; //Computer's score against user from total games played in a session
            string currentScoreTotal = ""; //Total running score from all matches played in session.
            string userName = ""; //User's name 
            char userChar; //User's initial input move of R, P, or S
            char userMove = 'K'; //Validate user input
            int computerNum = 0; //Number generated to determine computer's move
            char computerMove = 'K'; //Computer's move of Rock, Paper, or Scissors
            string gameWinner = ""; //Winner of the game played



            //Main flow of control
            userScoreTotal = 0;
            computerScoreTotal = 0;
            userName = getUserName(); //Get's user's name 
            getRulesExplained(); //Explain rules of the game to user
            bool playTime = getPlayTime(userName); //Does the user want to play? Y/N


            if (playTime == true)
            {
                while (playTime == true)
                {
                    userChar = getUserChar(); //Get User's initial input move of R, P, or S
                    userMove = getUserMove(userChar); // Get User's input converted to move of Rock, Paper, or Scissors
                    computerNum = getComputerNum(); //Get randomized number to determine computer's move
                    computerMove = getComputerMove(computerNum); //Get computer's move of Rock, Paper, or Scissors
                    gameWinner = getGameWinner(computerMove, userMove, userName); //Get the winner of the route 
                    //currentScoreTotal = getScoreUpdatedTotal(gameWinner, userName, userScoreTotal, computerScoreTotal); //Get running total score of all matches updated
                    //Console.WriteLine(currentScoreTotal);

                    if (string.Compare(gameWinner, userName, StringComparison.Ordinal) == 0)
                    {
                        userScoreTotal++;
                    }
                    else
                    {
                        computerScoreTotal++;
                    }

                        if (userScoreTotal > computerScoreTotal)
                        {
                            Console.WriteLine(userName + " is in the lead!");
                        }
                        else
                        {
                            Console.WriteLine("La Computadora is in the lead!");
                        }

                    Console.WriteLine("User Score: " + userScoreTotal);
                    Console.WriteLine("La Computadora: " + computerScoreTotal);

                    playTime = getPlayTime(userName); //Ask to play again.

                };
            }

            Console.WriteLine("See ya next time!");
            Console.ReadKey();

        }

        public static string getUserName()
        {
            //Get the user's name
            Console.WriteLine("Hi there, what is your name?");
            String userName = Console.ReadLine();
            return userName;
        }

        public static void getRulesExplained()
        {
            //Explain the rules of the game.
            Console.WriteLine("Would you like to play?");
            Console.WriteLine("To play, you will enter a move of Rock, Paper, or Scissors.");
            Console.WriteLine("Paper beats Rock, Rock beats Scissors, Scissors beats Paper!");
            Console.WriteLine("The person who wins the most rounds in a session wins!");
        }
        public static bool getPlayTime(string userName)
        {
            //Does the user want to play? Y/N
            string userPick = "";
            char playAnswer;

            Console.Write("Think you can beat me, " + userName + "? Enter Y for Yes or any other key to Quit.");
            userPick = Console.ReadLine();
            playAnswer = char.Parse(userPick);
            if (playAnswer == 'Y' || playAnswer == 'y')
            {
                return true;
                Console.WriteLine("Excellent, up for the challenge I see!");
            }
            else
            {
                return false;
                Console.WriteLine("Lame... See you next time.");
            }

        }

        public static char getUserChar()
        {
            //User's initial input move of R, P, or S

            string userInput = "";
            char userChar;

            Console.WriteLine("Please select your move. Enter R for Rock, P for Paper, or S for Scissors. Default is rock.");
            userInput = Console.ReadLine();
            userChar = char.Parse(userInput);

            return userChar;
        }

        public static char getUserMove(char userChar)
        {
            // Get User's character input and validate it

            char userMove = 'R';

            if (userChar == 'R' || userChar == 'r')
            {
                userMove = 'R';
            }

            if (userChar == 'P' || userChar == 'p')
            {
                userMove = 'P';
            }

            if (userChar == 'S' || userChar == 's')
            {
                userMove = 'S';
            }

            return userMove;
        }

        public static int getComputerNum()
        {
            //Get randomized number to determine computer's move
            Random rnd = new Random();
            int computerNum = rnd.Next(1, 4);
            return computerNum;
        }

        public static char getComputerMove(int computerNum)
        {
            //Computer's move of Rock, Paper, or Scissors

            char computerMove = 'K';

            switch (computerNum)
            {
                case 1:
                    computerMove = 'R';
                    break;
                case 2:
                    computerMove = 'P';
                    break;
                case 3:
                    computerMove = 'S';
                    break;
                default:
                    computerMove = 'R';
                    break;
            }

            return computerMove;
        }

        public static string getGameWinner(char computerMove, char userMove, string userName)
        {
            //Compare moves and determine the winner

            string gameWinner = "";

            if (userMove == computerMove)
            {
                if (computerMove == 'R')
                {
                    Console.WriteLine("You played Rock. I played Rock. It's a tie! We both rock...");
                }
                if (computerMove == 'R')
                {
                    Console.WriteLine("You played Scissors. I played Scissors. It's a tie. Hey cut it out...");
                }
                if (computerMove == 'S')
                {
                    Console.WriteLine("You played Scissors. I played Scissors. It's a tie. This is bull-sheet. You're going to paper this... Get it?");
                }
            }

            if (userMove == 'R' && computerMove == 'P')
            {
                Console.WriteLine("You played Rock. I played Paper. Paper covers Rock. I win!");
                gameWinner = "La Computadora";
            }

            if (userMove == 'P' && computerMove == 'S')
            {
                Console.WriteLine("You played Paper. I played Scissors. Scissors cut Paper. I win!");
                gameWinner = "La Computadora";
            }

            if (userMove == 'S' && computerMove == 'R')
            {
                Console.WriteLine("You played Scissors. I played Rock. Rock crushes Scissors. I win!");
                gameWinner = "La Computadora";
            }

            if (userMove == 'P' && computerMove == 'R')
            {
                Console.WriteLine("You played Paper. I played Rock. Paper covers Rock. You win, this time...");
                gameWinner = userName;
            }

            if (userMove == 'R' && computerMove == 'S')
            {
                Console.WriteLine("You played Rock. I played Scissors. Rock crushes Scissors. You win, this time...");
                gameWinner = userName;
            }

            if (userMove == 'S' && computerMove == 'P')
            {
                Console.WriteLine("You played Scissors. I played Paper. Scissors cut Paper. You win, this time...");
                gameWinner = userName;
            }

            return gameWinner;
        }

        //public static string getScoreUpdatedTotal(string gameWinner, string userName, int userScoreTotal, int computerScoreTotal)
        //{
        //    //Get the updated running score from all games played in the session
        //    string currentScoreTotal = "";

        //    if (string.Compare(gameWinner, userName, StringComparison.Ordinal) == 0)
        //    {
        //        userScoreTotal++;
        //    }
        //    else
        //    {
        //        computerScoreTotal++;
        //    }


        //    currentScoreTotal = "The current score is: " + userScoreTotal " to " + computerScoreTotal;

        //    if (userScoreTotal > computerScoreTotal)
        //    {
        //        Console.WriteLine(userName + " is in the lead!");
        //    }
        //    else
        //    {
        //        Console.WriteLine("La Computadora is in the lead!");
        //    }

        //    Console.WriteLine("User Score: " + userScoreTotal);
        //    Console.WriteLine("La Computadora: " + computerScoreTotal);

        }
    }

