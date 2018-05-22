using System.Collections.Generic;
using System.Linq;
using Ibotta.Extensions;
using Ibotta.Models;

namespace Ibotta.Repositories
{
    /// <summary>
    /// Data structure for holding dictionary of words in order to easily find anagrams
    /// </summary>
    /// <remarks>
    /// The easiest way to tell if two words are anagrams is to sort them both then do a simple comparison
    ///
    /// This takes a list of words, sorts each word and uses that word as a key in a dictionary. 
    /// The element of the dictionary will be a list of words that also share the same sorting (anagrams).static 
    /// </remarks>
    internal class Corpus
    {
        private class Word 
        {
            public string Original { get; set; }
            public string Sorted { get; set; }
        }

        private Dictionary<string, List<Word>> _corpus; 

        public Corpus(IEnumerable<string> words)
        {
            _corpus = new Dictionary<string, List<Word>>();
            foreach (var word in words)
                Add(word);
        }

        public void Add(IEnumerable<string> words)
        {
            foreach (var word in words)
                Add(word);
        }

        private void Add(string word)
        {
            var sorted = word.Sort();
            
            var w = new Word() { Original = word, Sorted = sorted };

            if (_corpus.ContainsKey(sorted))
                _corpus[sorted].Add(w);
            else 
                _corpus.Add(sorted, new List<Word>() { w });
        }       

        public void Delete(string word, bool includeAnagrams)
        {
            var sorted = word.Sort();
            if (_corpus.ContainsKey(sorted))
            {
                _corpus[sorted].RemoveAll(x => x.Original == word);

                // delete the whole key if the set of anagrams are empty 
                // OR if we're deleting anagrams along with the word
                if (!_corpus[sorted].Any() || includeAnagrams)
                    _corpus.Remove(sorted);
            }                
        }
        
        public void Delete()
        {
            _corpus.Clear();              
        } 

        public IEnumerable<string> GetAnagrams(string word, bool includeProperNouns)
        {   
            var sorted = word.Sort();

            if (_corpus.ContainsKey(sorted))
                return _corpus[sorted]
                    .Where(x => x.Original != word)
                    .Where(x => includeProperNouns ? true : char.IsLower(x.Original.First()))
                    .Select(x => x.Original);

            return new List<string>();
        }

        public CorpusStatsDTO GetStats()
        {
            var words = _corpus.Values.ToList().SelectMany(x => x).ToList();
            var sorted = words.OrderBy(x => x.Original.Length).Select(x => x.Original.Length);
            return new CorpusStatsDTO()
            {
                Count = words.Count,
                Min = sorted.Any() ? sorted.Min() : 0,
                Max = sorted.Any() ? sorted.Max() : 0,
                Average = sorted.Any() ? sorted.Average() : 0,
                Median = sorted.Any() ? sorted.OrderByDescending(x => x).ElementAt(words.Count() / 2) : 0
            };
        }   

        public IEnumerable<IEnumerable<string>> GetMostCommonAnagrams(int size)
        {
            return _corpus.Values.OrderByDescending(x => x.Count).Take(size).Select(x => x.Select(y => y.Original));        
        }       
    }
}