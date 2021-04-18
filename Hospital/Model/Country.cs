using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public class Country : INotifyPropertyChanged
    {
        private string countryName;

        public string CountryName
        {
            get { return countryName; }
            set 
            { 
                countryName = value;
                OnPropertyChanged("CountryName");
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
