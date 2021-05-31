using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public class City
    {
        private string cityName;

        public string CityName { get; set; }

        private string postalCode;

        public string PostalCode { get; set; }

        private Country country = new Country();

        public Country Country { get; set; }
    }
}
