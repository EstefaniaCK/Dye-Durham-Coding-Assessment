using System;
using System.Collections.Generic;
using System.IO;

namespace NameSorter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide a file name.");
                return;
            }

            string filePath = args[0];

            string[] unsortedNames = File.ReadAllLines(filePath);
            string[] sortedNames = SortNames(unsortedNames);

            PrintSortedNames(sortedNames);
            SaveSortedNamesToFile(sortedNames);
        }

        static string[] SortNames(string[] unsortedNames)
        {
            Array.Sort(unsortedNames, new NameComparer());
            return unsortedNames;
        }

        static void PrintSortedNames(string[] sortedNames)
        {
            foreach (string name in sortedNames)
            {
                Console.WriteLine(name);
            }
        }

        static void SaveSortedNamesToFile(string[] sortedNames)
        {
            File.WriteAllLines("sorted-names-list.txt", sortedNames);
        }
    }

    class NameComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            string[] name1Parts = x.Split(' ');
            string[] name2Parts = y.Split(' ');

            string lastName1 = name1Parts[name1Parts.Length - 1];
            string lastName2 = name2Parts[name2Parts.Length - 1];

            int result = lastName1.CompareTo(lastName2);

            if (result == 0)
            {
                int givenNameCount1 = name1Parts.Length - 1;
                int givenNameCount2 = name2Parts.Length - 1;
                int commonNameCount = Math.Min(givenNameCount1, givenNameCount2);

                for (int i = 0; i < commonNameCount; i++)
                {
                    result = name1Parts[i].CompareTo(name2Parts[i]);
                    if (result != 0)
                    {
                        return result;
                    }
                }

                result = givenNameCount1.CompareTo(givenNameCount2);
            }

            return result;
        }
    }
}
