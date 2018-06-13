using System;
using System.IO;

namespace TaylorBurchPA5
{
	class MainClass
	{
		public static void Main(string[] args)
		{

			//EXTRAS:
			//Autogenerate Listing ID
			//Verify credit card payment
			//User manual
			//Use of font and background color
			//Use of dateTime class to automatically and accurately generate transaction date
			//Sort listings when viewed by availability first. 
			//Option to leave a review and read reviews on a listing.
			//Verification that person writing the review has leased the place before writing the review to ensure reviews are legitamate previous renters.


			int menuSelection = 0;
			int reportSelection = 0;
			Listing[] listings = new Listing[500]; //Array of all listings
			Transaction[] transactions = new Transaction[500]; //Array of all transactions

			menuSelection = displayMenu(); //Display main menu

			while (menuSelection != 6)
			{
				if (menuSelection == 1)
				{
					Listing.populateListingArray(Listing.getListingCount(), listings); //Populate array from file of all listings in the system.
					Listing.addListing(listings, Listing.getListingCount()); //Add a new listing with all information to the program and save to file.
					Listing.saveFile(listings, Listing.getListingCount()); //Save new listing to listings file. 
				}
				else if (menuSelection == 2)
				{
					Listing.populateListingArray(Listing.getListingCount(), listings); //Populate array from file of all listings in the system.
					Listing.checkAvailable(listings, Listing.getListingCount());//Check listings to make sure properties have correct availability based on date or availability status
					Listing.sortByAvail(listings, Listing.getListingCount()); //Sort listings to show available properties first, then currently unavailable ones.
					Listing.viewListings(listings, Listing.getListingCount()); //View all condos available for rent
					Listing.viewReviews(); //Option to see any reviews on a specific listing if desired.
					Console.WriteLine("Would you like to list a property you've seen? Enter Y for Yes or any other key to return to main menu.");
					char userInput = char.Parse(Console.ReadLine());
					if (userInput == 'y' || userInput == 'Y')
					{
						Transaction.populateTransArray(transactions, Transaction.getTransCount());
						Listing.rentProperty(listings, Listing.getListingCount(), transactions); //Complete the rent a property transaction including printing receipt and asking for payment. 
						Transaction.saveFile(transactions, Transaction.getTransCount()); //Save and close transaction file
					}

					Listing.saveFile(listings, Listing.getListingCount()); //Save listings back to file. 
					Console.WriteLine("Returning to main menu...");
					Console.ReadKey();

				}
				else if (menuSelection == 3)
				{
					Listing.writeReview(transactions, Transaction.getTransCount()); //Write a review for a listing after leasing it.
				}
				else if (menuSelection == 4)
				{
					reportSelection = displayReportsMenu(); //Pull up menu to ask user what report they wish to see.
					Transaction.populateTransArray(transactions, Transaction.getTransCount());
					if (reportSelection == 1)
					{
						Report.individualCustomerReport(transactions, Transaction.getTransCount());
					}
					else if (reportSelection == 2)
					{
						Report.historicalCustomerReports(transactions, Transaction.getTransCount());
					}
					else if (reportSelection == 3)
					{
						Report.historicalRevenueReports(transactions, Transaction.getTransCount());
					}
					else
					{
						Console.WriteLine("Sorry, that input is invalid. Please try again.");
					}

				}
				else if (menuSelection == 5)
				{
					StreamReader manualFile = new StreamReader("manual.txt");
					string manual = manualFile.ReadToEnd();
					Console.Write(manual);
					Console.ReadKey();
				}
				else
				{
					Console.WriteLine("Sorry, that input is invalid. Please try again.");
					Console.ReadKey();
				}

				menuSelection = displayMenu();
			}

		}

		public static int displayMenu() //Display the driver main menu
		{
			Console.BackgroundColor = ConsoleColor.Magenta;
			Console.ForegroundColor = ConsoleColor.DarkBlue;
			Console.WriteLine("Welcome to Rent My Place!");
			Console.ResetColor();
			Console.WriteLine("Please select a menu option by inputing the corresponding number.");
			Console.WriteLine("Input 1 to add a listing.");
			Console.WriteLine("Input 2 to lease a condo.");
			Console.WriteLine("Input 3 to write a review on a property.");
			Console.WriteLine("Input 4 to run reports.");
			Console.WriteLine("Input 5 to open the instruction manual.");
			Console.WriteLine("Input 6 to exit.");

			int menuSelection = int.Parse(Console.ReadLine());
			return menuSelection;
		}

		public static int displayReportsMenu() //Display menu for viewing reports
		{
			Console.WriteLine("Please select the report you wish to view: ");
			Console.WriteLine("Input 1 for Individual Customer Rentals.");
			Console.WriteLine("Input 2 for Historical Customer Rentals.");
			Console.WriteLine("Input 3 for Historical Revenue Rentals.");

			int reportSelection = int.Parse(Console.ReadLine());
			return reportSelection;

		}

	}
}
