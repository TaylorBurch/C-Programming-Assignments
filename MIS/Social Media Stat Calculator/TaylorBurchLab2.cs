using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaylorBurchLab2
{
    class TaylorBurchLab2
    {
        static void Main(string[] args)
        {
            //initializing
            int likes = 0;
            int retweets = 0;
            int shares = 0;
            int rate = 0;
            int totalFollowers = 0;
            string inputStringFollowers = "";
            string inputStringLikes = "";
            string inputStringRetweets = "";

            Console.WriteLine("Enter your total Followers");
            inputStringFollowers = Console.ReadLine();
            totalFollowers = int.Parse(inputStringFollowers);

            Console.WriteLine("Enter number of likes");
            inputStringLikes = Console.ReadLine();
            likes = int.Parse(inputStringLikes);

            Console.WriteLine("Enter number of Retweets");
            inputStringRetweets = Console.ReadLine();
            retweets = int.Parse(inputStringRetweets);

            shares = reweets + likes;
            rate = (shares/totalFollowers) * 100;

            Console.WriteLine(rate); 
            Console.ReadLine();

           
            
        }
    }
}
