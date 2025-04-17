
namespace QuCodeChallenge
{
    //second posibility is... handling in a single string 
    //the array can be 64X64 this is 4.096 long, Adding the columns as rows is 4.096 *2=8.192... String can be  2^31, this is 2147483648 long... so I can put all the arrays in a single string.
    internal class WordFinder2 : IWordFinder
    {
        private string SingleRow { get; set; }

        private SortedDictionary<string, int> words = new SortedDictionary<string, int>();

        public WordFinder2(IEnumerable<string> matrix)
        {
            //create an array that has a length and height equal to the number of objects in the array 
            string[] array = new string[matrix.Count()];
            SingleRow = string.Empty;
            var i = 0;
            //read each position of the matriz and populate the i position creating a row for each position
            foreach (string line in matrix)
            {
                //put in a single string all the lines..
                SingleRow = string.Concat(SingleRow, string.Format("{0}{1}", SingleRow == String.Empty ? "" : ".", line));
                array[i] = line;
                i++;
            }
            //Transpose columns as rows
            string[] MyarrayColumns = Transpose(array);
            foreach (string line in MyarrayColumns)
            {
                //put in a single string all the lines..
                SingleRow = string.Concat(SingleRow, string.Format("{0}{1}", ".", line));
            }
        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            foreach (string word in wordstream)
            {
                //Search how many words in the row in the array save it
                FindWord ( word);
            }
            //retrieve all words order by Value..
            IEnumerable<string> sortedDict = (from entry in words
                                              orderby entry.Value descending
                                              select entry.Key
                                            ).Take(10).ToList();
            return sortedDict;
        }
        private string[] Transpose(string[] firstArray)
        {
            //turn the columns into rows so there is more easy to find,without nested loops
            int Count = firstArray.Length;
            var rowList = Enumerable.Range(0, Count)
                                     .Select(x => Enumerable.Range(0, Count)
                                                .Select(y => firstArray[y][x])
                                                .ToList())
                                     .ToList();

            string[] array = new string[Count];
            for (int i = 0; i < Count; i++)
            {
                array[i] = new string(rowList[i].ToArray());
            }
            return array;
        }

        private void FindWord(  string word)
        {
            //search for the word in the row... how many are.. and save it in a dictionary
            int cant = SingleRow.Split(word).Length - 1;
            if (cant > 0)
                AddWord(word, cant);
        }
        private void AddWord(string param, int cant)
        {
            //Add new word and increase in Cant if already exists
            if (words.ContainsKey(param))
                words[param] = words[param] + cant;
            else
                words.Add(param, cant);
        }
    }
}
