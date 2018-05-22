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
    [Route("")]
    [FormatFilter]
    public class StatsController : Controller
    {
        private ICorpusRepository _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        public StatsController(ICorpusRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets stats for entire dictionary
        /// </summary>        
        [HttpGet("stats")]
        [HttpGet("stats.{format}")]
        public IActionResult Get()
        {
            return Ok(_repository.GetStats());
        }

        /// <summary>
        /// Gets most common anagrams in the corpus
        /// </summary>        
        [HttpGet("stats/anagrams")]
        [HttpGet("stats/anagrams.{format}")]
        public IActionResult GetMostCommonAnagrams(int? limit = null)
        {
            return Ok(_repository.GetMostCommonAnagrams(limit ?? 10));
        }
    }
}
