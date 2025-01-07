using System.Diagnostics.Tracing;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;

//Jeg skal til at i gang med at lave bitMap. 

string[] _imPerfectWords = File.ReadAllLines("./new_beta.txt");




List<string> _perfectWords = new List<string>();




for (int i = 0; i < _imPerfectWords.Length; i++)
{

    List<char> chars = new List<char>();

    var word = _imPerfectWords[i];

    if (word.Length == 5)
    {
        for (int j = 0; j < word.Length; j++)
        {

            char c = word[j];
            if (chars.Contains(c)) continue;

            chars.Add(c);

        }
        if (chars.Count != 5) continue;

        if (UsedChars(chars, _perfectWords))
            continue;
        try
        {
            _perfectWords.Add(word);
            
        }
        catch (Exception ex) 
        {
            Console.WriteLine((ex) + " " +  "Adding word to the list failed");
        }
   

    }
}


const int wordCount = 5;
int solutions = 0;
matchingWords(_perfectWords.ToArray(), wordCount, _perfectWords.Count, new List<string>());

void matchingWords(string[] words, int wordCount, int index, List<string> selectedWords)
{
    if (selectedWords.Count == wordCount)
    {
        solutions++;
        Console.WriteLine(string.Join(" ", selectedWords));
        Console.WriteLine(solutions);
    }
    for (int i = index - 1; i >= 0; i--)
    {
        if ( string.Concat(string.Join("", selectedWords), words[i]).Distinct().Count() != 5 * (selectedWords.Count + 1)) continue;
        List<string> nextSelectedWord = new List<string>(selectedWords);
        nextSelectedWord.Add(words[i]);
        
        matchingWords(words, wordCount, i, nextSelectedWord);
        
    }
}



bool UsedChars(List<char> chars, List<string> perfectWords)
{
    for (int i = 0; i < perfectWords.Count; i++)
    {
        string word = perfectWords[i];

        for (int j = 0; j < word.Length; j++)
        {
            if (!chars.Contains(word[j])) break;
            if (j == 4) return true;
        }
    }


    return false;
}
