using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Variables
            string startTime = ""; //Enter the starting time
            int endMinutes = 0; //End time's minutes
            int endHours = 0; //End time's hours
            string endTime = ""; //End time output to user


            //Main Control
            bool enterTimeQuestion = getEnterTimeQuestion(); //Ask user if they have a time to enter.
            while (enterTimeQuestion == true) //Loop until the user no longer has a calculation they wish to preform
            {
                startTime = getStartTime(); //Get the starting time for calculation
                TimeSpan startTs = TimeSpan.Parse(startTime); //Convert the user's string into int format
                int startHours = startTs.Hours; //Starting hours 
                int startMinutes = startTs.Minutes; //Starting minutes

                int elapsedHours = getElapsedHours(); //Get number of hours that have elapsed
                int elapsedMinutes = getElapsedMinutes(); //Get number of minutes that have elapsed
                string elapsedTime = getElapsedTime(elapsedHours, elapsedMinutes); //Put elapsed hours and elapsed minutes together for final string

                endMinutes = getEndMinutes(startMinutes, elapsedMinutes); //Perform calculation to get end time minutes
                endHours = getEndHours(startHours, elapsedHours); //Perform calculation to get end time hours
                endTime = getEndTime(endMinutes, endHours); //Combine calculations to get final time 

                Console.WriteLine("You started with the time: " + startTime);
                Console.WriteLine("The elapsed time was: " + elapsedTime);
                Console.WriteLine("The ending time is: " + endTime);

                Console.ReadKey();

                enterTimeQuestion = getEnterTimeQuestion(); //Ask if user has another time to enter

            }

            
        }

        public static bool getEnterTimeQuestion()
        {
            string userInput = "";
            char userInputChar = 'N';
            Console.WriteLine("Do you want to enter a time? Enter Y for Yes, any other key for No.");
            userInput = Console.ReadLine();
            userInputChar = char.Parse(userInput);

            if (userInputChar == 'Y' || userInputChar == 'y')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string getStartTime()
        {
            string startTime = "";

            Console.WriteLine("Please enter your time. Enter it as shown in the example.");
            Console.WriteLine("Enter in 00:00 format. Example: 08:19 ");
            startTime = Console.ReadLine();

            return startTime;

        }

        public static bool tryStartParse(TimeSpan startTs)
        {

            if (TimeSpan.TryParse("00:00", out startTs))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int getElapsedHours()
        {
            int elapsedHours = 0;
            Console.WriteLine("How many HOURS have elapsed? Please enter only the hours. Ex: 00");
            elapsedHours = int.Parse(Console.ReadLine());
            return elapsedHours;
        }

        public static int getElapsedMinutes()
        {
            int elapsedMinutes = 0;
            Console.WriteLine("How many MINUTES have elapsed?");
            elapsedMinutes = int.Parse(Console.ReadLine());

            if (elapsedMinutes > 59)
            {
                Console.WriteLine("This value is not correct. Please enter up to 59 minutes.");
                while (elapsedMinutes > 59)
                {
                    elapsedMinutes = 0;
                    Console.WriteLine("How many MINUTES have elapsed?");
                    elapsedMinutes = int.Parse(Console.ReadLine());
                }
            }
            return elapsedMinutes;
        }

        public static string getElapsedTime(int elapsedHours, int elapsedMinutes)
        {
            string elapsedTime = elapsedHours + ":" + elapsedMinutes;
            return elapsedTime;
        }

        public static int getEndMinutes(int startMinutes, int elapsedMinutes)
        {
            int endMinutes = 0;
            endMinutes = startMinutes + elapsedMinutes;
            return endMinutes;
        }

        public static int getEndHours(int startHours, int elapsedHours)
        {
            int endHours = 0;
            endHours = startHours + elapsedHours;
            return endHours;
        }

        public static string getEndTime(int endMinutes, int endHours)
        {
            string endTime = "";
            while (endMinutes >= 60)
            {
                endMinutes = endMinutes - 60;
                endHours = endHours + 1;
            }
            while (endHours >= 24)
            {
                endHours = endHours - 24;
            }

            if (endMinutes < 10)
            {
                endTime = endHours + ":0" + endMinutes;
            }
            else
            {
                endTime = endHours + ":" + endMinutes;
            }
            return endTime;
        }
    }
}
