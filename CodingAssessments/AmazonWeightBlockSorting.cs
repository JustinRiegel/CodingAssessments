using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonWeightBlockSorting;

class Result
{
    public static int getMinNumMoves(int blocksSize, List<int> blocks)
    {
        
        //get difference of blocksSize-1 and max, add with min value's index. this is initial minimum moves
        //if max value's index is less than min value's index, subtract 1 from initial minimum moves as the min and max will have to cross each other
        //effectively doing 2 moves with 1 move

        //loop through and fine the minimum value and its index, as well as max and its index
        int smallestBlock = int.MaxValue;
        int smallestBlockIndex = -1;
        int largestBlock = -1;
        int largestBlockIndex = -1;
        int minNumMoves = -1;

        for (int i = 0; i < blocksSize; i++)
        {
            if (blocks[i] < smallestBlock)
            {
                smallestBlock = blocks[i];
                smallestBlockIndex = i;
            }
            else if (blocks[i] > largestBlock)
            {
                largestBlock = blocks[i];
                largestBlockIndex = i;
            }
        }
        
        //if smallest and largest are already at the start and end, respectively. no moves needed
        if(smallestBlockIndex == 0 && largestBlockIndex == blocksSize - 1)
        {
            minNumMoves = 0;
        }
        //if smallest is already at the start, we only need difference between largest and end
        else if(smallestBlockIndex == 0)
        {
            minNumMoves = (blocksSize - 1) - largestBlockIndex;
        }
        //likewise, if the larges is at the end, we only need the difference between the smallest and the start
        else if(largestBlockIndex == blocksSize - 1)
        {
            minNumMoves = smallestBlockIndex;
        }
        //otherwise, just add the differences from the start and end together
        else
        {
            minNumMoves = smallestBlockIndex + ((blocksSize - 1) - largestBlockIndex);
        }
        
        //if the largest block is before the smallest in the stack, at some point they are going to get swapped, resulting in 2 moves happening for the price of 1.
        //this is not factored in until now, so we need to account for it
        if(largestBlockIndex < smallestBlockIndex)
        {
            --minNumMoves;
        }

        return minNumMoves;
    }

}

class Solution
{
    public static void WeightSortingMain(string[] args)
    {
        //simulate the input values

        //this count was originally used by the template i was provided with for reading values in from the console. i do not used it for simulating the console input
        int blocksSize = 5;// Convert.ToInt32(Console.ReadLine().Trim());

        List<int> blocks = new List<int>();

        //case 1
        blocks.Clear();
        blocks.Add(2);
        blocks.Add(4);
        blocks.Add(3);
        blocks.Add(1);
        blocks.Add(6);

        //case 2
        //blocks.Clear();
        //blocks.Add(4);
        //blocks.Add(11);
        //blocks.Add(9);
        //blocks.Add(10);
        //blocks.Add(12);

        //case 3
        //blocks.Clear();
        //blocks.Add(2);
        //blocks.Add(6);
        //blocks.Add(3);
        //blocks.Add(1);
        //blocks.Add(4);

        //send the inputs to the processing method
        int result = Result.getMinNumMoves(blocksSize, blocks);

        //output the results
        Console.WriteLine(String.Join("\n", result));
    }
}