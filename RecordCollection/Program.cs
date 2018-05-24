using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordCollection
{
    class Program
    {
        public static List<Record> recordStore = null;
        static void Main(string[] args)
        {
            recordStore = new List<Record>();
            string file1 = Record.fileChecker("First");
            string file2 = Record.fileChecker("Second");
            string file3 = Record.fileChecker("Third");

            Record.fileReader(file1, recordStore);
            Record.fileReader(file2, recordStore);
            Record.fileReader(file3, recordStore);

            List<Record> sortedRecords = new List<Record>();
            sortedRecords = Record.fileSort("gender", recordStore);
            displaySortedData(sortedRecords);
            sortedRecords.Clear();

            sortedRecords = Record.fileSort("birth", recordStore);
            displaySortedData(sortedRecords);
            sortedRecords.Clear();

            sortedRecords = Record.fileSort("lastName", recordStore);
            displaySortedData(sortedRecords);
            sortedRecords.Clear();

            Console.ReadLine();
        }
   
        private static void displaySortedData(List<Record> sortedData)
        {
            foreach (Record data in sortedData)
            {
                Console.WriteLine($"Name:{data.FirstName} {data.LastName} Gender:{data.Gender} Favorite Color:{data.FavoriteColor} Date of Birth:{data.DateOfBirth}");
            }
            Console.WriteLine(Environment.NewLine);
        }
    }
}
