// File:    MedicalSupply.cs
// Author:  Marija
// Created: Wednesday, March 24, 2021 3:04:22 PM
// Purpose: Definition of Class MedicalSupply

using System;
using System.ComponentModel;

namespace Hospital
{
    public class MedicalSupply : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

       private String id;
       private String name;
       private double price;
       private int quantity;
       private UnitsType units;
       private string roomID;

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

        public double Price
        {
            get
            {
                return price;
            }

            set
            {
                if (value != price)
                {
                    price = value;
                    OnPropertyChanged("Price");
                }
            }
        }


        public int Quantity
        {
            get
            {
                return quantity;
            }

            set
            {
                if (value != quantity)
                {
                    quantity = value;
                    OnPropertyChanged("Quantity");
                }
            }
        }

        public UnitsType Units
        {
            get
            {
                return units;
            }

            set
            {
                if(value != units)
                {
                    units = value;
                    OnPropertyChanged("Units");
                }
            }
        }

        public string RoomID
        {
            get
            {
                return roomID;
            }

            set
            {
                if (value != roomID)
                {
                    roomID = value;
                    OnPropertyChanged("RoomID");
                }
            }
        }


    }
}

