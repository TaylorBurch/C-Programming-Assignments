using System;
using System.IO;

namespace TaylorBurch_PA3Game
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			//Variables
			int totalEP = 0; //Total energy points of child during the game
			int gameEP = 0; //Energy points lost or gained from game played
			int gameSelection = 0; //Select number on main menu for what game option user wants
			int childWinCount = 0; //Number of games child has won in that session of play
			int sitterWinCount = 0; //Number of games babysitter has won in that session of play

			//Main for Game Control
			displayWelcome(); //Display welcome page for the game
			bool playTime = getPlayTime(); //Ask user if they want to play Y/N
			while (playTime == true)
			{

				totalEP = getPreviousGameSave(); //Ask user if they want to load last save or start new
				childWinCount = 0;
				sitterWinCount = 0;

				while (totalEP < 300 || totalEP > 0)
				{
					gameEP = 0;
					saveScore(totalEP); //Save the score total
					gameSelection = displayMainMenu(); //Display the main menu for game selection
					if (gameSelection == 1)
					{
						displaySticksRules(); //Display rules of the Pick Up Sticks game
						bool playGame = true; //Play this particular game? Y/N
						while (playGame == true)
						{
							gameEP = playSticksGame(); //Play the Pick Up Sticks Game
							if (gameEP > 0)
							{
								childWinCount++;
							}
							else
							{
								sitterWinCount++;
							}
							totalEP = totalEP + gameEP; //Update new total energy points after game play.
							getScoreBoard(totalEP, childWinCount, sitterWinCount); //Update the game score board
							playGame = getReplayGame(); //Ask user if they want to play again or return to menu
						}
					}
					else if (gameSelection == 2)
					{
						displayMotherRules(); //Display rules of the Mother May I? game
						bool playGame = true; //Play this particular game? Y/N
						while (playGame == true)
						{
							gameEP = playMotherGame(); //Play the Mother May I? game
							if (gameEP > 0)
							{
								childWinCount++;
							}
							else
							{
								sitterWinCount++;
							}
							totalEP = totalEP + gameEP; //Update new total energy points after game play.
							getScoreBoard(totalEP, childWinCount, sitterWinCount); //Update the game score board
							playGame = getReplayGame(); //Ask user if they want to play again or return to menu
						}
					}
					else if (gameSelection == 3)
					{
						displayRiddlesRules(); //Display rules of the Hangman game
						bool playGame = true; //Play this particular game? Y/N
						while (playGame == true)
						{
							gameEP = playRiddles(); //Play the Hangman game
							if (gameEP > 0)
							{
								childWinCount++;
							}
							else
							{
								sitterWinCount++;
							}
							totalEP = totalEP + gameEP; //Update new total energy points after game play.
							getScoreBoard(totalEP, childWinCount, sitterWinCount); //Update the game score board
							playGame = getReplayGame(); //Ask user if they want to play again or return to menu
						}
					}
					else if (gameSelection == 4)
					{
						totalEP = 200;
						Console.WriteLine("The Total Energy Points has been reset. You have returned to 200 Energy Points.");
					}
					else if (gameSelection == 5)
					{
						saveScore(totalEP);
						Console.WriteLine("Your score has been saved. Press any key to exit.");
						Console.ReadKey();
						break;
					}
					else
					{
						Console.WriteLine("Sorry, your input was invalid. Please try again.");
					}
				}

				if (totalEP >= 300)
				{
					Console.WriteLine("The children won, you failed your babysitting duties...");
				}
				else if (totalEP <= 0)
				{
					Console.WriteLine("You won, you are a sucessful babysitter! The children are fast asleep!");
				}
				else
				{
					Console.WriteLine("Thanks for playing, see you when you get back!");
				}

				playTime = getPlayTime(); //Ask user if they want to play

			}

			Console.WriteLine("Goodbye.");
			Console.ReadKey();
		}

		//Methods for Game Play ------

		public static void saveScore(int totalEP) //Save score to file ... ** I looked up how to do this online and did a lot of research on it so my code may look similar to sources online but I learned a lot about the concept through trying to implement it.
		{
			FileStream F = new FileStream("score.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
			F.WriteByte((byte)totalEP);
			F.Close();
		}

		public static void displayWelcome() //Display welcome page for the game
		{
			Console.WriteLine("Welcome babysitter! It is time to put the kids to bed!");
			Console.WriteLine("Unfortunately, they are not going down without a fight...");
			Console.WriteLine("Your goal tonight is to get the kids to bed before the parents get home.");
			Console.WriteLine("If you fail, you will have to explain to angry parents why their kids are still up running wild past bed time!");
			Console.WriteLine("To win, play games against the children to tire them out. The children will start with 200 Energy Points.");
			Console.WriteLine("Get their energy points to 0 and you win! The children go to bed on time!");
			Console.WriteLine("However, if the children win games and their energy points increase past 300, they will be up past bed time and you lose...");
		}

		public static bool getPlayTime() //Ask user if they want to play Y/N
		{
			char userInput = 'n';
			Console.WriteLine("Would you like to play? Enter Y for Yes, or any other key to Quit.");
			userInput = char.Parse(Console.ReadLine());
			if (userInput == 'Y' || userInput == 'y')
			{
				return true;
			}
			else
			{
				return false;
			}

		}

		public static int getPreviousGameSave() //Ask user if they want to load last save or start new
		{
			char userInput = 'n';
			int totalEP = 0;
			Console.WriteLine("Would you like to continue from a previous save, or start new? Enter Y for Yes.");
			Console.WriteLine("If there is no previous game saved, you will be defaulted to a new game.");
			userInput = char.Parse(Console.ReadLine());
			if (userInput == 'y' || userInput == 'Y')
			{
				FileStream F = new FileStream("score.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
				totalEP = F.ReadByte();
				Console.WriteLine("Total energy points from previous save: " + totalEP);
				return totalEP;
			}
			else
			{
				totalEP = 200;
				Console.WriteLine("Starting total energy points: " + totalEP);
				return totalEP;
			}
		}

		public static int displayMainMenu() //Display the main menu for game selection
		{
			int gameSelection = 0;
			Console.WriteLine("Please select one of the following options by inputing the selection number.");
			Console.WriteLine("Input 1 to play Pick Up Sticks");
			Console.WriteLine("Input 2 to play Mother May I");
			Console.WriteLine("Input 3 to play Riddles");
			Console.WriteLine("Input 4 to Restart the game and return to 200 EP");
			Console.WriteLine("Input 5 to Quit the game completely");

			gameSelection = int.Parse(Console.ReadLine());
			return gameSelection;
		}

		public static void displaySticksRules() //Display rules for Pick Up Sticks game
		{
			Console.WriteLine("Welcome to Pick Up Sticks! Here is how you play!");
			Console.WriteLine("There is a pile of sticks on the ground.");
			Console.WriteLine("You, the babysitter, will take turns with the children to pick up sticks.");
			Console.WriteLine("You may choose between picking up 1, 2, or 3 sticks per turn.");
			Console.WriteLine("You will take away 1 Energy Point from the children per stick picked up.");
			Console.WriteLine("However, be careful! If you are the last one to pick up a stick, you lose and you get no points!");
		}

		public static int playSticksGame() //Play through the Pick Up Sticks game
		{
			int gameEP = 0;
			int sitterPoints = 0;
			int childPoints = 0;
			int numSticks = 0;
			int numPickedUp = 0;
			Console.WriteLine("How many sticks would you like to start with? Please select a number between 20 and 100.");
			numSticks = int.Parse(Console.ReadLine());
				while (numSticks < 20 || numSticks > 100)
				{
					Console.WriteLine("This is not a valid number of sticks, please try again.");
					Console.WriteLine("How many sticks would you like to start with? Please select a number between 20 and 100.");
					numSticks = int.Parse(Console.ReadLine());
				}
			while (numSticks > 0)
			{
				Console.WriteLine("Please enter how many sticks you wish to pick up. You have to pick up at least 1 but no more than 3.");
				numPickedUp = int.Parse(Console.ReadLine());
				switch (numPickedUp)
				{
					case 1:
						numSticks = numSticks - 1;
						sitterPoints = sitterPoints + 1;
						Console.WriteLine("You picked up 1 stick(s).");
						break;
					case 2:
						numSticks = numSticks - 2;
						sitterPoints = sitterPoints + 2;
						Console.WriteLine("You picked up 2 stick(s).");
						break;
					case 3:
						numSticks = numSticks - 3;
						sitterPoints = sitterPoints + 3;
						Console.WriteLine("You picked up 3 stick(s).");
						break;
					default:
						numSticks = numSticks - 1;
						sitterPoints = sitterPoints + 1;
						Console.WriteLine("You picked up 1 stick(s).");
						break;
				}

				if (numSticks <= 0)
				{
					Console.WriteLine("You busted. The children win!");
					gameEP = childPoints;
				}
				else  
				{
					Random rnd = new Random();
					int randomNum = rnd.Next(1, 4);
					numSticks = numSticks - randomNum;
					childPoints = childPoints + randomNum;
					Console.WriteLine("The computer picked up " + randomNum + " sticks.");

					if (numSticks <= 0)
					{
						Console.WriteLine("The children busted, you win!!");
						gameEP = -(sitterPoints);
					}
				}
							
				}

				return gameEP;
			}


		public static void displayMotherRules()
		{
			Console.WriteLine("Welcome to the Mother May I? game! The object of the game is to get as close to Mother as possible");
			Console.WriteLine("without going past her and busting! Go past Mother and she will catch you and you will lose!"); 
			Console.WriteLine("The player to get the closest to Mother wins! To take steps, you will first roll two 10-sided dice in a row.");
			Console.WriteLine("This will give you your starting total steps. Then it is up to you to take more. You have the option of rolling a third");
			Console.WriteLine("6 sided dye to determine how many more steps you take. You can roll this dye as many times as you wish. A point will");
			Console.WriteLine("be given for every step a player takes. However, if you bust, you lose 21 points regardless if the other player busts too!");
			Console.WriteLine("So be careful, and good luck!");
		}

		public static int playMotherGame() //Play the Mother May I? game
		{
			int gameEP = 0;
			int childScore = 0;
			int sitterScore = 0;
			int sitterTotal = 0;
			int childTotal = 0;
			char rollThirdDice = 'n';
				
			Console.WriteLine("Press any key to roll the first ten-sided dice.");
			Console.ReadKey();
			Random rollRand = new Random();
			int sitterFirstRoll = rollRand.Next(1, 11);
			Console.WriteLine("Your first dye rolled: " + sitterFirstRoll);

			Console.WriteLine("Press any key to roll the second ten-sided dice.");
			Console.ReadKey();
			int sitterSecondRoll = rollRand.Next(1, 11);
			Console.WriteLine("Your second dye rolled: " + sitterSecondRoll);

			sitterTotal = sitterFirstRoll + sitterSecondRoll;
			Console.WriteLine("Your total role is: " + sitterTotal);

			Console.WriteLine("Would you like to roll the third dice (6 sided)? Enter Y if Yes or any other key for No.");
			rollThirdDice = char.Parse(Console.ReadLine());
			if (rollThirdDice == 'y' || rollThirdDice == 'Y')
			{
				while (rollThirdDice == 'y' || rollThirdDice == 'Y')
				{
					Random finalDice = new Random();
					int sitterThirdRoll = finalDice.Next(1, 7);
					Console.WriteLine("Your third dye rolled: " + sitterThirdRoll);
					sitterTotal = sitterTotal + sitterThirdRoll;
					Console.WriteLine("Your total role is now: " + sitterTotal);
					if (sitterTotal > 21)
					{
						Console.WriteLine("You're busted! You went too far and Mother caught you.");
						childScore = 17;
						sitterScore = 0;
						break;

					}
					else
					{
						Console.WriteLine("Would you like to roll again?");
						rollThirdDice = char.Parse(Console.ReadLine());
					}
				}
			}
			if (sitterTotal > 21)
			{
				Console.WriteLine("The sitter busted already so the automatically children win and therefore will not roll.");
			}
			else
			{
				Console.WriteLine("Okay. Now the children will roll the dice...");
				Random rnd = new Random();

				int childFirstRoll = rnd.Next(1, 11);
				Console.WriteLine("The children's first dye rolled: " + childFirstRoll);
				Console.ReadKey();

				int childSecondRoll = rnd.Next(1, 11);
				Console.WriteLine("The children's second dye rolled: " + childSecondRoll);
				Console.ReadKey();

				childTotal = childFirstRoll + childSecondRoll;
				Console.WriteLine("The children's total roll is now: " + childTotal);
				Console.ReadKey();

					if (childTotal <= 17)
					{
						Console.WriteLine("The children have decided to roll the third (6-sided) dice!");
						while (childTotal <= 17)
						{
							int childThirdRoll = rnd.Next(1, 7);
							Console.WriteLine("The children's third dye rolled: " + childThirdRoll);
							childTotal = childTotal + childThirdRoll;
							Console.WriteLine("The children's total roll is now: " + childTotal);
							Console.ReadKey();

							int rollThirdAgain = rnd.Next(1, 3);
							if (childTotal < 19 && rollThirdAgain == 1)
							{
								Console.WriteLine("The children have decided to roll the third dice again!");
								childThirdRoll = rnd.Next(1, 7);
								Console.WriteLine("The children's third dye rolled: " + childThirdRoll);
								childTotal = childTotal + childThirdRoll;
								Console.WriteLine("The children's total roll is now: " + childTotal);
								Console.ReadKey();
							}

						}
					}

				if (childTotal > 21)
				{
					Console.WriteLine("The children busted, they went too far!");
					sitterScore = -(sitterTotal);
				}
				else if (childTotal > sitterTotal)
				{
					Console.WriteLine("The sitter took a total of: " + sitterTotal + " steps.");
					Console.WriteLine("The children took a total of: " + childTotal + " steps.");
					Console.WriteLine("The children got closer to Mother than the babysitter.");
					Console.WriteLine("The children win!");
					childScore = childTotal;
					sitterScore = 0;
				}
				else if (sitterTotal > childTotal)
				{
					Console.WriteLine("The sitter took a total of: " + sitterTotal + " steps.");
					Console.WriteLine("The children took a total of: " + childTotal + " steps.");
					Console.WriteLine("The babysitter got closer to Mother! The babysitter wins!");
					sitterScore = sitterTotal;
					childScore = 0;
				}
				else
				{
					Console.WriteLine("There has been a tie!");
					if (sitterTotal == 21 && childTotal == 21)
					{
						Console.WriteLine("Both children and the babysitter got exactly 21 steps to mom! What a tie!");
						Console.WriteLine("The children want to give this win to the babysitter for being pretty cool. Congrats babysitter!");
						sitterScore = -21;
						childScore = 0;
					}
					else
					{
						Console.WriteLine("There has been a tie! You both took the same amount of steps!");
						Console.WriteLine("Because you are a nice babysitter, you'll let the children have the win this time...");
						childScore = childTotal;
						sitterScore = 0;
					}
				}

				}
				gameEP = -(sitterScore) + childScore;
				Console.WriteLine("The total EP gained/lost from this game is: " + gameEP);
				return gameEP;
		}

		public static void displayRiddlesRules() //Display the rules for the Riddles Game.
		{
			Console.WriteLine("Let's play a game of riddles! To start, the children will say a riddle.");
			Console.WriteLine("The babysitter (you) will then guess the answer! Simple as that!");
			Console.WriteLine("The babysitter will get three guesses per riddle. If you can't guess the riddle in 3 tries, the children win.");
			Console.WriteLine("But guess correctly, and you win!");
			Console.WriteLine("IMPORTANT: All riddle answers are only ONE word! Guess the one word answer to the riddle to win!");
			Console.WriteLine("The winner of the round will recieve 20 Energy Points!");
			Console.WriteLine("Good luck!");
			Console.ReadKey();
		}

		public static int playRiddles() //Play the Riddles game. 
		{
			string[] riddles = new string [11];
			string[] answers = new string[11];
			int attempts = 0;
			int gameEP = 0;


			//Riddles came from http://www.funology.com/riddles/
			riddles[0] = "I’m tall when I’m young and I’m short when I’m old. What am I?";
			riddles[1] = "What comes down but never goes up?";
			riddles[2] = "Poor people have it.Rich people need it. If you eat it you die. What is it?";
			riddles[3] = "If I drink, I die. If i eat, I am fine. What am I?";
			riddles[4] = "What word becomes shorter when you add two letters to it?";
			riddles[5] = "If I have it, I don’t share it. If I share it, I don’t have it. What is it?";
			riddles[6] = "Take away my first letter, and I still sound the same. Take away my last letter, I still sound the same. Even take away my letter in the middle, I will still sound the same. I am a five letter word. What am I?";
			riddles[7] = "What has hands but can not clap?";
			riddles[8] = "What is so delicate that saying its name breaks it?";
			riddles[9] = "If a blue house is made out of blue bricks, a yellow house is made out of yellow bricks and a pink house is made out of pink bricks, what is a green house made of?";
			riddles[10] = "They come out at night without being called, and are lost in the day without being stolen. What are they?";

			answers[0] = "candle";
			answers[1] = "rain";
			answers[2] = "nothing";
			answers[3] = "fire";
			answers[4] = "short";
			answers[5] = "secret";
			answers[6] = "empty";
			answers[7] = "clock";
			answers[8] = "silence";
			answers[9] = "glass";
			answers[10] = "stars";

			Random rnd = new Random();
			int riddleSelector = rnd.Next(0, 11);
			int makeMoreRandom = rnd.Next(0, 11);

			riddleSelector = (riddleSelector * makeMoreRandom) / 10; 

			Console.WriteLine("Here is your riddle! Remember you get three guesses! Also remember to eliminate any extra words like A or The from your answer. You answer should be one single word.");
			Console.WriteLine(riddles[riddleSelector]);

			while (attempts < 3)
			{
				Console.WriteLine("Please input your one word answer.");
				string userAnswer = Console.ReadLine();
				userAnswer.ToLower();

				if (userAnswer == answers[riddleSelector])
				{
					Console.WriteLine("Congrats, that is correct! You win, babysitter!");
					attempts = 4;
				}
				else
				{
					Console.WriteLine("Sorry, that is an incorrect guess...");
					attempts++;
					Console.WriteLine("You have used " + attempts + "  attempts.");
				}
			}

			if (attempts == 3)
			{
				Console.WriteLine("Sorry, you have used all your attempts. The children win this round.");
				Console.WriteLine("The correct answer to the riddle was: " + answers[riddleSelector]);
				gameEP = 20;
			}
			else
			{
				gameEP = (-20);
			}
				return gameEP;
		}

		public static void getScoreBoard(int totalEP, int childWinCount, int sitterWinCount) //Update the scoreboard after each game
		{
			Console.WriteLine("Games babysitter has won " + sitterWinCount + " total games.");
			Console.WriteLine("The children have won " + childWinCount + " total games.");
			Console.WriteLine("The current total Energy Points is: " + totalEP);
			Console.ReadKey();
		}

		public static bool getReplayGame() //Ask user if they want to play the game again or return to menu
		{
			char userInput = 'n';
			Console.WriteLine("Would you like to play this game again?");
			Console.WriteLine("If Yes, enter Y. If not, press any key to return to the main menu.");
			userInput = char.Parse(Console.ReadLine());
			if (userInput == 'Y' || userInput == 'y')
			{
				return true;
			}
			else
			{
				return false;
			}
		}

	}
}

