using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kNearestNeighbor
{
    /// <summary>
    /// create a bunch of nodes that contain properties
    /// each node should be able to measure the distance from it to another node
    /// we store the distances
    /// we sort them by distance
    /// we then take K number of nodes from the top
    /// we then pick the majority Label and label ourselves as such
    /// </summary>
    public class NodeList
    {
        readonly IList<Node> _nodes = new List<Node>();
        readonly IList<DimensionRange> _dimensions = new List<DimensionRange>();
        readonly int _k;

        public NodeList(int k)
        {
            _k = k;
        }
        public NodeList(IList<Node> nodes, int k) : this(k)
        {
            _nodes = nodes;
        }

        public void AddDimensions(params DimensionRange[] dimensions)
        {
            foreach (var dimension in dimensions)
            {
                _dimensions.Add(dimension);    
            }
        }

        public void DetermineUnknown()
        {
            CalculateRanges();

            foreach (var node in _nodes.Where(node => node.IsNotLabelled))
            {
                foreach (var neighbor in _nodes.Where(neighbor => neighbor.IsLabelled))
                {
                    node.AddNeighbor(neighbor);
                }

                node.MeasureDistances(_dimensions.ToArray());
                node.SortByDistance();
                node.GuessType(_k);
            }
        }

        void CalculateRanges()
        {
            foreach (var node in _nodes)
            {
                foreach (var dimension in _dimensions)
                {
                    var dimensionValue = node.GetDimension(dimension.Label);
                    if (dimensionValue < dimension.Min)
                    {
                        dimension.Min = dimensionValue;
                    }
                    if (dimensionValue > dimension.Max)
                    {
                        dimension.Max = dimensionValue;
                    }
                }
                //var rooms = node.GetDimension("Room");
                //if (rooms < _roomsRange.Min) {
                //    _roomsRange.Min = rooms;
                //}
                //if (rooms > _roomsRange.Max) {
                //    _roomsRange.Max = rooms;
                //}

                //var area = node.GetDimension("Area");
                //if (area < _roomsRange.Min) {
                //    _roomsRange.Min = area;
                //}
                //if (area > _roomsRange.Max) {
                //    _roomsRange.Max = area;
                //}
            }

        }

        public void AddNode(Node node)
        {
            _nodes.Add(node);
        }
    }
}