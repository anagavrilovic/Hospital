using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public class City : INotifyPropertyChanged
    {
        private string cityName;

        public string CityName
        {
            get { return cityName; }
            set 
            { 
                cityName = value;
                OnPropertyChanged("CityName");
            }
        }

        private string postalCode;

        public string PostalCode
        {
            get { return postalCode; }
            set 
            { 
                postalCode = value;
                OnPropertyChanged("PostalCode");
            }
        }

        private Country country = new Country();

        public Country Country
        {
            get { return country; }
            set 
            { 
                country = value;
                OnPropertyChanged("Country");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }


    }
}
