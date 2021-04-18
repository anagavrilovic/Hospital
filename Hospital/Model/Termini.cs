using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Hospital.Model
{
    public class Termini : INotifyPropertyChanged
    {
        private string vreme;
        private Appointment ponedeljak = new Appointment();
        private Appointment utorak = new Appointment();
        private Appointment sreda = new Appointment();
        private Appointment cetvrtak = new Appointment();
        private Appointment petak = new Appointment();
        private Appointment subota = new Appointment();
        private Appointment nedelja = new Appointment();

        public string Vreme
        {
            get { return vreme; }
            set 
            { 
                vreme = value;
                OnPropertyChanged("Vreme");
            }
        }

        public Appointment Ponedeljak
        {
            get { return ponedeljak; }
            set 
            { 
                ponedeljak = value;
                OnPropertyChanged("Ponedeljak");
            }
        }

        public Appointment Utorak
        {
            get { return utorak; }
            set 
            { 
                utorak = value;
                OnPropertyChanged("Utorak");
            }
        }

        public Appointment Sreda
        {
            get { return sreda; }
            set 
            { 
                sreda = value;
                OnPropertyChanged("Sreda");
            }
        }

        public Appointment Cetvrtak
        {
            get { return cetvrtak; }
            set 
            { 
                cetvrtak = value;
                OnPropertyChanged("Cetvrtak");
            }
        }

        public Appointment Petak
        {
            get { return petak; }
            set 
            { 
                petak = value;
                OnPropertyChanged("Petak");
            }
        }

        public Appointment Subota
        {
            get { return subota; }
            set 
            { 
                subota = value;
                OnPropertyChanged("Subota");
            }
        }

        public Appointment Nedelja
        {
            get { return nedelja; }
            set 
            { 
                nedelja = value;
                OnPropertyChanged("Nedelja");
            }
        }

        public Termini()
        {
            this.Vreme = "";
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
