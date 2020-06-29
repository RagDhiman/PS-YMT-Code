using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsManager_UI_Web.Models
{
    public class AddressIndexModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Street { get; set; }
        [Display(Name = "City Town")]
        public string CityTown { get; set; }
        public string County { get; set; }
        [Display(Name = "Post Code")]
        public string PostCode { get; set; }
        public string Country { get; set; }
    }
}
