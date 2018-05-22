using System.Collections.Generic;

namespace Ibotta.Models
{
    /// <summary>
    /// Anagrams DTO
    /// </summary>
    public class AnagramDTO
    {
        /// <summary>
        /// An array of anagrams 
        /// </summary>
        /// <remarks>
        /// Not ordered
        /// </remarks>
        public IEnumerable<string> Anagrams { get; set; }
    }
}