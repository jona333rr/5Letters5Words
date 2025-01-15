namespace FWFL
{
    public class Class1
    {
        int[] lettersPositions = { 24, 9, 16, 14, 25, 8, 10, 11, 22, 1, 5, 17, 12, 19, 21, 13, 0, 23, 18, 20, 15, 4, 6, 3, 7, 2 };
        public int solution = 0;
        private int progressCounter = 0;
       public List <List <int>> ints = new List <List <int>>();
        public Dictionary<int, string> wordsLookUp = new();

        public Task Work(string filePath)
        {
            return Task.Run(() =>
            {
                List<int>[] words = readFile(filePath);
                matchingWords(words, 5, 0, 0, 0, new());
            });
        }

        public event EventHandler<int> SearchIndex;

        protected virtual void OnUpdatedSearchIndex(int e)
        {
            EventHandler<int> handler = SearchIndex;

            if (handler != null)
            {
                handler(this,e);
            }
        }
        //Læser filen
         public List<int>[] readFile(string filePath)
        {
            string[] _imPerfectWords = [];
 
            try
            {
                _imPerfectWords = File.ReadAllLines(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + " " + "File loading failed");
            }


            //Alle de filtrerede ord
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
                    wordsLookUp.Add(bittedWord, word);
                    _perfectWords[lowest].Add(bittedWord);
                }
            }
            return _perfectWords;
        }
        //Tjekker efter om næste ord er unik (recursive)
        public void matchingWords(List<int>[] words, int wordCount, int index, int usedChars, int currentDepth,List<int> usedWords , int skip = 0)
        {
            if (currentDepth == wordCount)
            {
                solution++;
                ints.Add(usedWords);
                return;
            }


            for (int i = index; i < words.Length; i++)
            {
                while ((usedChars & (1 << i)) > 0)
                {
                    i++;
                    if (i >= words.Length) return;
                }
                List<int> wordsWithLowestChar = words[i];
                for (int j = 0; j < wordsWithLowestChar.Count; j++)
                {
                    if ((usedChars & wordsWithLowestChar[j]) > 0) continue;
                    matchingWords(words, wordCount, i + 1, usedChars | wordsWithLowestChar[j], currentDepth + 1, new List<int>(usedWords) { wordsWithLowestChar[j] }, skip);
                    if(currentDepth == 0)
                    {
                        progressCounter++;
                        OnUpdatedSearchIndex(progressCounter * 100 / (words[0].Count + words[1].Count));
                    }
                }

                skip++;
                if (skip > 1) return;
            }
        }

        //Laver karakterne til bit
         int charToBit(int character)
        {
            return 1 << character;
        }

        //Ordet laves til bit
         int wordToBit(string Words, out int lowest)
        {
            int WordRepresentation = 0;
            lowest = 25;


            for (int i = 0; i < Words.Length; i++)
            {
                var letterPosition = lettersPositions[Words[i] - 'a'];
                WordRepresentation |= charToBit(letterPosition);
                lowest = Math.Min(lowest, letterPosition);
            }

            return WordRepresentation;
        }
    }
}
