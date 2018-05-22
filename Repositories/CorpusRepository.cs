using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Ibotta.Models;
using Ibotta.Extensions;
using Microsoft.Extensions.PlatformAbstractions;

namespace Ibotta.Repositories
{
    internal class CorpusRepository : ICorpusRepository
    {        
        private Corpus _corpus; 
        private Corpus Corpus
        {
            get
            {
                if (_corpus == null)
                {
                    var lines = File.ReadAllLines(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "App_Data/dictionary.txt"));                   
                    _corpus = new Corpus(lines);                
                }

                return _corpus;
            }
        }
        public void Add(IEnumerable<string> words)
        {
            Corpus.Add(words);
        }

        public void Delete(string word, bool includeAnagrams)
        {
            Corpus.Delete(word, includeAnagrams);            
        }

        public void Delete()
        {
            Corpus.Delete();
        }

        public IEnumerable<string> GetAnagrams(string word, bool includeProperNouns)
        {   
            return Corpus.GetAnagrams(word, includeProperNouns);
        }

        public CorpusStats GetStats()
        {
            return Corpus.GetStats();
        }

        public IEnumerable<IEnumerable<string>> GetMostCommonAnagrams(int size)
        {
            return Corpus.GetMostCommonAnagrams(size);
        }
    }
}