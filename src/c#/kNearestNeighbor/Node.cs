using System;
using System.Collections.Generic;
using System.Linq;

namespace kNearestNeighbor
{
    public class Node : ILabelable, IMeasureable
    {
        IDictionary<string, Dimension> _dimensions = new Dictionary<string, Dimension>();
        public IDictionary<string, Dimension> Dimensions
        {
            get { return _dimensions; }
            set { _dimensions = value; }
        }

        List<Node> _neighbors = new List<Node>();
        public List<Node> Neighbors
        {
            get { return _neighbors; }
            set { _neighbors = value; }
        }

        public Guess Guess;
        

        public string Label { get; set; }

        public bool IsLabelled { get { return string.IsNullOrWhiteSpace(Label) == false; } }
        public bool IsNotLabelled { get { return !IsLabelled; } }

        public Node(IDictionary<string, Dimension> dimensions)
        {
            _dimensions = dimensions;
        }

        public Node(params Dimension[] dimensions)
        {
            _dimensions = dimensions.ToDictionary(d => d.Name, d => d);
        }

        public double GetDimension(string name)
        {
            return Dimensions[name].Value;
        }
        
        public void AddNeighbor(Node node)
        {
            Neighbors.Add(node);
        }

        public void MeasureDistances(params DimensionRange[] dimensionRanges)
        {
            //var roomsRange = rooms.Max - rooms.Min;
            //var areaRange = area.Max - area.Min;

            foreach (var neighbor in Neighbors)
            {
                var deltas = new List<double>();
                foreach (var range in dimensionRanges)
                {
                    var dimensionRange = range.Max - range.Min;
                    var dimensionDelta = neighbor.GetDimension(range.Label) - GetDimension(range.Label);
                    dimensionDelta = (dimensionDelta) / dimensionRange;
                    deltas.Add(dimensionDelta);
                }

                //var deltaRooms = neighbor.GetDimension("Room") - GetDimension("Room");
                //deltaRooms = (deltaRooms)/roomsRange;

                //var deltaArea = neighbor.GetDimension("Area") - GetDimension("Area");
                //deltaArea = (deltaArea) / areaRange;
                
                //neighbor.Distance = Math.Sqrt(deltaRooms * deltaRooms + deltaArea * deltaArea);
                
                neighbor.Distance = Math.Sqrt(deltas.Select(d => d * d).Aggregate((d, f) => d + f));
            }

        }

        protected double Distance { get; set; }

        public void SortByDistance()
        {
            Neighbors.Sort((a, b) => a.Distance.CompareTo(b.Distance));
        }

        public void GuessType(int k)
        {
            var labels = new Dictionary<string, int>();

            foreach (var neighbor in Neighbors.Take(k))
            {
                if (labels.ContainsKey(neighbor.Label) == false)
                {
                    labels.Add(neighbor.Label, 1);        
                }

                labels[neighbor.Label] += 1;
            }

            Guess = new Guess();
            foreach (var label in labels.Where(label => label.Value > Guess.Count))
            {
                Guess.Label = label.Key;
                Guess.Count = label.Value;
            }

            Label = Guess.Label;
        }
    }
}