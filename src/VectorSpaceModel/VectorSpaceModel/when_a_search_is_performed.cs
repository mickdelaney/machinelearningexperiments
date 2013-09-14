using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;

namespace VectorSpaceModel
{
    [Subject(typeof(SearchEngine))]
    public class when_a_search_is_performed : SearchEngineSpecBase
    {
        static SearchEngine _searchEngine = new SearchEngine();
        static IList<DocumentSearchResult> _results;

        Because of = () =>
        {
            _searchEngine.AddDocuments(SampleDocs);
            _searchEngine.CreateIndex();

            _results = _searchEngine.Query(SampleQuery);
        };

        It should_return_the_documents_in_the_correct_order = () =>
        {
            _results[0].DocumentIndex.ShouldEqual(1);
            _results[1].DocumentIndex.ShouldEqual(2);
            _results[2].DocumentIndex.ShouldEqual(0);
        };
    }
}