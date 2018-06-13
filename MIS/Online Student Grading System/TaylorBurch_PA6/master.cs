using System;
using System.IO;
using System.Collections.Generic;

namespace TaylorBurch_PA6
{
	class master
	{
		//Variables
		private char recordType = 'x'; //Record type (D date, H header, T trailer)
		private long studentID = 0; //Student CWID identification number 
		private string studentName = ""; //Name of student
		private long evaluatorID = 0; //unique ID number of evaluator 
		private int teamID = 0; //Team ID number
		private string teamName = ""; //Team name student is on
		private int analScore = 0; //Analytical score given to student by evaluator
		private int commScore = 0; //Communication score given based on communication with team member's
		private int techScore = 0; //Technical score of student
		private int avgScore = 0; //Average of all scores combined to make total average score for student
		private string comments = ""; //Comments made by evaluators of student

		private static int masterCounter = 0; //count of records in master file. 
		private static string fileName = "Master 360 Eval File"; //Hard coded file name for master file
		private static DateTime beginDate = DateTime.Now; //Begin date of eval period
		private static DateTime endDate = DateTime.Now; //End date of eval period 

		//Getters and Setters
		public master(char type = 'x', long sID = 0, string name = "", long eID = 0, int tID = 0, string team = "", int aScore = 0, int cScore = 0, int tScore = 0, int avg = 0, string coms = "")
		{
			recordType = type;
			studentID = sID;
			studentName = name;
			evaluatorID = eID;
			teamID = tID;
			teamName = team;
			analScore = aScore;
			commScore = cScore;
			techScore = tScore;
			avgScore = avg;
			comments = coms;
		}

		public void setRecordType(char type)
		{
			recordType = type;
		}
		public void setStudentID(long ID)
		{
			studentID = ID;
		}
		public void setStudentName(string name)
		{
			studentName = name;
		}
		public void setEvaluatorID(long eID)
		{
			evaluatorID = eID;
		}
		public void setTeamID(int tID)
		{
			teamID = tID;
		}
		public void setTeamName(string tName)
		{
			teamName = tName;
		}
		public void setAnalScore(int aScore)
		{
			analScore = aScore;
		}
		public void setCommScore(int cScore)
		{
			commScore = cScore;
		}
		public void setTechScore(int tScore)
		{
			techScore = tScore;
		}
		public void setAvgScore(int avg)
		{
			avgScore = avg;
		}
		public void setComments(string comms)
		{
			comments = comms;
		}
		public char getRecordType()
		{
			return recordType;
		}
		public long getStudentID()
		{
			return studentID;
		}
		public string getStudentName()
		{
			return studentName;
		}
		public long getEvaluatorID()
		{
			return evaluatorID;
		}
		public int getTeamID()
		{
			return teamID;
		}
		public string getTeamName()
		{
			return teamName;
		}
		public int getAnalScore()
		{
			return analScore;
		}
		public int getCommScore()
		{
			return commScore;
		}
		public int getTechScore()
		{
			return techScore;
		}
		public int getAvgScore()
		{
			return avgScore;
		}
		public string getComments()
		{
			return comments;
		}

		public static void setMasterCounter(int count)
		{
			masterCounter = count;
		}

		public static int getMasterCounter()
		{
			return masterCounter;
		}

		public static void setBeginDate(DateTime date)
		{
			beginDate = date;
		}

		public static DateTime getBeginDate()
		{
			return beginDate;
		}

		public static void setEndDate(DateTime date)
		{
			endDate = date;
		}

		public static DateTime getEndDate()
		{
			return endDate;
		}


		//populate array of records from master file
		public static void populateMasterArray(int count, master[] masterRecords)
		{
			StreamReader masterFile = new StreamReader("360Eval.csv");

			string headerInfo = masterFile.ReadLine();
			string[] header = headerInfo.Split('#'); //Array containing header info from master file
			setBeginDate(DateTime.Parse(header[2]));
			setEndDate(DateTime.Parse(header[3]));

			string[] inputArray = new string[11];
			string fileInput = masterFile.ReadLine();
			while (fileInput != null)
			{
				masterRecords[count] = new master();
				inputArray = fileInput.Split('#');
				masterRecords[count].setRecordType(char.Parse(inputArray[0]));
				masterRecords[count].setStudentID(long.Parse(inputArray[1]));
				masterRecords[count].setStudentName(inputArray[2]);
				masterRecords[count].setEvaluatorID(long.Parse(inputArray[3]));
				masterRecords[count].setTeamID(int.Parse(inputArray[4]));
				masterRecords[count].setTeamName(inputArray[5]);
				masterRecords[count].setAnalScore(int.Parse(inputArray[6]));
				masterRecords[count].setCommScore(int.Parse(inputArray[7]));
				masterRecords[count].setTechScore(int.Parse(inputArray[8]));
				masterRecords[count].setAvgScore(int.Parse(inputArray[9]));
				masterRecords[count].setComments(inputArray[10]);
				count++;
				fileInput = masterFile.ReadLine();
			}

			setMasterCounter(count);
			masterFile.Close();
		}

