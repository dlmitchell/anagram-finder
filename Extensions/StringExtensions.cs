using System.Linq;

namespace Ibotta.Extensions
{
    /// <summary>
    /// Class for extending the string
    /// </summary>
    public static class StringExtensions    
    {
        /// <summary>
        /// Will sort the given string (ascending)
        /// </summary>
        public static string Sort(this string word, bool lower = true)
        {
            return lower ? string.Concat(word.ToLower().OrderBy(x => x)) : string.Concat(word.OrderBy(x => x));            
        }
    }
}