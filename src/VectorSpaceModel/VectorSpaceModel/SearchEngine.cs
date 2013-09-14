using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace VectorSpaceModel
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchEngine
    {
        readonly Regex _exp = new Regex("\\w+", RegexOptions.IgnoreCase);
        readonly MathsUtil _math = new MathsUtil();

        readonly IList<string> _documents = new List<string>();
        readonly IList<string> _allTerms = new List<string>();

        readonly IDictionary<string, IndexedDocument> _documentTermVectors = new Dictionary<string, IndexedDocument>();

        /// <summary>
        /// Store our documents & start the term extract
        /// </summary>
        /// <param name="docs"></param>
        public void AddDocuments(string[] docs)
        {
            foreach (var doc in docs)
            {
                _documents.Add(doc);    
            }

            AddDocumentsToIndex(docs);
        }

        /// <summary>
        /// Extract all unique terms from the documents 
        /// 
        /// In lucene, at this point plugins for document processing
        /// would process the documents using techniques like steming etc.
        /// And create 'fields' for the resulting output of those
        /// processes. 
        /// Those 'fields' are then searchable later.
        ///  
        /// </summary>
        /// <param name="docs"></param>
        void AddDocumentsToIndex(IEnumerable<string> docs)
        {
            var terms = docs.Select(doc => _exp.Matches(doc))
                            .SelectMany(words => words.Cast<Match>()
                            .Where(match => !_allTerms.Contains(match.Value)))
                            .Select(match => match.Value);
            
            foreach (var term in terms)
            {
                _allTerms.Add(term);    
            }
        }

        public void CreateIndex()
        {
            CreateDocumentVectors();
        }

        /// <summary>
        /// Create a 'vector', which is basically an array with a value for each
        /// unique term in our index (_allTerms). 
        ///  
        /// Where our document contains a term in the index, that position in the
        /// vector will be the tfidf.
        /// 
        /// The vector is sparse, meaning if the document does not contain the
        /// term then the value will not be stored.
        /// 
        /// OK - this is not really a sparse vector implementation, because a sparse 
        /// implementation should not store nulls/default for performance reasons 
        /// but you get the idea.
        ///  
        /// </summary>
        void CreateDocumentVectors()
        {
            for (var documentIndex = 0; documentIndex < _documents.Count; documentIndex++)
            {
                var document = _documents[documentIndex];
                var documentVector = GetDocumentVector(document);

                var indexedDocument = new IndexedDocument
                {
                    DocumentIndex = documentIndex,
                    DocumentData = document,
                    DocumentVector = documentVector,
                };

                _documentTermVectors.Add(documentIndex.ToString(CultureInfo.InvariantCulture), indexedDocument);
            }
        }

        /// <summary>
        /// For each value in document get its tfidf & store in the vector.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        double[] GetDocumentVector(string document)
        {
            var documentVector = new double[_allTerms.Count];

            for (var termIndex = 0; termIndex < _allTerms.Count; termIndex++)
            {
                var tfIdf = _math.GetTfIdf(_documents, document, _allTerms[termIndex]);

                documentVector[termIndex] = tfIdf;
            }
            return documentVector;
        }

        /// <summary>
        /// Here we create a vector for the query the same way that
        /// we created one for the documents we indexed.
        /// 
        /// Return Documents ranked by Cosine similarity
        /// </summary>
        public IList<DocumentSearchResult> Query(string query)
        {
            var results = new List<DocumentSearchResult>();
            var queryVector = GetDocumentVector(query);
            
            foreach (var document in _documentTermVectors)
            {
                var distance = _math.CosineSimilarity(queryVector, document.Value.DocumentVector);
                var result = new DocumentSearchResult
                {
                    CosineSimilarity = distance,
                    DocumentIndex = document.Value.DocumentIndex,
                    DocumentData = document.Value.DocumentData
                };
                results.Add(result);
            }

            return results.OrderByDescending(d => d.CosineSimilarity).ToList();
        }
    }
}