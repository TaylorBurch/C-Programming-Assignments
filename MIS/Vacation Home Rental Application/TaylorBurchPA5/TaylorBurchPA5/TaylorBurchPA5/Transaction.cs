using System;
using System.IO;

namespace TaylorBurchPA5
{
	class Transaction
	{
		private string renterName = ""; //Name of person renting out the property
		private string renterEmail = ""; //Email of person renting out property
		private DateTime transactionDate = DateTime.Now; //Date the renter completed their transaction to rent. 
		private int listID = 0; //Listing ID of property being rented
		private long rentAmount = 0; //Price of property being rented
		private string ownerEmail = ""; //Email of owner of the listing property

		public static int transCount = 0; //Count of transactions 

		public Transaction(string name, string email, DateTime date, int ID, long amt, string ownEmail)
		{
			renterName = name;
			renterEmail = email;
			transactionDate = date;
			listID = ID;
			rentAmount = amt;
			ownerEmail = ownEmail;
		}

		//Getters and Setters
		public void setRenterName(string name)
		{
			renterName = name;
		}

		public void setRenterEmail(string email)
		{
			renterEmail = email;
		}

		public void setTransactionDate(DateTime date)
		{
			transactionDate = date;
		}

		public void setListingID(int ID)
		{
			listID = ID;
		}

		public void setRentAmount(long amt)
		{
			rentAmount = amt;
		}

		public void setOwnerEmail(string ownEmail)
		{
			ownerEmail = ownEmail;
		}

		public string getRenterName()
		{
			return renterName;
		}

		public string getRenterEmail()
		{
			return renterEmail;
		}

		public DateTime getTransactionDate()
		{
			return transactionDate;
		}

		public int getListingID()
		{
			return listID;
		}

		public long getRentAmount()
		{
			return rentAmount;
		}

		public string getOwnerEmail()
		{
			return ownerEmail;
		}

		public static void setTransCount(int count)
		{
			transCount = count;
		}

		public static int getTransCount()
		{
			return transCount;
		}

		public static void populateTransArray(Transaction[] transactions, int transCount)
		{
			StreamReader inFile = new StreamReader("transactions.txt");
			string fileInput = inFile.ReadLine();
			string[] inputArray;
			int count = 0; //Count of all listings coming in from file. Used to update transaction count as well.
			transactions[transCount] = new Transaction(name: "", email: "", date: DateTime.Now, ID: 0, amt: 0, ownEmail: "");
			while (fileInput != null)
			{
				inputArray = fileInput.Split('#');
				transactions[count].setRenterName(inputArray[0]);
				transactions[count].setRenterEmail(inputArray[1]);
				transactions[count].setTransactionDate(DateTime.Parse(inputArray[2]));
				transactions[count].setListingID(int.Parse(inputArray[3]));
				transactions[count].setRentAmount(long.Parse(inputArray[4]));
				transactions[count].setOwnerEmail(inputArray[5]);
				count++;
				fileInput = inFile.ReadLine();
			}

			Transaction.setTransCount(count);
		}

		public String toString()
		{
			return "ListingID : " + getListingID() + " Renter Name: " + getRenterName() + " Renter Email: " + getRenterEmail() + " Transaction Date : " + getTransactionDate() + " Rent Amount: " + getRentAmount() + " Owner Email: " + getOwnerEmail();
		}

		public static void renterInfo(Listing[] listings, int index, Transaction[] transactions, int transCount)
		{
			string userInput = "";

			Console.WriteLine("Please enter renter's full name: ");
			userInput = Console.ReadLine();
			transactions[transCount].setRenterName(userInput);

			Console.WriteLine("Please enter an email address the leasing party may contact you at: ");
			userInput = Console.ReadLine();
			transactions[transCount].setRenterEmail(userInput);

			transactions[transCount].setTransactionDate(DateTime.Now); //Automatically set date of transaction to current date and time. 

			int ID = listings[index].getListingID();
			transactions[transCount].setListingID(ID);

			long rent = listings[index].getPrice();
			transactions[transCount].setRentAmount(rent);

			string email = listings[index].getEmail();
			transactions[transCount].setOwnerEmail(email);

		}

		public static void displayTransaction(Listing[] listings, int index, Transaction[] transactions, int transCount)
		{
			Console.WriteLine("Receipt for transaction: ");
			Console.WriteLine("Renter Name: " + transactions[transCount].getRenterName());
			Console.WriteLine("Renter Email: " + transactions[transCount].getRenterEmail());
			Console.WriteLine("Transaction Date: " + transactions[transCount].getTransactionDate());
			Console.WriteLine("");
			Console.WriteLine("Listing ID: " + listings[index].getListingID());
			Console.WriteLine("Listing Address: " + listings[index].getAddress());
			Console.WriteLine("Listing End Date: " + listings[index].getEndDate());
			Console.WriteLine("Listing Price: " + listings[index].getPrice());
			Console.WriteLine("Listing Party's Email: " + listings[index].getEmail());
			Console.WriteLine("");
			Console.WriteLine("Please save and print this reservation reciept.");
			Console.WriteLine("Congratualtions, your reservation of this listing has been sucessful!");
			setTransCount(transCount++);
			Console.ReadKey();
		}

		public static void saveFile(Transaction[] transactions, int transCount)
		{
			//Variables to output back to file.
			string outputRenterName = ""; //Name of person renting out the property
			string outputRenterEmail = ""; //Email of person renting out property
			DateTime outputTransDate = DateTime.Now; //Date the renter completed their transaction to rent. 
			int outputID = 0; //Listing ID of property being rented
			long outputRentAmount = 0; //Price of property being rented
			string outputOwnerEmail = ""; //Email of owner of the listing property
			System.IO.StreamWriter file = new System.IO.StreamWriter("transactions.txt");

			for (int x = 0; x < transCount; x++) //Write out each transaction variable back to transaction file.
			{
				outputRenterName = transactions[x].getRenterName();
				outputRenterEmail = transactions[x].getRenterEmail();
				outputTransDate = transactions[x].getTransactionDate();
				outputID = transactions[x].getListingID();
				outputRentAmount = transactions[x].getRentAmount();
				outputOwnerEmail = transactions[x].getOwnerEmail();

				file.WriteLine(outputRenterName + "#" + outputRenterEmail + "#" + outputTransDate + "#" + outputID + "#" + outputRentAmount + "#" + outputOwnerEmail + "#");
			}

			file.Close();

		}

	}
}