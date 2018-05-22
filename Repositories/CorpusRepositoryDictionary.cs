using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Ibotta.Models;
using Microsoft.Extensions.PlatformAbstractions;

namespace Ibotta.Repositories
{
    internal class Word 
    {
        public string Original { get; set; }
        public string Sorted { get; set; }
    }

    internal class CorpusRepositoryDictionary : ICorpusRepository
    {        
        private Dictionary<string, List<Word>> _corpus; 
        private Dictionary<string, List<Word>>  Corpus
        {
            get
            {
                if (_corpus == null)
                {
                    _corpus = new Dictionary<string, List<Word>>();
                    var lines = File.ReadAllLines(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "App_Data/dictionary.txt"));                     

                    foreach (var line in lines)
                    {
                        var sorted = SortWord(line);
            
                        var w = new Word() { Original = line, Sorted = sorted };

                        if (_corpus.ContainsKey(sorted))
                            _corpus[sorted].Add(w);
                        else 
                            _corpus.Add(sorted, new List<Word>() { w });
                    }
                }

                return _corpus;
            }
        }

        private string SortWord(string word)
        {
            return string.Concat(word.ToLower().OrderBy(x => x));
        }

        public void Add(IEnumerable<string> words)
        {
            foreach (var word in words)
                Add(word);
        }

        private void Add(string word)
        {
            var sorted = SortWord(word);
            
            var w = new Word() { Original = word, Sorted = sorted };

            if (Corpus.ContainsKey(sorted))
                Corpus[sorted].Add(w);
            else 
                Corpus.Add(sorted, new List<Word>() { w });
        }

        public void Delete(string word)
        {
            var sorted = SortWord(word);
            if (Corpus.ContainsKey(sorted))
            {
                Corpus[sorted].RemoveAll(x => x.Original == word);

                // if there aren't any more words in this sorted version, delete the whole key
                if (!Corpus[sorted].Any())
                    Corpus.Remove(sorted);
            }                
        }

        public void Delete()
        {
            Corpus.Clear();
        }

        public bool Exists(string word)
        {
            var sorted = SortWord(word);
            return Corpus.ContainsKey(sorted) && Corpus[sorted].Any(x => x.Original == word);
        }

        public IEnumerable<string> GetAnagrams(string word, bool includeProperNouns)
        {   
            var sorted = SortWord(word);

            if (Corpus.ContainsKey(sorted))
                return Corpus[sorted]
                    .Where(x => x.Original != word)
                    .Where(x => includeProperNouns ? true : char.IsLower(x.Original.First()))
                    .Select(x => x.Original);

            return new List<string>();
        }

        public CorpusStats GetStats()
        {
            var words = Corpus.Values.ToList().SelectMany(x => x).ToList();
            var sorted = words.OrderBy(x => x.Original.Length).Select(x => x.Original.Length);
            return new CorpusStats()
            {
                Count = words.Count,
                Min = sorted.Any() ? sorted.Min() : 0,
                Max = sorted.Any() ? sorted.Max() : 0,
                Average = sorted.Any() ? sorted.Average() : 0,
                Median = sorted.Any() ? sorted.OrderByDescending(x => x).ElementAt(words.Count() / 2) : 0,
                MostAnagrams = Corpus.Values.OrderByDescending(x => x.Count).FirstOrDefault()?.Select(x => x.Original)
            };
        }
    }
}