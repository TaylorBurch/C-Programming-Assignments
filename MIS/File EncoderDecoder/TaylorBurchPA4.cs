using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;

namespace TaylorBurch_PA4
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			//EXTRAS:
			//Count characters in a file, not just words
			//Find the number of occurances of a word
			//Option to replace all occurances of a word in the file with a new word
			//Count vowels and consonants in file.
			//Option to create a new file directly from the console application 
			//Option to save encrypted or decrypted data to same file or a new file. 

			//Variables used across menu:
			string dataString = "";
			string fileInput = ""; //Where the file will be stored as a string after reading it in
			char[] keyArray = new char[84]; //The array containing the key for encode/decode process

		
			StreamReader inFileKey = new StreamReader("key.txt"); //Read in the key file. 
			populateKeyArray(keyArray, inFileKey); //Create char array with key file
			inFileKey.Close();

			//********MAIN********

			int menuSelection = displayMenu(); //Get user's menu selection
			while (menuSelection != 5) //Loop through menu until user selects 5 to quit from main menu.
			{
				if (menuSelection == 1) //Encode
				{
					fileInput = readFile(); //Read in user's file.
					char[] charsEncode = fileInput.ToCharArray(); 
					dataString = encodeFile(charsEncode, keyArray); //Method to encode the file. 
					saveFile(dataString); //Save data back to file or a new file.
				}
				else if (menuSelection == 2) //Decode
				{
					fileInput = readFile(); //Read in user's file.
					char[] charsDecode = fileInput.ToCharArray();
					dataString = decodeFile(charsDecode, keyArray); //Method to decode file.
					saveFile(dataString); //Save data back to file or a new file. 
				}
				else if (menuSelection == 3) //Count
				{
					fileInput = readFile(); //Read in user's file.
					getCharCount(fileInput); //Get the character count of file.
					getWordCount(fileInput); //Get word count of file. 
					getWordOccurances(fileInput); //Find number of occurances of selected word in file with option to replace word. 
					countVowelsAndConsonants(fileInput); //Count vowels and consonants.
				}
				else if (menuSelection == 4)
				{
					createNewFile(); //Output a string to a new file created directly from the console application.
				}
				else //Incorrect input error handling 
				{
					Console.WriteLine("Sorry, that is not a correct value. Please try again.");
				}

				menuSelection = displayMenu(); //Display menu again

			}

			Console.WriteLine("Exiting program...");
			Console.ReadKey();

		}

		public static void populateKeyArray(char[] keyArray, StreamReader inFileKey) //Input the key from file into char array
		{
			for (int i = 0; i < keyArray.Length; i++)
			{
				keyArray[i] = char.Parse(inFileKey.ReadLine()); //Create array of characters from key file.
			}

		}

		public static int displayMenu() //Display Main Menu
		{
			int menuSelection = 0;
			Console.WriteLine("Please select an option from the menu below by inputting the corresponding number.");
			Console.WriteLine("Input 1 to encode a file.");
			Console.WriteLine("Input 2 to decode a file.");
			Console.WriteLine("Input 3 to obtain a file's word count.");
			Console.WriteLine("Input 4 to create and output to a new file.");
			Console.WriteLine("Input 5 to quit.");
			menuSelection = int.Parse(Console.ReadLine());
			return menuSelection;
		}

		public static string readFile() //Read in the user's file.
		{
			string fileInput = "";
			Console.WriteLine("Please enter the name of the file you wish to use. Do not forget to include the .txt extension.");
			string fileName = Console.ReadLine();
			if (File.Exists(fileName))
			{
				StreamReader inFile = new StreamReader(fileName);
				fileInput = inFile.ReadToEnd(); //Take in the file and copy it in as a string.
				inFile.Close();
			}
			else //Error handling for incorrect file name input
			{
				Console.WriteLine("File does not exist.");
				bool filePresent = false;
				while (filePresent == false)
				{
					Console.WriteLine("Please enter the name of the file you wish to use. Do not forget to include the .txt extension.");
					fileName = Console.ReadLine();
					if (File.Exists(fileName))
					{
						StreamReader inFile = new StreamReader(fileName);
						fileInput = inFile.ReadToEnd(); //Take in the file and copy it in as a string.
						inFile.Close();
						filePresent = true;
					}
					else
					{
						Console.WriteLine("File does not exist.");
						filePresent = false;
					}
				}
			}

			return fileInput;

		}


		public static string encodeFile(char[] fileChars, char[] key)
		{
			for (int x = 0; x < key.Length; x++)
			{
				for (int y = 0; y < fileChars.Length; y++)
				{
					if (key[x] == fileChars[y])
					{
						if (x <= 42)
						{
							fileChars[y] = key[x + 42];
						}
						else
						{
							fileChars[y] = key[x - 42];
						}
					}
				}

			}

			string dataString = new string(fileChars);
			Console.WriteLine("Data has been sucessfully encrypted: ");
			Console.WriteLine(dataString);
			return dataString;
		}

		public static string decodeFile(char[] fileChars, char[] key)
		{
			for (int x = 0; x < key.Length; x++)
			{
				for (int y = 0; y < fileChars.Length; y++)
				{
					if (key[x] == fileChars[y])
					{
						if (x <= 42)
						{
							fileChars[y] = key[x + 42];
						}
						else
						{
							fileChars[y] = key[x - 42];
						}
					}
				}
			}

			string dataString = new string(fileChars);
			Console.WriteLine("Data has been sucessfully decrypted: ");
			Console.WriteLine(dataString);
			return dataString;
		}

		public static void saveFile(string dataString) //Save data that has been worked on back to same file or new file. 
		{
			Console.WriteLine("Would you like to save this to a new file or existing file? Enter Y to create a new file to save to or any key for No.");
			char userAnswer = char.Parse(Console.ReadLine());
			if (userAnswer == 'y' || userAnswer == 'Y')
			{
				Console.WriteLine("Please enter a file name and extension for the new file you wish to create.");
				string newFileName = Console.ReadLine();
				System.IO.File.WriteAllText(newFileName, dataString);
				Console.WriteLine("File has been created.");
				Console.ReadKey();
			}
			else
			{
				Console.WriteLine("Please enter a name and file extension for the file you wish to save to.");
				string fileName = Console.ReadLine();
				if (File.Exists(fileName))
				{
					System.IO.File.WriteAllText(fileName, dataString);
					Console.WriteLine("File has been created.");
					Console.ReadKey();
				}
				else //Error handling for if file does not exist.
				{
					Console.WriteLine("File does not exist.");
					Console.WriteLine("Enter 1 to try again. Enter 2 to create a new file.");
					int tryAgain = int.Parse(Console.ReadLine());
					if (tryAgain == 1)
					{
						bool filePresent = false;
						while (filePresent == false)
						{
							Console.WriteLine("Please enter a name and file extension for the file you wish to save to.");
							fileName = Console.ReadLine();
							if (File.Exists(fileName))
							{
								System.IO.File.WriteAllText(fileName, dataString);
								Console.WriteLine("File has been created.");
								Console.ReadKey();
								filePresent = true;
								break;
							}
							else
							{
								Console.WriteLine("File does not exist.");
								Console.ReadKey();
								filePresent = false;
							}
						}
					}
					else
					{
						Console.WriteLine("Please enter a file name and extension for the new file you wish to create.");
						string newFileName = Console.ReadLine();
						System.IO.File.WriteAllText(newFileName, dataString);
						Console.WriteLine("File has been created.");
						Console.ReadKey();
					}

				}


			}
		}

		public static void getCharCount(string fileInput) //Count the characters in the file and display to user
		{
			int chars = fileInput.Split().Length;
			int spaceChars = fileInput.Count(Char.IsWhiteSpace);
			int totalChars = chars + spaceChars;
			Console.WriteLine("There are " + totalChars + " characters in this file.");
		}

		public static void getWordCount(string fileInput) //Count words in the file and display to user. 
		{
			int wordCount = 0;
			string[] words = fileInput.Split();
			wordCount = words.Length; 
			Console.WriteLine("There are " + wordCount + " words in this file.");
		}

		public static void countVowelsAndConsonants(string fileInput) //Count the vowels and consonants in file. 
		{
			Console.WriteLine("Would you like to count the vowels and consonants in the file? Enter Y for Yes or any other key to return to main menu.");
			char userInput = char.Parse(Console.ReadLine());
			char[] chars = fileInput.ToCharArray();
			char[] vowels = { 'a', 'e', 'i', 'o', 'u' };
			int vowelCount = 0;
			int consonantCount = 0;

			if (userInput == 'y' || userInput == 'Y')
			{
				for (int x = 0; x < chars.Length; x++)
				{
					for (int y = 0; y < vowels.Length; y++)
					{
						if (chars[x] == vowels[y])
						{
							vowelCount++;
						}
						else
						{
							consonantCount++;
						}
					}
				}
			}
		}

		public static void getWordOccurances(string fileInput) //Search file for number of occurances of the word user wishes to search.
		{
			int wordOccurance = 0;
			int wordsReplaced = 0;
			bool search = true;
			string[] words = fileInput.Split(' ', '.', ',', '!', '?', ':', ';', '\t', '*');

			while (search == true)
			{
				Console.WriteLine("Please enter the word you wish to count the occurances of or hit enter for no input.");
				string searchWord = Console.ReadLine();
				for (int i = 0; i <= words.Length - 1; i++)
				{
					if (words[i] == searchWord)
					{
						wordOccurance++;
						Console.WriteLine("Would you like to replace this word with something else? Enter Y for Yes or any other key for no");
						char userInput = char.Parse(Console.ReadLine());
						if (userInput == 'y' || userInput == 'Y')
						{
							Console.WriteLine("Please enter the word you wish to replace this occurance of " + searchWord + " with.");
							string replacement = Console.ReadLine();
							words[i] = replacement;
							Console.WriteLine(searchWord + " has been replaced with: " + replacement + " at this occurance.");
							wordsReplaced++;
						}
					}

					Console.WriteLine("The search value: " + searchWord + " was found " + wordOccurance + " times in the file.");
					Console.WriteLine(searchWord + " was replaced " + wordsReplaced + " times.");

					Console.WriteLine("Would you like to search for another word? Enter Y for Yes, any other key for No.");
					char userAnswer = char.Parse(Console.ReadLine());
					if (userAnswer == 'y' || userAnswer == 'Y')
					{
						search = true;
					}
					else
					{
						break;
					}
				}

			}
		}

		public static void createNewFile() //Create a new file and output to it directly from console application
		{
			Console.WriteLine("Please enter the data you would like to output to a new file, or hit enter to create a blank file.");
			string userInput = Console.ReadLine();
			Console.WriteLine("Please enter a name for the file. Remember to include the extension, .txt");
			string newFileName = Console.ReadLine();
			System.IO.File.WriteAllText(newFileName, userInput);
			Console.WriteLine("File has been created.");
			Console.ReadKey();
		}

	}
}

