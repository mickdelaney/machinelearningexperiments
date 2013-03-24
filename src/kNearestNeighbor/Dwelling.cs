namespace kNearestNeighbor
{
    public class Dwelling
    {
        public int NumberOfRooms { get; set; }
        public int Area { get; set; }
        public DwellingType Type { get; set; }

        public Dwelling(int numberOfRooms, int area, DwellingType type)
        {
            NumberOfRooms = numberOfRooms;
            Area = area;
            Type = type;
        }
    }
}