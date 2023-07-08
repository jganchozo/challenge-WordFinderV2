namespace WordFinderChallenge
{
    public class HorizontalFromTopToBottom : ISearchDirection
    {
        readonly ICounter counter;

        public HorizontalFromTopToBottom()
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

            for (int row = 0; row < matrix.Length; row++)
            {
                string rowString = new string(matrix[row]);
                count += counter.CountWordOccurrences(rowString, word);
            }

            return count;
        }
    }
}
