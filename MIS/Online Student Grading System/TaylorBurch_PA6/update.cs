using System;
using System.IO;
using System.Collections.Generic;

namespace TaylorBurch_PA6
{
	class update
	{
		//Variables
		private static DateTime updateDate = DateTime.Now;
		private char recordType = 'z'; //Record type
		private char actionCode = 'x'; //Action code for update process (A Add, C Change, D Delete)
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

		//Variables from trailer
		private static int numRecords = 0; //Number of records in update file as listed by trailer 
		private static int techSum = 0; //Sum of technical score for that update file as stated in trailer.

		private static int updateCounter = 0; //count of records in update file. 
		private static string fileName = "360 Eval Updates"; //Hard coded file name



		//Getters and Setters
		public update(char type = 'v', char action = 'x', long sID = 0, string name = "", long eID = 0, int tID = 0, string team = "", int aScore = 0, int cScore = 0, int tScore = 0, int avg = 0, string coms = "")
		{
			recordType = type;
			actionCode = action;
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

		public void setRecordType(char recType)
		{
			recordType = recType;
		}
		public void setActionCode(char type)
		{
			actionCode = type;
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
		public void setFileName(string file)
		{
			fileName = file;
		}
		public string getFileName()
		{
			return fileName;
		}
		public char getRecordType()
		{
			return recordType;
		}
		public char getActionCode()
		{
			return actionCode;
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

		public static void setUpdateCounter(int count)
		{
			updateCounter = count;
		}

		public static int getUpdateCounter()
		{
			return updateCounter;
		}

		public static void setUpdateDate(DateTime date)
		{
			updateDate = date;
		}

		public static DateTime getUpdateDate()
		{
			return updateDate;
		}

		public static void setNumRecords(int num)
		{
			numRecords = num;
		}
		public static int getNumRecords()
		{
			return numRecords;
		}

		public static void setTechSum(int sum)
		{
			techSum = sum;
		}
		public static int getTechSum()
		{
			return techSum;
		}


		//Populate Updates Array from Update File. 
		public static void populateUpdatesArray(update[] updateRecords, int count)
		{
			StreamReader updateFile = new StreamReader(fileName + ".csv");

			string headerInfo = updateFile.ReadLine();
			string[] header = headerInfo.Split(','); //Array containing header info from master file
			setUpdateDate(DateTime.Parse(header[2]));

			string[] inputArray = new string[11];
			string fileInput1 = updateFile.ReadLine();
			string fileInput2 = updateFile.ReadLine();
			while (fileInput2 != null)
			{
				updateRecords[count] = new update();
				inputArray = fileInput1.Split(',');
				updateRecords[count].setRecordType(char.Parse(inputArray[0]));
				updateRecords[count].setActionCode(char.Parse(inputArray[1]));
				updateRecords[count].setStudentID(int.Parse(inputArray[2]));
				updateRecords[count].setStudentName(inputArray[3]);
				updateRecords[count].setEvaluatorID(int.Parse(inputArray[4]));
				updateRecords[count].setTeamID(int.Parse(inputArray[5]));
				updateRecords[count].setTeamName(inputArray[6]);
				updateRecords[count].setAnalScore(int.Parse(inputArray[7]));
				updateRecords[count].setCommScore(int.Parse(inputArray[8]));
				updateRecords[count].setTechScore(int.Parse(inputArray[9]));
				updateRecords[count].setComments(inputArray[10]);
				count++;
				fileInput1 = fileInput2;
				fileInput2 = updateFile.ReadLine();
			}

			string[] trailInfo = fileInput1.Split(',');
			numRecords = int.Parse(trailInfo[3]);
			techSum = int.Parse(trailInfo[4]);

			setTechSum(techSum);
			setNumRecords(numRecords);

			setUpdateCounter(count);
			updateFile.Close();
		}

		//Validate update record before processing 
		public static bool preProcess(int numRecords, int techSum, DateTime updateDate, DateTime beginDate, DateTime endDate, int updateCount, update[] updateRecords)
		{
			bool verified = false;
			int calculatedTSum = 0; 

			//Ensure file is within evaluation period
			if(updateDate > endDate || updateDate < beginDate)
			{
				verified = false;
			}
			else
			{
				verified = true;

				//Ensure total records is accurate
				if (updateCount != numRecords)
				{
					numRecords = updateCount - 1;
					setNumRecords(numRecords);
				}

				//Ensure tech sum in trailer is accurate

				for (int y = 0; y < updateCount; y++)
				{
					calculatedTSum = calculatedTSum + updateRecords[y].getTechScore();
				}
				if (calculatedTSum != techSum)
				{
					techSum = calculatedTSum;
				}


			}

				return verified;
		}

		public static void compareArrays(update[] updateRecords, master[] masterRecords, int updateCounter, int masterCounter)
		{
			char action = 'j'; //Action code from update record to Add, Change, or Delete 
			for (int x = 0; x < updateCounter; x++)
			{
				action = updateRecords[x].getRecordType();

				if(action == 'A' || action == 'a') //Add new record
				{
					update.addRecord(updateRecords, masterRecords, x, masterCounter);
				}
				else if(action == 'C' || action == 'c') //Change existing record
				{
					update.changeRecord(updateRecords, masterRecords, x, masterCounter);
				}
				else if(action == 'D' || action == 'd') //Delete existing record
				{
					update.deleteRecord(updateRecords, masterRecords, updateCounter, masterCounter);
				}
			}
		}

		public static void addRecord(update[] updateRecords, master[] masterRecords, int x, int masterCounter)
		{
			masterRecords[masterCounter].setRecordType(updateRecords[x].getRecordType());
			masterRecords[masterCounter].setStudentID(updateRecords[x].getStudentID());
			masterRecords[masterCounter].setStudentName(updateRecords[x].getStudentName());
			masterRecords[masterCounter].setEvaluatorID(updateRecords[x].getEvaluatorID());
			masterRecords[masterCounter].setTeamID(updateRecords[x].getTeamID());
			masterRecords[masterCounter].setTeamName(updateRecords[x].getTeamName());
			masterRecords[masterCounter].setAnalScore(updateRecords[x].getAnalScore());
			masterRecords[masterCounter].setCommScore(updateRecords[x].getCommScore());
			masterRecords[masterCounter].setTechScore(updateRecords[x].getTechScore());
			masterRecords[masterCounter].setAvgScore(updateRecords[x].getAvgScore());
			masterRecords[masterCounter].setComments(updateRecords[x].getComments());
			masterCounter++;
			master.setMasterCounter(masterCounter);
		}

		public static void changeRecord(update[] updateRecords, master[] masterRecords, int x, int masterCounter)
		{

			long key = 0; //Key for finding change record, unique combo of student ID and evaluator ID. 
			key = updateRecords[x].getStudentID() + updateRecords[x].getEvaluatorID();
			long tryKey = 0; //Key to compare to key of record needing change

			for (int y = 0; y < masterCounter; y++)
			{
				tryKey = masterRecords[y].getStudentID() + masterRecords[y].getEvaluatorID();
				if(key == tryKey)
				{
					masterRecords[y].setRecordType(updateRecords[x].getRecordType());
					masterRecords[y].setStudentID(updateRecords[x].getStudentID());
					masterRecords[y].setStudentName(updateRecords[x].getStudentName());
					masterRecords[y].setEvaluatorID(updateRecords[x].getEvaluatorID());
					masterRecords[y].setTeamID(updateRecords[x].getTeamID());
					masterRecords[y].setTeamName(updateRecords[x].getTeamName());
					masterRecords[y].setAnalScore(updateRecords[x].getAnalScore());
					masterRecords[y].setCommScore(updateRecords[x].getCommScore());
					masterRecords[y].setTechScore(updateRecords[x].getTechScore());
					masterRecords[y].setAvgScore(updateRecords[x].getAvgScore());
					masterRecords[y].setComments(updateRecords[x].getComments());

				}
			}
		}
		public static void deleteRecord(update[] updateRecords, master[] masterRecords, int x, int masterCounter)
		{
			long key = 0; //Key for finding change record, unique combo of student ID and evaluator ID. 
			key = updateRecords[x].getStudentID() + updateRecords[x].getEvaluatorID();
			long tryKey = 0; //Key to compare to key of record needing to be deleted
			int count = 0;

			for (int a = 0; a < masterCounter; a++)
			{
				tryKey = masterRecords[a].getStudentID() + masterRecords[a].getEvaluatorID();
				if(key == tryKey)
				{
					count++;
				}
				else
				{
					masterRecords[a].setRecordType(masterRecords[count].getRecordType());
					masterRecords[a].setStudentID(masterRecords[count].getStudentID());
					masterRecords[a].setStudentName(masterRecords[count].getStudentName());
					masterRecords[a].setEvaluatorID(masterRecords[count].getEvaluatorID());
					masterRecords[a].setTeamID(masterRecords[count].getTeamID());
					masterRecords[a].setTeamName(masterRecords[count].getTeamName());
					masterRecords[a].setAnalScore(masterRecords[count].getAnalScore());
					masterRecords[a].setCommScore(masterRecords[count].getCommScore());
					masterRecords[a].setTechScore(masterRecords[count].getTechScore());
					masterRecords[a].setAvgScore(masterRecords[count].getAvgScore());
					masterRecords[a].setComments(masterRecords[count].getComments());
					count++;
				}
			}

			master.setMasterCounter(count - 1); //count of total records minus the one deleted

		}

	}

}