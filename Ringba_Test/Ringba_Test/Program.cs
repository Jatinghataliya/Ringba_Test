using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Ringba_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //string fPath = @"C:\Users\Administrator\source\repos\JatinApp\JatinApp\InputFile.txt";
            //string file = File.ReadAllText(fPath);

            //   string content = "TheCatWillRunBigCatWillToEatCat";
            //  Console.WriteLine($"Input string: {content}");

            WebRequest request = WebRequest.Create("https://ringba-test-html.s3-us-west-1.amazonaws.com/TestQuestions/output.txt");
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string content = reader.ReadToEnd();
            
            reader.Close();
            dataStream.Close();
            response.Close();


            string fileStream = Regex.Replace(content, "[A-Z]", " $0").Trim();

            //1
            int totalChar = fileStream.ToCharArray().Count();

            Console.WriteLine($"Total letters: {content.Length}");

            //2
            int totalCapital = fileStream.Count(c => char.IsUpper(c));
            Console.WriteLine($"Total capitalized letters: {totalCapital}");

            //3
            var streamArray = fileStream.Split(' ');
            var nameGroup = streamArray.GroupBy(x => x);
            var maxCount = nameGroup.Max(g => g.Count());
            string mostCommon = nameGroup.Where(x => x.Count() == maxCount).Select(x => x.Key).ToArray().First();
            Console.WriteLine($"Most common word: {mostCommon} seen by {maxCount} times");

            //4
            List<string> list = new List<string>();
            foreach (var item in streamArray.ToList().Where(a => a.Length > 2))
            {
                list.Add(item.Substring(0, 2));
            }

            Dictionary<string, int> keyValues = new Dictionary<string, int>();

            foreach (var item in list)
            {

                if (!keyValues.ContainsKey(item))
                {
                    int count = list.Where(a => a == item).Count();
                    if (count > 1)
                    {
                        keyValues.Add(item, count);
                    }
                }
            }

            StringBuilder @string = new StringBuilder();
            foreach (var item in keyValues)
            {
                @string.Append($"{item.Key}=>{item.Value}\n");
            }
            Console.WriteLine(@string);
            Console.Read();
        }
    }
}