using System.Text;
using System.Text.RegularExpressions;

namespace SnakeCaseAssessment
{
    class SnakeCaseAssessment
    {
        public static void SnakeCaseMain(string[] args)
        {
            //the inputs and expected output are taken from the examples in the questions
            List<string> testInputs = new List<string>
            {
                "my_counter",
                "_the_variable",
                "the_variable__",
                "This is the doc_string for __secret_fun",
                "__variable_one__"
            };

            List<string> expectedOutputs = new List<string>
            {
                "myCounter",
                "_theVariable",
                "theVariable__",
                "This is the docString for __secret_fun",
                "__variableOne__"
            };

            string convertedVariable;
            for (int i = 0; i < testInputs.Count; i++)
            {
                convertedVariable = ConvertSnakeCase(testInputs[i]);
                Console.WriteLine($"input: {testInputs[i]}{Environment.NewLine}expected: {expectedOutputs[i]}{Environment.NewLine}actual: {convertedVariable}");
                Console.WriteLine("--------------------------");
            }
            //this is the provided input string
            //ConvertSnakeCase();
        }

        public static string ConvertSnakeCase(string src)
        {
            //to identify variables, we need to identify individual words which are separated by spaces
            string[] wordsInSrc = src.Split(' ');

            //create a string builder to build the string we will eventually return
            StringBuilder returnString = new StringBuilder();

            for (int i = 0; i < wordsInSrc.Length; i++)
            {
                string currentWord = wordsInSrc[i];

                //if the word does not have a pattern matching snake case, add it to the return string without any modification
                if (Regex.IsMatch(currentWord, $"[a-z][_][a-z]"))
                {
                    //now we know the word needs to be converted from snake case to camel case
                    bool isCharacterBeforeLastUnderscore = false;
                    bool isPreviousCharacterUnderscore = false;
                    bool isCurrentCharacterUnderscore = false;
                    string firstHalf = string.Empty;
                    string secondHalf = string.Empty;

                    while (Regex.IsMatch(currentWord, $"[a-z][_][a-z]"))
                    {
                        //start the loop at 0 so we dont have to do a length check every time, but the minimum characters for a snake case word should be 3
                        for (int j = 0; j < currentWord.Length; j++)
                        {
                            //skip the first 2 characters in the word.
                            if (j == 0 || j == 1)
                            {
                                continue;
                            }

                            //if i was more practiced with regex, i could probably replace using groups of matches in regex, but i am not, so i am doing it this way
                            isCharacterBeforeLastUnderscore = currentWord[j - 2] == '_';
                            isPreviousCharacterUnderscore = currentWord[j - 1] == '_';
                            isCurrentCharacterUnderscore = currentWord[j] == '_';

                            //if this matches the pattern of [a-z]_[a-z], fix the snake case
                            if (!isCharacterBeforeLastUnderscore && isPreviousCharacterUnderscore && !isCurrentCharacterUnderscore)
                            {
                                firstHalf = currentWord.Substring(0, j - 1);
                                secondHalf = currentWord.Substring(j + 1);

                                currentWord = firstHalf + currentWord[j].ToString().ToUpper() + secondHalf;
                            }
                        }
                    }
                }

                returnString.Append(currentWord);
                if (i != wordsInSrc.Length - 1)
                {
                    returnString.Append(' ');
                }
            }

            //Console.WriteLine(returnString.ToString());
            return returnString.ToString();
        }
    }
}