namespace Ibotta.Models
{
    /// <summary>
    /// Stats on the corpus
    /// </summary>
    public class CorpusStats
    {
        /// <summary>
        /// Number of words in the corpus
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Minimum word length in the corpus
        /// </summary>
        public int Min { get; set; }

        /// <summary>
        /// Maximum word length in the corpus
        /// </summary>
        public int Max { get; set; }

        /// <summary>
        /// Median word length in the corpus
        /// </summary>
        public int Median { get; set; }

        /// <summary>
        /// Average word length in the corpus
        /// </summary>
        public double Average { get; set; }
    }
}