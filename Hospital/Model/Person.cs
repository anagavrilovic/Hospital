
using Hospital.Model;
using System;
using System.ComponentModel;

namespace Hospital
{
    public abstract class Person
    {
        private String firstName;
        private String lastName;
        private String personalID;
        private String phoneNumber;
        private String email;
        private DateTime dateOfBirth = DateTime.Today;
        private String cardID;
        private MaritalStatus maritalStatus;
        private Genders gender;
        private Address address = new Address();

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalID { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string CardID { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public Genders Gender { get; set; }
        public Address Address { get; set; }

    }
}