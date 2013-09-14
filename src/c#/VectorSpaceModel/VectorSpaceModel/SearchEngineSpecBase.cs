namespace VectorSpaceModel
{
    public class SearchEngineSpecBase
    {
        protected static string SampleQuery = "gold silver truck";   

        protected static string[] SampleDocs =
        {
            "shipment of gold damaged in a fire", //Doc 1
            "delivery of silver arrived in a silver truck", //Doc 2
            "shipment of gold arrived in a truck" //Doc 3
        }; 
    
    }
}