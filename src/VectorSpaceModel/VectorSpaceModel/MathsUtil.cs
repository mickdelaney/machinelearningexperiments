using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace VectorSpaceModel
{
    public class MathsUtil
    {
        public double DotProduct(double[] v1, double[] v2)
        {
            if (v1.Length != v2.Length)
            {
                return 0.0;
            }

            return v1.Select((t, i) => t * v2[i]).Sum();
        }

        public double VectorLength(double[] vector)
        {
            var length = vector.Sum(t => Math.Pow(t, 2));
            return Math.Sqrt(length);
        }
        
        public double GetTfIdf(IList<string> allDocuments, string document, string term)
        {
            var termFrequency = GetTermFrequency(document, term);
            var inverseDocumentFrequency = GetInverseDocumentFrequency(allDocuments, term);
            var tfIdf = termFrequency * inverseDocumentFrequency;
            return tfIdf;
        }
        
        public double GetTermFrequency(string document, string term)
        {
            var allTermsInDocument = Regex.Split(document, "\\s");
            return allTermsInDocument.Count(t => t == term);
        }

        public double GetInverseDocumentFrequency(IList<string> allDocuments, string term)
        {
            double documentFrequency = allDocuments.Count(t => t.Contains(term));
            return documentFrequency > 0 ? Math.Log(allDocuments.Count / documentFrequency) : 0.0;
        }

        /// <summary>
        /// Use Cosine similarity
        ///  http://en.wikipedia.org/wiki/Cosine_similarity
        /// 
        /// 
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <returns></returns>
        public double CosineSimilarity(double[] vector1, double[] vector2)
        {
            double lengthV1 = VectorLength(vector1);
            double lengthV2 = VectorLength(vector2);

            double dotprod = DotProduct(vector1, vector2);
            return dotprod / (lengthV1 * lengthV2);
        }
    }
}