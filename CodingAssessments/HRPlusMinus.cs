using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPlusMinus
{
    class Result
    {

        /*
         * Complete the 'plusMinus' function below.
         *
         * The function accepts INTEGER_ARRAY arr as parameter.
         */

        public static void plusMinus(List<int> arr)
        {
            arr.Sort();
            int firstZeroIndex = -1;
            int firstPositiveIndex = -1;
            int countNegatives = -1;
            int countZeroes = -1;
            int countPositives = -1;

            //with the sorted array, the first element and last element will be negative and positive, respectively, if they exist
            bool containsNegatives = arr[0] < 0;
            bool containsPositives = arr[arr.Count - 1] > 0;

            if (arr.Contains(0))
            {

                if (!containsNegatives)
                {
                    countNegatives = 0;

                    if (!containsPositives)
                    {
                        countPositives = 0;

                        //if the array contains neither negatives nor positives, it is all 0s
                        countZeroes = arr.Count;
                    }
                    else
                    {
                        //array contains only 0s and positives
                        firstPositiveIndex = arr.LastIndexOf(0) + 1;

                        countZeroes = firstPositiveIndex;//we dont need -1 here because index starts at 0
                        countPositives = arr.Count - firstPositiveIndex;
                    }
                }
                else if (!containsPositives)
                {
                    countPositives = 0;

                    //array contains only 0s and negatives
                    firstZeroIndex = arr.IndexOf(0);

                    countNegatives = firstZeroIndex;
                    countZeroes = arr.Count - firstZeroIndex;
                }
                else
                {
                    //array contains negatives, zeroes, and positives
                    firstZeroIndex = arr.IndexOf(0);
                    firstPositiveIndex = arr.LastIndexOf(0) + 1;

                    countPositives = arr.Count - firstPositiveIndex;
                    countZeroes = firstPositiveIndex - firstZeroIndex;
                    countNegatives = firstZeroIndex;
                }
            }
            //array does not contains 0s
            else
            {
                countZeroes = 0;

                if (!containsNegatives)
                {
                    countNegatives = 0;
                    countPositives = arr.Count;
                }
                else if (!containsPositives)
                {
                    countPositives = 0;
                    countNegatives = arr.Count;
                }
                else
                {
                    firstPositiveIndex = arr.FindIndex(i => i > 0);

                    countNegatives = firstPositiveIndex;
                    countPositives = arr.Count - firstPositiveIndex;
                }
            }

            NumberFormatInfo precision = new NumberFormatInfo();
            precision.NumberDecimalDigits = 6;

            float totalElements = arr.Count;
            Console.WriteLine((countPositives / totalElements).ToString("N", precision));
            Console.WriteLine((countNegatives / totalElements).ToString("N", precision));
            Console.WriteLine((countZeroes / totalElements).ToString("N", precision));
        }

    }

    class Solution
    {
        public static void HRPlusMinusMain(string[] args)
        {
            //int n = Convert.ToInt32(Console.ReadLine().Trim());

            //List<int> arr = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt32(arrTemp)).ToList();

            List<int> arr;

            //case 0
            arr = new List<int> { -4, 3, -9, 0, 4, 1 };

            //case 1
            //arr = new List<int> { 1, 2, 3, -1, -2, -3, 0, 0 };

            Result.plusMinus(arr);
        }
    }
}
