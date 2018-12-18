using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstatimate.Models
{
    public class RealEstateProperty
    {
        public Guid RealEstatePropertyId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Currency { get; set; }
    }
}
