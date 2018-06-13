using System;
using System.IO;
using System.Collections.Generic;

namespace TaylorBurch_PA6
{
	class MainClass
	{
		public static void Main(string[] args)
		{

			//Extras:
			//Student and evaluator versions of program
			//Student ability to view their reviews 
			//Students' option to download a copy of their individual report to print 
			//Student ability to leave a note or comment to professor after viewing eval.
			//Evaluator ability to see comments/questions/concerns on notes left by students
			//Evaluator ability to view all records in master file.
			//Report on number of evals per evaluator
			//Use of lists


			//Variables
			int mainSelector = 0; //Selection for student or evaluator
			int evalSelection = 0; //main menu selector
			int stuSelector = 0; //Student menu selection
			int reportSelection = 0; //Updator menu selection
			master[] masterRecords = new master[200]; //Array to store master records in (file will not exceed 200 students as stated in client's requirements
			update[] updateRecords = new update[200]; //Array to store all updates from update file.
			bool updateVerified = false;

			//Main
			Console.WriteLine("Welcome to 360 Student Eval!");
			Console.ReadKey();
			mainSelector = displayWelcomeMenu(); //Display welcome menu to select student or instructor option for program

			while (mainSelector != 3)
			{
				if (mainSelector == 1) //Student side program
				{
					Console.WriteLine("Enter 1 to view your report. Enter 2 to leave a note to the Evaluators. Enter 3 to return to main menu.");
					stuSelector = int.Parse(Console.ReadLine());
					while (stuSelector != 3)
					{
						if (stuSelector == 1)
						{
							master.populateMasterArray(master.getMasterCounter(), masterRecords); //read in file to array
							master.viewStudent(master.getMasterCounter(), masterRecords); //view master file
							master.saveFile(masterRecords, master.getMasterCounter()); //save data back to file and close
						}
						else if (stuSelector == 2)
						{
							master.studentComm(); //Students ability to leave a comment or note to TA or Evaluators
						}
						else
						{
							Console.WriteLine("Invalid input, please try again.");
							Console.ReadKey();
						}
					}
				}

				if (mainSelector == 2) //Evaluator/Professor/TA side program
				{
					evalSelection = displayEvalMenu(); //Display main menu
					while (evalSelection != 4)
					{
						if (evalSelection == 1)
						{
							master.populateMasterArray(master.getMasterCounter(), masterRecords); //read in file to array
							master.viewMasterFile(master.getMasterCounter(), masterRecords); //view master file
							master.saveFile(masterRecords, master.getMasterCounter()); //save data back to file and close
						}
						else if (evalSelection == 2)
						{
							update.populateUpdatesArray(updateRecords, update.getUpdateCounter()); //populate update requests from updater file to array
							master.populateMasterArray(master.getMasterCounter(), masterRecords); //populate existing entries from master file to array
							updateVerified = update.preProcess(update.getNumRecords(), update.getTechSum(), update.getUpdateDate(), master.getBeginDate(), master.getEndDate(), update.getUpdateCounter(), updateRecords); //verify correct data in array
							if (updateVerified == true)
							{
								update.compareArrays(updateRecords, masterRecords, update.getUpdateCounter(), master.getMasterCounter()); //compare master to updater file
								Console.WriteLine("Updates have been sucessfully added.");
							}
							else
							{
								Console.WriteLine("Sorry, updates could not be added, the record does not fall within the correct validation period.");
								Console.ReadKey();
							}
							master.saveFile(masterRecords, master.getMasterCounter()); //save master file and close it.
						}
						else if (evalSelection == 3)
						{
							List<string> studentList = new List<string>(); 
							List<string> teamAvgList = new List<string>();
							reportSelection = displayReportsMenu(); //Display menu to select what report to generate

							if (reportSelection == 1)
							{
								report.studentReport(masterRecords, master.getMasterCounter(), studentList, teamAvgList); //Calls report on total and avgs for individuals on every team
								      
							}
							else if (reportSelection == 2)
							{
								report.topFiveReport(masterRecords, master.getMasterCounter()); //Report on Top 5 students/teams and greatest team range
							}
							else if (reportSelection == 3)
							{
								report.evalNumReport(masterRecords, master.getMasterCounter());
							}
							else
							{
								Console.WriteLine("Sorry, that was an invalid report selection...");
								Console.ReadKey();
							}
						}
						else
						{
							Console.WriteLine("Sorry, that input was invalid. Please try again.");
							Console.ReadKey();
						}
					}


				}
				else
				{
					Console.WriteLine("Invalid input, please try again.");
				}
			}

			Console.WriteLine("Exiting 360 Eval...");
			Console.ReadLine();
		}

		public static int displayWelcomeMenu()
		{
			int selector = 0; //menu selection

			Console.WriteLine("If you are a student, input 1 to view your records.");
			Console.WriteLine("For Evaluators and Professors, input 2 to access the master and update records.");
			Console.WriteLine("If you would like to exit the program, please input 3.");
			Console.WriteLine("Please note: students and faculty must enter valid ID to access records.");

			selector = int.Parse(Console.ReadLine());
			return selector;
		}

		public static int displayEvalMenu()
		{
			int selector = 0; //menu selection

			Console.WriteLine("Please select an option from the menu below by inputing the corresponding key.");
			Console.WriteLine("Input 1 to view the Master File.");
			Console.WriteLine("Input 2 to update the Master File.");
			Console.WriteLine("Input 3 to generate evaluation reports.");
			Console.WriteLine("Input 4 to Quit.");

			selector = int.Parse(Console.ReadLine());
			return selector;

		}

		public static int displayReportsMenu() 
		{
			int selector = 0;

			Console.WriteLine("Please select a report to run: ");
			Console.WriteLine("Input 1 to print a report on averages and totals for each person on a team.");
			Console.WriteLine("Input 2 to print a report on top 5 students and teams and the 5 teams with greatest range of score.");
			Console.WriteLine("Input 3 to print a report on the number of evaluations per evaluator.");

			selector = int.Parse(Console.ReadLine());
			return selector;
		}
	}
}
