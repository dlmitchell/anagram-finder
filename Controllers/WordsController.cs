using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ibotta.Repositories;
using Ibotta.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ibotta.Controllers
{
    /// <summary>
    /// controller for managing words in the anagram dictionary
    /// </summary>
    [Route("")]
    [FormatFilter]
    public class WordsController : Controller
    {
        private ICorpusRepository _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        public WordsController(ICorpusRepository repository)
        {
            _repository = repository;            
        }

        /// <summary>
        /// Checks to see if the word exists in the dictionary
        /// </summary>
        [HttpGet("words/{word}.{format?}")]
        public IActionResult Get(string word)
        {
            return Ok(_repository.Exists(word));
        }

        /// <summary>
        /// Takes a JSON array of English-language words and adds them to the corpus (data store).
        /// </summary>
        /// <remarks>
        /// Any existing words will be replaced. No duplicates will be added.
        /// </remarks>
        [HttpPost("words")]
        [HttpPost("words.{format}")]
        public IActionResult Post([FromBody] WordsDTO words)
        {            
            if (words != null && words.Words != null && words.Words.Any())
                _repository.Add(words.Words);

            return Created("", null);
        }

        /// <summary>
        /// Deletes a single word from the data store.
        /// </summary>
        [HttpDelete("words/{word}.{format?}")]
        public IActionResult Delete(string word, bool? includeAnagrams = null)
        {
            _repository.Delete(word, includeAnagrams ?? false);
            return NoContent();
        }

        /// <summary>
        /// Deletes all contents of the data store.
        /// </summary>
        [HttpDelete("words")]
        [HttpDelete("words.{format}")]
        public IActionResult Delete()
        {            
            _repository.Delete();
            return NoContent();
        }
    }
}
