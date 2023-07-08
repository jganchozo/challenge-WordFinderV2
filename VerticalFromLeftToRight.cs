using System.Linq;

namespace WordFinderChallenge
{
    public class VerticalFromLeftToRight : ISearchDirection
    {
        readonly ICounter counter;
        public VerticalFromLeftToRight()
        {
            counter = new Counter();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public int Search(char[][] matrix, string word)
        {
            int count = 0;

            for (int col = 0; col < matrix[0].Length; col++)
            {
                string colString = new string(matrix.Select(row => row[col]).ToArray());
                count += counter.CountWordOccurrences(colString, word);
            }

            return count;
        }
    }
}
