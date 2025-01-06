using System.Diagnostics.Tracing;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

string[] PerfectWords = File.ReadAllLines("./new_beta.txt");
string[] ImPerfectWords = File.ReadAllLines("./imperfectData.txt");




List<string> CorrectWords = new List<string>();



for (int i = 0; i < ImPerfectWords.Length; i++)
{

    List<char> chars = new List<char>();

    string word = ImPerfectWords[i];

    if (word.Length == 5)
    {
        for (int j = 0; j < word.Length; j++)
        {

            char c = word[j];
            if (chars.Contains(c)) continue;

            chars.Add(c);

        }
        if (chars.Count != 5) continue;

        if (UsedChars(chars, CorrectWords))
            continue;        
        CorrectWords.Add(word);
        //Console.WriteLine(word);

    }
}

for (int k = 0; k < CorrectWords.Count - 1; k++)
{
    List<char> chars = new List<char>();
    

    string word = CorrectWords[k];
    string word2 = CorrectWords[k + 1];


    for (int j = 0; j < word.Length; j++)
    {

    }

}



bool UsedChars(List<char> chars, List<string> CorrectWords)
{
    for (int i = 0; i < CorrectWords.Count; i++)
    {
        string word = CorrectWords[i];

        for (int j = 0; j < word.Length; j++)
        {
            if (!chars.Contains(word[j])) break;
            if (j == 4) return true;
        }
    }


    return false;
}
