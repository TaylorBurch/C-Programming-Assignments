using System;
using System.IO;

namespace TaylorBurchPA5
{
	class Report
	{
		public static void individualCustomerReport(Transaction[] transactions, int transCount)
		{
			Console.WriteLine("Please enter the email address of the customer you would like a report on.");
			string inputEmail = Console.ReadLine(); //Email address the user inputs to look up customer
			string userEmail = ""; //Email address of customer pulled from transactions array.
			string[] customerReport = new string[500]; //Array of rentals for specific customer found by email. Used to display all listings once found for that customer.
			int count = 0; //Count for number of found rentings for that specific customer.
			string foundListing = ""; //String of rental information for a single rental found for that customer.

			for (int x = 0; x < transCount; x++) //Loop through array to find all transactions that match.
			{
				userEmail = transactions[x].getRenterEmail();
				if (userEmail == inputEmail)
				{
					foundListing = transactions[x].toString();
					customerReport[count] = foundListing;
					count++;
				}
			}

			for (int y = 0; y <= count; y++)
			{
				Console.WriteLine(customerReport[y]);
				Console.WriteLine("");
			}

			Console.WriteLine("Would you like to save this report to a new file? Enter Y for Yes or any key to return to main menu.");
			char userInput = char.Parse(Console.ReadLine());
			if (userInput == 'y'|| userInput == 'Y')
			{
				Console.WriteLine("Please enter a name for this new file. Remember to include the .txt extension.");
				string fileName = Console.ReadLine();
				System.IO.File.WriteAllLines(fileName, customerReport);
				Console.WriteLine("File sucessfully saved.");
			}

			Console.ReadKey();
		}

		public static void historicalCustomerReports(Transaction[] transactions, int transCount)
		{
			int minIndex;
			string customer1 = ""; //String used to compare customers in the array to sort them
			string customer2 = ""; //Second compare string for customer sort

			for (int x = 0; x < transCount - 1; x++) //Sort by customer
			{
				minIndex = x;
				for (int i = x + 1; i < transCount; i++)
				{
					customer1 = transactions[minIndex].getRenterName();
					customer2 = transactions[i].getRenterName();

					if (customer2.CompareTo(customer1) < 0)
					{
						minIndex = i;
					}
				}

				if (minIndex != x)
				{
					swapArray(transactions, x, minIndex);
				}
			}

			DateTime t1 = DateTime.Now; //DateTime of Transaction at minIndex
			DateTime t2 = DateTime.Now; //DateTime of Transaction at next spot in array being compared to t1
			for (int y = 0; y < transCount - 1; y++)//Sort by date after already having sorted by customer
			{
				minIndex = y;
				for (int z = 0; z < transCount; z++)
				{
					t1 = transactions[minIndex].getTransactionDate();
					t2 = transactions[z].getTransactionDate();

					if (t2.CompareTo(t1) < 0)
					{
						minIndex = z;
					}
				}

				if (minIndex != y)
				{
					swapArray(transactions, y, minIndex);
				}
			}

			//Display report results
			string displayString = "";
			string[] resultsArray = new string[transCount];
			for (int a = 0; a <= transCount; a++)
			{
				displayString = transactions[a].toString();
				Console.WriteLine(displayString);
				Console.WriteLine("");
				resultsArray[a] = displayString;
			}

			//Save report to a new file
			Console.WriteLine("Would you like to save this report to a new file? Enter Y for Yes or any key to return to main menu.");
			char userInput = char.Parse(Console.ReadLine());
			if (userInput == 'y' || userInput == 'Y')
			{
				Console.WriteLine("Please enter a name for this new file. Remember to include the .txt extension.");
				string fileName = Console.ReadLine();
				System.IO.File.WriteAllLines(fileName, resultsArray);
				Console.WriteLine("File sucessfully saved.");
			}

			Console.ReadKey();

		}

