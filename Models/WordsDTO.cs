using System.Collections.Generic;

namespace Ibotta.Models
{
    /// <summary>
    /// Model for adding words
    /// </summary>
    public class WordsDTO
    {
        /// <summary>
        /// List of words to add
        /// </summary>        
        public IEnumerable<string> Words { get; set; }
    }
}