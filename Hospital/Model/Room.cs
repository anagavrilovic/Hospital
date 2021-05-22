
using Hospital.Model;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Hospital
{
    public class Room : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
       
        private string _id;
        private string _name;
        private int _floor;
        private RoomStatus _status;
        private RoomType _type;
        private int freeBeds;

        public Room()
        {
            SerializeInfo = true;
        }

        public string Id 
        {
            get => _id;
            set
            {
               _id = value;
               OnPropertyChanged("Id");
            }
        }


        public String Name
        {
            get => _name;
            set
            {
               _name = value;
               OnPropertyChanged("Name");
            }
        }
 

        public int Floor
        {
            get => _floor;
            set
            {
                _floor = value;
                OnPropertyChanged("Floor");
            }
        }

        public RoomStatus Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }

        public RoomType Type
        {
            get => _type;
            set
            {
               _type = value;
               OnPropertyChanged("Type");
            }
        }
        public int FreeBeds
        {
            get => freeBeds;
            set
            {
                freeBeds = value;
                OnPropertyChanged("freeBeds");
            }
        }

        public bool ShouldSerializeName()
        {
            return this.SerializeInfo;
        }

        public bool ShouldSerializeFloor()
        {
            return this.SerializeInfo;
        }

        public bool ShouldSerializeStatus()
        {
            return this.SerializeInfo;
        }

        public bool ShouldSerializeType()
        {
            return this.SerializeInfo;
          
        }

        public bool ShouldSerializeFreeBeds()
        {
            return this.SerializeInfo;

        }

        [JsonIgnore]
        public bool SerializeInfo { get; set; }

        override
        public string ToString()
        {
            return this.Name;
        }

        public bool IsMagazine()
        {
            return this.Type.Equals(RoomType.MAGACIN);
        }

        public bool IsRoomAvaliableInSelectedPeriod(Appointment appointment)
        {
            ObservableCollection<Appointment> allAppointments = new AppointmentStorage().GetAll();

            foreach (Appointment a in allAppointments)
            {
                if (a.IsOverlappingWith(appointment) && this.Id.Equals(a.Room.Id))
                    return false;
            }

            return true;
        }

        public bool IsSuitableRoom(AppointmentType appointmentType)
        {
            if (appointmentType.Equals(AppointmentType.examination) || appointmentType.Equals(AppointmentType.urgentExamination))
                if (this.Type.Equals(RoomType.SALA_ZA_PREGLEDE))
                    return true;

            if (appointmentType.Equals(AppointmentType.operation) || appointmentType.Equals(AppointmentType.urgentOperation))
                if (this.Type.Equals(RoomType.OPERACIONA_SALA))
                    return true;

            return false;
        }
    }
}