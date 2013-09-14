namespace kNearestNeighbor
{
    public class Guess
    {
        public string Label { get; set; }
        public int Count { get; set; }

        public Guess()
        {
            Count = 0;
            Label = "";
        }
    }
}