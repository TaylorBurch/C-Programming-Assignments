using System;
using System.IO;

namespace TaylorBurchPA5
{
	class Listing
	{
		private int listingID = 0;
		private string address = "";
		private string endDate = "";
		private long price = 0;
		private string email = "";
		private bool available = false;

		private static int listingCount = 0; //Count total number of listings and use to autogenerate listing ID 

		//Getters and Setters:
		public Listing(int ID, string ad, string d, long p, string e, bool a)
		{
			listingID = ID;
			address = ad;
			endDate = d;
			price = p;
			email = e;
			available = a;
		}

		public void setListingID(int ID) 
		{
			listingID = ID;
		}

		public void setAddress(string A)
		{
			address = A;
		}

		public void setEndDate(string date)
		{
			endDate = date;
		}

		public void setPrice(long P)
		{
			price = P;
		}

		public void setEmail(string E)
		{
			email = E;
		}

		public int getListingID()
		{
			return listingID;
		}

		public string getAddress()
		{
			return address;
		}

		public string getEndDate()
		{
			return endDate;
		}

		public long getPrice()
		{
			return price;
		}

		public string getEmail()
		{
			return email;
		}

		public static void setListingCount(int count)
		{
			listingCount = count;
		}

		public static int getListingCount()
		{
			return listingCount;
		}

		public void setAvailability(bool t)
		{
			available = t;
		}

		public bool getAvailability()
		{
			return available;
		}


		//Populate array of listings
		public static void populateListingArray(int listingCount, Listing[] listings)
		{
			StreamReader inFile = new StreamReader("listings.txt");
			string fileInput = inFile.ReadLine();
			string[] inputArray;
			int count = 0; //Count of all listings coming in from file. Used to update listing count as well.
			listings[listingCount] = new Listing(ID: 0, ad: "", d: "", p: 0, e: "", a: false);
			while (fileInput != null)
			{
				inputArray = fileInput.Split('#');
				listings[count].setListingID(int.Parse(inputArray[0]));
				listings[count].setAddress(inputArray[1]);
				listings[count].setEndDate(inputArray[2]);
				listings[count].setPrice(long.Parse(inputArray[3]));
				listings[count].setEmail(inputArray[4]);
				listings[count].setAvailability(bool.Parse(inputArray[5]));
				count++;
				fileInput = inFile.ReadLine();
			}

			Listing.setListingCount(count);
		}

		//toString Method
		public String toString()
		{
			return "ListingID : " + getListingID() + " Address : " + getAddress() + " End Date : " + getEndDate() + " Price: " + getPrice() + " Email: " + getEmail() + "Available: " + getAvailability();
		}

		//Add new listing to listings array
		public static void addListing(Listing[] listings, int listingCount)
		{
			string userInput = "";
			listings[listingCount] = new Listing(ID: 0, ad: "", d: "", p: 0, e: "", a: false);
			listings[listingCount].setListingID(listingCount);
			Console.WriteLine("Please enter the prompted information to add a new listing!");

			Console.WriteLine("Enter the address of the listing: ");
			userInput = Console.ReadLine();
			listings[listingCount].setAddress(userInput);

			Console.WriteLine("Enter the end date of the listing: (Note: Enter in the format of mm/dd/yy)");
			userInput = Console.ReadLine();
			listings[listingCount].setEndDate(userInput);

			Console.WriteLine("Enter the price you would like to list at: ");
			long input = long.Parse(Console.ReadLine());
			listings[listingCount].setPrice(input);

			Console.WriteLine("Please enter seller email: ");
			userInput = Console.ReadLine();
			listings[listingCount].setEmail(userInput);

			listings[listingCount].setAvailability(true);

			listingCount++;
			setListingCount(listingCount);

		}

		//Save all listings to file
		public static void saveFile(Listing[] listings, int listingCount)
		{
			int outputID = 0;
			string outputAddress = "";
			string outputDate = "";
			long outputPrice = 0;
			string outputEmail = "";
			bool outAvail;
			System.IO.StreamWriter file = new System.IO.StreamWriter("listings.txt");

			for (int x = 0; x < listingCount; x++) //Write out each listing variable back to listings file.
			{
				outputID = listings[x].getListingID();
				outputAddress = listings[x].getAddress();
				outputDate = listings[x].getEndDate();
				outputPrice = listings[x].getPrice();
				outputEmail = listings[x].getEmail();
				outAvail = listings[x].getAvailability();

				file.WriteLine(outputID + "#" + outputAddress + "#" + outputDate + "#" + outputPrice + "#" + outputEmail + "#" + outAvail + "#");

			}

			file.Close();	       

		}

		//Check availability bool to make sure it corresponds end date
		public static void checkAvailable(Listing[] listings, int listingCount)
		{
			for (int x = 0; x < listingCount; x++)
			{
				DateTime date = DateTime.Parse(listings[x].getEndDate());
				if (date > DateTime.Today)
				{
					listings[x].setAvailability(false);
				}
			}
		}

		//Sort listings to show available ones before showing currently unavailable lisitngs.
		public static void sortByAvail(Listing[] listings, int listingCount)
		{
			int minIndex = 0;
			bool compare1;
			bool compare2;
			for (int x = 0; x < listingCount - 1; x++)
			{
				minIndex = x;
				for (int y = 0; y < listingCount; y++)
				{
					compare1 = listings[minIndex].getAvailability();
					compare2 = listings[y].getAvailability();
					if (compare2.CompareTo(compare1) < 0)
					{
						minIndex = y;
					}

					if (minIndex != x)
					{
						swapArray(listings, x, minIndex);
					}
				}
			}
		}

