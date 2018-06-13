using System;

namespace TaylorBurchLab5
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			string[] deck = new string[52];
			string[] suite = { "Hearts", "Spades", "Diamonds", "Clubs" };
			string[] valueNum = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };

			bool play = getRunProgram(); //Ask user if they want to shuffle cards 
			while (play == true)
			{
				populateDeck(deck, suite, valueNum); //Populate the deck of cards in order
				Console.WriteLine("Here is a full deck of cards.----------------------------------");
				displayDeck(deck); //Show that deck of cards is all there to user
				shuffleDeck(deck); //Shuffle the cards using swap method 
				Console.WriteLine("Here is the shuffled deck. ------------------------------------");
				displayDeck(deck); //Display the shuffled deck
				play = getRunProgram(); //Ask user to shuffle another deck Y/N
			}
		}

		static bool getRunProgram()
		{
			char userInput = 'q';
			Console.WriteLine("Do you want to shuffle a deck? Enter Y for Yes or any other key to quit.");
			userInput = char.Parse(Console.ReadLine());
			if (userInput == 'y' || userInput == 'Y')
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		static void populateDeck(string[] deck, string[] suite, string[] valueNum)
		{
			int i = 0;
			for (int x = 0; x < suite.Length; x++) //For every suite out of the four total
			{
				for (int y = 0; y < valueNum.Length; y++) //And for every value possible
				{
					deck[i] = valueNum[y] + " of " + suite[x]; //Take the value and suite and put them together into deck
					i++;
				} //Repeated until all values added for each suite
			}
		}

		static void displayDeck(string[] deck)
		{
			for (int i = 0; i < deck.Length; i++)
			{
				Console.WriteLine(deck[i]); //Write out all of the deck array's length
			}
		}

		static void shuffleDeck(string[] deck)
		{
			string temp = null;
			Random num = new Random();
			int n = deck.Length;
			while (n > 1)
			{
				int k = num.Next(n);
				n--;
				temp = deck[n];
				deck[n] = deck[k];
				deck[k] = temp;
			}

		}
	}
}