		//View all available records in the master file. 
		public static void viewMasterFile(int masterCount, master[] masterRecords)
		{
			Console.WriteLine("Here are all records in the Master 360 Eval File: ");
			Console.WriteLine();

			for (int x = 0; x < masterCounter; x++)
			{
				Console.WriteLine(masterRecords[masterCounter].toString());
				Console.WriteLine("______________________________________________________________");
			}

			Console.ReadKey();
		}

		//View specific student record accesible to student with valid ID
		public static void viewStudent(int masterCounter, master[] masterRecords)
		{
			Console.WriteLine("Please enter your student ID number to access your records:");
			long student = long.Parse(Console.ReadLine());
			bool studentFound = false;
			int index = 0; //Where in the records array the record was found.

			while (student != 999)
			{
				for (int z = 0; z < masterCounter; z++)
				{
					if (student == masterRecords[z].getStudentID())
					{
						studentFound = true;
						index = z;
						break;
					}
					else
					{
						studentFound = false;
					}
				}

				if (studentFound == true)
				{
					Console.WriteLine(masterRecords[index].toString());
					Console.WriteLine("Would you like to save your record to a file to download and print? If yes, enter y. Enter any other key for no.");
					char input = char.Parse(Console.ReadLine());
					if(input == 'y' || input == 'Y')
					{
						Console.WriteLine("Please enter a file name and include the .txt extension.");
						string studentReportName = Console.ReadLine();
						System.IO.StreamWriter studentFile = new System.IO.StreamWriter(studentReportName);
						Console.WriteLine("File was sucessfully saved.");
						Console.ReadKey();
					}
					Console.WriteLine("If you are done viewing records, enter 999 to return to the main menu. Otherwise, enter the next student ID number.");
				}
				else
				{
					Console.WriteLine("Sorry, this is not a valid ID in the system, please try again or enter 999 to return to main menu.");
				}

				student = int.Parse(Console.ReadLine());
			}
		}

		//to string method for ease of viewing file records
		public string toString()
		{
			return "Record Type: " + getRecordType() + " Student ID: " + getStudentID() + " Student Name: " + getStudentName() + "Evaluator ID: " + getEvaluatorID() + " Team ID: " + getTeamID() + " Team Name: " + getTeamName() + " Analytic Score: " + getAnalScore() + " Technical Score: " + getTechScore() + " Avg. Score: " + getAvgScore() + " Comments: " + getComments();
		}

		//save file
		public static void saveFile(master[] masterRecords, int masterCount)
		{
			char type = 'x';
			long stuID = 0;
			string stuName = "";
			long evalID = 0;
			int tID = 0;
			string tName = "";
			int aScore = 0;
			int cScore = 0;
			int tScore = 0;
			int avg = 0;
			string comms = "";

			System.IO.StreamWriter masterFile = new System.IO.StreamWriter("360Eval.txt");

			masterFile.WriteLine("Record Type: ('H')# File Name: Master 360 Eval File # Round Begin Date: " + beginDate + "# Round End Date: " + endDate + "#");

			for (int x = 0; x < masterCounter; x++)
			{
				type = masterRecords[x].getRecordType();
				stuID = masterRecords[x].getStudentID();
				stuName = masterRecords[x].getStudentName();
				evalID = masterRecords[x].getEvaluatorID();
				tID = masterRecords[x].getTeamID();
				tName = masterRecords[x].getTeamName();
				aScore = masterRecords[x].getAnalScore();
				cScore = masterRecords[x].getCommScore();
				tScore = masterRecords[x].getTechScore();
				avg = masterRecords[x].getAvgScore();
				comms = masterRecords[x].getComments();

				masterFile.WriteLine(type + "#" + stuID + "#" + stuName + "#" + evalID + "#" + tID + "#" + tName + "#" + aScore + "#" + cScore + "#" + tScore + "#" + avg + "#" + comms + "#");
			}

			masterFile.Close();
		}

		public static void studentComm()
		{
			Console.WriteLine("Enter your student ID: ");
			long student = long.Parse(Console.ReadLine());
			Console.WriteLine("Enter the evaluator ID of the Evaluator you wish to leave a note for: ");
			long eval = long.Parse(Console.ReadLine());
			DateTime time = DateTime.Now;

			Console.WriteLine("Please input the note, comment, question or concern here: ");
			string note = Console.ReadLine();

			Console.WriteLine("Thank you for your input. We will save this to our records and the evaluator should address your comment soon.");
			Console.ReadKey();

			string commentFileName = student + eval + "Comments" + time + ".txt";
			System.IO.StreamWriter commentFile = new System.IO.StreamWriter(commentFileName);
		}

	}
}