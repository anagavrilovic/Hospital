using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public class Address : INotifyPropertyChanged
    {
        private string street;

        public string Street
        {
            get { return street; }
            set 
            { 
                street = value;
                OnPropertyChanged("Street");
            }
        }


        private string streetNumber;

        public string StreetNumber
        {
            get { return streetNumber; }
            set 
            { 
                streetNumber = value;
                OnPropertyChanged("StreetNumber");
            }
        }


        private City city = new City();

        public City City
        {
            get { return city; }
            set
            {
                city = value;
                OnPropertyChanged("City");
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
