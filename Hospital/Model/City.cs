﻿using System;
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

        public string CityName
        {
            get { return cityName; }
            set
            {
                cityName = value;
            }
        }

        private string postalCode;

        public string PostalCode
        {
            get { return postalCode; }
            set
            {
                postalCode = value;
            }
        }

        private Country country = new Country();

        public Country Country
        {
            get { return country; }
            set
            {
                country = value;
            }
        }

    }
}
