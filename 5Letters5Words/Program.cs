using System.Diagnostics.Tracing;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;

//Jeg skal til at i gang med at lave bitMap. 

string[] _imPerfectWords = File.ReadAllLines("./words_alpha.txt");




Dictionary<int, int> _perfectWords = new();




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
        var bittedWord = wordToBit(word);
        if (_perfectWords.ContainsKey(bittedWord)) continue;
        _perfectWords.Add(bittedWord, bittedWord);
    }
}


const int wordCount = 5;
int solutions = 0;
matchingWords(_perfectWords.Values.ToArray(), wordCount, _perfectWords.Count, 0, 0);

void matchingWords(int[] words, int wordCount, int index, int usedChars, int currentDepth)
{
    if (currentDepth == wordCount)
    {
        solutions++;
        Console.WriteLine(solutions);
    }
    for (int i = index - 1; i >= 0; i--)
    {
        if ((usedChars & words[i]) > 0) continue;
        
        
        matchingWords(words, wordCount, i, usedChars | words[i], currentDepth + 1);
        
    }
}



int charToBit (char character)
{
    return 1 << (character - 'a');
}


int wordToBit (string Words)
{
    int WordRepresentation = 0;

    for (int i = 0; i < Words.Length; i++) 
    {
        WordRepresentation |= charToBit(Words[i]);    
    }

    return WordRepresentation;
}