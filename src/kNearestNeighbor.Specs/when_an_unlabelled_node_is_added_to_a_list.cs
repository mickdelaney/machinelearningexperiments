using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Machine.Specifications;

namespace kNearestNeighbor.Specs
{
    [Subject(typeof(NodeList))]
    public class when_an_unlabelled_node_is_added_to_a_list
    {
        static IList<Dwelling> _trainingData;

        static readonly NodeList NodeList = new NodeList(3);
        static readonly Node UnknownHouseNode = new Node
        (
            new Dimension("Area", 1000),
            new Dimension("Room", 8)
        );
        static readonly Node UnknownApartmentNode = new Node
        (
            new Dimension("Area", 200),
            new Dimension("Room", 1)
        );

        Establish context = () =>
        {
            _trainingData = new List<Dwelling>
            {
                new Dwelling(1, 350, DwellingType.Apartment),
                new Dwelling(2, 300, DwellingType.Apartment),
                new Dwelling(3, 300, DwellingType.Apartment),
                new Dwelling(4, 250, DwellingType.Apartment),
                new Dwelling(4, 500, DwellingType.Apartment),
                new Dwelling(4, 400, DwellingType.Apartment),
                new Dwelling(5, 450, DwellingType.Apartment),
                new Dwelling(7, 850, DwellingType.House),
                new Dwelling(7, 900, DwellingType.House),
                new Dwelling(7, 1200, DwellingType.House),
                new Dwelling(8, 1500, DwellingType.House),
                new Dwelling(9, 1300, DwellingType.House),
                new Dwelling(8, 1240, DwellingType.House),
                new Dwelling(10, 1700, DwellingType.House),
                new Dwelling(9, 1000, DwellingType.House),
                new Dwelling(1, 800, DwellingType.Flat),
                new Dwelling(3, 900, DwellingType.Flat),
                new Dwelling(2, 700, DwellingType.Flat),
                new Dwelling(1, 900, DwellingType.Flat),
                new Dwelling(2, 1150, DwellingType.Flat),
                new Dwelling(1, 1000, DwellingType.Flat),
                new Dwelling(2, 1200, DwellingType.Flat),
                new Dwelling(1, 1300, DwellingType.Flat),
            };

            NodeList.AddDimensions(new DimensionRange { Min = 1000000, Max = 0, Label = "Area" });
            NodeList.AddDimensions(new DimensionRange { Min = 1000000, Max = 0, Label = "Room"  });

            foreach (var dwelling in _trainingData)
            {
                var node = new Node
                (
                    new Dimension("Area", dwelling.Area),
                    new Dimension("Room", dwelling.NumberOfRooms)
                )
                {
                    Label = dwelling.Type.ToString()
                };

                NodeList.AddNode(node);
            }

            NodeList.AddNode(UnknownHouseNode);
            NodeList.AddNode(UnknownApartmentNode);
            NodeList.DetermineUnknown();
        };
        
        It should_be_labelled_from_the_training_data = () =>
        {
            UnknownHouseNode.Label.ShouldNotBeNull();
            UnknownApartmentNode.Label.ShouldNotBeNull();
        };
        It should_label_the_house_as_a_house = () => UnknownHouseNode.Label.ShouldEqual("House");
        It should_label_the_apartment_as_an_apartment = () => UnknownApartmentNode.Label.ShouldEqual("Apartment");
    }
}
