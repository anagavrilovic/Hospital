// File:    MedicalSupply.cs
// Author:  Marija
// Created: Wednesday, March 24, 2021 3:04:22 PM
// Purpose: Definition of Class MedicalSupply

using System;
using System.ComponentModel;

namespace Hospital
{
    public class DynamicInventory : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

       private String _id;
       private String _name;
       private double _price;
       private int _quantity;
       private UnitsType _units;
       private string _roomID;

        public string Id
        {

            get => _id;

            set
            {
                if (value != _id)
                {
                    _id = value;
                    OnPropertyChanged("Id");
                }
            }
        }


        public String Name
        {

            get => _name;

            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public double Price 
        {
            get => _price;

            set
            {
                if (value != _price)
                {
                    _price = value;
                    OnPropertyChanged("Price");
                }
            }
        }


        public int Quantity
        {
            get => _quantity;

            set
            {
                if (value != _quantity)
                {
                    _quantity = value;
                    OnPropertyChanged("Quantity");
                }
            }
        }

        public UnitsType Units
        {
            get => _units;

            set
            {
                if(value != _units)
                {
                    _units = value;
                    OnPropertyChanged("Units");
                }
            }
        }

        public string RoomID
        {
            get => _roomID;

            set
            {
                if (value != _roomID)
                {
                    _roomID = value;
                    OnPropertyChanged("RoomID");
                }
            }
        }
    }
}

