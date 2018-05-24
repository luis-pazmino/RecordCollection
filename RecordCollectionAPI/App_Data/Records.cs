using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace RecordCollectionAPI
{
    public class Record
    {
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Gender { get; set; }

        public string FavoriteColor { get; set; }

        public DateTime DateOfBirth { get; set; }

        public static List<Record> fileSort(string typeOfSort, List<Record> recordStore)
        {
            List<Record> sortedList = new List<Record>();
            switch (typeOfSort)
            {
                case "gender":
                    sortedList = recordStore.OrderBy(o => o.Gender).ThenBy(o => o.LastName).ToList();
                    break;
                case "dateofbirth":
                    sortedList = recordStore.OrderBy(o => o.DateOfBirth).ToList();
                    break;
                case "lastname":
                    sortedList = recordStore.OrderByDescending(o => o.LastName).ToList();
                    break;
            }
            return sortedList;
        }

        public static bool AddRecord(string data, MemoryCache cache)
        {
            try
            {
                char delimiter = findDelimiter(data);
                var cacheObj = cache.Get("recordStore");
                List<Record> recordStore = new List<Record>();
                if (cacheObj != null)
                {
                    recordStore = (List<Record>)cacheObj;
                }

                Record singleRecord = new Record();
                string[] lineSplit = data.Split(delimiter);

                singleRecord.LastName = lineSplit[0];
                singleRecord.FirstName = lineSplit[1];
                singleRecord.Gender = lineSplit[2];
                singleRecord.FavoriteColor = lineSplit[3];
                singleRecord.DateOfBirth = Convert.ToDateTime(lineSplit[4]);

                recordStore.Add(singleRecord);
                cache.Add("recordStore", recordStore, DateTimeOffset.UtcNow.AddHours(1));
                return true;
            }
            catch (Exception ex)
            {
                return false;
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
    }
}
