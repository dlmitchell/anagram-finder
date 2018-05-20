using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Ibotta.Models;
using Microsoft.Extensions.PlatformAbstractions;

namespace Ibotta.Repositories
{
    internal class CorpusRepository : ICorpusRepository
    {        
        private List<string> _corpus; 
        private List<string> Corpus
        {
            get
            {
                if (_corpus == null)
                {
                    _corpus = new List<string>();

                    var lines = File.ReadAllLines(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "App_Data/dictionary.txt")); 

                    _corpus.AddRange(lines);
                }

                return _corpus;
            }
        }

        public void Add(IEnumerable<string> words)
        {
            // TODO: dupe check?
            if (words != null && words.Any())
                Corpus.AddRange(words);
        }

        public void Delete(string word)
        {
            Corpus.RemoveAll(x => x == word);
        }

        public void Delete()
        {
            Corpus.Clear();
        }

        public bool Exists(string word)
        {
            return Corpus.Any(x => x == word);
        }

        public IEnumerable<string> GetAnagrams(string word, bool includeProperNouns)
        {   
            // sort the original word, e.g., read -> ader
            var sorted = string.Join("", word.OrderBy(x => x));
            
            // find all words with the same length and eliminate any words that don't match the letters
            var pool = Corpus.Where(x => x.Length == word.Length && x.ToLower().Except(word.ToLower()).Count() == 0);
            
            // sort the pool of candidates, look for 
            var matches = pool
                .Select(x => new { original = x, sorted = string.Join("", x.Select(y => y.ToString()).OrderBy(y => y, StringComparer.OrdinalIgnoreCase)) })
                .Where(x => 
                    x.original != word && // exclude the given word
                    x.sorted.Equals(sorted, includeProperNouns ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
                .Select(x => x.original);

            return matches;
        }

        public CorpusStats GetStats()
        {
            var counts = Corpus.Select(x => x.Length);
            return new CorpusStats()
            {
                Count = Corpus.Count,
                Min = counts.Min(),
                Max = counts.Max(),
                Average = counts.Average(),
                Median = counts.OrderByDescending(x => x).ElementAt(counts.Count() / 2)
            };
        }
    }
}