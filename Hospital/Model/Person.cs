// File:    Person.cs
// Author:  Marija
// Created: Monday, March 22, 2021 2:34:41 PM
// Purpose: Definition of Class Person

using System;

namespace Hospital
{
    public abstract class Person
    {
        private String firstName;
        private String lastName;
        private String personalID;
        private String phoneNumber;
        private String email;
        private String country;
        private String city;
        private DateTime dateOfBirth;
        private String township;
        private String cardID;
        private MaritalStatus maritalStatus;
        private Genders gender;
        private string address;

        public string FirstName 
        { 
            get => firstName;
            set
            {
                firstName = value;
            }
        }
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
            }
        }
        public string PersonalID
        {
            get => personalID;
            set
            {
                personalID = value;
            }
        }
        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                phoneNumber = value;
            }
        }
        public string Email
        {
            get => email;
            set
            {
                email = value;
            }
        }
        public string Country
        {
            get => country;
            set
            {
                country = value;
            }
        }
        public string City
        {
            get => city;
            set
            {
                city = value;
            }
        }
        public DateTime DateOfBirth 
        { 
            get => dateOfBirth; 
            set
            {
                dateOfBirth = value;
            }
        }
        public string Township
        {
            get => township;
            set
            {
                township = value;
            }
        }
        public string CardID
        {
            get => cardID;
            set
            {
                cardID = value;
            }
        }
        public MaritalStatus MaritalStatus 
        { 
            get => maritalStatus; 
            set
            {
                maritalStatus = value;
            }
        }
        public Genders Gender 
        { 
            get => gender; 
            set
            {
                gender = value;
            }
        }
        public string Address
        {
            get => address;
            set
            {
                address = value;
            }
        }
    }
}