		public static void swapArray(Transaction[] transactions, int x, int y)
		{
			Transaction temp;
			temp = transactions[x];
			transactions[x] = temp;
			transactions[y] = temp;
		}

		public static void historicalRevenueReports(Transaction[] transactions, int transCount)
		{
			DateTime date = DateTime.Now;
			int month = 0;
			int minIndex = 0;
			long revenue = 0;

			//Sort dates in order
			DateTime t1 = DateTime.Now; //DateTime of Transaction at minIndex
			DateTime t2 = DateTime.Now; //DateTime of Transaction at next spot in array being compared to t1
			for (int y = 0; y < transCount - 1; y++)//Sort by date
			{
				minIndex = y;
				for (int z = 0; z < transCount; z++)
				{
					t1 = transactions[minIndex].getTransactionDate();
					t2 = transactions[z].getTransactionDate();

					if (t2.CompareTo(t1) < 0)
					{
						minIndex = z;
					}
				}

				if (minIndex != y)
				{
					swapArray(transactions, y, minIndex);
				}
			}

			//Add up revenue by month
			long[] revenueByMonth = new long[12];
			int count = 0; //Count for month array
			for (int m = 1; m < 13; m++)
			{
				for (int a = 0; a < transCount; a++)
				{
					date = transactions[a].getTransactionDate();
					month = date.Month;
					if (month == m)
					{
						revenue = transactions[a].getRentAmount();
						revenueByMonth[count] = revenueByMonth[count] + revenue;
					}
				}

				count++;
			}

			//Display historical revenue by month
			Console.WriteLine("Here are the results of the historical revenue analysis by month for all records: ");
			Console.WriteLine("January: " + revenueByMonth[0]);
			Console.WriteLine("February: " + revenueByMonth[1]);
			Console.WriteLine("March: " + revenueByMonth[2]);
			Console.WriteLine("April: " + revenueByMonth[3]);
			Console.WriteLine("May: " + revenueByMonth[4]);
			Console.WriteLine("June: " + revenueByMonth[5]);
			Console.WriteLine("July: " + revenueByMonth[6]);
			Console.WriteLine("August: " + revenueByMonth[7]);
			Console.WriteLine("September: " + revenueByMonth[8]);
			Console.WriteLine("October: " + revenueByMonth[9]);
			Console.WriteLine("November: " + revenueByMonth[10]);
			Console.WriteLine("December: " + revenueByMonth[11]);

			string revMonth = revenueByMonth.ToString(); //Convert to string to save to file.
		

			//Add up revenue by year
			long[] revenueByYear = new long[transCount];
			count = 0; //Reset count back to zero to use for year array
			int year = 0;
			int nextYear = 0; //Compared to year

			for (int r = 0; r < transCount; r++)
			{
				date = transactions[r].getTransactionDate();
				year = date.Year;
				date = transactions[r + 1].getTransactionDate();
				nextYear = date.Year;
				if (year != nextYear)
				{
					revenue = transactions[r].getRentAmount();
					revenueByYear[r + 1] = revenueByYear[r + 1] + revenue;
				}
				else
				{
					revenue = transactions[r].getRentAmount();
					revenueByYear[r] = revenueByYear[r] + revenue;
				}
			}


			string revYear = revenueByYear.ToString(); //Convert to string to save to file. 

			//Save report to a new file
			Console.WriteLine("Would you like to save this report to a new file? Enter Y for Yes or any key to return to main menu.");
			char userInput = char.Parse(Console.ReadLine());
			if (userInput == 'y' || userInput == 'Y')
			{
				Console.WriteLine("Please enter a name for this new file. Remember to include the .txt extension.");
				string fileName = Console.ReadLine();
				System.IO.File.WriteAllText(fileName, revMonth);
				System.IO.File.WriteAllText(fileName, revYear);
				Console.WriteLine("File sucessfully saved.");
			}

			Console.ReadKey();
		}
	}
}