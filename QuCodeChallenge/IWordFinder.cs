namespace QuCodeChallenge
{
    internal interface IWordFinder
    {
        IEnumerable<string> Find(IEnumerable<string> wordstream);
    }
}
