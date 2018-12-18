using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstatimate.Models
{
    public class Flat : RealEstateProperty
    {
        public int SquareMeters { get; set; }
        public Location Location { get; set; }
        public int Floor { get; set; }
        public int NumberOfRooms { get; set; }
        public bool Elevator { get; set; }
        public bool Balcony { get; set; }
        public DateTime Built { get; set; }
        public DateTime Renovated { get; set; }
        public bool Parking { get; set; }
        public Address Address { get; set; }
        public string Description { get; set; }

    }
}
