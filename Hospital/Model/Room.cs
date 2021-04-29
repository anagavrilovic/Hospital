
using Hospital.Model;
using Newtonsoft.Json;
using System;
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
    }
}