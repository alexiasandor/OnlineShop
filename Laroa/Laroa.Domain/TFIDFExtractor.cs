using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laroa.Infrastructure
{
    public class TFIDFExtractor
    {
        public Dictionary<string, double> ExtractFeatures(string text, IEnumerable<string> allDocuments)
        {
            var features = new Dictionary<string, double>();

            // Tokenize the text into words
            var words = text.Split(new[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var term in words.Distinct())
            {
                var termFrequency = CalculateTermFrequency(term, words);
                var inverseDocumentFrequency = CalculateInverseDocumentFrequency(term, allDocuments);

                features[term] = termFrequency * inverseDocumentFrequency;
            }

            return features;
        }

        private double CalculateTermFrequency(string term, string[] words)
        {
            var termCount = words.Count(w => w.Equals(term, StringComparison.OrdinalIgnoreCase));
            var totalWords = words.Length;

            return termCount / (double)totalWords;
        }

        private double CalculateInverseDocumentFrequency(string term, IEnumerable<string> allDocuments)
        {
            var documentCount = allDocuments.Count(d => d.Contains(term, StringComparison.OrdinalIgnoreCase));
            var totalDocuments = allDocuments.Count();

            return Math.Log(totalDocuments / (double)(documentCount + 1));
        }

        public double CalculateCosineSimilarity(Dictionary<string, double> vector1, Dictionary<string, double> vector2)
        {
            var dotProduct = 0.0;
            var magnitude1 = 0.0;
            var magnitude2 = 0.0;

            foreach (var key in vector1.Keys)
            {
                if (vector2.ContainsKey(key))
                {
                    dotProduct += vector1[key] * vector2[key];
                }

                magnitude1 += Math.Pow(vector1[key], 2);
            }

            foreach (var value in vector2.Values)
            {
                magnitude2 += Math.Pow(value, 2);
            }

            if (magnitude1 == 0 || magnitude2 == 0)
            {
                return 0.0;
            }

            return dotProduct / (Math.Sqrt(magnitude1) * Math.Sqrt(magnitude2));
        }
    }
}