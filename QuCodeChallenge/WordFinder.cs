namespace QuCodeChallenge
{
    internal class WordFinder : IWordFinder
    {
        private string[] Myarray { get; set; }
        private SortedDictionary<string, int> words = new SortedDictionary<string, int>();

        public WordFinder(IEnumerable<string> matrix)
        {
            //create an array that has a length and height equal to the number of objects in the array 
            string[] array = new string[matrix.Count()];
            var i = 0;
            //read each position of the matriz and populate the i position creating a row for each position
            foreach (string line in matrix)
            {
                array[i] = line;
                i++;
            }
            //Transpose columns as rows
            string[] MyarrayColumns= Transpose(array);
            //Create an array with all rows and columns as rows
            Myarray = array.Concat(MyarrayColumns).ToArray();
        }
       
        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            foreach (string word in wordstream)
            {
                //start the searching from the first array
                for (int numArray = 0; numArray < Myarray.Length; numArray++)
                {
                    //Search how many words in the row of numArray position, if there are, save it
                    FindWord(  numArray, word);
                }
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
                array[ i] = new string(rowList[i].ToArray());
            }
            return array;
        }

        private void FindWord( int numArray, string word)
        {
            //search for the word in the row... how many are.. and save it in a dictionary
            int cant = Myarray[numArray].Split(word).Length - 1;
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
