using System;

namespace WordFinderChallenge
{
    public class Counter : ICounter
    {
        public int CountWordOccurrences(string input, string word)
        {
            int count = 0;
            int index = 0;
            int start = 0;
            int end = input.Length;

            while ((start <= end) && (index > -1))
            {
                index = input.IndexOf(word, start, StringComparison.Ordinal);

                if (index == -1)
                    break;

                start = index + 1;
                count++;
            }

            return count;
        }
    }
}
