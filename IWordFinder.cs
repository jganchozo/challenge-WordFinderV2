using System.Collections.Generic;

namespace WordFinderChallenge
{
    public interface IWordFinder
    {
        IEnumerable<string> Find(IEnumerable<string> wordstream);
        IEnumerable<ISearchDirection> SearchDirections { get; set; }
    }
}