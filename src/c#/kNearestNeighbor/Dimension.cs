namespace kNearestNeighbor
{
    public class Dimension
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public Dimension(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }
}