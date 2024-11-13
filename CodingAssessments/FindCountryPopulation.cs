using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace FindCountryPopulation
{
    class Result
    {

        /*
         * Complete the 'findCountry' function below.
         *
         * The function is expected to return a STRING_ARRAY.
         * The function accepts following parameters:
         *  1. STRING region
         *  2. STRING keyword
         * Base URL for copy/paste: 
         * https://jsonmock.hackerrank.com/api/countries/search?region={region} &name={keyword}
         */


        public static List<string> findCountry(string region, string keyword)
        {
            //For some reason, test cases 3, 7, and 10 are failing on the browser, yet I get the expected response when I run the code in my local IDE.
            //I genuinely have no idea how to troubleshoot this as it seems my code is correct.
            //Locally, I changed the Solution class to mimic command line input, but that should have no bearing on this class or function.
            List<string> returnList = new List<string>();

            string uri = $"https://jsonmock.hackerrank.com/api/countries/search?region={region.ToLower()}&name={keyword.ToLower()}";
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(uri);

            Dictionary<string, int> countryPopDictionary = new Dictionary<string, int>();

            int currentPage = 1;
            int totalPages = 1;

            //use a do-while to ensure the body is executed at least once
            do
            {
                Console.WriteLine($"current page: {currentPage}, total pages: {totalPages}");
                //get the result of the query for the specified page
                HttpResponseMessage response = httpClient.GetAsync(uri + $"&page={currentPage}").Result;
                if (response.IsSuccessStatusCode)
                {
                    string rawData = response.Content.ReadAsStringAsync().Result;
                    JObject jobject = JObject.Parse(rawData);
                    currentPage = Int32.Parse(jobject.SelectToken("page").ToString());
                    totalPages = Int32.Parse(jobject.SelectToken("total_pages").ToString());

                    //for each object in the "data" field, which will be a country, get the name and population and store it in the dictionary
                    var resultCountries = jobject.SelectToken("data");
                    foreach (JObject thingie in resultCountries)
                    {
                        countryPopDictionary.Add(thingie.SelectToken("name").ToString(), Int32.Parse(thingie.SelectToken("population").ToString()));
                    }
                }
                currentPage++;
            } while (currentPage < totalPages);

            //sort the dictionary by population
            var sortedPopulationDictionary = from item in countryPopDictionary orderby item.Value ascending select item;

            foreach (var item in sortedPopulationDictionary)
            {
                returnList.Add(item.Key + "," + item.Value);
            }

            return returnList;
        }

    }

    class Solution
    {
        public static void FindCountryPopulationMain(string[] args)
        {
            //TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);


            string region = "";
            string keyword = "";
            int testCase = 0;

            switch (testCase)
            {
                case 0:
                    region = "Europe";
                    keyword = "de";
                    break;

                case 1:
                    region = "Asia";
                    keyword = "ab";
                    break;

                case 3:
                    region = "Africa";
                    keyword = "ia";
                    break;

                case 7:
                    region = "Europe";
                    keyword = "ia";
                    break;

                case 10:
                    region = "Asia";
                    keyword = "an";
                    break;
                default:
                    region = "Europe";
                    keyword = "de";
                    break;

            }


            List<string> result = Result.findCountry(region, keyword);
            result.ForEach(Console.WriteLine);

            //textWriter.WriteLine(String.Join("\n", result));

            //textWriter.Flush();
            //textWriter.Close();
        }
    }
}



/* QUESTION 2 SQL answer
 SET NOCOUNT ON;


--Enter your query below.
--Please append a semicolon ";" at the end of the query


--I am using table variable here even though I know it would be faster to use temp tables because, to be honest, I dont remember the exact syntax to create a temp table off the top of my head
--In a real world scenario, I would simply look it up, but I am supposed to not do that in this and i am not going to waste my very limited time trying stuff until I do remember the syntax.
--I will simply use what I know can get the job done and move on to the next problem.
--Similarly, I know this is probably not the most efficient answer. In a real world scenario, I would invest more time trying to optimize and clean this up, but I have 2 more problems to get to.
DECLARE @BirthCountTable as TABLE
(
    Count_Birth int,
    [Year] int
);

DECLARE @DeathCountTable as TABLE
(
    Count_Death int,
    [Year] int
);

INSERT INTO @BirthCountTable 
(
    Count_Birth,
    [Year]
)
SELECT COUNT(id) AS Count_Birth, [Year] FROM records WHERE [type] = 'birth' GROUP BY [Year];

INSERT INTO @DeathCountTable 
(
    Count_Death,
    [Year]
)
SELECT COUNT(id) AS Count_Death, [Year] FROM records WHERE [type] = 'death' GROUP BY [Year];

SELECT TOP(1) bct.[Year], SUM(bct.Count_Birth - dct.Count_Death) OVER (ORDER BY bct.[Year]) AS CumulativeCount FROM @BirthCountTable bct
INNER JOIN @DeathCountTable dct
ON dct.[Year] = bct.[Year]
ORDER BY [CumulativeCount] DESC, [Year] ASC;
go
*/