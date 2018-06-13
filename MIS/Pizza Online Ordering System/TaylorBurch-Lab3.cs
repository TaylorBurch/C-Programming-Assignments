using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaylorBurchLab3
{
    class Program
    {
        // Constants
        const double TAX_RATE = .08;     //Sales Tax Rate
        const double TOP_COST = 1.25;   //Cost of Additional Toppints
        const double DISCOUNT = -2.00;

        static void Main(string[] args)
        {


            //Variables
            String firstName;               //User's first name
            bool discount = false;          //Flag for discount
            int inches;                     //Size of the pizza
            String crust = "Hand-Tossed";   //Name of crust
            double cost = 12.99;            //Cost of the pizza
            double tax;                     //Amount of tax
            String toppings = "Cheese ";    //List of toppings
            int numberOfToppings = 0;       //Number of toppints

            // Main flow of control

            firstName = getFirstName();                     // Get user first name
            discount = determineDiscount(firstName);        // Decide if user should get a discount
            inches = getPizzaSize();                        // Prompt user and get pizza size
            cost = getBaseCost(inches);                     // Set cost based on size of pizza ordered
            crust = getCrustType();                         // Prompt user and get crust type
            toppings = getToppingsList(ref numberOfToppings, toppings);   // Prompt user and get topping list

            // Display order confirmation
            Console.WriteLine("Your order is as follows: ");
            Console.WriteLine(inches + " inch pizza");
            Console.WriteLine(crust + " crust");
            Console.WriteLine(toppings);

            cost = updateCost(cost, numberOfToppings * TOP_COST);   // update cost to reflect additional toppings

            if (discount)                                   //if user is due a discount tell them and update cost
            {
                Console.WriteLine("You appear to be eligible for a " + DISCOUNT * -1 + " dolloar discount.");
                cost = updateCost(cost, DISCOUNT);          //update cost with discount
            }

            Console.WriteLine("The cost of your order is: " + cost);
            tax = cost * TAX_RATE;                            // calculate taxes owed
            Console.WriteLine("The tax is: " + tax);

            cost = updateCost(cost, tax);                   //update cost with taxes

            Console.WriteLine("The total due is: " + cost);

            Console.ReadKey();
        }

        //Method to get the users first name
        static String getFirstName()
        {

            //Prompt user and get first name
            Console.WriteLine("Welcome to Mike and Diane's Pizza");
            Console.Write("Enter your first name: ");
            String userInput = Console.ReadLine();

            // Return the name
            return userInput;
        }


        //Method to decide if user should get discount
        static bool determineDiscount(String firstName)
        {

            bool discount = false;  // local variable to determine if user is eligible
            string nameDiane = "Diane";
            string nameMike = "Mike";
            if (nameDiane.CompareTo(firstName) == 0)
            {
                discount = true;  // local variable to determine if user is eligible
            }
            else if (nameMike.CompareTo(firstName) == 0)
            {
                discount = true;  // local variable to determine if user is eligible
            }
            else
            {
                discount = false;  // local variable to determine if user is eligible
            };

            return discount;

        }

        //Method to get pizza size choice
        static int getPizzaSize()
        {
            //Prompt user and get pizza size choice
            Console.WriteLine("Pizza Size (inches)       Cost");
            Console.WriteLine("        10                $10.99");
            Console.WriteLine("        12                $12.99");
            Console.WriteLine("        14                $14.99");
            Console.WriteLine("        16                $16.99");
            Console.WriteLine("What size pizza would you like?");
            Console.WriteLine("enter the number only :");

            String userInput = Console.ReadLine();

            return int.Parse(userInput);  // Cast and return the size
        }

        // Method to set base cost based on size
        static double getBaseCost(int inches)
        {

            double cost = 10.99;

            switch (inches)
            {
                case 10:
                    cost = 10.99;
                    break;
                case 12:
                    cost = 12.99;
                    break;
                case 14:
                    cost = 14.99;
                    break;
                case 16:
                    cost = 16.99;
                    break;
                default:
                    cost = 10.99;
                    Console.WriteLine("You entered an invalid pizza size, you have been defaulted to 10 inch");
                    break;
            }

            return cost;  // return the base cost
        }

        // Method to prompt user and set crust type
        static String getCrustType()
        {
            char userChoice = 'H';      // Default the user choice to Hand-Tossed
            String crust = "Hand-Tossed";

            // Prompt user for choice
            Console.WriteLine("What type of crust do you want?");
            Console.WriteLine("(H)and-tossed, (T)hin-crust, or (D)eep-dish.");
            Console.Write("Enter H, T, or D: ");
            String userInput = Console.ReadLine();
            userChoice = char.Parse(userInput);

            switch (userInput)
            {
                case "T":
                    crust = "Thin-crust";
                    break;
                case "D":
                    crust = "Deep-dish";
                    break;
                default:
                    crust = "Hand-tossed";
                    Console.WriteLine("Defaulted to Hand-tossed.");
                    break;
            }


            return crust;     // return the users crust choice
        }

        //Method to get topping list and number of toppings
        static String getToppingsList(ref int numberOfToppings, String toppings)
        {
            String userInput;
            char choice;

            //Prompt user and get topping choices one at a time
            Console.WriteLine("All pizzas come with cheese.");
            Console.WriteLine("Additional toppings are $1.25 each, " +
                                "choose from:");
            Console.WriteLine("Pepperoni, Sausage, Onion, Mushroom");

            //If topping is desired, add to topping list and number of toppings
            Console.Write("Do you want Pepperoni? (Y/N:) ");
            userInput = Console.ReadLine();
            choice = char.Parse(userInput);
            if (choice == 'Y' || choice == 'y')
            {
                numberOfToppings++;
                toppings = toppings + "Pepperoni ";
            }

            Console.Write("Do you want Sausage? (Y/N:) ");
            userInput = Console.ReadLine();
            choice = char.Parse(userInput);
            if (choice == 'Y' || choice == 'y')
            {
                numberOfToppings++;
                toppings = toppings + "Sausage ";
            }

            Console.Write("Do you want Onion? (Y/N:) ");
            userInput = Console.ReadLine();
            choice = char.Parse(userInput);
            if (choice == 'Y' || choice == 'y')
            {
                numberOfToppings++;
                toppings = toppings + "Onion ";
            }

            Console.Write("Do you want Mushroom? (Y/N:) ");
            userInput = Console.ReadLine();
            choice = char.Parse(userInput);
            if (choice == 'Y' || choice == 'y')
            {
                numberOfToppings++;
                toppings = toppings + "Mushroom ";
            }

            return toppings;
        }

        //Method to update cost
        static double updateCost(double cost, double changeAmount)
        {
            cost = cost + changeAmount;
            return cost;

        }
    }
}