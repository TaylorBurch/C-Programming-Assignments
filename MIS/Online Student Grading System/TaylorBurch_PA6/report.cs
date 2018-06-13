using System;
using System.IO;
using System.Collections.Generic;

namespace TaylorBurch_PA6
{
	class report
	{
		//Sort method
		public static void sortArray(master[] masterRecords, int count)
		{
			//Sort by team
			int minIndex = 0;
			long compare1 = 0;
			long compare2 = 0;
			for (int t = 0; t < count - 1; t++)
			{
				minIndex = t;
				for (int y = 0; y < count; y++)
				{
					compare1 = masterRecords[minIndex].getTeamID();
					compare2 = masterRecords[y].getTeamID();

					if (compare2.CompareTo(compare1) < 0)
					{
						minIndex = y;
					}

					if (minIndex != t)
					{
						swapArray(masterRecords, t, minIndex);
					}
				}
			}

			//Sort by student
			for (int s = 0; s < count - 1; s++)
			{
				minIndex = s;
				for (int y = 0; y < count; y++)
				{
					compare1 = masterRecords[minIndex].getStudentID();
					compare2 = masterRecords[y].getStudentID();

					if (compare2.CompareTo(compare1) < 0)
					{
						minIndex = y;
					}

					if (minIndex != s)
					{
						swapArray(masterRecords, s, minIndex);
					}
				}
			}
		}

		//Individual student report sorted by team
		public static void studentReport(master[] masterRecords, int count, List<string> studentList, List<string> teamAvgList)
		{
			long student = 0;
			long nextStudent = 0;
			long team = 0;
			long nextTeam = 0;
			int avgAScore = 0; //Average analytic score of inidividual
			int avgCommScore = 0; //Average communication score of inidividual
			int avgTechScore = 0; //Average technical score of individual
			int totalAvg = 0;
			int teamAvg = 0;
			string output = ""; //Output to add to student avg report
			string teamOutput = ""; //Output of team avg to file.

			sortArray(masterRecords, count);


			//Calculate averages
			for (int x = 0; x < count; x++)
			{
				student = masterRecords[x].getStudentID();
				nextStudent = masterRecords[x + 1].getStudentID();

				team = masterRecords[x].getTeamID();
				nextTeam = masterRecords[x].getTeamID();

				avgAScore = avgAScore + masterRecords[x].getAnalScore();
				avgCommScore = avgCommScore + masterRecords[x].getCommScore();
				avgTechScore = avgTechScore + masterRecords[x].getTechScore();
				totalAvg = totalAvg + masterRecords[x].getAvgScore();
				teamAvg = teamAvg + totalAvg;

				if(student != nextStudent)
				{
					output = "Team: " + team + " Student: " + student + " Avg Analytic Score: " + avgAScore + " Avg Communication Score: " + avgCommScore + " Avg TechScore: " + avgTechScore + " Total Avg: " + totalAvg;
					studentList.Add(output);
					avgAScore = 0;
					avgCommScore = 0;
					avgTechScore = 0;
					totalAvg = 0;
				}

				if(team != nextTeam)
				{
					teamOutput = "Team Name: " + masterRecords[x].getTeamName() + "Team ID: " + team + " Total Team Avg: " + totalAvg;
					teamAvgList.Add(teamOutput);
					teamAvg = 0;
				}
			}

			//Save report to output file.
			Console.WriteLine("Please enter a name you would like to save this report as. Remember to include the .txt extension.");
			string file = Console.ReadLine();
			StreamWriter avgReport = new System.IO.StreamWriter(file);

			for (int j = 0; j < teamAvgList.Count; j++)
			{
				avgReport.WriteLine(teamAvgList[j]);
			}
			for (int i = 0; i < studentList.Count; i++)
			{
				avgReport.WriteLine(studentList[i]);
			}

			Console.WriteLine("Report sucessfully generated.");
			Console.ReadKey();
		}

		//Method to swap positions in array when sorting
		public static void swapArray(master[] masterRecords, int x, int minIndex)
		{
			master temp;
			temp = masterRecords[x];
			masterRecords[x] = masterRecords[minIndex];
			masterRecords[minIndex] = temp;
		}

		//Top 5 students and teams report
		public static void topFiveReport(master[] masterRecords, int count)
		{
			sortArray(masterRecords, count);
			long curStu = 0;
			long nextStu = 0;
			int studentAvg = 0;
			long curTeam = 0;
			long nextTeam = 0;
			int teamAvg = 0;
			int i = 0; //Counter of students in top 5
			int j = 0; //Counter of teams in top 5
			int stuBottom = masterRecords[0].getAvgScore();
			int teamBottom = masterRecords[0].getAvgScore();

			List<string> top5Stu = new List<string>();
			List<string> top5Teams = new List<string>();

			for (int x = 0; x < count; x++)
			{
				studentAvg = studentAvg + masterRecords[x].getAvgScore();
				curStu = masterRecords[x].getStudentID();
				nextStu = masterRecords[x + 1].getStudentID();

				teamAvg = teamAvg + studentAvg;
				curTeam = masterRecords[x].getTeamID();
				nextTeam = masterRecords[x + 1].getTeamID();

				if(curStu != nextStu)
				{
					if(stuBottom < studentAvg)
					{

						top5Stu[i] = masterRecords[x].getStudentName();
						i++;
					}

					studentAvg = 0;

					if(teamBottom < teamAvg)
					{
						top5Teams[j] = masterRecords[x].getTeamName();
						j++;
					}
				}

			}


			//Save report to output file.
			Console.WriteLine("Please enter a name you would like to save this report as. Remember to include the .txt extension.");
			string file = Console.ReadLine();
			StreamWriter top5Report = new System.IO.StreamWriter(file);

			top5Report.WriteLine("Top 5 Teams: ");
			for (int y = 0; y < top5Teams.Count; y++)
			{
				top5Report.WriteLine(top5Teams[y]);
			}

			top5Report.WriteLine("Top 5 Students: ");
			for (int z = 0; z < top5Stu.Count; z++)
			{
				top5Report.WriteLine(top5Stu[z]);
			}

			Console.WriteLine("Report sucessfully generated.");
			Console.ReadKey();


		}

		//Report on number of evaluations done by a specific evaluator
		public static void evalNumReport(master[] masterRecords, int count)
		{
			//Sort by evaluator
			int minIndex = 0;
			long compare1 = 0;
			long compare2 = 0;
			for (int x = 0; x < count - 1; x++)
			{
				minIndex = x;
				for (int y = 0; y < count; y++)
				{
					compare1 = masterRecords[minIndex].getEvaluatorID();
					compare2 = masterRecords[y].getEvaluatorID();

					if (compare2.CompareTo(compare1) < 0)
					{
						minIndex = y;
					}

					if (minIndex != x)
					{
						swapArray(masterRecords, x, minIndex);
					}
				}
			}

			//Find all evals per evaluator
			StreamWriter evalCountReport = new System.IO.StreamWriter("EvaluatorCountReport.txt");
			int evalCounter = 0; //Number of evals per evaluator
			long eval1 = 0;
			long eval2 = 0;
			for (int z = 0; z < count; z++)
			{
				eval1 = masterRecords[z].getEvaluatorID();
				eval2 = masterRecords[z + 1].getEvaluatorID();

				evalCounter = evalCounter + 1;

				if(eval1 != eval2)
				{
					evalCountReport.WriteLine("Evaluator: " + eval1 + " Number of Evals: " + evalCounter);
					evalCounter = 0;
				}
			}

			Console.WriteLine("Report sucessfully generated.");
			Console.ReadKey();
		}
	}
}