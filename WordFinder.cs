using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordFinderChallenge
{
    public class WordFinder : IWordFinder
    {
        readonly IWordFinderValidator _wordFinderValidator;
        public IEnumerable<ISearchDirection> SearchDirections { get; set; }
        private readonly char[][] matrix;

        public WordFinder(IEnumerable<string> matrix)
        {
            //Using the WordFinderValidator class for validations. Because, I consider that validations are not the responsibility of the WordFinder class
            _wordFinderValidator = new WordFinderValidator();
            SearchDirections = new List<ISearchDirection>()
            {
                new HorizontalFromTopToBottom(),
                new VerticalFromLeftToRight()
            };
            this.matrix = ConvertListToMatrix(matrix);
        }

        public WordFinder()
        {
            
        }

        public char[][] ConvertListToMatrix(IEnumerable<string> matrix)
        {
            int rowCount = matrix.Count();
            int colCount = matrix.First().Length;
            char[][] charMatrix = new char[rowCount][];

            for (int i = 0; i < rowCount; i++)
            {
                if (_wordFinderValidator.ValidateSizesBetweenTwoValues(matrix.ElementAt(i).Length, colCount))
                {
                    throw new ArgumentException("Number of characters in matrix are different");
                }

                charMatrix[i] = matrix.ElementAt(i).ToCharArray();
            }

            return charMatrix;
        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            Dictionary<string, int> wordCounts = new Dictionary<string, int>();
            List<Task<int>> tasks;

            foreach (string word in wordstream)
            {
                int count = 0;

                if (!wordCounts.ContainsKey(word))
                {
                    //Using Task to run vertical and horizontal searches in parallel
                    tasks = new List<Task<int>>();

                    foreach (var direction in SearchDirections)
                    {
                        tasks.Add(Task.Run(() => direction.Search(matrix, word)));
                    }

                    Task.WaitAll(tasks.ToArray());

                    foreach (var task in tasks)
                    {
                        count += task.Result;
                        tasks = null;
                    }

                    if (count > 0)
                    {
                        wordCounts[word] = count;
                    }
                }
            }

            return wordCounts.OrderByDescending(x => x.Value).Select(x => x.Key).Take(10);
        }
    }
}
