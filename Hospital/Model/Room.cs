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

        private int id;
        private string name;
        private int floor;
        private Boolean isAvaliable;
        private RoomType type;

        public int Id 
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

        public Boolean IsAvaliable
        {
            get
            {
                return isAvaliable;
            }

            set
            {
                if (value != isAvaliable)
                {
                    isAvaliable = value;
                    OnPropertyChanged("IsAvaliable");
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

    }
}