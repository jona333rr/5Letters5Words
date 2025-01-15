using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FWFL;

namespace _5Letters5Words
{
    public class a5Letters5Words
    {
        int solutions = 0;
        [Benchmark]
        public void tester()
        {
            
            solutions = 0;
            int StartIndex = 0;
            Class1 wordsFile = new Class1();
            const int wordCount = 5;
            List<int>[] words = wordsFile.readFile("./words_alpha");
            wordsFile.matchingWords(words, wordCount, 0, 0, 0, new());
        }        
    }
}
