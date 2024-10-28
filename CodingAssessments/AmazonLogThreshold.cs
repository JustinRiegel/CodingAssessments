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

namespace AmazonAssessmentLogThreshold;

class Result
{

    /*
     * Complete the 'processLogs' function below.
     *
     * The function is expected to return a STRING_ARRAY.
     * The function accepts following parameters:
     *  1. STRING_ARRAY logs
     *  2. INTEGER threshold
     *
     *  the list is, at most, 100,000 entries long.
     *  the threshold is between 1 and the length of the logs list, inclusive
     *  the log entries are only numeric characters, and all start with a non-zero digit
     *  each piece of a log entries is at most 9 digits, and they are separated by a space
     *  check if either the sending OR receiving user is over the threshold
     */


    public static List<string> processLogs(List<string> logs, int threshold)
    {
        string senderUserId;
        string receiverUserId;
        string amount;
        string[] splitLog;
        //holds the userId and the number of times is occurs
        Dictionary<string, int> userIdOccurranceRate = new Dictionary<string, int>();

        foreach (string logEntry in logs)
        {
            splitLog = logEntry.Split(' ');
            senderUserId = splitLog[0];
            receiverUserId = splitLog[1];
            amount = splitLog[2];//do we even need the amount? not for this problem, i dont think

            //check if the sender is in the occurrance rate dictionary.
            //if it is not in there, add it with a value of 1 (as we have now encountered it)
            //otherwise, increment the value by 1
            if (!userIdOccurranceRate.ContainsKey(senderUserId))
            {
                userIdOccurranceRate.Add(senderUserId, 1);
            }
            else
            {
                userIdOccurranceRate[senderUserId]++;
            }

            //do the same as above for the receiver
            if (!userIdOccurranceRate.ContainsKey(receiverUserId))
            {
                userIdOccurranceRate.Add(receiverUserId, 1);
            }
            else
            {
                userIdOccurranceRate[receiverUserId]++;
            }
        }

        //i was initially sorting this array of userIdOccurrances, but i have to believe it is more efficient to just check each element once, rather than sorting the array and then checking a subset of unknown length
        List<string> userIdList = new List<string>();
        
        //KeyValuePair in the dictionary is userId and # of occurrances
        foreach(KeyValuePair<string, int> userIdOccurrance in userIdOccurranceRate)
        {
            if (userIdOccurrance.Value >= threshold)
            {
                userIdList.Add(userIdOccurrance.Key);
            }
        }

        return userIdList;
    }

}

class Solution
{
    public static void ThresholdMain(string[] args)
    {
        //simulate the input values
        int logsCount = 4;

        List<string> logs = new List<string>();

        int threshold;

        //case 1
        logs.Add("1 2 50");
        logs.Add("1 7 70");
        logs.Add("1 3 20");
        logs.Add("2 2 17");
        threshold = 2;

        //case 2
        //logs.Add("9 7 50");
        //logs.Add("22 7 20");
        //logs.Add("33 7 50");
        //logs.Add("22 7 30");
        //threshold = 3;

        //send the inputs to the processing method
        List<string> result = Result.processLogs(logs, threshold);

        //output the results
        Console.WriteLine(String.Join("\n", result));
    }
}
