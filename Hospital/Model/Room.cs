
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
        private int freeBeds;
        private string id;
        private string name;
        private int floor;
        private RoomStatus status;
        private RoomType type;

        public Room()
        {
            SerializeInfo = true;
        }

        public string Id 
        { 
            
            get
            {
                return id;
            }
            
            set
            {
                if (value != id)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }


        public String Name
        {

            get
            {
                return name;
            }

            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
 

        public int Floor
        { 
            get
            {
                return floor;
            }

            set
            {
                if (value != floor)
                {
                    floor = value;
                    OnPropertyChanged("Floor");
                }
            }
        }

        public RoomStatus Status
        {
            get
            {
                return status;
            }

            set
            {
                if (value != status)
                {
                    status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        public RoomType Type
        {
            get
            {
                return type;
            }

            set
            {
                if (value != type)
                {
                    type = value;
                    OnPropertyChanged("Type");
                }
            }
        }
        public int FreeBeds
        {

            get
            {
                return freeBeds;
            }

            set
            {
                if (value != freeBeds)
                {
                    freeBeds = value;
                    OnPropertyChanged("freeBeds");
                }
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