using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

namespace AmazonServerReplacement
{
    class Result
    {

        /*
         * Complete the 'getTotalRequests' function below.
         *
         * The function is expected to return an INTEGER_ARRAY.
         * The function accepts following parameters:
         *  1. INTEGER_ARRAY server
         *  2. INTEGER_ARRAY replaceId
         *  3. INTEGER_ARRAY newId
         */

        public static List<int> getTotalRequests(List<int> server, List<int> replaceId, List<int> newId)
        {
            List<int> requestCountByDay = new List<int>();
            int currentDayRequestCount;

            //n, the count of days, is not passed in, but all the lists are of size n, so using any of them will accomplish the goal here
            for (int i = 0; i < replaceId.Count; i++)
            {
                //reset the day's count
                currentDayRequestCount = 0;

                //loop over the server ids to see what needs to be replaced
                for (int j = 0; j < server.Count; j++)
                {
                    //if the server id matches the id to be replaced today, replace it
                    if (server[j] == replaceId[i])
                    {
                        server[j] = newId[i];
                    }

                    //add the server id to the day's count
                    currentDayRequestCount += server[j];
                }

                //done looping over server ids, add the count to the return list
                requestCountByDay.Add(currentDayRequestCount);
            }

            return requestCountByDay;
        }

    }

    class Solution
    {
        public static void ServerReplacementMain(string[] args)
        {
            //simulate the input values

            //these counts were originally used by the template i was provided with for reading values in from the console. i do not used them for simulating the console input
            int serverCount;// = Convert.ToInt32(Console.ReadLine().Trim());
            int replaceIdCount;// = Convert.ToInt32(Console.ReadLine().Trim());
            int newIdCount;// = Convert.ToInt32(Console.ReadLine().Trim());

            List<int> server = new List<int>();
            List<int> replaceId = new List<int>();
            List<int> newId = new List<int>();

            //case 1
            server.Add(3);
            server.Add(3);
            replaceId.Add(3);
            replaceId.Add(1);
            newId.Add(1);
            newId.Add(5);

            //case 2
            //server.Add(2);
            //server.Add(5);
            //server.Add(2);
            //replaceId.Add(2);
            //replaceId.Add(5);
            //replaceId.Add(3);
            //newId.Add(3);
            //newId.Add(1);
            //newId.Add(5);

            //send the inputs to the processing method
            List<int> result = Result.getTotalRequests(server, replaceId, newId);

            //output the results
            Console.WriteLine(String.Join("\n", result));
        }
    }
}