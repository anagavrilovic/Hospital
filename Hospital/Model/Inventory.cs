// File:    Inventory.cs
// Author:  Marija
// Created: Wednesday, March 24, 2021 3:04:22 PM
// Purpose: Definition of Class Inventory

using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace Hospital
{
    public class Inventory : INotifyPropertyChanged
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
       private String _name;
       private double _price;
       private int _quantity;
       private string _roomID;

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

        public double Price
        {
            get => _price;
            set
            {
               _price = value;
               OnPropertyChanged("Price");
            }
        }


        public int Quantity
        {
            get => _quantity;
            set
            {
               _quantity = value;
                OnPropertyChanged("Quantity");
            }
        }

        public string RoomID
        {
            get => _roomID;
            set
            {
               _roomID = value;
                OnPropertyChanged("RoomID");
            }
        }

        [JsonIgnore]
        public Room Room { get; set; }
    }
}
