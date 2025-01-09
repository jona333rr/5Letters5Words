using System.Diagnostics.Tracing;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Diagnostics;

Stopwatch stopwatch = Stopwatch.StartNew();

string[] _imPerfectWords = [];

try
{
_imPerfectWords = File.ReadAllLines("./words_alpha.txt");
} catch (Exception ex)
{
    Console.WriteLine(ex + " " + "File loading failed");
}





List<int>[] _perfectWords = new List<int>[26];

for (int i = 0; i < _perfectWords.Length; i++)
{
    _perfectWords[i] = new();
}

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
        var bittedWord = wordToBit(word, out int lowest);
        if (_perfectWords[lowest].Contains(bittedWord)) continue;
        _perfectWords[lowest].Add(bittedWord);
    }
}


const int wordCount = 5;
int solutions = 0;
matchingWords(_perfectWords, wordCount, _perfectWords.Length, 0, 0);

void matchingWords(List <int>[] words, int wordCount, int index, int usedChars, int currentDepth)
{
    if (currentDepth == wordCount)
    {
        solutions++;
        Console.WriteLine(solutions);
    }

    for (int i = index - 1; i >= 0; i--)
    {
        List <int> wordsWithLowestChar = words[i];
        for (int j = 0;j < wordsWithLowestChar.Count; j++)
        {
            if ((usedChars & wordsWithLowestChar[j]) > 0) continue;
            matchingWords(words, wordCount, i, usedChars | wordsWithLowestChar[j], currentDepth + 1);
        }
        
    }
}




int charToBit (char character)
{
    return 1 << (character - 'a');
}


int wordToBit (string Words, out int lowest)
{
    int WordRepresentation = 0;
    lowest = 25;

    for (int i = 0; i < Words.Length; i++) 
    {
        WordRepresentation |= charToBit(Words[i]);
        lowest = Math.Min(lowest, Words[i] - 'a');
    }

    return WordRepresentation;
}

 stopwatch.Stop();
Console.WriteLine(stopwatch.Elapsed.TotalSeconds);