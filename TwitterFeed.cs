using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

#region Author Info
///-----------------------------------------------------------------
///   Namespace:      Coding_Assignment
///   Class:          TwitterFeed
///   Description:    TwitterFeed contains two file reader and a display tweet methods. It basically reads two files, one contain users and their followers and the other containing tweets from the users
///   Author:         Yandisa Mtimba                    Date: 08/03/2018
///   Email:          ymtimba04@gmail.com
///-----------------------------------------------------------------
#endregion

namespace Coding_Assignment
{
    /// <summary>
    /// TwitterFeed contains two file reader and a display tweet methods
    /// </summary>
    class TwitterFeed
    {
        // declare variables within the class scope
        private ArrayList userLines, tweetLines;
        private static ArrayList userList;
        private static SortedList<string,string> followersList;

        #region User Data File Reader

        /// <summary>UserReader is a protected method in the TwitterFeed class.
        /// <para>This reads the file content and store it in a user ArrayList.</para>
        /// </summary>
        /// <param name="userFilePath">Used as a file path string, for a file to be read and processed.</param>
        /// <returns>Returns ArrayList</returns>
        ///
        protected ArrayList UserReader(string userFilePath)
        {
            // Initialize user array list
            userLines = new ArrayList();
            try
            {
                // Read all file content into a string array and close the file
                string[] lines = File.ReadAllLines(userFilePath);

                // Loop trhough file content array and add it to the list of users
                foreach (string line in lines)
                {
                    // Accumulate user list
                    userLines.Add(line);
                }
            }
            catch(IOException)
            {
                throw;
            }
            // Return the user list
            return userLines;
        }
        #endregion

        #region Tweet Data File Reader

        /// <summary>TweetReader is a protected method in the TwitterFeed class.
        /// <para>This reads the file content and store it in a tweet ArrayList.</para>
        /// </summary>
        /// <param name="tweetFilePath">Used as a file path string, for a file to be read and processed.</param>
        /// <returns>Returns ArrayList</returns>
        ///
        protected ArrayList TweetReader(string tweetFilePath)
        {
            // Initialize tweet array list
            tweetLines = new ArrayList();
            try
            {
                // Read all file content into a string array and close the file
                string[] lines = File.ReadAllLines(tweetFilePath);

                // Loop trhough file content array and add it to the list of tweets
                foreach (string line in lines)
                {
                    // Accumulate tweet list
                    tweetLines.Add(line);
                }
            }
            catch (IOException)
            {
                throw;
            }
            // Return the tweet list
            return tweetLines;
        }
        #endregion

        #region Tweet Display 

        /// <summary>DisplayTweets is a method in the TwitterFeed class.
        /// <para>This manipulates file outputs, process, and print the tweets.</para>
        /// </summary>
        /// <param name="userFilePath">Used as a user file path string, for a file to be read and processed.</param>
        /// <param name="tweetFilePath">Used as a tweet file path string, for a file to be read and processed.</param>
        /// <returns>Returns void</returns>
        ///
        public static void DisplayTweets(string userFilePath, string tweetFilePath)
        {
            // Instantiate the TwitterFeed class
            TwitterFeed objTwitterFeed  = new TwitterFeed();

            // Read the file list from "user.txt"
            ArrayList userData          = objTwitterFeed.UserReader(userFilePath);

            // Declare a user list variable to store cleaned user list
            userList                    = new ArrayList();

            // Initiate the followers list
            followersList               = new SortedList<string, string>();

            // Loop through the data from the user file to process it
            for (int i = 0; i < userData.Count; i++)
            {
                // Split the user data read from the file by the word "follows"
                string[] userSplitted = userData[i].ToString().Split(new [] {"follows"}, StringSplitOptions.None);

                // Check if user is not on the list and if not, add the user to the list
                if (!userList.Contains(userSplitted[0].Trim()))
                {
                    userList.Add(userSplitted[0].Trim());
                }

                // Split the user followers by comma
                string[] followersSplitted = userSplitted[1].Trim().Split(',');

                // Loop through splited user followers list
                for (int j = 0; j < followersSplitted.Length; j++)
                {
                    // Check if a follower is also on the twitter user list, if not add the follower as a user
                    if (!userList.Contains(followersSplitted[j].Trim()))
                    {
                        userList.Add(followersSplitted[j].Trim());
                    }

                    // Fill in the followers list
                    followersList.Add(userSplitted[0].Trim() + "_" + i + "_" + j, followersSplitted[j].Trim());
                }
            }

            // Sort the user list alphabetically
            userList.Sort();

            // Loop through the users and print each user's tweets plus the user's followers tweets
            for (int i = 0; i < userList.Count; i++)
            {
                // Get the tweets array list
                ArrayList tweetData = objTwitterFeed.TweetReader(tweetFilePath);

                // Print user name
                Console.WriteLine(userList[i].ToString());

                // Loop through all tweets and display the ones relevant to the user
                for (int j = 0; j < tweetData.Count; j++)
                {
                    // A bollean variable to check whether a tweet has been printed
                    bool printed = false;

                    // Seperate the tweet by greater than sign to get the user and the tweet
                    string[] tweetSplitted = tweetData[j].ToString().Trim().Split('>');

                    // Check if current user has a tweet
                    if (tweetSplitted[0].Trim().Equals(userList[i].ToString()))
                    {
                        Console.WriteLine("\t@{0}: {1}",userList[i].ToString().Trim(), tweetSplitted[1].Trim());
                        printed = true;
                    }
                    // Check if the user's followers have tweets to print
                    else
                    {
                        // Current read tweet has not been printed
                        if (!printed)
                        {
                            // If you need to find the string associated with the user in followers list
                            foreach (KeyValuePair<string, string> follower in followersList)
                            {
                                // Split the followersList key and get the user name
                                string[] keySplitted = follower.Key.Split('_');

                                // Check if the current user has a follower with the tweet to print and if yes, print it
                                if (keySplitted[0].Equals(userList[i].ToString()) && follower.Value.Equals(tweetSplitted[0].Trim()))
                                {
                                    Console.WriteLine("\t@{0}: {1}", tweetSplitted[0].Trim(), tweetSplitted[1].Trim());
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
