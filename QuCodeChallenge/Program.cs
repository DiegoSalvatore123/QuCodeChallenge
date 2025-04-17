using QuCodeChallenge;

string[] s1 = { "cold", "bbbr", "ccci", "dddt" };

//It has to ways to do it.. this is handling the Matrix
WordFinder wf = new WordFinder(s1);
string[] s2 = { "cold", "drit" };
IEnumerable<string> value = wf.Find(s2);

//this is handling the Matriz as string 
WordFinder2 wf2 = new WordFinder2(s1);
IEnumerable<string> value2 = wf2.Find(s2);

foreach (var row in value.ToList())
{
    Console.WriteLine("Word :" + row);
    Console.ReadLine();
}

foreach (var row in value2.ToList())
{
    Console.WriteLine("Word :" + row);
    Console.ReadLine();
}


