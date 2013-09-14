namespace kNearestNeighbor
{
    public interface ILabelable
    {
        bool IsLabelled { get; }
        string Label { get; }
    }
}