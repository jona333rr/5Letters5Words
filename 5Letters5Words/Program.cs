using System.Diagnostics.Tracing;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

//Jeg har forsøgt at tjekke næste ord med forrige ord, for at der ikke bliver gentaget samme bogstaver i næste ord, som allerede er nævnt i forrige.


string[] ImPerfectWords = File.ReadAllLines("./new_beta.txt");




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
for (int k = 0; k < CorrectWords.Count; k++)
{
    for (int j = k + 1; j < CorrectWords.Count; j++)
    {
        var matching = string.Concat(CorrectWords[k], CorrectWords[j]);
        if (matching.Distinct().Count() != 10) continue;

        for (int n = j + 1; n < CorrectWords.Count; n++)
        {
            var matching2 = string.Concat(CorrectWords[n], CorrectWords[k], CorrectWords[j]);

            if (matching2.Distinct().Count() != 15) continue;
            
            for (int o = n + 1; o < CorrectWords.Count; o++)
            {
                var matching3 = string.Concat(CorrectWords[o], CorrectWords[n], CorrectWords[k], CorrectWords[j]);

                if (matching3.Distinct().Count() != 20) continue;

                for (int p = o + 1; p < CorrectWords.Count; p++)
                {
                    var matching4 = string.Concat(CorrectWords[p], CorrectWords[o], CorrectWords[n], CorrectWords[k], CorrectWords[j]);

                    if (matching4.Distinct().Count() != 25) continue;

                    Console.WriteLine(matching4);
                }
            }
        }
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
