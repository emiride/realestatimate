using System;

namespace RealEstatimate.Models
{
    public class Address
    {
        public Guid AddressId { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string Municipality { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
    }
}