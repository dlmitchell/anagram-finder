using System.Collections.Generic;
using Ibotta.Models;

namespace Ibotta.Repositories
{
    /// <summary>
    /// Repository for basic CRUD on the corpus
    /// </summary>
    public interface ICorpusRepository
    {
        /// <summary>
        /// Adds a list of words to the corpus
        /// </summary>
        void Add(IEnumerable<string> words);

        /// <summary>
        /// Deletes a word from the corpus
        /// </summary>
        void Delete(string word, bool includeAnagrams);

        /// <summary>
        /// Deletes the entire corpus
        /// </summary>
        void Delete();

        /// <summary>
        /// Returns a list of anagrams based on the given word
        /// </summary>
        IEnumerable<string> GetAnagrams(string word, bool includeProperNouns);

        /// <summary>
        /// Returns a set of stats based on the corpus
        /// </summary>
        IEnumerable<IEnumerable<string>>  GetMostCommonAnagrams(int size);

        /// <summary>
        /// Returns a set of stats based on the corpus
        /// </summary>
        CorpusStats GetStats();
    }
}