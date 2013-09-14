using System.Diagnostics;

namespace VectorSpaceModel
{
    [DebuggerDisplay("Index:{DocumentIndex} Vector:{DocumentVector} Data:{DocumentData}")]
    public class IndexedDocument
    {
        public string DocumentData { get; set; }
        public int DocumentIndex { get; set; }
        public double[] DocumentVector { get; set; }
    }
}