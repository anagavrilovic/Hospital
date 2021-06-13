using Hospital.Factory;
using Hospital.Model;
using Hospital.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.ViewModels.Patient
{
    class PatientNotesViewModel
    {
        public ObservableCollection<PatientNote> Notes
        {
            get;
            set;
        }

        private PatientNotesService patientNotesService = new PatientNotesService();
        public PatientNotesViewModel()
        {
            Notes = new ObservableCollection<PatientNote>(patientNotesService.GetByPatientID());
        }
    }
}
