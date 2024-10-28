using SnakeCaseAssessment;
using System.Text;
using System.Text.RegularExpressions;

namespace CodingAssessments
{
    internal class CodingAssessments
    {
        static void Main(string[] args)
        {
            //if you want to see a specific assessment run, i recommend just commenting the rest out and running it that way
            AmazonAssessmentLogThreshold.Solution.ThresholdMain(args);
            AmazonServerReplacement.Solution.ServerReplacementMain(args);
            AmazonWeightBlockSorting.Solution.WeightSortingMain(args);
            SnakeCaseAssessment.SnakeCaseAssessment.SnakeCaseMain(args);
        }
    }
}