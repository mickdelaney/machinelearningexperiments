using System;
using System.Diagnostics;

namespace VectorSpaceModel
{
    [DebuggerDisplay("Index:{DocumentIndex} Distance:{CosineDistance} Data:{DocumentData}")]
    public class DocumentSearchResult : IComparable
    {
        public string DocumentData { get; set; }
        public int DocumentIndex { get; set; }
        public double CosineSimilarity { get; set; }

        public override bool Equals(object other)
        {
            if (other == null)
            {
                throw new ArgumentException("Must not ne null");
            }
            var otherResult = other as DocumentSearchResult;
            if (otherResult == null)
            {
                throw new ArgumentException("Must be a DocumentSearchResult");
            }

            return CosineSimilarity.Equals(otherResult.CosineSimilarity);
        }

        public override int GetHashCode()
        {
            return CosineSimilarity.GetHashCode();
        }

        public int CompareTo(object other)
        {
            if (other == null)
            {
                throw new ArgumentException("Must not ne null");
            }
            var otherResult = other as DocumentSearchResult;
            if (otherResult == null)
            {
                throw new ArgumentException("Must be a DocumentSearchResult");
            }

            return CosineSimilarity.CompareTo(otherResult.CosineSimilarity);
        }
    }
}