using Hospital.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DTO.DoctorDTO
{
    public class HospitalTreatmentDTO : ViewModel
    {
        private Room selectedRoom = new Room();
        public Room SelectedRoom
        {
            get
            {
                return selectedRoom;
            }
            set
            {
                if (value != selectedRoom)
                {
                    selectedRoom = value;
                    OnPropertyChanged("selectedRoom");
                }
            }
        }
        private MedicalRecord medicalRecord;
        public MedicalRecord MedicalRecord
        {
            get
            {
                return medicalRecord;
            }
            set
            {
                if (value != medicalRecord)
                {
                    medicalRecord = value;
                    OnPropertyChanged("MedicalRecord");
                }
            }
        }
        private DateTime patientsStayDuration;
        public DateTime PatientsStayDuration
        {
            get
            {
                return patientsStayDuration;
            }
            set
            {
                if (value != patientsStayDuration)
                {
                    patientsStayDuration = value;
                    OnPropertyChanged("PatientsStayDuration");
                }
            }
        }
        private ObservableCollection<Room> roomsForDisplay = new ObservableCollection<Room>();
        public ObservableCollection<Room> RoomsForDisplay
        {
            get
            {
                return roomsForDisplay;
            }
            set
            {
                if (value != roomsForDisplay)
                {
                    roomsForDisplay = value;
                    OnPropertyChanged("RoomsForDisplay");
                }
            }
        }
    }
}
