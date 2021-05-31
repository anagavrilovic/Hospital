using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public class Address
    {
        private string street;

        public string Street
        {
            get { return street; }
            set 
            { 
                street = value;
            }
        }


        private string streetNumber;

        public string StreetNumber
        {
            get { return streetNumber; }
            set 
            { 
                streetNumber = value;
            }
        }


        private City city = new City();

        public City City
        {
            get { return city; }
            set
            {
                city = value;
            }
        }
    }
}