		//Swap method 
		public static void swapArray(Listing[] listings, int x, int minIndex)
		{
			Listing temp;
			temp = listings[x];
			listings[x] = listings[minIndex];
			listings[minIndex] = temp;
		}

		//View all available listings 
		public static void viewListings(Listing[] listings, int listingCount)
		{
			Console.WriteLine("Here are all of the listings in the system: ");
			for (int x = 0; x < listingCount; x++)
			{
				bool avail = listings[x].getAvailability();
				if (avail == true)
				{
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine(listings[x].toString());
					Console.ResetColor();
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(listings[x].toString());
					Console.ResetColor();
				}
			}

			Console.ReadKey();
			
		}

		//Select and rent out property
		public static void rentProperty(Listing[] listings, int listingCount, Transaction[] transactions)
		{
			int findID = 0;
			int indexFoundAt = 0;
			bool found = false;

			Console.WriteLine("Please enter the listing ID of the property you wish to rent.");
			findID = int.Parse(Console.ReadLine());

			for (int x = 0; x < listingCount; x++)
			{
				if (listings[x].getListingID() == findID)
				{
					found = true;
					indexFoundAt = x;
					break;
				}
				else 
				{
					found = false;
				}
			}

			if (found == false)
			{
				Console.WriteLine("Sorry, that listing ID was not found in our system. Please look through our properties and try again.");
				Console.ReadKey();
			}
			else
			{
				Console.WriteLine(listings[indexFoundAt].toString());
				Console.WriteLine("If this is the property you wish to rent, enter Y for Yes. Otherwise, enter any other key to start over.");
				char userInput = char.Parse(Console.ReadLine());
				if (userInput == 'y' || userInput == 'Y')
				{
					if (listings[indexFoundAt].getAvailability() == true)
					{
						Transaction.renterInfo(listings, indexFoundAt, transactions, Transaction.getTransCount());
						Transaction.displayTransaction(listings, indexFoundAt, transactions, Transaction.getTransCount());
						listings[indexFoundAt].setAvailability(false);
					}
				}
			}

			Console.WriteLine("Take payment now or pay later? Enter Y for Yes, any key to return to main menu.");
			Console.WriteLine("Note: Payment must be made within 3 days of booking, or reservation will be cancelled.");
			char input = char.Parse(Console.ReadLine());
			if (input == 'y' || input == 'Y')
			{
				Payment.inputPaymentMethod();
			}
		}

		//View reviews for a specific property if desired.
		public static void viewReviews()
		{
			int inputID = 0; //Listing ID input by user to search for reviews.
			string fileName = ""; //Name of file containing reviews for the specified property if it exists.

			Console.WriteLine("Would you like to see the reviews on a specific property? Enter Y for Yes, or any other key for No.");
			char userInput = char.Parse(Console.ReadLine());
			if (userInput == 'y' || userInput == 'Y')
			{
				Console.WriteLine("Please input the listing ID of the property you wish to view the reviews for.");
				inputID = int.Parse(Console.ReadLine());
				while (inputID != 999)
				{
					fileName = "reviews" + inputID + ".txt";
					if (File.Exists(fileName))
					{
						StreamReader reviewFile = new StreamReader(fileName);
						string reviews = reviewFile.ReadToEnd();
						Console.Write(reviews);
						Console.ReadKey();
					}
					else
					{
						Console.WriteLine("Sorry, there are currently no reviews for that property.");
						Console.ReadKey();
					}

					Console.WriteLine("Please enter a listing ID of a property you would like to view the reviews for, or enter 999 to continue on.");
					inputID = int.Parse(Console.ReadLine());
				}

			}
		}

		//Write a review for a property.
		public static void writeReview(Transaction[] transactions, int transCount)
		{
			string fileName = ""; //Name of file review will be saved to.

			Console.WriteLine("Please enter the listing ID you would like to write a review for.");
			Console.WriteLine("Please note that our system must verify you have previously rented this property before leaving a review.");
			int inputID = int.Parse(Console.ReadLine());
			int foundID = 0;
			int foundIndex = 0;
			bool found = false;
			for (int x = 0; x < transCount; x++)
			{
				foundID = transactions[x].getListingID();
				if (foundID == inputID)
				{
					found = true;
					foundIndex = x;
					break;
				}
				else
				{
					found = false;
				}
					
			}

			if (found == false)
			{
				Console.WriteLine("We did not find that listing ID in our system. Please try again.");
			}
			else
			{
				Console.WriteLine("Please enter the renter name from the rental transaction for this listing to verify review eligibility.");
				string name = Console.ReadLine();
				string transName = transactions[foundIndex].getRenterName();
				if (name == transName)
				{
					Console.WriteLine("You have been approved to write a review for this listing.");
					Console.WriteLine("Please write your review here. Be sure to include any information future listers might want to know about this listing. We appreciate your feedback!");
					Console.WriteLine("Enter review: ");
					string reviewInput = Console.ReadLine();

					fileName = "reviews" + transactions[foundIndex].getListingID() + ".txt";

					if (File.Exists(fileName))
					{
						System.IO.StreamWriter file = new System.IO.StreamWriter(fileName);
						file.Write("________________________________________________________________________");
						file.Write(reviewInput);
					}
					else
					{
						System.IO.File.WriteAllText(fileName, reviewInput);
						Console.ReadKey();
					}

					Console.WriteLine("Thank you for your review! It has been saved to our database to help future renters!");
				}
			}

			
		}

	}

}


