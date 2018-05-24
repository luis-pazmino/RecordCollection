using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordCollection
{
    public class Record
    {
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Gender { get; set; }

        public string FavoriteColor { get; set; }

        public DateTime DateOfBirth { get; set; }

        public static string fileChecker(string fileNumber)
        {
            string fileLocation = string.Empty;
            Console.WriteLine($"Enter location of {fileNumber} file.");
            fileLocation = Console.ReadLine();
            bool fileExists = File.Exists(fileLocation);

            while (!fileExists)
            {
                Console.WriteLine("File does not exist. Please try again.");
                fileLocation = Console.ReadLine();
                fileExists = File.Exists(fileLocation);
            }

            Console.WriteLine($"{fileNumber} File Exists, proceeding...{Environment.NewLine}");
            return fileLocation;
        }

        public static void fileReader(string fileLocation, List<Record> recordStore)
        {           
            string data = File.ReadAllText(fileLocation);
            char delimiter = findDelimiter(data);
            string[] lines = data.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (string line in lines)
            {
                Record singleRecord = new Record();
                string[] lineSplit = line.Split(delimiter);

                singleRecord.LastName = lineSplit[0];
                singleRecord.FirstName = lineSplit[1];
                singleRecord.Gender = lineSplit[2];
                singleRecord.FavoriteColor = lineSplit[3];
                singleRecord.DateOfBirth = Convert.ToDateTime(lineSplit[4]);

                recordStore.Add(singleRecord);
            }
        }

        private static char findDelimiter(string data)
        {
            int maxCount = 0;
            char delimiter = char.MinValue;
            List<char> delimiters = new List<char> { ' ', ',', '|' };
            Dictionary<char, int> counts = delimiters.ToDictionary(key => key, value => 0);
            foreach (char c in delimiters)
            {
                counts[c] = data.Count(t => t == c);
                if (counts[c] > maxCount)
                {
                    maxCount = counts[c];
                    delimiter = c;
                }
            }
            return delimiter;
        }

        public static List<Record> fileSort(string typeOfSort, List<Record> recordStore)
        {
            List<Record> sortedList = new List<Record>();
            switch (typeOfSort)
            {
                case "gender":
                    sortedList = recordStore.OrderBy(o => o.Gender).ThenBy(o => o.LastName).ToList();
                    break;
                case "birth":
                    sortedList = recordStore.OrderBy(o => o.DateOfBirth).ToList();
                    break;
                case "lastName":
                    sortedList = recordStore.OrderByDescending(o => o.LastName).ToList();
                    break;
            }
            return sortedList;
        }
    }
}
