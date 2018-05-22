using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ibotta.Models;
using Ibotta.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Ibotta.Controllers
{
    /// <summary>
    /// controller for fetching anagrams
    /// </summary>
    [Route("anagrams")]
    [FormatFilter]
    public class AnagramsController : Controller
    {
        private ICorpusRepository _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        public AnagramsController(ICorpusRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets all matching anagrams for a single word
        /// </summary>
        /// <remarks>
        /// Returns a JSON array of English-language words that are anagrams of the word passed in the URL.
        /// </remarks>
        [HttpGet("{word}.{format?}")]
        public IActionResult Get(string word, int? limit = null, bool? includeProperNouns = null)
        {
            var anagrams = _repository.GetAnagrams(word, includeProperNouns ?? true).Take(limit ?? int.MaxValue);
            return Ok(new AnagramDTO() { Anagrams = anagrams });
        }


        /// <summary>
        /// Takes a JSON array of English-language words and adds them to the corpus (data store).
        /// </summary>
        /// <remarks>
        /// Any existing words will be replaced. No duplicates will be added.
        /// </remarks>
        [HttpPost("")]
        [HttpPost(".{format}")]
        public IActionResult Post([FromBody] WordsDTO words)
        {       
            var areAnagrams = false;

            if (words != null && words.Words != null && words.Words.Any())
                areAnagrams = _repository.AreAnagrams(words.Words);

            return Ok(new { anagrams = areAnagrams });
        }
    }
}
