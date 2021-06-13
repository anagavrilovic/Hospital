using Hospital.Model;
using Hospital.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for PatientNotifications.xaml
    /// </summary>
    public partial class PatientNotifications : Page
    {
        private Context context = new Context();
        public ObservableCollection<IPatientNotification> NotificationList
        {
            get;
            set;
        }
        public PatientNotifications()
        {
            InitializeComponent();
            this.DataContext = this;
            NotificationList = new ObservableCollection<IPatientNotification>();
            GetNotifications();
            dataGridApp.SelectedIndex = 0;
            dataGridApp.Focus();
        }

        private void GetNotifications()
        {
            context.SetStrategy(new ConcreteStrategyTherapy());
            AddToList();
            context.SetStrategy(new ConcreteStrategyNote());
            AddToList();
            context.SetStrategy(new ConcreteStrategyGeneralNotification());
            AddToList();
        }

        private void AddToList()
        {
            ObservableCollection<IPatientNotification> auxiliaryList = new ObservableCollection<IPatientNotification>(context.GetNotifications());
            foreach (IPatientNotification pt in auxiliaryList)
            {
                NotificationList.Add(pt);
            }
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            var selectedItem = dataGridApp.SelectedItem;
            if (selectedItem == null) return;

            SetStrategy((IPatientNotification)selectedItem);

            if (e.Key == Key.Space)
            {
                context.Update();
                this.NavigationService.Navigate(new PatientNotification((IPatientNotification)selectedItem));
            }

            if (e.Key == Key.Subtract)
            {
                if (MessageBox.Show("Da li ste sigurni da želite da obrišete obaveštenje?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No) return;
                context.Delete();
                NotificationList.Remove((IPatientNotification)selectedItem);
            }

            if (e.Key == Key.Escape)
            {
                this.NavigationService.Navigate(new PatientMenu());
            }

            dataGridApp.Focus();
        }

        private void SetStrategy(IPatientNotification selectedItem)
        {
            if (dataGridApp.SelectedItem.GetType() == typeof(PatientTherapyMedicineNotification))
            {
                context.SetStrategy(new ConcreteStrategyTherapy((PatientTherapyMedicineNotification)selectedItem));

            }
            else if (dataGridApp.SelectedItem.GetType() == typeof(PatientNotesNotification))
            {
                context.SetStrategy(new ConcreteStrategyNote((PatientNotesNotification)selectedItem));
            }
            else
            {
                context.SetStrategy(new ConcreteStrategyGeneralNotification((NotificationsUsersAdapter)selectedItem));
            }
        }
    }
}
