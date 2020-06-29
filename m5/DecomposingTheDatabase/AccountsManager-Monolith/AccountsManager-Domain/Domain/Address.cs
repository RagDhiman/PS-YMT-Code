using System;
using System.Collections.Generic;
using System.Text;

namespace AccountsManager_Domain
{
    public class Address
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Street { get; set; }
        public string CityTown { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
    }
}
