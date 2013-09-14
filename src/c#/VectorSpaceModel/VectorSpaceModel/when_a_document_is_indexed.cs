using System.Text;
using System.Threading.Tasks;
using Machine.Specifications;

namespace VectorSpaceModel
{
    [Subject(typeof(SearchEngine))]
    public class when_a_document_is_indexed : SearchEngineSpecBase
    {
        It should_map_the_terms_to_a_vector = () => {  };
    }
}
