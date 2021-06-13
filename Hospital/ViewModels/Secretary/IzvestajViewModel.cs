using Hospital.Commands.Secretary;
using Hospital.ReportsPatterns;
using Hospital.Services;
using Hospital.View.Secretary;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Tables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Navigation;

namespace Hospital.ViewModels.Secretary
{
    public class IzvestajViewModel : ViewModel
    {
        #region Properties

        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
        public Model.Doctor SelectedDoctor { get; set; }
        public ObservableCollection<Model.Doctor> AllDoctors { get; set; }
        public ICollectionView DoctorsCollection { get; set; }

        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set 
            { 
                searchText = value;
                OnPropertyChanged("SearchText");
                CollectionViewSource.GetDefaultView(AllDoctors).Refresh();
            }
        }


        public DoctorService DoctorService { get; set; }
        public AppointmentService AppointmentService { get; set; }
        public MedicalRecordService MedicalRecordService { get; set; }
        public NavigationService NavigationService { get; set; }

        #endregion

        #region Konstruktor
        public IzvestajViewModel(NavigationService navigationService)
        {
            this.NavigationService = navigationService;
            InitializeEmptyProperties();
            LoadDoctors();
            GenerateReportCommand = new RelayCommand(Execute_GenerateReportCommand, CanExecuteCommands);
        }
        #endregion

        #region Metode

        private void InitializeEmptyProperties()
        {
            DateBegin = DateTime.Now;
            DateEnd = DateTime.Now;
            DoctorService = new DoctorService();
            AppointmentService = new AppointmentService();
            MedicalRecordService = new MedicalRecordService();
        }

        private void LoadDoctors()
        {
            AllDoctors = new ObservableCollection<Model.Doctor>(DoctorService.GetAll());
            DoctorsCollection = CollectionViewSource.GetDefaultView(AllDoctors);
            DoctorsCollection.Filter = CustomFilterDoctor;
        }
        
        private bool CustomFilterDoctor(object obj)
        {
            if (string.IsNullOrEmpty(SearchText))
                return true;
            else
                return ((obj.ToString()).IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private bool Validate()
        {
            if (SelectedDoctor == null)
            {
                InformationBox informationBox = new InformationBox("Izaberite lekara!");
                informationBox.Show();
                return false;
            }

            if (DateBegin > DateEnd)
            {
                InformationBox informationBox = new InformationBox("Početni dan ne može biti nakon krajnjeg!");
                informationBox.Show();
                return false;
            }

            return true;
        }

        #endregion

        #region Komande

        public RelayCommand GenerateReportCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        #endregion

        #region Akcije

        public void Execute_GenerateReportCommand(object obj)
        {
            if (!Validate())
                return;

            List<Appointment> appointments = AppointmentService.GetAppointmentsByDoctorInSelectedPeriod(SelectedDoctor, DateBegin, DateEnd);
            appointments = appointments.OrderBy(a => a.DateTime).ToList();

            ReportGenerator generator = new SecretaryReport(SelectedDoctor, appointments, DateBegin, DateEnd);
            generator.GenerateReport();

            InformationBox informationBox = new InformationBox("Izveštaj se nalazi u folderu Reports.");
            informationBox.Show();
        }

        public bool CanExecuteCommands(object obj)
        {
            return true;
        }

        #endregion
    }
}
