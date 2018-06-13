using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaylorBurchLab7
{
    class Program
    {
        static void Main(string[] args)
        {

            // Declare variables
            int station;         // The user's channel choice

            // Declare and instantiate a television object
            Television bigScreen = new Television("Toshiba", 55);

            // Turn the power on
            bigScreen.power();

            // Display the state of the television
            Console.WriteLine("A " +
                             bigScreen.getScreenSize() +
                             " inch " +
                             bigScreen.getManufacturer() +
                             " has been turned on.");

            // Prompt the user for input and store into station
            Console.Write("What channel do you want? ");
            station = int.Parse(Console.ReadLine());

            // Change the channel on the television
            bigScreen.setChannel(station);
            bigScreen.increaseVolume();


            // Display the the current channel and
            // volume of the television
            Console.WriteLine("Channel: " +
                             bigScreen.getChannel() +
                             " Volume: " +
                             bigScreen.getVolume());
            Console.WriteLine("Too loud! Lowering the volume.");

            // Decrease the volume of the television
            bigScreen.decreaseVolume();
            bigScreen.decreaseVolume();
            bigScreen.decreaseVolume();
            bigScreen.decreaseVolume();
            bigScreen.decreaseVolume();
            bigScreen.decreaseVolume();

            // Display the the current channel and
            // volume of the television
            Console.WriteLine("Channel: " +
                             bigScreen.getChannel() +
                             " Volume: " +
                             bigScreen.getVolume());

            Console.WriteLine();   // For a blank line
            Console.ReadKey();

           


            Television portable = new Television("Sharp", 19); //Declare new TV and instatiate it
            portable.power(); //Turn power on.
            Console.WriteLine("A " +
                            portable.getScreenSize() +
                            " inch " +
                            portable.getManufacturer() +
                            " has been turned on.");


            Console.Write("What channel do you want? "); // Prompt the user for station input
            station = int.Parse(Console.ReadLine()); //Set station
            portable.setChannel(station); //Change to channel specified
            portable.decreaseVolume();
            portable.decreaseVolume(); //Decrease volume two times

            //Display changes to status of TV
            Console.WriteLine("Channel: " + portable.getChannel() +
                             " Volume: " + portable.getVolume());

            Console.WriteLine();
            Console.ReadKey();

        }

    }
}

