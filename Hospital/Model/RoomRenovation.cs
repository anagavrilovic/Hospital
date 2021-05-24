using Hospital.Services;
using Hospital.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public class RoomRenovation : INotifyPropertyChanged
    {
        private string _id;
        private DateTime _startDate;
        private DateTime _endDate;
        private string _description;
        private ObservableCollection<Room> _roomsDestroyedDuringRenovation;
        private ObservableCollection<Room> _roomsCreatedDuringRenovation;

        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged("StartDate");
            }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged("EndDate");
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public ObservableCollection<Room> RoomsDestroyedDuringRenovation
        {
            get => _roomsDestroyedDuringRenovation;
            set
            {
                _roomsDestroyedDuringRenovation = value;
                OnPropertyChanged("RoomsDestroyedDuringRenovation");
            }
        }

        public ObservableCollection<Room> RoomsCreatedDuringRenovation
        {
            get => _roomsCreatedDuringRenovation;
            set
            {
                _roomsCreatedDuringRenovation = value;
                OnPropertyChanged("RoomsCreatedDuringRenovation");
            }
        }

        public Room Room { get; set; }
        public Room WareHouse { get; set; }

        public  RoomRenovation()
        {
            RoomsCreatedDuringRenovation = new ObservableCollection<Room>();
            RoomsDestroyedDuringRenovation = new ObservableCollection<Room>();
            Room = new Room();
            WareHouse = new Room();
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
