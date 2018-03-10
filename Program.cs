using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

#region Author Info
///-----------------------------------------------------------------
///   Author:         Yandisa Mtimba                    Date: 08/03/2018
///   Email:          ymtimba04@gmail.com
///-----------------------------------------------------------------
#endregion

namespace Coding_Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            // Validate number of passed arguments
            if (args.Length == 2)
            {
                try
                {
                    // Call the method that does file validation and run the program
                    Go(args[0], args[1]);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Arguments Error: The program expects 2 file paths arguments and you have suppled {0}", args.Length);
            }
            Console.ReadLine();
        }

        #region Program Runner

        // The entry method to the twitter program
        public static void Go(string userFilePath, string tweetFilePath)
        {
            // Validate user file path
            if (!File.Exists(userFilePath))
            {
                Console.WriteLine("Error: Invalid user file path: {0}", userFilePath);
            }

            // Validate user file name and extension
            if (!Path.GetFileName(userFilePath).ToLower().Equals("user.txt"))
            {
                Console.WriteLine("Error: Invalid file name, the expected first argument file name is user.txt and you supplied {0}", Path.GetFileName(userFilePath));
            }

            // Validate user file path
            if (!File.Exists(tweetFilePath))
            {
                Console.WriteLine("Error: Invalid tweet file path: {0}", tweetFilePath);
            }

            // Validate user file name and extension
            if (!Path.GetFileName(tweetFilePath).ToLower().Equals("tweet.txt"))
            {
                Console.WriteLine("Error: Invalid file name, the expected second argument file name is tweet.txt and you supplied {0}", Path.GetFileName(tweetFilePath));
            }

            try
            {
                // Process and display the tweets
                TwitterFeed.DisplayTweets(userFilePath, tweetFilePath);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
