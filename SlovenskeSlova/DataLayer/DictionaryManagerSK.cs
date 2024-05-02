
namespace SlovenskeSlova.DataLayer
{
    public class DictionaryManagerSK : IDictionaryManager
    {
        private List<string> wordList;
        private Random random;

        public DictionaryManagerSK()
        {
            random = new Random();
            wordList = new List<string>();

            var input = File.ReadAllLines("sk.txt");

            // Take the most common words, skip the first line
            for (int i = 1; i < input.Length; i++)
            {
                string[] line = input[i].Split("\t");
                if (line.Length != 2 || int.Parse(line[1]) < 20)
                    break;
                wordList.Add(line[0]);
            }
        }

        /// <summary>
        /// Get random words from the dictionary.
        /// </summary>
        /// <param name="count">Number of words to get.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">When requested more than the size of the dictionary or 0 or less.</exception>
        public string[] GetWords(int count)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count), "Argument lower or equal to 0.");

            if (count >= wordList.Count)
                throw new ArgumentOutOfRangeException(nameof(count), "Argument higher than the dictionary size.");

            HashSet<int> indices = new HashSet<int>();
            string[] result = new string[count];

            for (int i = 0; i < count; i++)
            {
                int index = GetUniqueIndex(indices);
                result[i] = wordList[index];
            }

            return result;
        }

        /// <summary>
        /// Gets the unique index using recursion if it is duplicated.
        /// </summary>
        /// <param name="indices">Set of already chosen indices.</param>
        /// <param name="index">Index to check, if null generate a new one.</param>
        /// <returns></returns>
        private int GetUniqueIndex(HashSet<int> indices, int? index = null)
        {
            if (!index.HasValue)
                index = random.Next(wordList.Count);

            if (index >= wordList.Count || index < 0)
                index = 0;

            if(indices.Contains(index.Value))
                return GetUniqueIndex(indices, index + 1);

            indices.Add(index.Value);
            return index.Value;
        }
    }
}
