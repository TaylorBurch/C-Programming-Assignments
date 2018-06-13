using System;

namespace TaylorBurch_Lab4
{
	class MainClass
	{
		public static void Main(string[] args)
		{

			//Variables
			int userCardType = 0; //User enter's number based on what card type they have. (ex: 1 = Visa)
			long userCardNum = 0; //User's full card number 
			long evenTotal = 0; //Total of all even numbers gotten from card number
			long oddTotal = 0; //Total of all odd numbers gotten from card number


			//Main Control

			bool cardEnterReady = getCardEnterReady(); //Ask user if there is a card number ready to enter

			while (cardEnterReady == true)
			{
				userCardType = getUserCardType(); //Get user's card type (Visa, MasterCard, etc.)
				userCardNum = getUserCardNum(); //Get user's full card number input
				getCardLengthValidated(userCardNum); //Validate if user's input is correct number of digits long
				getCardTypeValidated(userCardNum, userCardType); //Validate if the card is the correct type as the user stated
				evenTotal = getEvenTotal(userCardNum); //Double every other number right to left and add up all for total
				oddTotal = getOddTotal(userCardNum); //Get the odd number total from adding all odd numbers up
				getFinalValidation(oddTotal, evenTotal); //Add sum from Odd and Even totals and divide by 10 to determine valid or invalid credit card number
				cardEnterReady = getCardEnterReady(); //Ask user if there is another card ready for input
			}

		}

		public static bool getCardEnterReady()
		{
			char userInput = 'Y';
			Console.WriteLine("Would you like to enter a card number? Enter Y for Yes or any other key to Quit.");
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


		public static int getUserCardType()
		{
			int userCardType = 0;
			Console.WriteLine("Please select your card type.");
			Console.WriteLine("Enter 1 for Visa");
			Console.WriteLine("Enter 2 for MasterCard");
			Console.WriteLine("Enter 3 for American Express");
			Console.WriteLine("Enter 4 for Discover");
			userCardType = int.Parse(Console.ReadLine());

			if (userCardType > 4 || userCardType < 1) //Do this for incorrect input.
			{
				Console.WriteLine("Input not valid, try again.");
				while (userCardType > 4 || userCardType < 1)
				{
					Console.WriteLine("Please select your card type.");
					Console.WriteLine("Enter 1 for Visa");
					Console.WriteLine("Enter 2 for MasterCard");
					Console.WriteLine("Enter 3 for American Express");
					Console.WriteLine("Enter 4 for Discover");
					userCardType = int.Parse(Console.ReadLine());
				}
			}

			return userCardType;

		}

		public static long getUserCardNum()
		{
			long userCardNum = 0;
			Console.WriteLine("Please enter your full card number.");
			userCardNum = long.Parse(Console.ReadLine());
			return userCardNum;
		}

		public static void getCardLengthValidated(long userCardNum)
		{
			int count = 0;
			for (count = 0; count <= userCardNum; count++)
			{
				userCardNum = userCardNum / 10;
			}

			if (count > 16 || count < 12) //Do this if card is not valid length
			{
				while (count > 16 || count < 12)
				{
					Console.WriteLine("This input is invalid length, please enter again.");
					getUserCardNum();
					for (count = 0; count <= userCardNum; count++)
					{
						userCardNum = userCardNum / 10;
					}
				}
			}
		}

		public static void getCardTypeValidated(long userCardNum, int userCardType)
		{

			if (userCardType == 1) //Validate start number for Visa
			{
				while (userCardNum > 9)
				{
					userCardNum = userCardNum / 10;
				}
				if (userCardNum != 4)
				{
					Console.WriteLine("Sorry, this is not a valid entry. Please exit and try again.");
				}
			}

			if (userCardType == 2) //Validate start number for MasterCard
			{
				while (userCardNum > 9)
				{
					userCardNum = userCardNum / 10;
				}
				if (userCardNum != 5)
				{
					Console.WriteLine("Sorry, this is not a valid entry. Please exit and try again.");
				}
			}

			if (userCardType == 3) //Validate start number for American Express
			{
				while (userCardNum >= 37)
				{
					userCardNum = userCardNum / 10;
				}
				if (userCardNum != 37)
				{
					Console.WriteLine("Sorry, this is not a valid entry. Please exit and try again.");
				}
			}

			if (userCardType == 4) //Validate start number for Discover 
			{
				while (userCardNum > 9)
				{
					userCardNum = userCardNum / 10;
				}
				if (userCardNum != 6)
				{
					Console.WriteLine("Sorry, this is not a valid entry. Please exit and try again.");
				}
			}

		}

		public static long getEvenTotal(long userCardNum)
		{
			long evenTotal = 0;
			long evenNum = 0;

			while (userCardNum > 0)
			{
				evenNum = 0;
				userCardNum = userCardNum / 10;
				evenNum = userCardNum % 10;
				evenNum = evenNum * 2;
				if (evenNum > 9)
				{
					evenNum = (evenNum / 10) + (evenNum % 10);
				}

				evenTotal = evenTotal + evenNum;

			}

			return evenTotal;
		}

		public static long getOddTotal(long userCardNum)
		{
			long oddTotal = 0;
			long oddNum = 0;

			while (userCardNum > 0)
			{
				oddNum = 0;
				oddNum = userCardNum % 10;
				userCardNum = userCardNum / 10;
				oddTotal = oddTotal + oddNum;
			}

			return oddTotal;
		}

		static public void getFinalValidation(long oddTotal, long evenTotal)
		{
			long sumTotal = 0;

			sumTotal = oddTotal + evenTotal;

			if (sumTotal % 10 == 0)
			{
				Console.WriteLine("This card is valid.");
			}
			else
			{
				Console.WriteLine("This card is invalid.");
			}
		}

		}
	}

